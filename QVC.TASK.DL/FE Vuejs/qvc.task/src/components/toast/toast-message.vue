<template>
  <div class="toast-container display-toast" v-if="visible">
    <div class="toast-message">
      <div class="icon icon-24 icon-info" v-if="isShowIconInfo"></div>
      <div class="icon icon-24 icon-waring" v-if="isShowIconWarning"></div>
      <div class="icon icon-24 icon-success" v-if="isShowIconSuccess"></div>
      <div class="title">{{ title }}</div>
      <div class="content">{{ message }}</div>
      <div class="icon icon-16 close" @click="close"></div>
    </div>
  </div>
</template>
<script>
import { ENUMTOAST } from "@/enum.js";
export default {
  name: "ToastMessage",
  components: {},
  emits: [],
  props: [],
  watch: {},
  created() {},
  methods: {
    /**
     * Sự kiện hiển thị toast
     */
    show(title, message, type) {
      this.title = title;
      this.message = message;
      this.visible = true;

      switch (type) {
        case ENUMTOAST.Waring:
          this.isShowIconWarning = true;
          this.isShowIconInfo = false;
          this.isShowIconSuccess = false;
          break;
        case ENUMTOAST.Info:
          this.isShowIconWarning = false;
          this.isShowIconInfo = true;
          this.isShowIconSuccess = false;
          break;
        case ENUMTOAST.Success:
          this.isShowIconWarning = false;
          this.isShowIconInfo = false;
          this.isShowIconSuccess = true;
          break;
        default:
          break;
      }

      clearTimeout(this.timeout);
      this.timeout = setTimeout(() => {
        this.close();
      }, 5000);
    },

    /**
     * Sự kiện tắt toast
     */
    close() {
      this.visible = false;
    },
  },
  data() {
    return {
      /**Hiển thị toast */
      visible: false,

      /**Nội dung toast */
      message: "",

      /**Nội dung title */
      title: "",

      /**Thời gian tắt */
      timeout: null,

      /**Hiển thị icon warning */
      isShowIconWarning: false,

      /**Hiển thị icon info */
      isShowIconInfo: false,

      /**Hiển thị icon success */
      isShowIconSuccess: false,

      /**Dữ liệu enum */
      ENUMTOAST,
    };
  },
};
</script>

<style scoped>
.toast-container {
  position: fixed;
  bottom: 20px;
  right: 20px;
  z-index: 9999;
  min-width: 450px;
  height: 56px;
  width: auto;
}

.toast-message {
  display: flex;
  align-items: center;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  animation-duration: 0.5s;
  animation-fill-mode: both;
  padding: 10px;
  height: 30px;
}

.title {
  color: green;
  font-weight: 700;
  padding-right: 5px;
}

.content {
  flex: 1;
  font-size: 15px;
  font-weight: 400;
}

.icon-info {
  background-image: url(./../../assets/img/info.svg);
  margin-right: 8px;
}

.icon-waring {
  background-image: url(./../../assets/img/waring-tron.svg);
  margin-right: 8px;
}

.icon-success {
  background-image: url(./../../assets/img/check-verify.svg);
  margin-right: 8px;
}

.close {
  background-image: url(./../../assets/img/Close.svg);
  cursor: pointer;
  margin-left: 8px;
}

.display-toast {
  animation: toastLeft ease 0.3s, toastOut linear 1s 5s forwards;
  box-shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}

.text-red {
  color: red !important;
}

@keyframes toastLeft {
  from {
    opacity: 0;
    transform: translateX(calc(100% + 32px));
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes toastOut {
  from {
    opacity: 1;
    transform: translateX(0);
  }
  to {
    opacity: 0;
    transform: translateX(calc(100% + 32px));
  }
}
</style>