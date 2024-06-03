<template>
  <!-- The Modal -->
  <div class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <span class="icon icon-24 close" @click="onClose"></span>
      <!-- Modal header -->
      <div class="title">{{ textRole.title }}</div>
      <div class="form-body">
        <div class="from-item">
          <div class="item-content">Tên dự án <b class="requied">&#42;</b></div>
          <input
            ref="nameproject"
            placeholder="Nhập tên dự án..."
            class="input"
            type="text"
            v-model="this.project.ProjectName"
          />
        </div>
        <div class="group-item">
          <div
            class="from-item-group"
            style="position: relative; margin-right: 12px"
          >
            <div class="item-content">{{ textRole.titleDepart }}</div>
            <input
              :placeholder="textRole.tooltipDepart"
              class="input item-cbb"
              type="text"
              :objid="this.project.DepartmentID"
              v-model="this.project.DepartmentName"
            />
            <div class="container-cbb" @click="onShowDepartment">
              <div class="icon icon-16 cbb"></div>
            </div>
          </div>
          <div
            v-if="isShowDropDeprt"
            class="popup-combobox popup-deprt scrollbar"
          >
            <div class="p-s-content">
              <div
                class="item-ccb"
                v-for="(item, index) in departments"
                :key="index"
                :objid="item.DepartmentID"
                @click="onSelectDeprt(item)"
              >
                <span>{{ item.DepartmentName }}</span>
                <!-- <div class="icon icon-cbb"></div> -->
              </div>
            </div>
          </div>
          <div class="from-item-group" style="margin-right: 12px">
            <div class="item-content">Từ ngày</div>
            <input class="input" type="date" v-model="this.project.StartDay" />
          </div>
          <div class="from-item-group">
            <div class="item-content">Đến ngày</div>
            <input class="input" type="date" v-model="this.project.EndDay" />
          </div>
        </div>
        <div class="from-item">
          <div class="item-content">Mô tả</div>
          <textarea
            placeholder="Mô tả..."
            class="input"
            v-model="this.project.Description"
            type="text"
          ></textarea>
        </div>
        <div class="from-item form-select" v-if="false">
          <div class="item-content">Thành viên (1)</div>
          <input
            placeholder="Tìm theo tên hoặc email để thêm nhanh"
            class="input"
            type="text"
          />
          <div class="item-select">
            <div class="icon icon-16 icon-userblue"></div>
            <div class="content-select">Chọn</div>
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
import { ENUMSTATE } from "@/enum.js";
import { ENUMROLE } from "@/enum.js";
import { ENUMMODE } from "@/enum.js";
import { ENUMSCREEN } from "@/enum.js";
export default {
  name: "ProjectDetail",
  props: [
    "iddeprt",
    "namedeprt",
    "idcompany",
    "mode",
    "data",
    "stateedit",
    "screen",
  ],
  emits: ["onClose", "onCancel", "onConfirm"],
  components: {},
  mounted() {
    this.onCheckState();
    this.onCheckRole();
    this.setViewRole();
  },
  created() {
    console.log(this.idcompany, "=", this.namedeprt, "=", this.iddeprt);
    this.onCheckState();
    this.onCheckRole();
    this.setViewRole();
    this.$nextTick(() => this.$refs.nameproject.focus());
    // Hiển thị thông tin phòng ban đang chọn
    if (this.namedeprt && this.iddeprt) {
      this.project.DepartmentName = this.namedeprt;
      this.project.DepartmentID = this.iddeprt;
    }
    if (this.mode == ENUMMODE.Edit) {
      this.textRole.title = "Chỉnh sửa dự án";
      this.project = this.data;
      //Binding ngày
      if (this.onConvertDate(this.project.StartDay) != "") {
        this.project.StartDay = this.onConvertDate(this.project.StartDay);
      } else {
        this.project.StartDay = null;
      }
      if (this.onConvertDate(this.project.EndDay) != "") {
        this.project.EndDay = this.onConvertDate(this.project.EndDay);
      } else {
        this.project.EndDay = null;
      }
      this.getDepartmentById(this.project.DepartmentID);
      console.log(this.data);
    } else {
      this.textRole.title = "Thêm mới dự án";
    }
    //Thiết lập thêm dự án từ trang dự án
    if (this.screen == ENUMSCREEN.Project) {
      this.project.DepartmentID = this.iddeprt;
      this.getDepartmentById(this.iddeprt);
    }
  },
  methods: {
    onCheckRole() {
      this.role = parseInt(localStorage.getItem("role"));
    },
    onCheckState() {
      if (
        !localStorage.getItem("domain-company") ||
        localStorage.getItem("domain-company") == "null"
      ) {
        this.state = ENUMSTATE.CaNhan;
      } else {
        //Dạng công ty
        this.state = ENUMSTATE.CongTy;
      }
    },
    setViewRole() {
      this.onCheckState();
      if (this.state == ENUMSTATE.CaNhan) {
        this.textRole.titleDepart = "Chọn danh mục";
        this.textRole.tooltipDepart = "-- Chọn danh mục --";
        this.keyRole.isShowAddEmp = false;
      } else {
        this.textRole.titleDepart = "Chọn phòng ban";
        this.textRole.tooltipDepart = "-- Chọn phòng ban --";
        if (!this.idcompany) {
          this.keyRole.isShowAddEmp = false;
        } else {
          this.keyRole.isShowAddEmp = true;
        }
      }
    },
    /**
     * Thực hiện convert ngày tháng năm để binding lên form
     * @param {Any} date Dữ liệu ngày tháng
     * Author: QVCANH (05/12/2022)
     */
    onConvertDate(date) {
      try {
        date = new Date(date);

        //Lấy ngày:
        var day = date.getDate();
        day = day < 10 ? `0${day}` : day;
        //Lấy tháng:
        var month = date.getMonth() + 1;
        month = month < 10 ? `0${month}` : month;
        //Lấy năm:
        var year = date.getFullYear();

        //Trả về định dạng tháng/năm/ngày
        var data = `${year}-${month}-${day}`;
        return data;
      } catch (error) {
        console.log(error);
      }
    },
    getDepartmentById(id) {
      console.log(this.stateedit);
      var db =
        this.stateedit == ENUMSTATE.CaNhan
          ? localStorage.getItem("domain-db")
          : localStorage.getItem("domain-company");
      this.axios
        .get(
          `http://localhost:56428/api/v2/Department/getby-id?id=${id}&domaindb=${db}`
        )
        .then((res) => {
          if (res.data) {
            this.project.DepartmentName = res.data.DepartmentName;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    /**
     * Lấy danh sách phòng ban
     */
    getDepartment() {
      // Chuẩn bị dữ liệu
      var data = {
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: this.state,
      };

      this.axios
        .post(
          "http://localhost:56428/api/v2/Department/getall-department",
          data
        )
        .then((res) => {
          if (res) {
            this.departments = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
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
      this.$emit(
        "onConfirm",
        this.project,
        this.employeeId,
        this.idcompany,
        this.mode
      );
    },

    /**
     * Hiển thị popup phòng ban
     */
    onShowDepartment() {
      if (this.isShowDropDeprt) {
        this.isShowDropDeprt = false;
      } else {
        // Hiển thị popup
        this.isShowDropDeprt = true;
        this.onCheckState();
        this.getDepartment();
      }
    },

    onSelectDeprt(item) {
      // Đóng popup
      this.isShowDropDeprt = false;

      // Gán dữ liệu
      this.project.DepartmentID = item.DepartmentID;
      this.project.DepartmentName = item.DepartmentName;
    },
  },
  data() {
    return {
      textRole: {},
      keyRole: {},
      role: ENUMROLE.Leve0,
      state: ENUMSTATE.CaNhan,

      /**Đối tượng thông tin dự án */
      project: {},

      /**Danh sách thành viên được chọn */
      employeeId: [],

      /**Hiển thị drop phòng ban */
      isShowDropDeprt: false,

      /**Danh sách phòng ban */
      departments: [],
      ENUMSTATE,
      ENUMROLE,
      ENUMMODE,
      ENUMSCREEN,
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

textarea {
  height: 78px;
}

.group-item {
  display: flex;
  margin-top: 12px;
  margin-bottom: 12px;
}

.item-cbb {
  width: 255px;
  padding-right: 30px;
}

.container-cbb {
  width: 30px;
  height: 30px;
  position: absolute;
  display: flex;
  justify-content: center;
  align-items: center;
  bottom: 3px;
  right: 3px;
  cursor: pointer;
}

.cbb {
  background-image: url(./../../assets/img/cbb.svg);
}

.group-item {
  position: relative;
}

.popup-deprt {
  width: 278px;
  position: absolute;
  top: 60px;
  max-height: 300px;
}
</style>
