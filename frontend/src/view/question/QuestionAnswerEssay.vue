<template lang="">
  <div class="answer-modal">
    <div class="answer-backdrop"></div>
    <div class="answer-box">
      <div class="answer-content">
        <vue-editor
          id="editor-answer"
          v-model="questionAnswerContent"
          :editorToolbar="customToolbar"
          class="answer-editor"
          ref="editor"
          @focus="isShowPlaceholder = false"
          @blur="isShowPlaceholder = true"
        ></vue-editor>
        <div class="answer-placeholder" v-if="questionAnswerContent === ''">
          <span>{{ resource.text.answerEssayPlaceholder }}</span>
        </div>
        <div class="answer-btns">
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
    </div>
    <div class="button-dialog">
      <div
        class="button-icon icon-backToQuestion"
        @click="handleOpenQuestionDialog"
      ></div>
      <p>{{ resource.btns.backToQuestion }}</p>
    </div>
  </div>
</template>

<script>
import { VueEditor } from 'vue2-editor';
import { mapState } from 'vuex';
import Enum from '../../script/enum';
import BaseButton from '../../components/base/BaseButton.vue';
export default {
  components: {
    VueEditor,
    BaseButton,
  },
  data() {
    return {
      questionAnswerContent: '',
      isShowPlaceholder: true,
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
    };
  },
  computed: mapState({
    question: (state) => state.question.questionData,
    exercise: (state) => state.exercise.exerciseData,
    resource: (state) => state.resource.resourceData,
    strippedContent() {
      let regex = /(<([^>]+)>)/gi;
      return this.questionAnswerContent.replace(regex, '');
    },
  }),
  methods: {
    handleOpenQuestionDialog() {
      this.$store.commit('OPEN_QUESTION_DIALOG', {
        questionType: this.question.QuestionType,
        questionMode: Enum.QuestionMode.Edit,
      });
      this.$store.commit('CLOSE_ANSWER_DIALOG');
    },
    handleCancelButton() {
      this.$store.commit('CLOSE_ANSWER_DIALOG');
    },
    handleSaveQuestionData() {
      this.$store.commit('OPEN_QUESTION_DIALOG', {
        questionType: this.question.QuestionType,
        questionMode: Enum.QuestionMode.Edit,
      });
      this.$store.commit('SET_QUESTION_ANSWER_CONTENT', this.strippedContent);
      this.$store.commit('CLOSE_ANSWER_DIALOG');
      console.log(this.question);
    },
  },
};
</script>
<style scoped>
@import url('../../style/page/question/questionAnswerEssay.css');
</style>
