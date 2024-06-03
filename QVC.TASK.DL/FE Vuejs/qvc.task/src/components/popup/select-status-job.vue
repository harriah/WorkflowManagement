<template>
  <div class="popup-combobox popup-select-status">
    <div class="arror arrow-top"></div>
    <div class="p-s-content">
      <h3>Chọn nhóm công việc</h3>
      <div class="item-ccb" @click="onSelectJobStatus(ENUMJOBSTATUS.Todo)">
        <span>Cần thực hiện</span>
        <div class="icon icon-cbb"></div>
      </div>
      <div
        class="item-ccb"
        @click="onSelectJobStatus(ENUMJOBSTATUS.Processing)"
      >
        <span>Đang thực hiện</span>
      </div>
      <div class="item-ccb" @click="onSelectJobStatus(ENUMJOBSTATUS.Complete)">
        <span>Đã hoàn thành</span>
      </div>
    </div>
  </div>
</template>

<script>
import { ENUMJOBSTATUS } from "@/enum.js";
export default {
  name: "SelectStatusJob",
  emits: ["onSelectJobStatus"],
  components: {},
  created() {},
  methods: {
    /**
     * Thực hiện sự kiện khi chọn trạng thái công việc
     * @param {*} type Loại status
     */
    onSelectJobStatus(type) {
      this.$emit("onSelectJobStatus", type);
    },
  },
  data() {
    return {
      /**Dữ liệu enum */
      ENUMJOBSTATUS,
    };
  },
};
</script>

<style scoped>
.popup-select-status {
  width: 200px;
  top: 55px;
  left: 200px;
}
</style>
