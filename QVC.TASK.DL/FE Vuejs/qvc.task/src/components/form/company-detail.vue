<template>
  <!-- The Modal -->
  <div class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <span class="icon icon-24 close" @click="onClose"></span>
      <!-- Modal header -->
      <div class="title">Thêm tổ chức (công ty/doanh nghiệp)</div>
      <div class="form-body">
        <div class="from-item">
          <div class="item-content">
            Tên công ty / doanh nghiệp <b class="requied">&#42;</b>
          </div>
          <input
            ref="namedeprt"
            placeholder="Nhập tên công ty / doanh nghiệp..."
            class="input"
            type="text"
            v-model="company.CompanyName"
          />
        </div>
        <div class="from-item form-select">
          <div class="item-content">Thành viên (1)</div>
          <div class="c-title">
            <div class="role">
              <div
                ref="leve0"
                select="true"
                class="icon icon-20 icon-checked"
                @click="onGetRole($event, 0)"
              ></div>
              <div class="text-role">Toàn quyền</div>
            </div>
            <div class="role">
              <div
                ref="leve1"
                select="false"
                class="icon icon-20 icon-checkbox"
                @click="onGetRole($event, 1)"
              ></div>
              <div class="text-role">Giới hạn đến phòng ban</div>
            </div>
            <div class="role">
              <div
                ref="leve2"
                select="false"
                class="icon icon-20 icon-checkbox"
                @click="onGetRole($event, 2)"
              ></div>
              <div class="text-role">Giới hạn đến công việc</div>
            </div>
          </div>
          <input
            placeholder="Tìm theo tên hoặc email để thêm nhanh"
            class="input"
            type="text"
            @click="onShowPopSeleteEmp"
          />
          <div class="item-select" @click="onShowPopSeleteEmp">
            <div class="icon icon-16 icon-userblue"></div>
            <div class="content-select">Chọn</div>
          </div>
        </div>
        <div class="info-select">
          <div
            class="info-emp"
            v-for="(item, index) in empSelected"
            :key="index"
          >
            {{ item.EmployeeName }}
          </div>
        </div>
        <div class="popup-select" v-if="isShowPopEmp">
          <div class="pop-body">
            <div
              class="item-add"
              v-for="(item, index) in emp"
              :key="index"
              :id="item.EmployeeID"
            >
              <div
                class="icon icon-20 icon-checkbox"
                select="false"
                ref="selectemp"
                @click="onSelectEmp($event, item)"
                :id="item.EmployeeID"
                :name="item.EmployeeName"
              ></div>
              <div class="icon icon-32 avatar"></div>
              <div class="s-text">
                <div class="t-name">{{ item.EmployeeName }}</div>
                <div class="t-email">{{ item.Email }}</div>
              </div>
            </div>
          </div>
          <div class="pop-foodter">
            <button class="btn btn-light" @click="onClickCancel">Huỷ bỏ</button>
            <button class="btn btn-success" @click="onClickSelect">Chọn</button>
          </div>
        </div>
      </div>
      <div class="modal-foodter">
        <button class="btn btn-light" @click="onCancel">Huỷ bỏ</button>
        <button class="btn btn-success" @click="onConfirm">Lưu</button>
      </div>
    </div>
  </div>
</template>
<script>
import { ENUMTYPEEMP } from "@/enum.js";
export default {
  name: "CompanyDetail",
  props: [],
  emits: ["onClose", "onCancel", "onConfirm"],
  components: {},
  created() {
    //   this.$nextTick(() => this.$refs.namedeprt.focus());
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
      this.$emit("onConfirm", this.company, this.empSelected, this.role);
    },

    onGetRole(e, role) {
      if (e.target.attributes["select"].value == "true") {
        //
      } else {
        this.$refs.leve0.classList.remove("icon-checked");
        this.$refs.leve0.classList.add("icon-checkbox");
        this.$refs.leve0.attributes["select"].value = "false";

        this.$refs.leve1.classList.remove("icon-checked");
        this.$refs.leve1.classList.add("icon-checkbox");
        this.$refs.leve1.attributes["select"].value = "false";

        this.$refs.leve2.classList.remove("icon-checked");
        this.$refs.leve2.classList.add("icon-checkbox");
        this.$refs.leve2.attributes["select"].value = "false";
        //chọn
        e.target.attributes["select"].value = "true";
        e.target.classList.remove("icon-checkbox");
        e.target.classList.add("icon-checked");
      }
      this.role = role;
    },

    generateCodeDeprt(key) {
      // Generate a random integer between 0 and 99999
      const randomNumber = Math.floor(Math.random() * 100000);

      // Pad the number with leading zeros to ensure it has 5 digits
      const randomCode = randomNumber.toString().padStart(5, "0");

      // Concatenate the code with the prefix 'DP'
      const finalCode = key + randomCode;

      return finalCode;
    },

    onSelectEmp(e, val) {
      //Lấy ra trạng thái select
      var state = e.target.attributes["select"].value;
      if (state == "true") {
        //bỏ chọn
        e.target.attributes["select"].value = "false";
        e.target.classList.remove("icon-checked");
        e.target.classList.add("icon-checkbox");
        for (let i = 0; i < this.empSelected.length; i++) {
          if (
            this.empSelected[i].EmployeeID == e.target.attributes["id"].value
          ) {
            this.empSelected.splice(i, 1);
          }
        }
      } else {
        //chọn
        e.target.attributes["select"].value = "true";
        e.target.classList.remove("icon-checkbox");
        e.target.classList.add("icon-checked");
        this.empSelected.push(val);
      }
    },

    onClickSelect() {
      console.log(this.empSelected);
      this.isShowPopEmp = false;
    },
    /**
     * Lấy danh sách người dùng
     */
    onGetAllEmpInfo() {
      this.axios
        .get(`http://localhost:56428/api/v2/Employee/qvc_task_info`)
        .then((res) => {
          if (res) {
            this.emp = res.data;
            this.emp = this.emp.filter(
              (e) => e.EmployeeID != localStorage.getItem("userid")
            );
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },

    onClickCancel() {
      this.isShowPopEmp = false;
    },
    onShowPopSeleteEmp() {
      if (this.isShowPopEmp) {
        this.isShowPopEmp = false;
      } else {
        this.empSelected = [];
        this.onGetAllEmpInfo();
        this.isShowPopEmp = true;
      }
    },
  },
  data() {
    return {
      isShowPopEmp: false,
      emp: [],
      empSelected: [],
      company: {},
      role: 1,
      ENUMTYPEEMP,
    };
  },
};
</script>

<style scoped>
.text-role {
  font-size: 12px;
  font-weight: 600;
}
.role {
  display: flex;
  align-items: center;
  margin-bottom: 3px;
}
.role + .role {
  margin-left: 24px;
}

.info-select {
  display: flex;
  margin-top: 12px;
  flex-wrap: wrap;
}
.c-title {
  display: flex;
  align-items: center;
}
.info-emp {
  background-color: #50b83c;
  border-radius: 3px;
  padding: 3px 8px 3px 8px;
  cursor: pointer;
  user-select: none;
  margin-bottom: 5px;
}
.info-emp + .info-emp {
  margin-left: 12px;
}
.pop-foodter {
  display: flex;
  justify-content: flex-end;
}
.icon-checkbox {
  background-image: url(./../../assets/img/check.svg);
  margin-right: 12px;
  cursor: pointer;
}

.icon-checked {
  background-image: url(./../../assets/img/checked.svg);
  margin-right: 12px;
  cursor: pointer;
}
.avatar {
  background-image: url(./../../assets/img/avatar.svg);
  margin-right: 12px;
}
.item-add {
  display: flex;
  width: calc(100% - 16px);
  background-color: #fefefe;
  border-radius: 8px;
  padding: 8px;
  align-items: center;
}
.item-add:hover {
  background-color: #dcf1d8;
  cursor: pointer;
}
.pop-body {
  overflow-y: scroll;
}
.popup-select {
  width: calc(100% - 24px);
  min-height: 200px;
  max-height: 200px;
  background-color: #fff;
  position: absolute;
  border-radius: 8px;
  padding: 12px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
}
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
  min-width: 500px;
  width: auto;
  border-radius: 8px;
}

.title {
  font-size: 24px;
  font-weight: 600;
}

.form-body {
  padding-top: 20px;
  padding-bottom: 20px;
  position: relative;
}

.modal-foodter {
  display: flex;
  justify-content: flex-end;
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

.requied {
  color: red;
  font-size: 18px;
}

.item-content {
  font-size: 13.5px;
  font-weight: 600;
  color: #4e4e4e;
  margin-bottom: 8px;
}

.item-select {
  position: absolute;
  width: auto;
  height: 36px;
  bottom: 0px;
  right: 0px;
  display: flex;
  align-items: center;
  cursor: pointer;
}

.form-select {
  position: relative;
  user-select: none;
}

.icon-userblue {
  background-image: url(./../../assets/img/user-blue.svg);
  margin-right: 4px;
}

.content-select {
  color: #2196f3;
  font-size: 15px;
  padding-right: 8px;
}
</style>
