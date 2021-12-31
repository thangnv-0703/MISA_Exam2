<template lang="">
  <div class="exercise-detail">
    <Header />
    <div class="exercise-detail-list">
      <div class="exercise-list">
        <QuestionCard
          v-for="(question, index) in questions"
          :key="index"
          :question="question"
        />
        <div class="question-type-container">
          <ul class="question-type-toolbar">
            <li
              class="question-type-item"
              v-for="item in questionType"
              :key="item.id"
            >
              <div
                class="question-type-icon"
                :class="item.icon"
                @click="handleOpenQuestionDetail(item.id)"
              ></div>
              <div class="question-type-tooltip">{{ item.label }}</div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Resource from '../../script/resource';
import Enum from '../../script/enum';
import Header from '../../components/layout/TheHeader.vue';
import QuestionCard from '../question/QuestionCard.vue';
export default {
  components: { Header, QuestionCard },
  data() {
    return {
      questionType: Resource.questionType,
      questionTypeId: null,
    };
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    exercise: (state) => state.exercise.exerciseData,
    questions() {
      return this.$store.getters.questionList;
    },
  }),
  watch: {
    questions: function () {
      if (this.questions.length === 0) {
        this.$router.replace('/new-exercise');
      }
    },
  },
  created() {
    const id = this.$route.params.id;
    this.$store.commit('SET_EXERCISE_EXERCISEID', id);
    this.$store.dispatch('getExerciseById');
  },
  methods: {
    /**
     * Hàm xư lý việc hiện thị câu hỏi
     * Author: NVThang (20/12/2021)
     */
    handleOpenQuestionDetail(id) {
      this.$store.commit('OPEN_QUESTION_DIALOG', {
        questionType: id,
        questionMode: Enum.QuestionMode.Add,
      });
    },
  },
};
</script>

<style scoped>
@import url('../../style/page/exercise/exerciseDetail.css');
</style>
