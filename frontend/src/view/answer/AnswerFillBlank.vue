<template lang="">
  <div class="answer-fill-blank">
    <div class="answer-row-fb" v-for="(input, index) in answers" :key="index">
      <div class="answer-label-fb">
        {{ resource.label.blank }} {{ index + 1 }}
      </div>
      <BaseInputTags
        classList="answer-input-tags"
        v-model="answers[index].AnswerContent"
        @onDeleteInput="handleDeleteInput(index)"
        ref="inputTag"
      />
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import BaseInputTags from '../../components/base/BaseInputTags.vue';
export default {
  components: {
    BaseInputTags,
  },
  props: ['increaseInputBox'],
  data() {
    return {
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
    increaseInputBox: function () {
      this.answers.push({
        AnswerContent: '',
        AnswerImgUrl: '',
        AnswerCorrect: false,
      });
    },
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    question: (state) => state.question.questionData,
  }),
  created() {
    if (this.question.Answers.length === 0) {
      this.answers.push({
        AnswerContent: '',
        AnswerImgUrl: '',
        AnswerCorrect: false,
      });
    } else {
      this.answers = [...this.question.Answers];
      for (let i = 0; i < this.answers.length; i++) {
        this.answers[i].AnswerContent =
          this.answers[i].AnswerContent.split('; ');
      }
    }
  },
  methods: {
    handleDeleteInput(index) {
      this.answers.splice(index, 1);
    },
  },
};
</script>
<style scoped>
@import url('../../style/page/answer/answerFillBlank.css');
</style>
