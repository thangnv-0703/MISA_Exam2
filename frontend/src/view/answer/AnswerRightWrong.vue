<template lang="">
  <div class="answer-right-wrong">
    <div
      class="answer-box-rw"
      v-for="(answer, index) in answers"
      :key="index"
      :style="`backgroundColor:${answerBoxes[index].bgColor}`"
    >
      <BaseCheckBox
        classList="answer-checkbox-rw"
        v-model="answer.AnswerCorrect"
      />
      <div class="answer-input-area-rw">
        <textarea
          class="answer-input-rw"
          placeholder="Nhập đáp án..."
          v-model="answer.AnswerContent"
          :style="`backgroundColor:${answerBoxes[index].bgColor}`"
        ></textarea>
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
  data() {
    return {
      answerBoxes: [...Resource.rightWrong],
      answers: [],
    };
  },
  watch: {
    answers: {
      deep: true,
      handler() {
        this.$store.commit('SET_ANSWERS', this.answers);
      },
    },
    correct0: function () {
      if (this.correct0) {
        this.answers[1].AnswerCorrect = false;
      }
    },
    correct1: function () {
      if (this.correct1) {
        this.answers[0].AnswerCorrect = false;
      }
    },
  },
  computed: mapState({
    question: (state) => state.question.questionData,
    correct0: function () {
      return this.answers.length === 2 ? this.answers[0].AnswerCorrect : null;
    },
    correct1: function () {
      return this.answers.length === 2 ? this.answers[1].AnswerCorrect : null;
    },
  }),
  created() {
    if (this.question.Answers.length === 0) {
      for (var i = 0; i < 2; i++) {
        this.answers.push({
          AnswerContent: Resource.rightWrong[i].text,
          AnswerImgUrl: '',
          AnswerCorrect: false,
        });
      }
    } else {
      this.answers = [...this.question.Answers];
    }
  },
};
</script>
<style>
@import url('../../style/page/answer/answerRightWrong.css');
</style>
