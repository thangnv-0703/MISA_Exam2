<template lang="">
  <div class="question-card" :style="`border-color: ${borderColor}`">
    <p>
      <span class="question-number">{{ question.QuestionNumber }}</span
      >. {{ question.QuestionContent }}
    </p>
    <img
      :src="question.QuestionImage"
      class="question-img"
      v-if="question.QuestionImage"
    />
    <div class="question-answers">
      <div
        class="question-answer"
        v-for="(answer, index) in question.Answers"
        :key="index"
      >
        <div
          class="question-answer-letter answer-correct"
          v-if="question.QuestionType === questionTypes.FillBlank"
        >
          {{ index + 1 }}
        </div>
        <div
          class="question-answer-letter"
          v-else
          :class="answer.AnswerCorrect ? 'answer-correct' : ''"
        >
          {{ String.fromCodePoint(index + 65) }}
        </div>
        <img :src="answer.AnswerImgUrl" class="question-answer-image" />
        <p class="question-answer-content">
          {{ answer.AnswerContent }}
        </p>
      </div>
    </div>
    <div
      class="question-answer-essay-content"
      v-if="question.QuestionAnswerContent"
    >
      <span style="font-weight: 700">
        {{ resource.text.essayAnswer }}
      </span>
      {{ question.QuestionAnswerContent }}
    </div>
    <div class="line-break"></div>
    <div class="question-btn-group">
      <BaseButton classList="m-second-btn" @click="handleEditQuestion">{{
        resource.btns.editQuestion
      }}</BaseButton>
      <BaseButton
        classList="icon-replication m-icon-btn m-second-btn"
        @click="handleCloneQuestion"
      />
      <BaseButton
        classList="icon-trash m-icon-btn m-second-btn"
        @click="isShowPopup = true"
      />
    </div>
    <BasePopup
      v-if="isShowPopup"
      :confirm="true"
      :messages="[
        resource.popup.message.confirmDeleteQuestion(question.QuestionNumber),
      ]"
      @onClosePopup="isShowPopup = false"
      @onConfirm="handleDeleteQuestion"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex';
import axios from 'axios';
import Enum from '../../script/enum';
import Resource from '../../script/resource';
import BaseButton from '../../components/base/BaseButton.vue';
import BasePopup from '../../components/base/BasePopup.vue';
export default {
  components: {
    BaseButton,
    BasePopup,
  },
  props: ['question'],
  data() {
    return {
      questionTypes: Enum.QuestionType,
      isShowPopup: false,
    };
  },

  computed: mapState({
    resource: (state) => state.resource.resourceData,
    exercise: (state) => state.exercise.exerciseData,
    borderColor: function () {
      return Resource.questionType[this.question.QuestionType - 1].borderColor;
    },
  }),
  methods: {
    /**
     * Hàm mở dialog câu hỏi để chỉnh sửa
     * Author: NVThang (20/12/2021)
     */
    handleEditQuestion() {
      this.$store.commit(
        'SET_QUESTION',
        JSON.parse(JSON.stringify(this.question)),
      );
      this.$store.commit('SET_EXERCISE_STATUS', 0);
      this.$store.commit('OPEN_QUESTION_DIALOG', {
        questionType: this.question.QuestionType,
        questionMode: Enum.QuestionMode.Edit,
      });
    },
    /**
     * Hàm xóa câu hỏi khỏi bài tập
     * Author: NVThang (20/12/2021)
     */
    handleDeleteQuestion() {
      const me = this;
      me.$store.commit('DELETE_QUESTION', me.question.QuestionNumber - 1);
      me.$store.commit('UPDATE_EXERCISE_QUESTION_NUMBER');
      const exerciseData = me.exercise;
      axios
        .put(
          me.resource.api.exercise + '/' + me.exercise.ExerciseId,
          exerciseData,
        )
        .then(() => {
          me.$store.commit('SHOW_TOAST', me.resource.toast.deleteSuccess);
          me.$store.commit('SET_EXERCISE_STATUS', 0);
        })
        .catch(() => {
          me.$store.commit('SHOW_TOAST', me.resource.toast.deleteFailExercise);
        });
      me.isShowPopup = false;
    },
    /**
     * Hàm hiển thị chi tiết câu hỏi được nhân bản
     * Author: NVThang (20/12/2021)
     */
    handleCloneQuestion() {
      this.$store.commit(
        'SET_QUESTION',
        JSON.parse(JSON.stringify(this.question)),
      );
      this.$store.commit('OPEN_QUESTION_DIALOG', {
        questionType: this.question.QuestionType,
        questionMode: Enum.QuestionMode.Clone,
      });
    },
  },
};
</script>
<style>
@import url('../../style/page/question/questionCard.css');
</style>
