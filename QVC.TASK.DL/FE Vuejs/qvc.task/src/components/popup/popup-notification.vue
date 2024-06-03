<template>
  <!-- The Modal -->
  <div class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <span class="icon icon-24 close" @click="onClose"></span>
      <!-- Modal header -->
      <h2>{{ title }}</h2>
      <div
        class="item-warning"
        v-for="(item, index) in this.content"
        :key="index"
      >
        <div v-if="isShowIconWaring" class="icon icon-24 icon-waring"></div>
        <div v-if="isShowIconInfo" class="icon icon-24 icon-info"></div>
        <div v-if="isShowIconVerify" class="icon icon-24 icon-verify"></div>
        <div class="item-content">{{ item }}</div>
      </div>
      <div class="modal-foodter">
        <button
          v-show="isShowBtnCancel"
          class="btn btn-light"
          @click="onCancel"
        >
          Huỷ
        </button>
        <button class="btn btn-success" @click="onConfirm">Đồng ý</button>
      </div>
    </div>
  </div>
</template>
<script>
import { ENUMICON } from "@/enum.js";
export default {
  name: "PopupNotification",
  props: ["title", "content", "typeicon", "showbtncancel"],
  emits: ["onClose", "onCancel", "onConfirm"],
  components: {},
  created() {
    /**Switch loại icon cần hiển thị */
    switch (this.typeicon) {
      case ENUMICON.Waring:
        this.isShowIconWaring = true;
        break;
      case ENUMICON.Info:
        this.isShowIconInfo = true;
        break;
      case ENUMICON.Verify:
        this.isShowIconVerify = true;
        break;
    }
    /**Kiểm tra hiển thị btn Huỷ */
    if (this.showbtncancel == true) {
      this.isShowBtnCancel = true;
    } else {
      this.isShowBtnCancel = false;
    }
  },
  methods: {
    /**
     * Sự kiện ẩn popup
     */
    onClose() {
      this.$emit("onClose");
    },

    /**
     * Sự kiện ẩn popup
     */
    onCancel() {
      this.$emit("onCancel");
    },

    /**
     * Sự kiện ấn đồng ý
     */
    onConfirm() {
      this.$emit("onConfirm");
    },
  },
  data() {
    return {
      // Hiển thị button huỷ
      isShowBtnCancel: true,

      /**Hiển thị icon waring */
      isShowIconWaring: false,

      /**Hiển thị icon info */
      isShowIconInfo: false,

      /**Hiển thị icon verify */
      isShowIconVerify: false,
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

.icon-info {
  background-image: url(./../../assets/img/info.svg);
  margin-right: 8px;
}

.icon-verify {
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
