<template>
  <div class="card" :class="classList">
    <div class="card-type">{{ cardTitle }}</div>
    <img class="card-image" :src="exercise.ExerciseImage" />
    <div class="card-body">
      <div class="card-info">
        <span class="card-title">{{ exercise.ExerciseName }}</span>
        <div class="card-question-count">
          <div class="card-icon-courseware"></div>
          <span>{{ exercise.QuestionCount }} {{ resource.text.sentence }}</span>
          <div
            class="card-status"
            :style="`background-color: ${exerciseStatus.bgColor}; color: ${exerciseStatus.color}`"
          >
            {{ exerciseStatus.label }}
          </div>
        </div>
      </div>
      <div
        class="card-icon-ellipsis icon-ellipsis-v"
        @click="isShowPopover = !isShowPopover"
      >
        <div class="card-popover" v-if="isShowPopover">
          <div class="card-popover-item" @click="handleOpenExercise">
            {{ resource.cardPopoverItem[0] }}
          </div>
          <div class="card-popover-item" @click="isShowPopup = true">
            {{ resource.cardPopoverItem[1] }}
          </div>
        </div>
      </div>
    </div>
    <BasePopup
      :confirm="true"
      v-if="isShowPopup"
      @onConfirm="handleDeleteExercise"
      @onClosePopup="isShowPopup = false"
      :messages="[
        resource.popup.message.confirmDeleteExercise(exercise.ExerciseName),
      ]"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex';
import axios from 'axios';
import vClickOutside from 'v-click-outside';
import BasePopup from '../../components/base/BasePopup.vue';

export default {
  name: 'Card',
  components: {
    BasePopup,
  },
  props: ['classList', 'exercise'],
  data() {
    return {
      isShowPopover: false,
      isShowPopup: false,
    };
  },
  directives: {
    clickOutside: vClickOutside.directive,
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    cardTitle() {
      return (
        this.exercise.SubjectName + ' ' + this.exercise.GradeName.split(' ')[1]
      );
    },
    exerciseStatus() {
      return this.resource.exerciseStatus.filter(
        (status) => status.id === this.exercise.Status,
      )[0];
    },
  }),
  methods: {
    /**
     * Hàm hiện điều hướng sang trang danh sách câu hỏi của bài tập
     * Author: NVThang (20/12/2021)
     */
    handleOpenExercise() {
      // this.$store.commit('SET_EXERCISE', this.exercise);
      this.$router.push('/exercise/' + this.exercise.ExerciseId);
    },
    /**
     * Hàm xóa bài tập
     * Author: NVThang (20/12/2021)
     */
    handleDeleteExercise() {
      const me = this;
      axios
        .delete(me.resource.api.exercise + '/' + me.exercise.ExerciseId)
        .then((response) => {
          console.log(response);
          me.$store.commit('DELETE_EXERCISE_BY_ID', me.exercise.ExerciseId);
          me.$store.commit(
            'SHOW_TOAST',
            me.resource.toast.deleteSuccessExercise,
          );
        })
        .catch(() => {
          me.$store.commit('SHOW_TOAST', me.resource.toast.deleteFailExercise);
        });
      me.isShowPopup = false;
    },
  },
};
</script>

<style scoped>
@import url('../../style/page/exercise/exerciseCard.css');
</style>
