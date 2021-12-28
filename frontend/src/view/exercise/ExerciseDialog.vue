<template>
  <div class="dialog-container">
    <!-- <transition name="fade" > -->
    <div class="dialog-modal"></div>
    <div class="dialog-box">
      <div class="dialog-header">
        <p>{{ resource.btns.addInfor }}</p>
        <span
          class="dialog-close-x icon-x"
          @click="$emit('closeDialog')"
        ></span>
      </div>
      <div class="dialog-content">
        <div class="dialog-left">
          <span>{{ resource.label.avatar }}</span>
          <div class="dialog-image">
            <img class="exercise-image" :src="exerciseData.ExerciseImage" />
            <div class="upload-img icon-upload">
              <input type="file" class="input-file" @change="uploadImage" />
            </div>
          </div>
        </div>
        <div class="dialog-right">
          <div class="m-row">
            <div class="m-input-group">
              <label class="m-label">
                {{ resource.label.exerciseName }}
                <span style="color: red">(*)</span>
              </label>
              <BaseInput
                classList="input-block"
                placeholder="Nhập tên bài tập"
                v-model="exerciseData.ExerciseName"
                ref="exerciseData.ExerciseName"
                requiredType="requireExerciseName"
              />
            </div>
          </div>
          <div class="m-row">
            <div class="m-input-group" style="margin-right: 16px">
              <label class="m-label">
                {{ resource.label.subject }}
                <span style="color: red">(*)</span>
              </label>
              <BaseCombobox
                classList="input-half-block"
                v-model="exerciseData.SubjectId"
                :options="subjects"
                ref="subject"
                requiredType="requireSubject"
              />
            </div>
            <div class="m-input-group">
              <label class="m-label">
                {{ resource.label.grade }} <span style="color: red">(*)</span>
              </label>
              <BaseCombobox
                classList="input-half-block"
                v-model="exerciseData.GradeId"
                :options="grades"
                ref="grade"
                requiredType="requireGrade"
              />
            </div>
          </div>
          <div class="m-row">
            <div class="m-input-group">
              <label class="m-label">
                {{ resource.label.topic }}
              </label>
              <BaseCombobox
                classList="input-block"
                v-model="exerciseData.TopicId"
                :options="topics"
              />
            </div>
          </div>
          <div class="m-row">
            <div class="m-input-group" style="margin-bottom: 0">
              <label class="m-label">
                {{ resource.label.searchTag }}
              </label>
              <div class="search-tags">
                <div
                  class="search-tag"
                  v-for="(tag, index) in defaultTags"
                  :key="index"
                  :class="
                    exerciseData.SearchTags.includes(tag) ? 'chosen-tag' : ''
                  "
                  @click="handleAddSearchTag(tag)"
                >
                  {{ tag }}
                </div>
              </div>
              <div class="chosen-tags">
                <div
                  class="search-tag"
                  v-for="tag in exerciseData.SearchTags"
                  :key="tag"
                  @click="handleRemoveSearchTag(tag)"
                >
                  {{ tag }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="dialog-footer">
        <BaseButton classList="m-second-btn" @click="$emit('closeDialog')">
          {{ resource.btns.close }}
        </BaseButton>
        <BaseButton
          classList="m-primary-btn ml-10"
          @click="handleSaveExerciseData"
        >
          {{ resource.btns.save }}
        </BaseButton>
      </div>
    </div>
    <!-- </transition> -->
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Enum from '../../script/enum';
import BaseInput from '../../components/base/BaseInput.vue';
import BaseCombobox from '../../components/base/BaseCombobox.vue';
import BaseButton from '../../components/base/BaseButton.vue';

export default {
  components: { BaseInput, BaseCombobox, BaseButton },
  props: ['mode'],
  data() {
    return {
      formMode: Enum.ExerciseFormMode.Add,
      isChooseFile: false,
      exerciseData: {
        ExerciseName: '',
        ExerciseImage: null,
        SubjectId: null,
        GradeId: null,
        TopicId: null,
        SearchTags: [],
      },
    };
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    subjects: (state) => state.subject.subjects,
    grades: (state) => state.grade.grades,
    topics: (state) => state.topic.topics,
    exercise: (state) => state.exercise.exerciseData,
    defaultTags: function () {
      return this.$store.getters.getSearchTags;
    },
  }),
  watch: {
    'exerciseData.GradeId': function () {
      const data = this.grades
        .filter((grade) => grade.id === this.exerciseData.GradeId)
        .map((grade) => {
          return {
            GradeId: grade.id,
            GradeName: grade.label,
            GradeCode: grade.code,
          };
        });
      this.$store.commit('SET_EXERCISE_GRADE', data[0]);
    },
    'exerciseData.SubjectId': function () {
      const data = this.subjects
        .filter((subject) => subject.id === this.exerciseData.SubjectId)
        .map((subject) => {
          return {
            SubjectId: subject.id,
            SubjectName: subject.label,
            SubjectCode: subject.code,
          };
        });
      this.$store.commit('SET_EXERCISE_SUBJECT', data[0]);
      this.$store.commit('SET_SEARCH_TAGS', this.exerciseData.SubjectId);
      this.$store.commit('UPDATE_TOPICS', this.exerciseData.SubjectId);
      if (!this.isChooseFile) {
        this.handleFile(data[0].SubjectCode);
      }
      this.exerciseData.SearchTags = this.exerciseData.SearchTags.filter(
        (tag) => this.defaultTags.includes(tag),
      );
    },
    'exerciseData.ExerciseImage': function () {
      console.log(this.exerciseData.ExerciseImage);
    },
    topics: function () {
      let exitTopicId = 0;
      this.topics.forEach((topic) => {
        if (topic.id === this.exerciseData.TopicId) {
          exitTopicId = 1;
          return;
        }
      });
      if (!exitTopicId) {
        this.exerciseData.TopicId = null;
      }
    },
    'exerciseData.ExerciseName': function () {
      this.$store.commit('SET_EXERCISE_NAME', this.exerciseData.ExerciseName);
    },
  },
  created() {
    this.$store.dispatch('getSubjects');
    this.$store.dispatch('getGrades');
    if (this.mode === Enum.ExerciseFormMode.Edit) {
      this.formMode = Enum.ExerciseFormMode.Edit;
      this.exerciseData = this.exercise;
      console.log(this.exerciseData.ExerciseImage);
    }
    if (!this.exerciseData.ExerciseImage) {
      this.handleFile(null);
    } else {
      this.isChooseFile = true;
    }
  },
  methods: {
    /**
     * Hàm xử lý việc tải file ảnh
     * Author: NVThang (24/12/2021)
     */
    uploadImage() {
      const file = document.querySelector('input[type=file]').files[0];
      const reader = new FileReader();

      let rawImg;
      reader.onloadend = () => {
        rawImg = reader.result;
        this.exerciseData.ExerciseImage = rawImg;
      };
      reader.readAsDataURL(file);
      this.isChooseFile = true;
      // this.exerciseData.ExerciseImage = file;
    },
    /**
     * Hàm xử lý việc thay đổi ảnh theo môn học
     * Author: NVThang (11/12/2021)
     */
    handleFile(subjectCode) {
      console.log('edit theo môn');
      if (!subjectCode) {
        this.exerciseData.ExerciseImage = this.resource.imageSubject[0].image;
      } else {
        this.exerciseData.ExerciseImage = this.resource.imageSubject.filter(
          (imageSubject) => imageSubject.subjectCode === subjectCode,
        )[0].image;
      }
    },
    /*
    Hàm validate dữ liệu khi bấm nút lưu
    Author: NVThang (17/11/2021)
    */
    validateData() {
      let valid = true;
      let firstChildFocus = false;
      const items = this.$refs;
      for (let [key] of Object.entries(items)) {
        items[key].validateValue();
        if (items[key].isError) {
          valid = false;
          if (firstChildFocus === false) {
            items[key].focusInput();
            firstChildFocus = true;
          }
        }
      }
      return valid;
    },
    /**
     * Hàm xử lí nút lưu của dialog
     * Author: NVThang (11/12/2021)
     */
    handleSaveExerciseData() {
      let isValid = this.validateData();
      if (isValid) {
        this.$store.commit(
          'SET_EXERCISE_SEARCHTAGS',
          this.exerciseData.SearchTags,
        );
        this.$store.commit('SET_EXERCISE_TOPICID', this.exerciseData.TopicId);
        this.$store.commit(
          'SET_EXERCISE_IMAGE',
          this.exerciseData.ExerciseImage,
        );
        console.log(this.exerciseData.ExerciseImage);
        if (this.formMode === Enum.ExerciseFormMode.Add) {
          this.$router.push('/new-exercise');
        } else {
          this.$emit('closeDialog');
        }
      }
    },
    /**
     * Hàm thêm thẻ tìm kiếm vào ô
     * Author: NVThang (11/12/2021)
     */
    handleAddSearchTag(tag) {
      if (!this.exerciseData.SearchTags.includes(tag)) {
        this.exerciseData.SearchTags.push(tag);
      }
    },
    /**
     * Hàm xóa thẻ tìm kiếm vào ô
     * Author: NVThang (11/12/2021)
     */
    handleRemoveSearchTag(tag) {
      this.exerciseData.SearchTags = this.exerciseData.SearchTags.filter(
        (item) => item !== tag,
      );
    },
  },
};
</script>

<style scoped>
@import url('../../style/page/exercise/exerciseDialog.css');
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.fade-enter-active,
.fade-leave-active {
  transition: all 1s ease;
}
</style>
