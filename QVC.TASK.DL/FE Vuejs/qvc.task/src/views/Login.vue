<template>
  <div class="container login">
    <div class="wrap-login">
      <div class="w-l-header">
        <div class="login-header-logo"></div>
        <div class="login-header-title">QVC TASK</div>
      </div>
      <div class="w-l-content">
        <div class="item-control">
          <input
            ref="username"
            placeholder="Tên đăng nhập/email"
            class="input c-username"
            type="text"
            v-model="login.Username"
          />
        </div>
        <div class="item-control">
          <input
            placeholder="Mật khẩu"
            class="input c-password"
            type="password"
            v-model="login.Password"
            @keyup.enter="onLogin()"
          />
          <div class="icon icon-16 showpass"></div>
        </div>
        <div class="item-control" v-if="isShowCompany">
          <input
            ref="company"
            placeholder="Mã công ty/doanh nghiệp"
            class="input c-username"
            type="text"
            v-model="login.CompanyCode"
          />
        </div>
        <div class="company">
          <div
            class="icon icon-20 icon-check"
            checked="false"
            style="cursor: pointer"
            @click="toggleCompany"
          ></div>
          <div class="text-company">Công ty/Doanh nghiệp</div>
        </div>
        <div class="c-restore-pass"><span>Quên mật khẩu?</span></div>
        <button class="item-control c-btn-login" @click="onLogin">
          <span>Đăng nhập</span>
        </button>
      </div>
      <div class="w-l-footer">
        <span class="no-account">Chưa có tài khoản?</span>
        <router-link to="/register" class="no-sigup"
          ><b>Đăng ký</b></router-link
        >
      </div>
    </div>
    <div class="copy-right">Copyright © 2022 QVC TASK</div>
  </div>
  <PopupNotification
    v-if="isShowWaring"
    title="Thông báo"
    :content="content"
    :typeicon="ENUMICON.Waring"
    :showbtncancel="false"
    @onClose="isShowWaring = false"
    @onConfirm="isShowWaring = false"
  ></PopupNotification>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>

<script>
import PopupNotification from "./../components/popup/popup-notification.vue";
import QvcLoading from "./../components/dialog/qvc-loading.vue";
import { ENUMICON } from "@/enum.js";
import { ENUMSTATE } from "@/enum.js";
export default {
  name: "LoginAccount",
  components: { PopupNotification, QvcLoading },
  created() {
    this.autoFocus();
  },
  methods: {
    onLogin() {
      if (!this.validateLogin()) {
        // Hiển thị thông báo
        this.isShowWaring = true;
      } else {
        // Hiển thị loading
        this.isShowLoading = true;
        // Kiểm tra xem nhập mail hay username
        if (this.validateEmail(this.login.Username)) {
          this.login.Email = this.login.Username;
        }
        // Gọi API đăng nhập
        this.axios
          .post("http://localhost:56428/api/v2/Login", this.login)
          .then((res) => {
            // Đăng nhập thành công - lưu thông tin vào local storage
            setTimeout(() => {
              // Ẩn loading
              this.isShowLoading = false;
              if (res) {
                this.setInfoToLocalStorage(res);
                // Đi tới trang Home
                this.$router.push("/");
              }
            }, 1000);
          })
          .catch(() => {
            setTimeout(() => {
              // Ẩn loading
              this.isShowLoading = false;
              // Hiển thị thông báo đăng nhập k thành công
              this.content = [];
              this.content.push("Tài khoản hoặc mật khẩu không chính xác!");
              this.isShowWaring = true;
            }, 1000);
          });
      }
    },

    /**
     * Thực hiện validate trước khi đăng nhập
     */
    validateLogin() {
      this.content = [];
      if (!this.login.Username) {
        this.content.push("Tên đăng nhập không được để trống.");
        this.autoFocus();
        return false;
      }
      if (!this.login.Password) {
        this.content.push("Mật khẩu không được để trống.");
        this.$nextTick(() => this.$refs.Password.focus());
        return false;
      }
      if (this.isShowCompany) {
        if (!this.login.CompanyCode) {
          this.content.push("Vui lòng nhập mã công ty của bạn.");
          this.$nextTick(() => this.$refs.company.focus());
          return false;
        }
      }
      return true;
    },

    /**
     * Kiểm tra xem có phải là email hay không
     * @param {*} email Email
     */
    validateEmail(email) {
      const re = /\S+@\S+\.\S+/;
      return re.test(email);
    },

    /**
     * Lưu thông tin vào local
     * @param {*} res dữ liệu res trả về
     */
    setInfoToLocalStorage(res) {
      localStorage.setItem("access-token", res.data.AccessToken);
      localStorage.setItem("user-name", res.data.Info.UserName);
      localStorage.setItem("full-name", res.data.Info.EmployeeName);
      localStorage.setItem("email", res.data.Info.Email);
      localStorage.setItem("domain-db", res.data.Info.UserName + "_qvc_task");
      localStorage.setItem("company-code", this.login.CompanyCode);
      localStorage.setItem("domain-company", res.data.DomainCompany);
      localStorage.setItem("company-id", res.data.CompanyID);
      localStorage.setItem("role", res.data.Info.Role);
      localStorage.setItem("userid", res.data.Info.EmployeeID);
      localStorage.setItem(
        "state",
        !this.login.CompanyCode ? ENUMSTATE.CaNhan : ENUMSTATE.CongTy
      );
    },

    /**
     * Thực hiện thay đổi kiểu đăng nhập (Cá nhân/Tổ chức)
     * @param {*} e Phần tử đang tương tác
     */
    toggleCompany(e) {
      console.log(e);
      if (e.target.attributes["checked"].value == "true") {
        // Bỏ check
        e.target.classList.remove("icon-checked");
        e.target.classList.add("icon-check");
        e.target.attributes["checked"].value = "false";
        this.isShowCompany = false;
        this.autoFocus();
      } else {
        // Check
        e.target.classList.remove("icon-check");
        e.target.classList.add("icon-checked");
        e.target.attributes["checked"].value = "true";
        this.isShowCompany = true;
        this.$nextTick(() => this.$refs.company.focus());
      }
    },

    /**
     * Thực hiện focus vào ô nhập liệu đầu tiên khi form được mở lên
     * Author: QVCANH (01/12/2022)
     */
    autoFocus() {
      try {
        this.$nextTick(() => this.$refs.username.focus());
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      /**Hiển thị input nhập mã công ty */
      isShowCompany: false,

      /**Đối tượng login */
      login: {},

      /**Hiển thị thông báo */
      isShowWaring: false,

      /**Danh sách thông báo */
      content: [],

      /**Hiển thị loading */
      isShowLoading: false,

      /**Dữ liệu enum */
      ENUMICON,
      ENUMSTATE,
    };
  },
};
</script>

<style scoped>
.company {
  display: flex;
  align-items: center;
  margin-top: 12px;
}
.text-company {
  color: #666666;
  font-size: 13px;
  font-weight: 600;
  padding-top: 2px;
}
@import url(./../css/main.css);
</style>
