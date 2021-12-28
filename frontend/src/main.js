import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';
import VueAxios from 'vue-axios';
import router from './router/index';
import store from './store/index';
import vClickOutside from 'v-click-outside';

Vue.use(vClickOutside);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  VueAxios,
  axios,
  render: (h) => h(App),
}).$mount('#app');
