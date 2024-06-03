<template>
  <div
    class="popup-add-job pop-add"
    :style="{ top: top + 'px', right: right + 'px' }"
  >
    <div class="arror arrow-top" :style="{ right: arrow + 'px' }"></div>
    <div class="p-a-item" v-if="false">
      <div class="item-icon icon icon-24 add-job"></div>
      <div class="item-text">Thêm công việc từ mẫu</div>
    </div>
    <div class="p-a-item">
      <div class="item-icon icon icon-24 job-column"></div>
      <div class="item-text">Thêm cột mốc công việc</div>
    </div>
    <div class="p-a-item" v-if="keyRole.isShowAddProject" @click="addProject">
      <div class="item-icon icon icon-24 add-project-group"></div>
      <div class="item-text">Thêm dự án/nhóm</div>
    </div>
    <div class="p-a-item" v-if="keyRole.isShowAddDepart" @click="addDepartment">
      <div class="item-icon icon icon-24 add-deprt"></div>
      <div class="item-text">{{ textRole.depart }}</div>
    </div>
    <div class="p-a-item" v-if="keyRole.isShowAddCompay" @click="addCompany">
      <div class="item-icon icon icon-24 add-uo"></div>
      <div class="item-text">Tạo tổ chức (công ty/doanh nghiệp)</div>
    </div>
    <div class="p-a-item" v-if="keyRole.isShowAddEmp">
      <div class="item-icon icon icon-24 add-user"></div>
      <div class="item-text">
        {{ textRole.addemp }}
      </div>
    </div>
  </div>
</template>

<script>
import { ENUMSTATE } from "@/enum.js";
export default {
  name: "AddJob",
  components: {},
  emits: ["addDepartment", "addDepartment", "addCompany"],
  props: ["top", "right", "arrow"],
  watch: {},
  mounted() {},
  created() {
    // khởi tạo
    if (parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan) {
      this.textRole.addemp = "Thêm thành viên vào danh mục/dự án";
      this.textRole.depart = "Thêm danh mục công việc";
    } else {
      this.textRole.addemp = "Thêm nhanh thành viên vào phòng ban/dự án";
      this.textRole.depart = "Thêm phòng ban";
    }
    switch (parseInt(localStorage.getItem("role"))) {
      case 0:
        this.keyRole.isShowAddCompay =
          parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan
            ? true
            : false;
        this.keyRole.isShowAddEmp =
          parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan
            ? false
            : true;
        break;
      case 1:
        this.keyRole.isShowAddCompay = false;
        this.keyRole.isShowAddEmp =
          parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan
            ? false
            : true;
        break;
      case 2:
        this.keyRole.isShowAddProject = false;
        this.keyRole.isShowAddDepart = false;
        this.keyRole.isShowAddEmp = false;
        this.keyRole.isShowAddCompay = false;
        break;
    }
  },
  methods: {
    addDepartment() {
      this.$emit("addDepartment");
    },
    addProject() {
      this.$emit("addProject");
    },
    addCompany() {
      this.$emit("addCompany");
    },
  },
  data() {
    return {
      keyRole: {
        isShowAddProject: true,
        isShowAddDepart: true,
        isShowAddCompay: true,
        isShowAddEmp: true,
      },
      textRole: {
        depart: "Thêm danh mục công việc",
        addemp: "Thêm thành viên vào danh mục/dự án",
      },
      ENUMSTATE,
    };
  },
};
</script>

<style scoped>
.pop-add {
  box-shadow: 0 12px 20px rgb(0, 0, 0, 0.5);
}
</style>
