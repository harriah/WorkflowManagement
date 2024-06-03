<template>
  <!-- The Modal -->
  <div id="AccountVerification" class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <span
        class="icon icon-24 close"
        @click="closePopup(ENUMBTN.Close)"
      ></span>
      <!-- Modal header -->
      <h2>Thông báo</h2>
      <div
        class="item-warning"
        v-for="(item, index) in this.error"
        :key="index"
      >
        <div
          v-if="isShowIconVerify"
          class="icon icon-24 icon-checkverify"
        ></div>
        <div v-if="isShowIconWaring" class="icon icon-24 icon-waring"></div>
        <div class="item-content">{{ item }}</div>
      </div>
      <div class="modal-foodter">
        <button v-show="isShowBtnCancel" class="btn btn-light">Huỷ</button>
        <button class="btn btn-success" @click="closePopup(ENUMBTN.Agree)">
          Đồng ý
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { ENUMBTN } from "./../../enum.js";
import { ENUMWARNING } from "./../../enum.js";
export default {
  name: "PopupWarning",
  props: ["error", "icon", "keywarning"],
  emits: ["closePopup"],
  components: {},
  created() {
    if (this.icon == true) {
      this.isShowIconVerify = true;
      this.isShowIconWaring = false;
    } else {
      this.isShowIconVerify = false;
      this.isShowIconWaring = true;
    }
  },
  methods: {
    closePopup(key) {
      switch (key) {
        case ENUMBTN.Close:
          break;
        case ENUMBTN.Agree:
          if (this.keywarning == ENUMWARNING.Verify) {
            //
          }
          break;
        case ENUMBTN.Cancel:
          break;
        default:
          break;
      }
      this.$emit("closePopup", this.keywarning, key);
    },
  },
  data() {
    return {
      // Hiển thị button huỷ
      isShowBtnCancel: false,

      /**Hiển thị icon waring */
      isShowIconWaring: true,

      /**Hiển thị icon verify */
      isShowIconVerify: false,

      ENUMBTN,
      ENUMWARNING,
    };
  },
};
</script>

<style scoped>
/* The Modal (background) */
.modal {
  position: fixed; /* Stay in place */
  z-index: 10; /* Sit on top */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0, 0, 0); /* Fallback color */
  background-color: rgba(0, 0, 0, 0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
  position: absolute;
  left: 50%; /* Change from left: 0; */
  top: 50%; /* Change from top: 0; */
  transform: translate(
    -50%,
    -50%
  ); /* Center the popup vertically and horizontally */
  background-color: #fefefe;
  margin: auto;
  padding: 20px;
  border: 1px solid #888;
  width: auto;
  border-radius: 8px;
}

.content-note {
  font-size: 13px;
  color: #9e9e9e;
  font-style: italic;
}

/* The Close Button */
.close {
  float: right;
  background-image: url(./../../assets/img/Close.svg);
}

.icon-waring {
  background-image: url(./../../assets/img/warning.svg);
  margin-right: 8px;
}

.icon-checkverify {
  background-image: url(./../../assets/img/check-verify.svg);
  margin-right: 8px;
}

.close:hover,
.close:focus {
  filter: invert(57%) sepia(50%) saturate(596%) hue-rotate(64deg)
    brightness(97%) contrast(91%);
  text-decoration: none;
  cursor: pointer;
}

.modal-foodter {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
.item-warning {
  display: flex;
  align-items: center;
}
.item-warning + .item-warning {
  margin-top: 12px;
}
</style>
