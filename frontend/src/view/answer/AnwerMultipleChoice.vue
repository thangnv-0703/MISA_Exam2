<template lang="">
  <div class="answer-multiple-choice">
    <div
      class="answer-box"
      v-for="(answer, index) in answers"
      :key="index"
      :style="`backgroundColor:${answerBox[index % 4]}`"
    >
      <div class="answer-toolbar">
        <div class="answer-letter">
          {{ String.fromCodePoint(index + 65) }}
        </div>
        <div
          class="answer-toolbar-option icon-ellipsis-v"
          @click="togglePopover(index)"
        ></div>
        <div class="answer-popover" v-if="index === isShowIndex">
          <div class="answer-popover-item">
            {{ resource.answerPopoverItem[0] }}
            <input
              type="file"
              class="answer-upload-file"
              ref="inputFile"
              @change="uploadImage(index)"
            />
          </div>
          <div
            class="answer-popover-item"
            @click="handleRemoveAnswerBox(index)"
          >
            {{ resource.answerPopoverItem[1] }}
          </div>
        </div>
        <BaseCheckBox
          classList="answer-checkbox"
          v-model="answer.AnswerCorrect"
        />
      </div>
      <div class="answer-input-area">
        <div
          class="answer-input"
          @input="onInput($event, index)"
          :placeholder="
            !textContent[index] && !answers[index].AnswerImgUrl
              ? resource.text.answerContentPlaceholder
              : ' '
          "
          :tabindex="index + 1"
          :contentEditable="
            answers[index].AnswerImgUrl ? false : 'plaintext-only'
          "
          :style="
            answers[index].AnswerImgUrl
              ? `background-image: url(${answers[index].AnswerImgUrl}); height:100%`
              : ''
          "
        >
          {{ textContent[index] }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Resource from '../../script/resource';
import BaseCheckBox from '../../components/base/BaseCheckbox.vue';
export default {
  components: {
    BaseCheckBox,
  },
  props: ['increaseBox'],
  data() {
    return {
      answerBox: [...Resource.multiChoice],
      answers: [],
      textContent: [],
      placeholder: [],
      isShowIndex: -1,
    };
  },
  watch: {
    answers: {
      deep: true,
      handler() {
        this.$store.commit('SET_ANSWERS', this.answers);
      },
    },
    increaseBox: function () {
      this.handleAddAnswerBox();
    },
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    question: (state) => state.question.questionData,
    listeners() {
      return { ...this.$listeners, input: this.onInput };
    },
  }),
  created() {
    if (this.question.Answers.length === 0) {
      for (var i = 0; i < 4; i++) {
        this.answers.push({
          AnswerContent: '',
          AnswerImgUrl: '',
          AnswerCorrect: false,
        });
        this.textContent[i] = this.answers[i].AnswerContent;
      }
    } else {
      this.answers = [...this.question.Answers];
      this.question.Answers.forEach((answer, index) => {
        this.textContent[index] = answer.AnswerContent;
      });
    }
  },
  methods: {
    /**
     * Hàm xử lý việc upload file
     * Author: NVThang (24/12/2021)
     */
    uploadImage(index) {
      const file = this.$refs.inputFile[0].files[0];
      const reader = new FileReader();

      let rawImg;
      reader.onloadend = () => {
        rawImg = reader.result;
        this.answers[index].AnswerImgUrl = rawImg;
      };
      reader.readAsDataURL(file);
      this.togglePopover(-1);
      this.answers[index].AnswerContent = '';
      this.textContent[index] = '';
    },
    /**
     * Hàm toggle popover
     * Author: NVThang (24/12/2021)
     */
    togglePopover(index) {
      if (this.isShowIndex === index) {
        this.isShowIndex = -1;
      } else {
        this.isShowIndex = index;
      }
    },
    /**
     * Gắn giá trị lên nội dung phương án
     * Author: NVThang (24/12/2021)
     */
    onInput(e, index) {
      this.answers[index].AnswerContent = e.target.innerText;
      this.textContent[index] = e.target.innerText;
      this.placeholder[index] = '';
      console.log(
        !this.textContent[index] && !this.answers[index].AnswerImgUrl,
      );
    },
    /**
     * Hàm xử lí việc thêm đáp án
     * Author: NVThang (11/12/2021)
     */
    handleAddAnswerBox() {
      this.answers.push({
        AnswerContent: '',
        AnswerImgUrl: '',
        AnswerCorrect: false,
      });
    },
    /**
     * Hàm xử lí việc xóa đáp án
     * Author: NVThang (11/12/2021)
     */
    handleRemoveAnswerBox(index) {
      this.answers.splice(index, 1);
      this.textContent.splice(index, 1);
      this.togglePopover(-1);
    },
  },
};
</script>
<style scoped>
@import url('../../style/page/answer/answerMultipleChoice.css');
</style>
