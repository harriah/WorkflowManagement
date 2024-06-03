<template>
  <div class="chart-container">
    <div class="chart-body">
      <div class="scalesx-title rotate">Số lượng công việc</div>
      <div class="scalesx"></div>
      <div class="scalesy">
        <span class="y-text">0</span>
        <span
          class="y-text"
          v-for="(item, index) in dataSets.Data"
          :key="index"
          :style="{ bottom: (item / Math.max(...dataSets.Data)) * 88 + '%' }"
          >{{ item }}</span
        >
      </div>
      <div
        class="column-chart"
        v-for="(value, index) in dataSets.Data"
        :key="index"
        :style="{
          height: (value / Math.max(...dataSets.Data)) * 90 + '%',
          width: dataSets.ColumnSize + 'px',
          backgroundColor: dataSets.Labels[index].Color,
        }"
      >
        <span style="font-size: 11px; font-weight: 600">{{ value }}</span>
        <div class="note">{{ dataSets.Labels[index].Text }}</div>
      </div>
    </div>
    <div class="chart-labels">
      <div
        class="item-chart"
        v-for="(item, index) in dataSets.Labels"
        :key="index"
      >
        <div class="i-color" :style="{ backgroundColor: item.Color }"></div>
        <div class="i-text">{{ item.Text }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChartColumn",
  components: {},
  emits: [],
  props: ["values"],
  watch: {},
  mounted() {
    this.dataSets = this.values;
  },
  created() {},
  methods: {},
  data() {
    return {
      /**Dữ liệu để tạo ra 1 biểu đồ cột */
      dataSets: {
        /**Tiêu đề */
        Title: {
          X: "Số lượng công việc",
          Y: "Dự án",
        },
        /**Các loại item hiển thị dữ liệu */
        Labels: [
          {
            Text: "Đã hoàn thành",
            Color: "rgb(53, 190, 69)",
          },
          {
            Text: "Đang thực hiện",
            Color: "rgb(164, 207, 48)",
          },
          {
            Text: "Cần thực hiện",
            Color: "rgb(141, 163, 166)",
          },
          {
            Text: "Quá hạn",
            Color: "rgb(255, 101, 102)",
          },
        ],
        /**Thông số dữ liệu */
        Data: [0, 0, 0, 0],
        /**Độ rộng cột */
        ColumnSize: 28,
        /**Tuỳ chọn hiển thị biểu đồ */
        Options: {
          ScalesX: true,
          ScalesY: true,
          TitleX: true,
          TitleY: true,
        },
      },
    };
  },
};
</script>

<style scoped>
.scalesx-title {
  position: absolute;
  font-size: 14px;
  color: #555555;
  left: 9px;
  bottom: 100px;
}

.rotate {
  -moz-transform: translateX(-50%) translateY(-50%) rotate(-90deg);
  -webkit-transform: translateX(-50%) translateY(-50%) rotate(-90deg);
  transform: translateX(-50%) translateY(-50%) rotate(-90deg);
}
.y-text {
  position: absolute;
  left: -23px;
  font-size: 12px;
  color: #555555;
  text-align: right;
  font-weight: 600;
}
.i-text {
  font-size: 13px;
  width: 108px;
  color: #767676;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.item-chart {
  display: flex;
  align-items: center;
  width: 122px;
}
.i-color {
  width: 12px;
  height: 12px;
  border-radius: 2px;
  margin-right: 8px;
}
.note {
  position: absolute;
  bottom: 10px;
  width: 100px;
  height: 30px;
  text-align: center;
  font-size: 13px;
  color: #666;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
  overflow-wrap: break-word;
}
.column-chart {
  background-color: chartreuse;
  display: flex;
  justify-content: center;
  cursor: pointer;
}

.chart-container {
  width: 100%;
  height: 100%;
  background-color: rgb(255, 255, 255);
}
.chart-body {
  height: calc(100% - 110px);
  width: calc(100% - 50px);
  position: relative;
  padding-left: 50px;
  padding-bottom: 50px;
  display: flex;
  align-items: end;
  justify-content: space-evenly;
}
.chart-labels {
  height: 60px;
  display: flex;
  justify-content: space-evenly;
  border-top: 1px solid #878787;
}
.scalesx {
  width: calc(100% - 50px);
  height: 1px;
  background-color: #e0e0e0;
  position: absolute;
  bottom: 50px;
  left: 50px;
}
.scalesy {
  width: 1px;
  height: calc(100% - 50px);
  background-color: #e0e0e0;
  position: absolute;
  bottom: 50px;
  left: 50px;
  display: flex;
  flex-direction: column;
  justify-content: end;
}
</style>
