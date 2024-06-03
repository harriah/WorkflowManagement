<template>
  <div class="popup-combobox popup-select-popover" style="padding: 24px">
    <div class="arror arrow-top"></div>
    <div class="p-s-content">
      <div>
        <h3>Chọn người thực hiện</h3>
        <div class="icon icon-16 search-all" style="top: 85px"></div>
        <input
          class="input input-icon-left i-search-all"
          type="text"
          placeholder="Tìm kiếm thành viên"
          style="width: calc(100% - 46px); margin-bottom: 16px"
        />
      </div>
      <div class="scrollbar" style="max-height: 150px">
        <div
          v-for="(item, index) in employees"
          :key="index"
          class="item"
          :objid="item.EmployeeID"
          @click="onSelectEmployee(item)"
        >
          <div class="item-avt">
            <div class="icon icon-16 icon-user-v2"></div>
          </div>
          <div class="item-content">
            <div class="c-name">{{ item.EmployeeName }}</div>
            <div class="c-email">{{ item.Email }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ENUMSTATE } from "@/enum.js";
export default {
  name: "SelectPopoverAssign",
  components: {},
  emits: ["onSelectEmployee"],
  created() {
    this.onGetAllEmployee();
  },
  methods: {
    /**
     * Thực hiện sự kiện khi chọn người thực hiện công việc
     * @param {*} data thông tin người dùng
     */
    onSelectEmployee(data) {
      this.$emit("onSelectEmployee", data);
    },

    /**
     * Lấy danh sách người dùng
     */
    onGetAllEmployee() {
      var state = parseInt(localStorage.getItem("state"));
      var db =
        state == ENUMSTATE.CaNhan
          ? localStorage.getItem("domain-db")
          : localStorage.getItem("domain-company");
      this.axios
        .get(`http://localhost:56428/api/v2/Employee/${db}`)
        .then((res) => {
          if (res) {
            this.employees = res.data;
            console.log(res);
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
  },
  data() {
    return {
      /**Thông tin người dung */
      employees: [],
      ENUMSTATE,
    };
  },
};
</script>

<style scoped>
.item-avt {
  width: 32px;
  height: 32px;
  background-color: rgb(255, 145, 248);
  border-radius: 50px;
  margin-right: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.icon-user-v2 {
  background-image: url(./../../assets/img/user-v2.svg);
  filter: brightness(0) invert(1);
}
.c-name {
  font-weight: 600;
}
.c-email {
  font-size: 13px;
}
.item {
  width: calc(100% - 16px);
  height: 50px;
  display: flex;
  align-items: center;
  font-size: 15px;
  color: #1f1f1f;
  font-weight: 400;
  border-radius: 4px;
  padding: 0px 8px 0px 8px;
}
.item:hover {
  background-color: #dcf1d8;
  cursor: pointer;
}

.item > span {
  white-space: nowrap;
  padding-right: 12px;
}
.popup-select-popover {
  width: auto;
  top: 65px;
}
.arror {
  position: absolute;
  top: -10px;
  right: 170px;
}
</style>
