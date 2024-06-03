<template>
  <div class="qvc-loading">
    <div class="loading__content">
      <div class="loading__icon"></div>
      <div class="loading__text"></div>
    </div>
  </div>
</template>

<script>
export default {
  name: "QvcLoading",
  components: {},
  emits: [],
  props: [],
  watch: {},
  created() {},
  methods: {},
  data() {
    return {};
  },
};
</script>

<style scoped>
.qvc-loading {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #38383834;
  z-index: 20;
}

.loading__content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.loading__icon {
  width: 32px;
  height: 32px;
  background-image: url(../../assets/loading.svg);
  background-repeat: no-repeat;
  background-color: transparent;
  animation: rotation 1s infinite linear;
}

@keyframes rotation {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(359deg);
  }
}
</style>
