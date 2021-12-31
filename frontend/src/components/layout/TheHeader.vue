<template lang="">
  <div class="m-header">
    <div class="m-header-icon-back icon-back" @click="handleTurnBack"></div>
    <div class="exercise-info">
      <div
        class="subject-avatar"
        :style="`background-image: url(${exercise.ExerciseImage});`"
      ></div>
      <!-- <div class="exercise-name">Nhập tên bài tập</div> -->
      <input
        class="exercise-name"
        placeholder="Nhập tên bài tập..."
        v-model="exerciseName"
      />
    </div>
    <div class="m-header-toolbar">
      <div class="m-header-toolbar-left">
        <BaseCombobox
          v-model="subjectId"
          :options="subjects"
          classList="m-header-subject"
        />
        <BaseCombobox
          v-model="gradeId"
          :options="grades"
          classList="m-header-grade ml-12"
        />
        <BaseButton class="m-second-btn ml-12" @click="isShowDialog = true">
          {{ resource.btns.addInfor }}
        </BaseButton>
      </div>
      <div class="m-header-toolbar-right">
        <BaseButton class="m-primary-btn" @click="handleChangeStatusExercise">
          {{ resource.btns.finish }}
        </BaseButton>
      </div>
    </div>
    <ExerciseDialog
      v-if="isShowDialog"
      @closeDialog="isShowDialog = false"
      :mode="2"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex';
import axios from 'axios';
import Enum from '../../script/enum';
import BaseButton from '../base/BaseButton.vue';
import BaseCombobox from '../base/BaseCombobox.vue';
import ExerciseDialog from '../../view/exercise/ExerciseDialog.vue';
export default {
  components: { BaseCombobox, BaseButton, ExerciseDialog },
  data() {
    return {
      isShowDialog: false,
      gradeId: null,
      subjectId: null,
      exerciseName: '',
      subjects: [],
      grades: [],
    };
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    exercise: (state) => state.exercise.exerciseData,
  }),
  watch: {
    exercise: {
      handler: function () {
        this.updateExerciseValue();
      },
      deep: true,
    },
    gradeId: function () {
      const data = this.grades
        .filter((grade) => grade.id === this.gradeId)
        .map((grade) => {
          return {
            GradeId: grade.id,
            GradeName: grade.label,
            GradeCode: grade.code,
          };
        });
      this.$store.commit('SET_EXERCISE_GRADE', data[0]);
    },
    subjectId: function () {
      const data = this.subjects
        .filter((subject) => subject.id === this.subjectId)
        .map((subject) => {
          return {
            SubjectId: subject.id,
            SubjectName: subject.label,
            SubjectCode: subject.code,
          };
        });
      this.$store.commit('SET_EXERCISE_SUBJECT', data[0]);
    },
    exerciseName: function () {
      this.$store.commit('SET_EXERCISE_NAME', this.exerciseName);
    },
  },
  async created() {
    let gradeList = [];
    if (sessionStorage.getItem('grades')) {
      gradeList = JSON.parse(sessionStorage.getItem('grades'));
    } else {
      await this.$store.dispatch('getGrades');
      gradeList = this.$store.getters.getGradeList;
      sessionStorage.setItem('grades', JSON.stringify(gradeList));
    }
    this.grades = gradeList;

    // Lấy dánh sách môn học
    let subjectList = [];
    if (sessionStorage.getItem('subjects')) {
      subjectList = JSON.parse(sessionStorage.getItem('subjects'));
    } else {
      await this.$store.dispatch('getSubjects');
      subjectList = this.$store.getters.getSubjectList;
      sessionStorage.setItem('subjects', JSON.stringify(subjectList));
    }
    this.subjects = subjectList;
    this.$store.dispatch('getSubjects');
    this.$store.dispatch('getGrades');
    this.updateExerciseValue();
  },
  /**
   * Hàm xử lý nút quay lại trang trước
   * Author: NVThang (16/12/2021)
   */
  methods: {
    /**
     * Hàm cập nhật trạng thái bài tập
     * Author: NVThang (20/12/2021)
     */
    handleChangeStatusExercise() {
      const me = this;
      if (me.exercise) {
        me.$store.commit('SET_EXERCISE_STATUS', Enum.ExerciseStatus.Finish);
        axios
          .put(
            me.resource.api.exercise + '/' + me.exercise.ExerciseId,
            me.exercise,
          )
          .then(() => {
            me.$router.push('/');
            me.$store.commit('RESET_EXERCISE');
            // me.$store.commit('RESET_EXERCISE_LIST');
          })
          .catch();
      }
    },
    /**
     * Hàm cập nhật dữ liệu cho Id khối, Id môn học,tên môn học
     * Author: NVThang (21/12/2021)
     */
    updateExerciseValue() {
      this.gradeId = this.exercise.GradeId;
      this.subjectId = this.exercise.SubjectId;
      this.exerciseName = this.exercise.ExerciseName;
    },
    /**
     * Hàm xử lý khi ấn nút back
     * Author: NVThang (16/12/2021)
     */
    handleTurnBack() {
      this.$router.push('/');
      this.$store.commit('RESET_EXERCISE');
      // this.$store.commit('RESET_EXERCISE_LIST');
    },
  },
};
</script>

<style>
@import url('../../style/layout/header.css');
</style>
