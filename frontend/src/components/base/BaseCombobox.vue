<template lang="">
  <div
    class="m-combobox"
    v-click-outside="clickAway"
    :class="[classList, isError ? 'error' : '']"
    :style="`border-color: ${isShow ? '#8a6bf6' : ''}`"
  >
    <div
      class="m-combobox-input"
      :tabindex="tabindex"
      ref="input"
      :title="textValue"
    >
      {{ requiredText }} {{ textValue }}
    </div>
    <div
      class="m-combobox-btn icon-angle-down"
      @click="isShow = !isShow"
      ref="comboboxBtn"
    ></div>
    <div v-if="isShow" class="m-combobox-select">
      <div
        v-for="option in options"
        :value="option.id"
        :key="option.id"
        @click="handleChooseItem(option.id)"
        class="m-combobox-item"
        :class="{ 'm-item-selected': option.id === value }"
        :title="option.label"
      >
        {{ option.label }}
      </div>
    </div>
    <div class="m-combobox-error">
      {{ errorContent }}
    </div>
  </div>
</template>

<script>
// import Vue from 'vue';
import Resource from '../../script/resource';
import vClickOutside from 'v-click-outside';

// ẩn select khi click ra ngoài combobox
// Vue.directive('click-outside', {
//   bind(el, binding, vnode) {
//     el.clickOutsideEvent = (event) => {
//       if (!(el === event.target || el.contains(event.target))) {
//         vnode.context[binding.expression](event);
//       }
//     };
//     document.body.addEventListener('click', el.clickOutsideEvent);
//   },
//   unbind(el) {
//     document.body.removeEventListener('click', el.clickOutsideEvent);
//   },
// });

export default {
  name: 'base-combobox',
  props: [
    'options',
    'value',
    'placeholder',
    'classList',
    'tabindex',
    'requiredType',
    'requiredText',
  ],
  data() {
    return {
      textValue: '',
      isShow: false,
      isError: false,
      errorContent: '',
    };
  },
  directives: {
    clickOutside: vClickOutside.directive,
  },
  methods: {
    /**
     * Hàm validate giá trị của combobox
     */
    validateValue() {
      if (this.requiredType) {
        if (this.value) {
          this.errorContent = '';
          this.isError = false;
        } else {
          this.isError = true;
          this.errorContent = Resource.errorContent[this.requiredType];
          return;
        }
      }
    },
    /**
     * Hàm focus vào combobox
     * Author: NVThang (17/12/2021)
     */
    focusInput() {
      this.isShow = true;
    },
    /**
     * Hàm ẩn select khi click ra ngoài
     * Author: NVThang (11/12/2021)
     */
    clickAway() {
      this.isShow = false;
    },
    /*
    Hàm chọn id của rồi bind ra bên ngoài
    Author: NVThang (09/11/2021)
    */
    handleChooseItem(id) {
      this.$emit('input', id);
      this.isShow = false;
    },
    /**
     * Hàm kiểm tra giá trị value để cập nhật giá trị cho combobox
     * Author: NVThang (21/12/2021)
     */
    updateComboboxValue() {
      if (this.value !== null && this.options.length > 0) {
        this.textValue = this.options.filter(
          (option) => option.id === this.value,
        )[0].label;
      } else {
        this.textValue = '';
      }
    },
  },
  created() {
    this.updateComboboxValue();
  },
  watch: {
    value: function () {
      this.updateComboboxValue();
      this.validateValue();
    },
    options: function () {
      this.updateComboboxValue();
    },
  },
};
</script>

<style>
@import url('../../style/component/combobox.css');
</style>
