<template>
  <div class="container login">
    <div class="wrap-register">
      <div class="w-l-header">
        <div class="login-header-logo"></div>
        <div class="register-header-title">Đăng ký tài khoản QVC TASK</div>
      </div>
      <div class="w-l-content">
        <div class="reg-content">
          <div class="reg-content-info">
            <div class="reg-info-title">
              <span>Thông tin tài khoản</span>
            </div>
            <div class="layout-row"></div>
            <div class="reg-info-body">
              <div class="from-item">
                <div class="f-i-title f-title-reg">
                  Họ và tên <b class="requied">(*)</b>
                </div>
                <input
                  ref="name"
                  placeholder="Nhập họ và tên"
                  class="input place-reg"
                  type="text"
                  v-model="employee.EmployeeName"
                  @blur="validateBlur($event, employee.EmployeeName)"
                />
              </div>
              <div class="from-item item-group">
                <div class="from-item" style="margin-right: 12px">
                  <div class="f-i-title f-title-reg">
                    Số điện thoại <b class="requied">(*)</b>
                  </div>
                  <input
                    placeholder="Nhập số điện thoại"
                    class="input place-reg"
                    type="text"
                    v-model="employee.PhoneNumber"
                    @blur="validateBlur($event, employee.PhoneNumber)"
                  />
                </div>
                <div class="from-item">
                  <div class="f-i-title f-title-reg">
                    Email <b class="requied">(*)</b>
                  </div>
                  <input
                    placeholder="Nhập email"
                    class="input place-reg"
                    type="text"
                    v-model="employee.Email"
                    @blur="validateBlur($event, employee.Email)"
                  />
                </div>
              </div>
              <div class="from-item">
                <div class="f-i-title f-title-reg">Ngày sinh</div>
                <input
                  placeholder="Nhập địa chỉ"
                  class="input place-reg"
                  type="date"
                  v-model="employee.DateOfBirth"
                />
              </div>
              <div class="from-item">
                <div class="f-i-title f-title-reg">Địa chỉ</div>
                <input
                  placeholder="Nhập địa chỉ"
                  class="input place-reg"
                  type="text"
                  v-model="employee.Address"
                />
              </div>
              <div class="from-item">
                <div class="f-i-title f-title-reg">Giới tính</div>
                <div class="f-gender">
                  <div class="gender">
                    <div
                      ref="male"
                      class="icon icon-20 icon-checked-tron"
                      value="1"
                      checked="true"
                      @click="selectGender"
                    ></div>
                    <div class="text-gender">Nam</div>
                  </div>
                  <div class="gender">
                    <div
                      ref="female"
                      class="icon icon-20 icon-uncheck-tron"
                      value="0"
                      checked="false"
                      @click="selectGender"
                    ></div>
                    <div class="text-gender">Nữ</div>
                  </div>
                  <div class="gender">
                    <div
                      ref="other"
                      class="icon icon-20 icon-uncheck-tron"
                      value="2"
                      checked="false"
                      @click="selectGender"
                    ></div>
                    <div class="text-gender">Khác</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="reg-content-info-login">
            <div class="reg-info-title">
              <span>Thông tin đăng nhập</span>
            </div>
            <div class="layout-row"></div>
            <div class="reg-info-body">
              <div class="from-item">
                <div class="f-i-title f-title-reg">
                  Tên đăng nhập <b class="requied">(*)</b>
                </div>
                <input
                  placeholder="Nhập tên đăng nhập"
                  class="input place-reg"
                  type="text"
                  v-model="employee.UserName"
                  @blur="validateBlur($event, employee.UserName)"
                />
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title f-title-reg">
                  Mật khẩu <b class="requied">(*)</b>
                </div>
                <input
                  placeholder="Nhập mật khẩu"
                  class="input input-pass place-reg"
                  type="password"
                  v-model="employee.Password"
                  @blur="validateBlur($event, employee.Password)"
                />
                <div class="icon icon-16 showpass sp-form-reg"></div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title f-title-reg">
                  Nhập lại mật khẩu <b class="requied">(*)</b>
                </div>
                <input
                  placeholder="Nhập lại mật khẩu"
                  class="input input-pass place-reg"
                  type="password"
                  v-model="employee.RePassword"
                  @blur="validateBlur($event, employee.RePassword)"
                />
                <div class="icon icon-16 showpass sp-form-reg"></div>
              </div>
            </div>
          </div>
        </div>
        <div class="item-control c-btn-login" @click="onRegister">
          <span>Đăng ký</span>
        </div>
      </div>
      <div class="w-l-footer">
        <span class="no-account">Bạn đang sử dụng QVC TASK?</span>
        <router-link to="/login" class="no-sigup"
          ><b>Mời đăng nhập</b></router-link
        >
      </div>
    </div>
    <div class="copy-right">Copyright © 2022 QVC TASK</div>
  </div>
  <AccountVerification
    v-if="isShowVerification"
    :employee="employee"
    @closePopup="closePopupVerification"
    @onGoToLogin="onGoToLogin"
  ></AccountVerification>
  <PopupWarning
    :error="error"
    v-if="isShowWaring"
    @closePopup="closePopup"
  ></PopupWarning>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>

<script>
import AccountVerification from "./../components/popup/account-verification.vue";
import PopupWarning from "./../components/popup/popup-warning.vue";
import QvcLoading from "./../components/dialog/qvc-loading.vue";

export default {
  name: "RegisterAccount",
  components: { AccountVerification, PopupWarning, QvcLoading },
  emits: ["onGoToLogin"],
  created() {
    this.autoFocus();
    this.employee.Gender = 1;
  },
  methods: {
    /**
     * Thực hiện focus vào ô nhập liệu đầu tiên khi form được mở lên
     * Author: QVCANH (01/12/2022)
     */
    autoFocus() {
      try {
        this.$nextTick(() => this.$refs.name.focus());
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Chuyển hướng đến trang login
     */
    onGoToLogin() {
      this.isShowVerification = false;
      this.$router.push("/login");
    },

    /**
     * Đóng popup thông báo
     */
    closePopup() {
      this.isShowWaring = false;
    },

    /**
     * Đóng popup xác minh tài khoản
     */
    closePopupVerification() {
      this.isShowVerification = false;
    },

    /**
     * Validate dữ liệu không được để trống
     */
    validateNotNull() {
      if (
        !this.employee.EmployeeName ||
        !this.employee.PhoneNumber ||
        !this.employee.RePassword ||
        !this.employee.UserName ||
        !this.employee.Password ||
        !this.employee.Email
      ) {
        return false;
      }
      return true;
    },
    /**
     * Validate dữ liệu khi blur
     * @param {any} $event phần tử tương tác
     * @param {any} val giá trị cần validate
     */
    validateBlur($event, val) {
      if (!val) {
        $event.target.classList.add("border-red");
      } else {
        $event.target.classList.remove("border-red");
      }
    },
    /**
     * Chọn giới tính
     * @param {any} e phần tử tương tác
     */
    selectGender(e) {
      var val = parseInt(e.target.attributes["value"].value);
      var state = e.target.attributes["checked"].value;

      if (state == "false") {
        e.target.attributes["checked"].value = "true";
        e.target.classList.add("icon-checked-tron");
        e.target.classList.remove("icon-uncheck-tron");
      }

      this.employee.Gender = val;
      switch (val) {
        case 0:
          this.$refs.male.classList.remove("icon-checked-tron");
          this.$refs.male.classList.add("icon-uncheck-tron");
          this.$refs.male.attributes["checked"].value = "false";
          this.$refs.other.classList.remove("icon-checked-tron");
          this.$refs.other.classList.add("icon-uncheck-tron");
          this.$refs.other.attributes["checked"].value = "false";
          break;

        case 1:
          this.$refs.female.classList.remove("icon-checked-tron");
          this.$refs.female.classList.add("icon-uncheck-tron");
          this.$refs.female.attributes["checked"].value = "false";
          this.$refs.other.classList.remove("icon-checked-tron");
          this.$refs.other.classList.add("icon-uncheck-tron");
          this.$refs.other.attributes["checked"].value = "false";
          break;

        case 2:
          this.$refs.female.classList.remove("icon-checked-tron");
          this.$refs.female.classList.add("icon-uncheck-tron");
          this.$refs.female.attributes["checked"].value = "false";
          this.$refs.male.classList.remove("icon-checked-tron");
          this.$refs.male.classList.add("icon-uncheck-tron");
          this.$refs.male.attributes["checked"].value = "false";
          break;

        default:
          break;
      }
    },

    validatePass(){
      if(this.employee.Password != this.employee.RePassword){
        return false;
      }else{
        return true;
      }
    },

    /**
     * Thực hiện đăng ký tài khoản
     */
    onRegister() {
      if (!this.validateNotNull()) {
        this.error = [];
        this.error.push("Vui lòng nhập đầy đủ thông tin.");
        this.isShowWaring = true;
        return;
      }else if(!this.validatePass){
        this.error = [];
        this.error.push("Mật khẩu không khớp. Vui lòng nhập lại!");
        this.isShowWaring = true;
      }
      else {
        // Buld data
        this.employee.EmployeeCode = this.employee.UserName;
        this.employee.AccountType = 2;
        // Hiển thị loading
        this.isShowLoading = true;
        // Gọi API
        this.axios
          .post("http://localhost:56428/api/v2/Registers/SignUp", this.employee)
          .then(() => {
            // Ẩn loading
            this.isShowLoading = false;
            // Hiển thị form xác nhận tài khoản
            this.isShowVerification = true;
          })
          .catch((res) => {
            // Ẩn loading
            this.isShowLoading = false;
            console.log(res);
          });
      }
    },
  },
  data() {
    return {
      // Hiển thị popup xác nhận tài khoản
      isShowVerification: false,

      // Thông tin tài khoản đăng ký
      employee: {},

      // Nội dung lỗi
      error: [],

      /**Hiển thị thông báo */
      isShowWaring: false,

      /**
       * Hiển thị loading
       */
      isShowLoading: false,
    };
  },
};
</script>

<style scoped>
.f-gender {
  width: 100%;
  height: 36px;
  display: flex;
}
.gender {
  display: flex;
  align-items: center;
}
.text-gender {
  margin-left: 8px;
  margin-right: 20px;
}
.icon-checked-tron {
  background-image: url(./../assets/img/checked-tron.svg);
  cursor: pointer;
}
.icon-uncheck-tron {
  background-image: url(./../assets/img/uncheck-tron.svg);
  cursor: pointer;
}
.requied {
  color: red;
}
.border-red {
  border-color: red;
}
</style>
