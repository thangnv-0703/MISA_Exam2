import Resource from '../script/resource';

const moduleResource = {
  state: () => ({
    resourceData: Resource,
    toastMessage: {
      isShowToast: false,
      toast: {
        type: 'success',
        message: 'Thêm thành công',
        title: 'Thành công',
      },
    },
  }),

  mutations: {
    SHOW_TOAST(state, data) {
      state.toastMessage.isShowToast = true;
      state.toastMessage.toast = data;
    },
    HIDE_TOAST(state) {
      state.toastMessage.isShowToast = false;
      state.toastMessage.toast = {};
    },
  },
};

export default moduleResource;
