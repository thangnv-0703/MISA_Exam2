<template>
  <div class="m-popup">
    <div class="m-popup-backdrop"></div>
    <div class="m-popup-container">
      <div class="m-popup-header">
        <p class="m-popup-title">{{ resource.popup.title }}</p>
        <div class="m-popup-btn-close" @click="$emit('onClosePopup')"></div>
      </div>
      <div class="m-popup-body">
        <ul class="m-popup-text">
          <li v-for="(message, index) in messages" :key="index">
            {{ message }}
          </li>
        </ul>
      </div>
      <div v-if="confirm" class="m-popup-footer">
        <BaseButton classList="m-second-btn" @click="$emit('onClosePopup')">
          {{ resource.btns.close }}
        </BaseButton>
        <BaseButton classList="m-primary-btn" @click="$emit('onConfirm')">
          {{ resource.btns.delete }}
        </BaseButton>
      </div>
      <div v-else class="m-popup-footer">
        <BaseButton classList="m-primary-btn" @click="$emit('onClosePopup')">
          {{ resource.btns.ok }}
        </BaseButton>
      </div>
    </div>
  </div>
</template>
<script>
import { mapState } from 'vuex';
import BaseButton from './BaseButton.vue';
export default {
  components: { BaseButton },
  props: ['messages', 'confirm'],
  computed: mapState({
    resource: (state) => state.resource.resourceData,
  }),
};
</script>
<style>
@import url('../../style/component/popup.css');
</style>
