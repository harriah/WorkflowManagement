<template>
  <div class="popup-combobox cbb-other scrollbar">
    <div class="arror arrow-top"></div>
    <div class="p-s-content">
      <div
        class="item-ccb"
        v-for="(item, index) in departments"
        :key="index"
        @click="onSelect(item.DepartmentID, item.CompanyID)"
      >
        <span>{{ item.DepartmentName }}</span>
        <!-- <div class="icon icon-cbb"></div> -->
      </div>
    </div>
  </div>
</template>

<script>
import { RESAPI } from "@/res.js";
import { ENUMSTATE } from "@/enum.js";
export default {
  name: "SelectDepartment",
  components: {},
  emits: ["onSelectData"],
  created() {
    this.checkState();
    this.getDepartment();
  },
  methods: {
    checkState() {
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
    /**
     * Xử lý khi chọn phòng ban
     * @param {*} data dữ liệu phòng ban
     */
    onSelect(id, companyid) {
      this.$emit("onSelectData", id, companyid);
      this.$router.replace({
        path: "/department",
        query: { id: id, companyid: companyid },
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
  },
  data() {
    return {
      /**Dữ liệu phòng ban */
      departments: [],
      state: ENUMSTATE.CaNhan,

      /**Dữ liệu Res */
      RESAPI,
      ENUMSTATE,
    };
  },
};
</script>

<style scoped>
.cbb-other {
  top: calc(100% + 5px);
  right: 90px;
  width: 200px !important;
  max-height: 300px;
}
</style>
