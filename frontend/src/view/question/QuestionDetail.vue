<template lang="">
  <div class="question-modal">
    <div class="question-backdrop"></div>
    <div class="question-box">
      <!-- Phần câu hỏi -->
      <div
        class="question-content"
        :style="questionType === questionTypes.Essay ? 'height: 100%' : ''"
      >
        <BaseCombobox
          v-model="questionType"
          :options="questionOptions"
          classList="question-type-combobox"
          :requiredText="requiredText"
        />
        <vue-editor
          id="editor"
          v-model="questionContent"
          :editorToolbar="customToolbar"
          class="question-editor"
          ref="editor"
          @focus="isShowPlaceholder = false"
          @blur="isShowPlaceholder = true"
        ></vue-editor>
        <div class="question-placeholder" v-if="questionContent === ''">
          <span>{{ resource.text.questionPlaceholder }}</span>
        </div>
        <div class="question-image">
          <div class="question-img-area" v-if="questionImage">
            <img :src="questionImage" class="question-img" />
            <div class="btn-delete-img" @click="handleDeleteImage"></div>
          </div>

          <div class="question-upload-image">
            <input
              type="file"
              ref="inputFile"
              class="question-upload-file"
              accept="image/png, image/jpeg"
              @change="uploadImage"
            />
          </div>
        </div>
      </div>

      <!-- Phần đáp án -->
      <div class="question-answer">
        <!-- Chọn đáp án -->
        <AnswerMultipleChoice
          v-if="questionType === questionTypes.MultiChoice"
          :increaseBox="increaseBox"
        />
        <!-- Đúng sai -->
        <AnswerRightWrong v-if="questionType === questionTypes.RightWrong" />
        <!-- Điền vào chỗ trống -->
        <AnswerFillBlank
          v-if="questionType === questionTypes.FillBlank"
          :increaseInputBox="increaseInputBox"
        />
      </div>

      <!-- Button -->
      <div class="question-btn-group">
        <div class="question-btn-left">
          <BaseButton
            classList="m-second-btn"
            @click="handleAddAnswerBox"
            v-if="isShowAddAnswerBtn"
            >{{ resource.btns.addAnswer }}</BaseButton
          >
        </div>
        <div class="question-btn-right">
          <BaseButton classList="m-second-btn" @click="handleCancelButton">{{
            resource.btns.cancel
          }}</BaseButton>
          <BaseButton
            classList="m-primary-btn"
            @click="handleSaveQuestionData"
            >{{ resource.btns.save }}</BaseButton
          >
        </div>
      </div>

      <!-- Nút điều hướng ở 2 bên -->
      <div class="button-dialog-left">
        <div class="button-icon icon-view-ui"></div>
        <p>{{ resource.btns.viewUI }}</p>
      </div>
      <div class="button-dialog-right" @click="handleOpenAnswerDialog">
        <div class="button-icon icon-add-answer-essay"></div>
        <p>{{ resource.btns.addAnswerEssay }}</p>
      </div>
    </div>
    <BasePopup
      v-if="isShowPopup"
      :messages="popupMsg"
      @onClosePopup="isShowPopup = false"
    ></BasePopup>
  </div>
</template>

<script>
import { VueEditor } from 'vue2-editor';
import { mapState } from 'vuex';
import axios from 'axios';

import Enum from '../../script/enum';
import Resource from '../../script/resource';

import BaseCombobox from '../../components/base/BaseCombobox.vue';
import BaseButton from '../../components/base/BaseButton.vue';
import BasePopup from '../../components/base/BasePopup.vue';
import AnswerMultipleChoice from '../answer/AnwerMultipleChoice.vue';
import AnswerRightWrong from '../answer/AnswerRightWrong.vue';
import AnswerFillBlank from '../answer/AnswerFillBlank.vue';

export default {
  components: {
    BaseCombobox,
    BaseButton,
    BasePopup,
    AnswerMultipleChoice,
    AnswerRightWrong,
    AnswerFillBlank,
    VueEditor,
  },
  props: ['typeQuestionId', 'exerciseMode'],
  data() {
    return {
      questionTypes: Enum.QuestionType,
      questionOptions: Resource.questionType,

      questionType: null,
      questionContent: '',
      questionNumber: 1,
      questionImage: null,

      isShowPlaceholder: true,
      // customize toolbar editor
      customToolbar: [
        ['bold', 'italic', 'underline'],
        [
          { align: '' },
          { align: 'center' },
          { align: 'right' },
          { align: 'justify' },
        ],
        ['formula', 'image', 'video'],
      ],
      // câu chọn đáp án
      increaseBox: 0,
      // điền vào chỗ trống
      increaseInputBox: 0,

      isShowPopup: false,
      popupMsg: [],
    };
  },
  computed: mapState({
    questionDialog: (state) => state.question.questionDialog,
    question: (state) => state.question.questionData,
    exercise: (state) => state.exercise.exerciseData,
    resource: (state) => state.resource.resourceData,
    strippedContent() {
      let regex = /(<([^>]+)>)/gi;
      return this.questionContent.replace(regex, '');
    },
    isShowAddAnswerBtn() {
      return (
        this.questionType === this.questionTypes.MultiChoice ||
        this.questionType === this.questionTypes.FillBlank
      );
    },
    requiredText() {
      return 'Câu ' + this.questionNumber + ' - ';
    },
  }),
  watch: {
    questionType: function () {
      this.$store.commit('RESET_ANSWER');
      this.$store.commit('SET_QUESTION_TYPE', this.questionType);
    },
    questionContent: function () {
      this.$store.commit('SET_QUESTION_CONTENT', this.strippedContent);
    },
    questionImage: function () {
      this.$store.commit('SET_QUESTION_IMAGE', this.questionImage);
    },
  },
  created() {
    this.questionType = this.questionDialog.questionType;
    this.questionNumber = this.exercise.Questions.length + 1;

    if (this.questionDialog.questionMode === Enum.QuestionMode.Edit) {
      this.questionImage = this.question.QuestionImage;
      this.questionContent = this.question.QuestionContent;
      this.questionNumber = this.question.QuestionNumber;
    } else if (this.questionDialog.questionMode === Enum.QuestionMode.Clone) {
      this.questionImage = this.question.QuestionImage;
      this.questionContent = this.question.QuestionContent;
    }
    this.$store.commit('SET_QUESTION_NUMBER', this.questionNumber);
    this.$store.commit('SET_EXERCISE_STATUS', Enum.ExerciseStatus.Composing);
  },
  methods: {
    handleDeleteImage() {
      this.$refs.inputFile.value = null;
      this.questionImage = null;
    },
    /**
     * Hàm xử lý việc tải file ảnh
     * Author: NVThang (24/12/2021)
     */
    uploadImage() {
      const file = document.querySelector('input[type=file]').files[0];
      const reader = new FileReader();

      let rawImg;
      reader.onloadend = () => {
        rawImg = reader.result;
        this.questionImage = rawImg;
      };
      reader.readAsDataURL(file);
    },
    /**
     * Hàm hiện thị dialog lời giải
     * Author: NVThang (24/12/2021)
     */
    handleOpenAnswerDialog() {
      this.$store.commit('CLOSE_QUESTION_DIALOG');
      this.$store.commit('OPEN_ANSWER_DIALOG');
    },
    /**
     * Hàm gắn giá trị cho popup
     * Author: NVThang (21/12/2021)
     */
    setPopupMsg(msg) {
      this.isShowPopup = true;
      this.popupMsg = msg;
    },
    /**
     * Hàm validate dữ liệu của câu hỏi
     * Author: NVThang (21/12/2021)
     */
    validateData() {
      const type = this.question.QuestionType;
      const msg = [];
      let isValid = true;
      // check nội dung câu hỏi
      if (!this.question.QuestionContent) {
        msg.push(this.resource.popup.message.invalidQuestionContent);
        isValid = false;
      }

      // check só lượng đáp án
      if (type !== 4 && this.question.Answers.length === 0) {
        msg.push(this.resource.popup.message.invalidAnswer);
        isValid = false;
      }

      let numberOfCorrectAnswer = 0;
      for (const answer of this.question.Answers) {
        // check nội dung đáp án
        if (!(answer.AnswerContent.length > 0 || answer.AnswerImgUrl !== '')) {
          msg.push(this.resource.popup.message.invalidAnswerContent);
          isValid = false;
          break;
        }
        if (answer.AnswerCorrect) {
          numberOfCorrectAnswer++;
        }
      }

      // check số lượng đáp án đúng
      if ((type === 1 || type === 2) && numberOfCorrectAnswer === 0) {
        msg.push(this.resource.popup.message.invalidAnswerCorrect);
        isValid = false;
      }

      if (!isValid) {
        this.setPopupMsg(msg);
      }
      return isValid;
    },
    /**
     * Hàm xử lí việc thêm đáp án
     * Author: NVThang (11/12/2021)
     */
    handleAddAnswerBox() {
      if (this.questionType === this.questionTypes.MultiChoice) {
        this.increaseBox++;
      } else if (this.questionType === this.questionTypes.FillBlank) {
        this.increaseInputBox++;
      }
    },
    /**
     * Hàm xử lý nút hủy
     */
    handleCancelButton() {
      this.$store.commit('RESET_QUESTION');
      this.$store.commit('CLOSE_QUESTION_DIALOG');
    },
    /**
     * Hàm xử lí việc lưu câu hỏi
     * Author: NVThang (11/12/2021)
     */
    handleSaveQuestionData() {
      const me = this;
      const isEdit = me.questionDialog.questionMode === Enum.QuestionMode.Edit;
      const isClone =
        me.questionDialog.questionMode === Enum.QuestionMode.Clone;
      console.log(isEdit);

      // validate dữ liệu
      const valid = me.validateData();
      if (!valid) {
        return;
      }

      // format lại dữ liệu cho câu hỏi điền vào chỗ trống
      const data = this.question;
      if (me.questionType === 3) {
        data.Answers.forEach((answer) => {
          answer.AnswerContent = answer.AnswerContent.join('; ');
        });
      }

      // Đẩy dữ liệu câu hỏi vào bài tập
      switch (me.questionDialog.questionMode) {
        case Enum.QuestionMode.Add:
          me.$store.commit('PUSH_EXERCISE_QUESTION', data);
          break;
        case Enum.QuestionMode.Edit:
          me.$store.commit('UPDATE_EXERCISE_QUESTION', {
            question: data,
            index: this.questionNumber - 1,
          });
          break;
        case Enum.QuestionMode.Clone:
          me.$store.commit('PUSH_EXERCISE_QUESTION', data);
          break;
      }

      const exerciseData = me.exercise;
      // gọi api để lưu bài tập vào db
      // debugger;
      if (me.exercise.ExerciseId) {
        axios
          .put(
            me.resource.api.exercise + '/' + me.exercise.ExerciseId,
            exerciseData,
          )
          .then(() => {
            // Điều hướng khi lưu câu hỏi thành công
            if (me.$route.path === '/new-exercise') {
              me.$router.push(`/exercise/${me.exercise.ExerciseId}`);
            }

            // Hiện thị toast message
            // debugger;
            if (isEdit) {
              me.$store.commit('SHOW_TOAST', me.resource.toast.editSuccess);
            } else if (isClone) {
              me.$store.commit('SHOW_TOAST', me.resource.toast.cloneSuccess);
            } else {
              me.$store.commit('SHOW_TOAST', me.resource.toast.addSuccess);
            }
          })
          .catch(() => {
            // Hiện thị toast message
            if (me.questionDialog.questionMode === Enum.QuestionMode.Edit) {
              me.$store.commit('SHOW_TOAST', me.resource.toast.editFail);
            } else {
              me.$store.commit('SHOW_TOAST', me.resource.toast.cloneFail);
            }
          });
      } else {
        axios
          .post(me.resource.api.exercise, exerciseData)
          .then((response) => {
            var exerciseId = response.data;

            // Hàm xử lí việc điều hướng và ẩn dialog khi lưu câu hỏi thành công
            me.$store.commit('SET_EXERCISE_EXERCISEID', exerciseId);
            me.$router.push(`/exercise/${exerciseId}`);
            me.$store.commit('SHOW_TOAST', me.resource.toast.addSuccess);
            console.log(response.data);
          })
          .catch(() => {
            me.$store.commit('SHOW_TOAST', me.resource.toast.addFail);
          });
      }
      me.$store.commit('CLOSE_QUESTION_DIALOG');
      me.$store.commit('RESET_QUESTION');
    },
  },
};
</script>

<style scoped>
@import url('../../style/page/question/questionDetail.css');
</style>
