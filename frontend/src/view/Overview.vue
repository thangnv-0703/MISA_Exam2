<template lang="">
  <div class="overview">
    <BaseLoading v-if="isShowLoadding" />
    <!-- Header -->
    <div class="header">
      <div class="header-left">
        <div
          class="header-tab"
          v-for="item in menuItems"
          :key="item.text"
          :class="item.active ? 'active-header' : ''"
          @click="handleClickMenuItems(item.active)"
        >
          <div class="tab-icon" :class="item.icon"></div>
          <div class="tab-text">{{ item.text }}</div>
        </div>
      </div>
      <div class="header-right">
        <ul class="icon-symbol-list">
          <li class="icon-symbol icon-sprites"></li>
          <li class="icon-symbol icon-setting"></li>
          <li class="icon-symbol icon-help"></li>
          <li class="icon-symbol icon-notification">
            <span class="icon-number-notification">2</span>
          </li>
        </ul>
        <div class="user-info">
          <div class="user-name">Nam</div>
          <div class="user-avatar icon-avatar"></div>
        </div>
      </div>
    </div>

    <!-- Content -->
    <div class="content">
      <div class="content-compose">
        <div class="tags-compose">
          <div
            class="tag-item"
            v-for="(tag, index) in tagItems"
            :key="tag"
            :class="index === 0 ? 'tag-active' : ''"
          >
            {{ tag }}
          </div>
        </div>
        <div class="content-btn-group">
          <BaseButton
            classList="m-primary-btn btn-compose"
            @click="isShowDialog = true"
          >
            {{ resource.btns.compose }}
          </BaseButton>
        </div>
      </div>
      <div class="m-row filter-bars">
        <div class="filter-left">
          <BaseCombobox v-model="gradeId" :options="grades" />
          <BaseCombobox v-model="subjectId" :options="subjects" />
          <BaseCombobox
            v-model="topicId"
            :options="topics"
            classList="combobox-topic"
          />
          <BaseCombobox v-model="exerciseStatusId" :options="exerciseStatus" />
        </div>
        <div class="filter-right">
          <BaseInput
            v-model="searchText"
            placeholder="Nhập tên học liệu để tìm kiếm"
            iconClass="icon-search"
            @keypressEnter="filterData()"
          />
        </div>
      </div>
      <div class="m-row grid-container">
        <div v-if="noData" style="">
          {{ resource.text.noExercise }}
        </div>
        <ExerciseCard
          classList="card-item"
          v-for="exercise in exercises"
          :exercise="exercise"
          :key="exercise.ExerciseId"
        />
      </div>
    </div>
    <div class="footer">{{ resource.label.totalRecord(totalRecord) }}</div>
    <transition name="fade">
      <ExerciseDialog
        v-if="isShowDialog"
        @closeDialog="isShowDialog = false"
        :mode="1"
      />
    </transition>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import axios from 'axios';
import Resource from '../script/resource';
import BaseInput from '../components/base/BaseInput.vue';
import BaseButton from '../components/base/BaseButton.vue';
import BaseCombobox from '../components/base/BaseCombobox.vue';
import BaseLoading from '../components/base/BaseLoading.vue';
import ExerciseCard from './exercise/ExerciseCard.vue';
import ExerciseDialog from './exercise/ExerciseDialog.vue';

export default {
  name: 'Overview',
  components: {
    BaseCombobox,
    BaseButton,
    BaseInput,
    ExerciseCard,
    ExerciseDialog,
    BaseLoading,
  },
  data() {
    return {
      menuItems: Resource.menuItems,
      tagItems: Resource.tagsItem,
      isShowDialog: false,
      isShowLoadding: false,
      noData: false,
      gradeId: -1,
      subjectId: -1,
      topicId: -1,
      exerciseStatusId: -1,
      searchText: '',

      subjects: [],
      grades: [],
      topics: [],
      pageSize: 40,
      pageIndex: 1,
      totalRecord: 0,
    };
  },
  computed: mapState({
    resource: (state) => state.resource.resourceData,
    exercises() {
      return this.$store.getters.exerciseList;
    },
    exerciseStatus() {
      return this.resource.exerciseStatus;
    },
    trigger() {
      return [
        this.subjectId,
        this.gradeId,
        this.topicId,
        this.exerciseStatusId,
      ];
    },
  }),
  watch: {
    trigger() {
      this.filterData();
    },
  },
  async created() {
    // Lấy danh sách khối lớp
    let gradeList = [];
    if (sessionStorage.getItem('grades')) {
      gradeList = JSON.parse(sessionStorage.getItem('grades'));
    } else {
      await this.$store.dispatch('getGrades');
      gradeList = this.$store.getters.getGradeList;
      sessionStorage.setItem('grades', JSON.stringify(gradeList));
    }
    this.grades = this.resource.grades.concat(gradeList);

    // Lấy dánh sách môn học
    let subjectList = [];
    if (sessionStorage.getItem('subjects')) {
      subjectList = JSON.parse(sessionStorage.getItem('subjects'));
    } else {
      await this.$store.dispatch('getSubjects');
      subjectList = this.$store.getters.getSubjectList;
      sessionStorage.setItem('subjects', JSON.stringify(subjectList));
    }
    this.subjects = this.resource.subjects.concat(subjectList);

    // Lấy dánh sách chủ đề
    let topicList = [];
    if (sessionStorage.getItem('topics')) {
      topicList = JSON.parse(sessionStorage.getItem('topics'));
    } else {
      await this.$store.dispatch('getTopics');
      topicList = this.$store.getters.getTopicList;
      sessionStorage.setItem('topics', JSON.stringify(topicList));
    }
    this.topics = this.resource.topics.concat(topicList);

    if (!sessionStorage.getItem('searchTags')) {
      sessionStorage.setItem(
        'searchTags',
        JSON.stringify(this.$store.getters.getSearchTags),
      );
    }
    this.filterData();
  },
  methods: {
    /**
     * Hàm filter dữ liệu
     * Author: NVThang (22/12/2021)
     */
    filterData() {
      const me = this;
      me.isShowLoadding = true;
      axios
        .get(
          me.resource.api.exercise +
            `/filter?searchText=${me.searchText}&subjectId=${me.subjectId}&gradeId=${me.gradeId}&topicId=${me.topicId}&status=${me.exerciseStatusId}&pageSize=${me.pageSize}&pageIndex=${me.pageIndex}`,
        )
        .then((response) => {
          // me.filterExercises = response.data;
          me.$store.commit('SET_EXERCISE_LIST', response.data.exercise);
          me.totalRecord = response.data.totalRecord;
          me.noData = response.data.exercise.length === 0;
          me.isShowLoadding = false;
        })
        .catch(() => {});
    },
    handleClickMenuItems(active) {
      if (!active) {
        this.$store.commit('SHOW_TOAST', this.resource.toast.developingFeature);
      }
    },
  },
};
</script>

<style scoped>
@import url('../style/page/overview.css');
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.fade-enter-active,
.fade-leave-active {
  transition: all 1s ease;
}
</style>
