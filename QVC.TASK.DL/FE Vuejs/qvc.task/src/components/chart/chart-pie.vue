<template>
  <div style="width: auto; height: 100%">
    <canvas ref="chart"></canvas>
  </div>
</template>

<script>
import Chart from "chart.js/auto";
export default {
  name: "ChartPie",
  components: {},
  emits: [],
  props: {
    completed: {
      type: Number,
      required: true,
    },
    inProgress: {
      type: Number,
      required: true,
    },
    toDo: {
      type: Number,
      required: true,
    },
    overdue: {
      type: Number,
      required: true,
    },
  },
  mounted() {
    this.renderChart();
  },
  created() {},
  methods: {
    renderChart() {
      const chartData = {
        datasets: [
          {
            data: [this.completed, this.inProgress, this.toDo, this.overdue],
            backgroundColor: [
              "rgb(53, 190, 69)",
              "rgb(164, 207, 48)",
              "rgb(141, 163, 166)",
              "rgb(255, 101, 102)",
            ],
          },
        ],
        labels: ["Đã hoàn thành", "Đang thực hiện", "Cần thực hiện", "Quá hạn"],
      };

      const options = {
        responsive: true,
        cutout: 40,
        plugins: {
          legend: {
            display: false,
          },
        },
      };

      new Chart(this.$refs.chart, {
        type: "doughnut",
        data: chartData,
        options: options,
      });
    },
  },
  data() {
    return {};
  },
};
</script>

<style scoped></style>
