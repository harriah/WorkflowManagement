<template>
  <!-- The Modal -->
  <div id="AccountVerification" class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <span class="icon icon-24 close" @click="closePopup"></span>
      <!-- Modal header -->
      <h2>Xác nhận tài khoản</h2>
      <p class="content-note">
        Một mã xác nhận tài khoản đã được gửi đến email của bạn.<br />Vui lòng
        kiểm tra email để lấy mã xác nhận.
      </p>
      <div class="from-item">
        <div class="f-i-title f-title-reg">Mã xác nhận</div>
        <input
          ref="code"
          placeholder="Nhập mã xác nhận tài khoản"
          class="input place-reg"
          type="text"
          v-model="code"
        />
      </div>
      <div class="modal-foodter">
        <button class="btn btn-light" @click="closePopup">Huỷ</button>
        <button class="btn btn-success" @click="onVerifyAccount">
          Xác nhận
        </button>
      </div>
    </div>
  </div>
  <PopupWarning
    v-if="isShowWaring"
    :error="error"
    :icon="this.icon"
    :keywarning="keyWarning"
    @closePopup="closeWaring"
  ></PopupWarning>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>

<script>
import PopupWarning from "./popup-warning.vue";
import { ENUMWARNING } from "./../../enum.js";
import { ENUMBTN } from "./../../enum.js";
import QvcLoading from "./../dialog/qvc-loading.vue";
export default {
  name: "AccountVerification",
  props: ["employee"],
  emits: ["closePopup", "onGoToLogin"],
  components: { PopupWarning, QvcLoading },
  created() {
    this.autoFocus();
  },
  methods: {
    /**
     * Thực hiện focus vào ô nhập liệu đầu tiên khi form được mở lên
     * Author: QVCANH (01/12/2022)
     */
    autoFocus() {
      try {
        this.$nextTick(() => this.$refs.code.focus());
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Thực hiện xử lý khi ấn các nút trên popup thông báo
     * @param {*} keywaring loại thông báo
     * @param {*} keyntn loại btn
     */
    closeWaring(keywaring, keyntn) {
      if (keywaring == ENUMWARNING.Verify && keyntn == ENUMBTN.Agree) {
        this.$emit("onGoToLogin");
      }
      this.isShowWaring = false;
    },
    closePopup() {
      this.$emit("closePopup");
    },

    /**
     * Thực hiện xác minh tài khoản
     */
    onVerifyAccount() {
      if (this.employee && this.code) {
        this.isShowLoading = true;
        // Gọi API xác thực tài khoản
        this.axios
          .post(
            `http://localhost:56428/api/v2/Registers/GetOne?id=${this.employee.EmployeeID}&username=${this.employee.UserName}&email=${this.employee.Email}`
          )
          .then((res) => {
            setTimeout(() => {
              this.isShowLoading = false;
              if (res.data) {
                if (res.data.Code == this.code) {
                  // Gọi API cập nhật thông tin
                  this.updateAccountType();
                  // Gọi API thực hiện tạo db domain
                  this.createDBDomain(res.data);
                  this.error = [];
                  this.error.push(
                    "Tài khoản đã được xác minh. Đăng nhập để tiếp tục."
                  );
                  this.icon = true;
                  this.keyWarning = ENUMWARNING.Verify;
                } else {
                  this.error = [];
                  this.error.push(
                    "Mã xác thực không chính xác. Vui lòng thử lại."
                  );
                  this.icon = false;
                  this.keyWarning = ENUMWARNING.Waring;
                }
                this.isShowWaring = true;
              }
            }, 1000);
          })
          .catch((res) => {
            setTimeout(() => {
              this.isShowLoading = false;
              this.error = [];
              this.error.push("Lỗi xác thực. Vui lòng thử lại sau.");
              this.icon = false;
              this.keyWarning = ENUMWARNING.Waring;
              this.isShowWaring = true;
              console.log(res);
            }, 3000);
          });
      } else {
        this.isShowLoading = false;
        this.error = [];
        if (!this.employee) {
          this.error.push("Lỗi xác thực. Vui lòng thử lại sau.");
        } else {
          this.error.push("Vui lòng nhập mã xác thực để tiếp tục.");
        }
        this.icon = false;
        this.keyWarning = ENUMWARNING.Waring;
        this.isShowWaring = true;
      }
    },

    /**
     * Thực hiện tạo db domain
     */
    createDBDomain(data) {
      this.axios
        .post("http://localhost:56428/api/v2/Registers/CreateDBDomain", data)
        .then(() => {})
        .catch(() => {});
    },

    /**
     * Update trạng thái tài khoản
     */
    updateAccountType() {
      this.employeeTemp = this.employee;
      this.employeeTemp.AccountType = 1; // 1 là trạng thái tài khoản đã đang sử dụng
      this.axios
        .post(
          "http://localhost:56428/api/v2/Registers/UpdateByUserName",
          this.employeeTemp
        )
        .then(() => {})
        .catch(() => {});
    },
  },
  data() {
    return {
      /**Mã xác thực tài khoản */
      code: "",
      /**Hiển thị thông báo */
      isShowWaring: false,
      /**Danh sách lỗi */
      error: [],
      /**Loại icon hiển thị trên thông báo */
      icon: false,
      /**Đối tượng nhân viên gán tạm */
      employeeTemp: {},
      /**Loại waring */
      keyWarning: ENUMWARNING.Verify,
      /**Hiển thị loading */
      isShowLoading: false,
      /**Dữ liệu enum */
      ENUMWARNING,
      ENUMBTN,
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
  justify-content: space-between;
}
</style>
