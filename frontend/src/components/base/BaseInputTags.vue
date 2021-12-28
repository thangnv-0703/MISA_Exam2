<template lang="">
  <div
    class="m-inputs-container"
    :class="classList"
    @click="$refs.inputs.focus()"
  >
    <div class="m-tag" v-for="(tag, index) in value" :key="index">
      <span class="m-tag-name">{{ tag }}</span>
      <span class="m-tag-x" @click="handleRemoveTag(index)"></span>
    </div>
    <input
      type="text"
      class="m-inputs"
      ref="inputs"
      v-model="content"
      placeholder="Nhập đán án rồi ấn enter..."
      @keypress.enter="handleAddTag"
    />
    <div class="m-inputs-x" @click="$emit('onDeleteInput')"></div>
  </div>
</template>
<script>
export default {
  props: ['value', 'classList', 'index'],
  data() {
    return {
      content: '',
    };
  },
  methods: {
    /**
     * Hàm thêm tag vào input
     * Author: NVThang (21/12/2021)
     */
    handleAddTag() {
      if (this.content) {
        let tags = [...this.value];
        tags.push(this.content);
        this.content = '';
        this.$emit('input', tags);
      }
    },
    /**
     * Hàm xóa tag khỏi input
     * Author: NVThang (21/12/2021)
     */
    handleRemoveTag(index) {
      let tags = [...this.value];
      tags.splice(index, 1);
      this.$emit('input', tags);
    },
    /**
     * Hàm focus vào input
     * Author: NVThang (21/12/2021)
     */
    focusInput() {
      this.$refs.inputs.focus();
    },
  },
};
</script>
<style></style>
