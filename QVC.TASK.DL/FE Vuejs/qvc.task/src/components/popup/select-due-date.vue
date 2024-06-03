<template>
  <div class="popup-combobox popup-select-due-date" style="padding: 24px">
    <div class="arror arrow-top"></div>
    <div class="p-s-content">
      <div class="content">
        <div class="content-start">
          <div class="c-container">
            <div class="c-text">Ngày bắt đầu</div>
            <input class="input" type="date" v-model="timeJob.StartDate" />
          </div>
          <div class="c-container">
            <div class="c-text">Giờ bắt đầu</div>
            <input
              placeholder="HH:mm"
              class="input time"
              type="time"
              v-model="timeJob.StartTime"
            />
          </div>
        </div>
        <div class="content-end">
          <div class="c-container">
            <div class="c-text">Hạn hoàn thành</div>
            <input class="input" type="date" v-model="timeJob.EndDate" />
          </div>
          <div class="c-container">
            <div class="c-text">Giờ kết thúc</div>
            <input
              placeholder="HH:mm"
              class="input time"
              type="time"
              v-model="timeJob.EndTime"
            />
          </div>
        </div>
      </div>
      <div class="add-date-start" style="display: none">
        <div class="icon icon-24 icon-plus-blue"></div>
        <div class="option-plus-text">Thêm ngày bắt đầu</div>
      </div>
    </div>
    <div class="modal-foodter">
      <button class="btn btn-light" @click="onCancel">Huỷ bỏ</button>
      <button class="btn btn-success" @click="onConfirm">Lưu</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "SelectDueDate",
  components: {},
  emits: ["onConfirm", "onCancel"],
  created() {},
  methods: {
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
      this.$emit("onConfirm", this.timeJob);
    },
  },
  data() {
    return {
      /**Thông tin thời gian công việc */
      timeJob: {},
    };
  },
};
</script>

<style scoped>
.c-container + .c-container {
  margin-left: 12px;
}
.time,
.number {
  width: 118px;
}
.c-text {
  margin-bottom: 6px;
}
.content-start {
  margin-bottom: 24px;
  display: flex;
}
.content-end {
  display: flex;
  margin-bottom: 24px;
}
.modal-foodter {
  margin-top: 24px;
  display: flex;
  justify-content: flex-end;
}
.add-date-start {
  display: flex;
  align-items: center;
  user-select: none;
  cursor: pointer;
}
.option-plus-text {
  color: #2196f3;
  font-size: 15px;
}
.icon-plus-blue {
  background-image: url(./../../assets/img/plus-blue.svg);
  margin-right: 8px;
}
.popup-select-due-date {
  width: auto;
  top: 65px;
  right: 30px;
}
.arror {
  position: absolute;
  top: -10px;
  right: 170px;
}
</style>
