import Vue from 'vue';
import Vuex from 'vuex';
import moduleGrade from './grade';
import moduleSubject from './subject';
import moduleTopic from './topic';
import moduleExercise from './exercise';
import moduleQuestion from './question';
import moduleResource from './resource';

Vue.use(Vuex);

const store = new Vuex.Store({
  modules: {
    grade: moduleGrade,
    subject: moduleSubject,
    topic: moduleTopic,
    exercise: moduleExercise,
    question: moduleQuestion,
    resource: moduleResource,
  },
});

export default store;
