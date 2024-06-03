<template>
  <div
    class="popup-combobox popup-select-project scrollbar"
    style="padding: 24px"
  >
    <div class="arror arrow-top"></div>
    <div class="p-s-content">
      <h3>Chọn dự án/nhóm</h3>
      <div class="icon icon-16 search-all" style="top: 85px"></div>
      <input
        class="input input-icon-left i-search-all"
        type="text"
        placeholder="Tìm kiếm"
        style="width: calc(100% - 46px); margin-bottom: 16px"
      />
      <ListProject
        v-for="(item, index) in departments"
        :key="index"
        :name="item.DepartmentName"
        :id="item.DepartmentID"
        :idcompany="item.CompanyID"
        @onSelectProject="onSelectProject"
      ></ListProject>
    </div>
  </div>
</template>

<script>
import ListProject from "./../menu/list-project.vue";
export default {
  name: "SelectProject",
  components: { ListProject },
  emits: ["onSelectProject"],
  created() {
    this.getDepartment();
  },
  methods: {
    /**
     * Thực hiện sự kiện khi chọn dự án
     * @param {*} data dữ liệu dự án
     */
    onSelectProject(data, idcompany) {
      this.$emit("onSelectProject", data, idcompany);
    },
    /**
     * Lấy danh sách phòng ban
     */
    onGetAllDepartment() {
      var domain = localStorage.getItem("domain-db");
      this.axios
        .get(`http://localhost:56428/api/v2/Department/${domain}`)
        .then((res) => {
          if (res) {
            this.departments = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },

    getDepartment() {
      // Chuẩn bị dữ liệu
      var data = {
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
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
  },
  data() {
    return {
      /**Dữ liệu phòng ban */
      departments: [],

      /**Dữ liệu dự án */
      projects: [],

      /**Hiển thị list dự án */
      isShowProject: false,
    };
  },
};
</script>

<style scoped>
.icon-pro {
  width: 24px;
  height: 24px;
  border-radius: 50px;
  background-color: rgb(255, 145, 248);
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 12px;
}
.item-pro {
  width: calc(100% - 16px);
  height: 36px;
  display: flex;
  align-items: center;
  font-size: 15px;
  color: #1f1f1f;
  font-weight: 400;
  border-radius: 4px;
  padding: 0px 8px 0px 8px;
}
.item-pro:hover {
  background-color: #dcf1d8;
  cursor: pointer;
}
.collapse {
  background-image: url(../../assets/img/collapse.svg);
  padding-right: 12px;
}
.iconl-i__icon {
  width: 16px;
  height: 16px;
}
.item-project {
  padding-left: 50px;
  width: calc(100% - 56px);
}

.item-project > span {
  padding-right: 12px;
}

.item-pro > span {
  white-space: nowrap;
  padding-right: 12px;
}
.popup-select-project {
  min-width: 400px;
  width: auto;
  top: 55px;
  max-height: 400px;
}
.arror {
  position: absolute;
  top: -10px;
  left: 150px;
}
</style>
