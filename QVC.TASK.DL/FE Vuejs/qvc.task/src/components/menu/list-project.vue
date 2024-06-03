<template>
  <div class="item-pro item-deprt" @click="onShowProject(id)" :objid="id">
    <div ref="icon" class="icon icon-16 collapse" type="false"></div>
    <span>{{ name }}</span>
  </div>
  <div v-if="isShowProject">
    <div
      v-for="(item, index) in projects"
      :key="index"
      class="item-pro item-project"
      :objid="item.ProjectID"
      @click="onSelectProject(item, idcompany)"
    >
      <div class="icon-pro">
        <div class="icon icon-16 icon-leaf"></div>
      </div>
      <span>{{ item.ProjectName }}</span>
      <!-- <div class="icon icon-cbb"></div> -->
    </div>
  </div>
</template>

<script>
import { ENUMSTATE } from "@/enum.js";
import { ENUMROLE } from "@/enum.js";
export default {
  name: "ListProject",
  components: {},
  emits: ["onSelectProject"],
  props: ["id", "name", "idcompany"],
  created() {},
  methods: {
    /**
     * Thực hiện khi chọn dự án để thêm công việc
     * @param {*} data đối tượng dự án
     */
    onSelectProject(data, idcompany) {
      this.$emit("onSelectProject", data, idcompany);
    },

    /**
     * THực hiện sự kiện khi click vào phòng ban
     * @param {*} id id phòng ban
     */
    onShowProject(id) {
      var status = this.$refs.icon.getAttribute("type");
      if (status == "true") {
        //Đóng lại
        this.$refs.icon.setAttribute("type", "false");
        this.$refs.icon.classList.remove("extend");
        this.$refs.icon.classList.add("collapse");
        this.isShowProject = false;
      } else {
        // Mở ra
        this.$refs.icon.setAttribute("type", "true");
        this.$refs.icon.classList.remove("collapse");
        this.$refs.icon.classList.add("extend");
        // Gọi hàm lấy danh sách dự án theo phòng ban
        this.getProjectById(id);
        this.isShowProject = true;
      }
    },

    /**
     * Thực hiện lấy danh sách dự án theo phòng ban
     * @param {*} id id của phòng ban
     */
    getProjectById(id) {
      // Chuẩn bị dữ liệu
      var db = !this.idcompany
        ? localStorage.getItem("domain-db")
        : localStorage.getItem("domain-company");
      this.axios
        .get(
          `http://localhost:56428/api/v2/Project/getall-byid?id=${id}&domain=${db}`
        )
        .then((res) => {
          if (res.data) {
            this.projects = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
  },
  data() {
    return {
      /**Hiển thị list project */
      isShowProject: false,

      /**Danh sách dự án */
      projects: [],
      ENUMSTATE,
      ENUMROLE,
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

.extend {
  background-image: url(../../assets/img/extend.svg);
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
  width: auto;
  top: 55px;
}
.arror {
  position: absolute;
  top: -10px;
  right: 170px;
}
</style>
