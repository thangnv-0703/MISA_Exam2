<template lang="">
  <div id="toast">
    <div :class="'m-toast m-toast-' + toast.type" ref="toast">
      <div class="m-toast-icon" :class="'m-toast-icon-' + toast.type"></div>
      <div class="m-toast-body">
        <div class="m-toast-title">{{ toast.title }}</div>
        <div class="m-toast-message">{{ toast.message }}</div>
      </div>
      <div class="m-toast-close icon-24" @click="removeToast"></div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
export default {
  props: ['type', 'message', 'title'],
  data() {
    return {
      icon: {
        success: 'fas fa-check',
        error: 'fas fa-exclamation-triangle',
        infor: 'fas fa-info-circle',
        warning: 'fas fa-exclamation',
      },
    };
  },
  computed: mapState({
    toast: (state) => state.resource.toastMessage.toast,
  }),
  methods: {
    /*
    Hàm ẩn toast khi ấn nút X
    Author: NVThang (10/11/2021)
    */
    removeToast() {
      this.$store.commit('HIDE_TOAST');
    },
  },
  // Hiển thị tooltip trong 3s
  mounted() {
    setTimeout(() => this.removeToast(), 5000);
  },
};
</script>
<style lang=""></style>
