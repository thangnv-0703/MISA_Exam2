<template lang="">
  <div>
    <Header />
    <div class="new-ex-content">
      <p>{{ resource.text.importQuestion }}</p>
      <div class="import-region">
        <div class="import-region-icon icon-import"></div>
        <p>{{ resource.text.clickToUpload }}</p>
        <p>{{ resource.text.fileFormat }}</p>
        <BaseButton classList="m-second-btn" style="border: none">
          {{ resource.btns.uploadExcel }}
        </BaseButton>
      </div>
      <p>{{ resource.text.createNewQuestion }}</p>
      <ul class="question-type-list">
        <li
          v-for="item in questionType"
          class="question-type-item"
          :key="item.id"
        >
          <div
            class="question-type-icon"
            :class="item.icon"
            @click="handleOpenQuestionDetail(item.id)"
          ></div>
          <span>{{ item.label }}</span>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Resource from '../../script/resource';
import Enum from '../../script/enum';
import Header from '../../components/layout/TheHeader.vue';
import BaseButton from '../../components/base/BaseButton.vue';
export default {
  components: { Header, BaseButton },
  data() {
    return {
      questionType: Resource.questionType,
    };
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    exercise: (state) => state.exercise.exerciseData,
  }),
  created() {
    if (this.exercise.ExerciseName === '') {
      this.$router.replace('/');
    }
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
@import url('../../style/page/exercise/newExercise.css');
</style>
