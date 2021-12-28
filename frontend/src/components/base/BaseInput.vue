<template>
  <div class="m-input-container" :class="classList">
    <input
      :tabindex="tabindex"
      :type="type"
      :placeholder="placeholder"
      class="m-input"
      :class="[classList, isError ? 'error' : '']"
      :value="value"
      :style="iconClass ? 'padding-left: 35px' : 'padding-left: 12px'"
      @input="onInput"
      @blur="validateValue"
      @change="onChange"
      @keypress.enter="$emit('keypressEnter')"
      @focus="$emit('focus', $event.target.value)"
      ref="input"
    />
    <div v-if="iconClass" class="m-input-icon" :class="iconClass"></div>
    <div v-if="isError" class="m-input-error">
      {{ errorContent }}
    </div>
  </div>
</template>
<script>
import Resource from '../../script/resource';

export default {
  name: 'base-input',
  props: [
    'type',
    'valueType',
    'classList',
    'value',
    'placeholder',
    'iconClass',
    'requiredType',
    'classContainer',
    'tabindex',
  ],
  data() {
    return {
      isError: false,
      errorContent: '',
      active: false,
      isChange: false,
      showTooltip: false,
    };
  },
  methods: {
    onInput(event) {
      // Can add validation here

      this.$emit('input', event.target.value);
    },
    onChange(event) {
      // Can add validation here
      this.$emit('change', event.target.value);
    },
    focusInput() {
      // focus vào ô input
      this.$refs.input.focus();
    },
    /*
    Hàm validate dữ liệu và xét error
    Author: NVThang (10/11/2021)
    */
    validateValue() {
      if (this.requiredType) {
        if (this.value && this.value.length > 0) {
          this.errorContent = '';
          this.isError = false;
        } else {
          this.isError = true;
          this.errorContent = Resource.errorContent[this.requiredType];
          return;
        }
      }
    },
  },
};
</script>
<style scoped>
.error {
  border-color: #ff4747 !important;
}
</style>
