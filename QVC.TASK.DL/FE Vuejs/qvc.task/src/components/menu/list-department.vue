<template>
  <div class="s-l-item" :objid="id">
    <div class="s-l-i-left">
      <div
        ref="icon"
        class="l-i__icon icon-collapse"
        type="false"
        @click="onClickDepartment(id, companyid)"
      ></div>
      <div
        @click.stop="navigatorDeprt(id, companyid)"
        class="l-i__content-list list-content"
      >
        {{ name }}
      </div>
    </div>
    <div class="s-l-i-right">
      <div class="l-i__icon add-project" @click.stop="onClickAdd"></div>
      <div class="l-i__icon view-project" @click="onClickView"></div>
    </div>
  </div>
  <div v-if="isShowProject">
    <div
      class="s-list-item__project"
      @click="onNavigatorProject(id, item.ProjectID)"
      v-for="(item, index) in projects"
      :key="index"
      :objid="item.ProjectID"
    >
      <div class="s-l__p-icon">
        <div class="icon-project icon_my-job"></div>
      </div>
      <div class="s-l__p-text">
        <span class="list-content">{{ item.ProjectName }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { ENUMSTATE } from "@/enum.js";
import { ENUMROLE } from "@/enum.js";
export default {
  name: "ListDepartment",
  props: ["name", "id", "isloadproject", "companyid"],
  emits: ["onShowProject"],
  components: {},
  watch: {
    isloadproject() {
      this.getProjectById(this.id);
    },
  },
  created() {},
  methods: {
    onNavigatorProject(iddepart, idproject) {
      this.$router.push({
        path: "/project",
        query: {
          projectid: idproject,
          departid: iddepart,
          state: !this.companyid ? ENUMSTATE.CaNhan : ENUMSTATE.CongTy,
        },
      });
    },
    /**
     * Thực hiện chuyển tới trạng phòng ban
     * @param {*} id id phòng ban
     */
    navigatorDeprt(id, companyid) {
      this.$router.push({
        path: "/department",
        query: { id: id, companyid: companyid },
      });
    },

    /**
     * Sự kiện click vào phòng ban để xem danh sách dự án
     * @param {*} id id của phòng ban
     */
    onClickDepartment(id, companyid) {
      var status = this.$refs.icon.getAttribute("type");
      if (status == "true") {
        //Đóng lại
        this.$refs.icon.setAttribute("type", "false");
        this.$refs.icon.classList.remove("icon-extend");
        this.$refs.icon.classList.add("icon-collapse");
        this.isShowProject = false;
      } else {
        // Mở ra
        this.$refs.icon.setAttribute("type", "true");
        this.$refs.icon.classList.remove("icon-collapse");
        this.$refs.icon.classList.add("icon-extend");
        // Gọi hàm lấy danh sách dự án theo phòng ban
        this.getProjectById(id, companyid);
        this.isShowProject = true;
      }
    },

    /**
     * Thực hiện lấy danh sách dự án theo phòng ban
     * @param {*} id id của phòng ban
     */
    getProjectById(id, companyid) {
      var db = !companyid
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

    /**
     * Hiển thị form thêm dự án
     *
     */
    onClickAdd() {
      this.$emit("onShowProject");
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
.s-l-item,
.s-list-item__project {
  user-select: none;
}
</style>
