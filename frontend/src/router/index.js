import Vue from 'vue';
import VueRouter from 'vue-router';
import Overview from '../view/Overview.vue';
import NewExercise from '../view/exercise/NewExercise.vue';
import ExerciseDetail from '../view/exercise/ExerciseDetail.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Overview',
    component: Overview,
  },
  {
    path: '/new-exercise',
    name: 'NewExercise',
    component: NewExercise,
  },
  {
    path: '/exercise/:id',
    name: 'ExerciseDetail',
    component: ExerciseDetail,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});
export default router;
