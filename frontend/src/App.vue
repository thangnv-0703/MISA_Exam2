<template>
  <div id="app">
    <router-view />
    <QuestionAnswerEssay v-if="isShowAnswerDialog" />
    <QuestionDetail v-if="isShowQuestionDialog" />
    <BaseToast v-if="isShowToast" />
  </div>
</template>

<script>
import { mapState } from 'vuex';
import QuestionDetail from './view/question/QuestionDetail.vue';
import QuestionAnswerEssay from './view/question/QuestionAnswerEssay.vue';
import BaseToast from './components/base/BaseToast.vue';
export default {
  name: 'App',
  components: {
    QuestionDetail,
    QuestionAnswerEssay,
    BaseToast,
  },
  computed: mapState({
    isShowQuestionDialog: (state) => state.question.questionDialog.isShowDialog,
    isShowAnswerDialog: (state) => state.question.isShowAnswerDialog,
    isShowToast: (state) => state.resource.toastMessage.isShowToast,
  }),
};
</script>
<style>
@import url('./style/main.css');
</style>
