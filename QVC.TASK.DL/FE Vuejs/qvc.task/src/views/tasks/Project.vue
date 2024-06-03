<template>
  <div class="container my-task">
    <div class="my-task-header" style="position: relative">
      <div class="mt-header-left">
        <div class="h-left-icon">
          <div class="icon icon-24 icon-toggle"></div>
          <router-link to="/" class="icon icon-24 icon-home"></router-link>
        </div>
        <div class="mt-layout"></div>
        <div class="h-left-tab">
          <div class="btn-name-department" @click="onShowPopListProject">
            <span class="btn-h-text" style="color: #000; font-size: 15px">{{
              project.ProjectName
            }}</span>
            <div class="btn-add-plus">
              <div class="icon-h-drop drop-black"></div>
            </div>
          </div>
          <SelectDepartment
            v-if="false"
            @onSelectData="onSelectDataDeprt"
          ></SelectDepartment>
          <div
            class="icon icon-24 icon-setting-deprt"
            @click="isShowSetting = !isShowSetting"
          ></div>
          <!-- Popup hiển thị thiết lập dự án -->
          <div
            class="popup-combobox cbb-setting"
            v-if="isShowSetting"
            style="right: 158px"
          >
            <div class="p-s-content">
              <div class="item-ccb" @click="onShowEditProject">
                <span>Thông tin dự án/nhóm</span>
              </div>
              <div class="item-ccb" @click="onShowDialogDeleteProject">
                <span>Xoá dự án/nhóm</span>
              </div>
            </div>
          </div>
          <!-- Popup hiển thị thiết lập dự án -->
          <div
            ref="tabtable"
            class="tab-item tab-active"
            style="margin-left: 24px"
            @click="toggleTab(0)"
          >
            <div class="tab-item-text">Bảng</div>
          </div>
          <div
            ref="tablist"
            class="tab-item"
            style="margin-left: 24px"
            @click="toggleTab(1)"
          >
            <div class="tab-item-text">Danh sách</div>
          </div>
          <!-- Popup hiển thị chọn dự án -->
          <div class="popup-combobox cbb-project" v-if="isShowPopListProject">
            <div class="arror arrow-top" style="right: 185px"></div>
            <div class="p-s-content">
              <div
                class="item-ccb"
                @click="onSelecteProject(item.ProjectID, item.DepartmentID)"
                :objid="item.ProjectID"
                v-for="(item, index) in listProject"
                :key="index"
              >
                <span>{{ item.ProjectName }}</span>
              </div>
            </div>
          </div>
          <!-- Popup hiển thị chọn dự án -->
        </div>
      </div>
      <div class="mt-header-right">
        <div class="btn-header btn-layout">
          <div
            style="display: flex; align-items: center"
            @click="isShowJobDetail = !isShowJobDetail"
          >
            <div class="icon__btn-header icon-add"></div>
            <span class="btn-h-text">Thêm công việc</span>
          </div>
          <div class="btn-h-layout"></div>
          <div class="btn-add-plus" @click="isShowAddJob = !isShowAddJob">
            <div class="icon-h-drop"></div>
          </div>
        </div>
      </div>
      <AddJob
        :top="55"
        :right="20"
        :arrow="90"
        v-if="isShowAddJob"
        @addDepartment="onAddDepartment"
        @addProject="onAddProject"
        @addCompany="onAddCompany"
      ></AddJob>
    </div>
    <ProjectTable
      :id="this.$route.query.projectid"
      :state="this.$route.query.state"
      v-if="isShowTable"
      :isload="isLoadDataJob"
      :nameproject="project.ProjectName"
    ></ProjectTable>
    <ProjectList
      :id="this.$route.query.projectid"
      :state="this.$route.query.state"
      :nameproject="project.ProjectName"
      :isload="isLoadDataJob"
      v-if="isShowList"
    ></ProjectList>
  </div>
  <ProjectDetail
    :iddeprt="this.$route.query.departid"
    :idcompany="this.$route.query.companyid"
    :mode="modeProjectNow"
    :data="projectNow"
    :screen="ENUMSCREEN.Project"
    :stateedit="this.$route.query.state"
    v-if="isShowProjectDetail"
    @onClose="isShowProjectDetail = false"
    @onCancel="isShowProjectDetail = false"
    @onConfirm="onConfirmProject"
  ></ProjectDetail>
  <ToastMessage ref="toast"></ToastMessage>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
  <PopupNotification
    :title="titleWarning"
    :content="contentWaring"
    :typeicon="typeIcon"
    :showbtncancel="true"
    @onClose="isShowWarning = false"
    @onConfirm="onDeleteProject"
    @onCancel="isShowWarning = false"
    v-if="isShowWarning"
  ></PopupNotification>
  <JobDetail
    v-if="isShowJobDetail"
    :idcompany="this.$route.query.companyid"
    :iddepart="this.$route.query.departid"
    :idproject="this.$route.query.projectid"
    :nameproject="project.ProjectName"
    :screen="ENUMSCREEN.Project"
    :mode="ENUMMODE.Add"
    @onClose="isShowJobDetail = false"
    @onCancel="isShowJobDetail = false"
    @onConfirm="insertJob"
  ></JobDetail>
  <DepartmentDetail
    v-if="isShowDeparDetail"
    @onConfirm="insertDepartment"
    @onClose="isShowDeparDetail = false"
    @onCancel="isShowDeparDetail = false"
  ></DepartmentDetail>
  <CompanyDetail
    v-if="isShowCompanyDetail"
    @onCancel="isShowCompanyDetail = false"
    @onClose="isShowCompanyDetail = false"
    @onConfirm="insertCompany"
  ></CompanyDetail>
</template>
<script>
import SelectDepartment from "./../../components/popup/select-department.vue";
import ProjectTable from "./../../components/project/project-table.vue";
import ProjectList from "./../../components/project/project-list.vue";
import { ENUMSTATE } from "@/enum";
import { ENUMSCREEN } from "@/enum";
import { ENUMMODE } from "@/enum.js";
import { ENUMTOAST } from "@/enum.js";
import { ENUMICON } from "@/enum.js";
import ProjectDetail from "./../../components/form/project-detail.vue";
import ToastMessage from "./../../components/toast/toast-message.vue";
import QvcLoading from "./../../components/dialog/qvc-loading.vue";
import PopupNotification from "./../../components/popup/popup-notification.vue";
import AddJob from "./../../components/popup/add-job.vue";
import JobDetail from "./../../components/form/job-detail.vue";
import DepartmentDetail from "./../../components/form/department-detail.vue";
import CompanyDetail from "./../../components/form/company-detail.vue";
import { RESAPI } from "@/res.js";
const { v4: uuidv4 } = require("uuid");
export default {
  name: "ProjectTask",
  components: {
    SelectDepartment,
    ProjectTable,
    ProjectList,
    ProjectDetail,
    ToastMessage,
    QvcLoading,
    PopupNotification,
    AddJob,
    JobDetail,
    DepartmentDetail,
    CompanyDetail,
  },
  emits: [],
  props: [],
  watch: {},
  mounted() {
    this.getProjectById();
  },
  created() {
    this.getProjectById();
    this.checkState();
  },
  methods: {
    insertCompany(data, emps, role) {
      var datainput = {
        Data: {
          CompanyCode: this.generateCodeDeprt("CP"),
          CompanyName: data.CompanyName,
          UserName: localStorage.getItem("user-name"),
        },
        DBDomain: "qvc_task_info",
      };
      this.isShowLoading = true;
      this.axios
        .post("http://localhost:56428/api/v2/Company/insert", datainput)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowCompanyDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu đã được cập nhật.",
              ENUMTOAST.Success
            );
          }, 500);
          // Thực hiện thêm mới nhân viên vào cong ty
          //Buldi dữ liệu
          for (const emp of emps) {
            var data = {
              Data: emp,
              DBDomain: localStorage.getItem("domain-db"),
            };
            data.Data.Role = role;
            this.insertEmpToCompay(data);
          }
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể thêm mới công ty. Vui lòng kiểm tra lại.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
    },
    onAddCompany() {
      this.isShowCompanyDetail = true;
      this.isShowAddJob = false;
    },
    insertProject(project, listid, idcompany) {
      // Bulid dữ liệu
      project.ProjectCode = this.generateCodeDeprt("PR");
      var data = {
        Data: project,
        DBDomain: this.$common.getNameDB(idcompany),
      };
      data.Data.CreatedBy = localStorage.getItem("full-name");
      this.isShowLoading = true;
      //Gọi API
      this.axios
        .post("http://localhost:56428/api/v2/Project/insert", data)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowProjectDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu dự án đã được lưu lại.",
              ENUMTOAST.Success
            );
            //Lấy lại danh sách dự án
            // this.$nextTick(() => {
            //   this.getProjectById(this.iddepart);
            // });
          }, 500);
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể thêm mới dự án. Vui lòng kiểm tra lại.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
      console.log(listid);
    },
    onConfirmProject(project, emp, idcompany, mode) {
      if (mode == ENUMMODE.Add) {
        //
        this.insertProject(project, emp, idcompany);
      } else {
        this.updateProject(project, emp);
      }
    },
    onAddProject() {
      this.modeProjectNow = ENUMMODE.Add;
      this.projectNow = null;
      this.isShowProjectDetail = true;
      this.isShowAddJob = false;
    },
    /**
     * Tạo mã bản ghi random
     */
    generateCodeDeprt(key) {
      // Generate a random integer between 0 and 99999
      const randomNumber = Math.floor(Math.random() * 100000);

      // Pad the number with leading zeros to ensure it has 5 digits
      const randomCode = randomNumber.toString().padStart(5, "0");

      // Concatenate the code with the prefix 'DP'
      const finalCode = key + randomCode;

      return finalCode;
    },
    /**
     * Thực hiên thêm mới phòng ban
     * @param {*} deprt thông tin phòng ban
     * @param {*} listid danh sách id nhân viên
     */
    insertDepartment(deprt, emps, role) {
      // Bulid dữ liệu
      this.uuidv4Temp = uuidv4();
      var data = {
        Data: {
          DepartmentID: this.uuidv4Temp,
          DepartmentName: deprt.DepartmentName,
          DepartmentCode: this.generateCodeDeprt("DP"),
          CompanyID:
            this.state == ENUMSTATE.CongTy
              ? localStorage.getItem("company-id")
              : null,
          CreatedBy: localStorage.getItem("full-name"),
        },
        DBDomain: this.$common.getNameDB(localStorage.getItem("company-id")),
      };
      this.isShowLoading = true;
      //Gọi API
      this.axios
        .post("http://localhost:56428/api/v2/Department/insert", data)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowDeparDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu phòng ban đã được lưu lại.",
              ENUMTOAST.Success
            );
            //Thực hiên đi tới phòng ban vừa tạo
            this.iddepart = this.uuidv4Temp;

            this.$nextTick(() => {
              const cpnid =
                parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan
                  ? null
                  : localStorage.getItem("company-id");
              this.$router.push({
                path: "/department",
                query: {
                  id: this.iddepart,
                  companyid: cpnid,
                },
              });
            });

            // Thực hiện thêm mới nhân viên vào cong ty
            //Buldi dữ liệu
            for (const emp of emps) {
              var data = {
                Data: emp,
                DBDomain: localStorage.getItem("domain-company"),
              };
              data.Data.Role = role;
              this.insertEmpToCompay(data);
            }
          }, 500);
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể thêm mới phòng ban. Vui lòng kiểm tra lại.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
    },
    insertEmpToCompay(data) {
      this.axios
        .post("http://localhost:56428/api/v2/Employee/insert", data)
        .then((res) => {
          console.log(res);
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onAddDepartment() {
      this.isShowDeparDetail = true;
      this.isShowAddJob = false;
    },
    insertJob(job, jobchild) {
      // Bulid dữ liệu
      var db = this.$common.getNameDB(job.CompanyID);

      var data = {
        DataInsert: {
          Data: job,
          DBDomain: db,
        },
        DataList: jobchild,
      };
      this.isShowLoading = true;
      //Gọi API
      this.axios
        .post(RESAPI.InsertJob, data)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowJobDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu công việc đã được cập nhật.",
              ENUMTOAST.Success
            );
            this.getProjectById(this.iddepart);
            //Tải lại trang
            location.reload();
          }, 500);
          // Nếu chọn người giao việc thì thực hiện giao việc
          if (!job.CompanyID && !job.EmployeeID) {
            // Giao việc cho cá nhân
            // Build dữ liệu giao việc
            var assigns = {
              CreatedBy: !localStorage.getItem("full-name")
                ? ""
                : localStorage.getItem("full-name"),
              EmployeeID: localStorage.getItem("userid"),
              JobID: job.JobID,
              State: ENUMSTATE.CaNhan,
            };
            // Gọi hàm giao việc
            this.insertAssign(assigns, db);
          } else {
            if (job.EmployeeID) {
              // Build dữ liệu giao việc
              var ass = {
                CreatedBy: !localStorage.getItem("full-name")
                  ? ""
                  : localStorage.getItem("full-name"),
                EmployeeID: job.EmployeeID,
                JobID: job.JobID,
                State: ENUMSTATE.CongTy,
              };
              // Gọi hàm giao việc
              this.insertAssign(ass, db);
            }
          }
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể thêm mới công. Vui lòng kiểm tra lại.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
    },
    insertAssign(assign, db) {
      // Kiểm tra và lấy domain để insert giao việc
      var data = {
        Data: assign,
        DBDomain: db,
      };
      this.axios
        .post("http://localhost:56428/api/v2/Assign/insert", data)
        .then(() => {})
        .catch((res) => {
          console.log(res);
        });
    },
    updateProject(project, listid) {
      // Bulid dữ liệu
      var data = {
        Data: project,
        DBDomain:
          this.state == ENUMSTATE.CaNhan
            ? localStorage.getItem("domain-db")
            : localStorage.getItem("domain-company"),
      };
      data.Data.ModifiedBy = localStorage.getItem("full-name");
      this.isShowLoading = true;
      //Gọi API
      this.axios
        .post("http://localhost:56428/api/v2/Project/updateby-id", data)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowProjectDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu dự án đã được cập nhật.",
              ENUMTOAST.Success
            );
            //Lấy lại danh sách dự án
            this.isLoadProject = !this.isLoadProject;
          }, 500);
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể thêm mới dự án. Vui lòng kiểm tra lại.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
      console.log(project, listid);
    },
    onShowEditProject() {
      this.projectNow = this.project;
      this.modeProjectNow = ENUMMODE.Edit;
      this.isShowSetting = false;
      this.isShowProjectDetail = true;
    },

    checkState() {
      if (
        !localStorage.getItem("domain-company") ||
        localStorage.getItem("domain-company") == "null"
      ) {
        this.state = ENUMSTATE.CaNhan;
        // Dạng cá nhân
      } else {
        //Dạng công ty
        this.state = ENUMSTATE.CongTy;
      }
    },
    getDataUrl() {
      var data = {
        Projectid: this.$route.query.projectid,
        Departid: this.$route.query.departid,
        State: this.$route.query.state,
      };
      return data;
    },
    getDataLocal() {
      var data = {
        Dbdomain: localStorage.getItem("domain-db"),
        Dbcompany: localStorage.getItem("domain-company"),
        Role: parseInt(localStorage.getItem("role")),
        Fullname: localStorage.getItem("full-name"),
      };
      return data;
    },
    onShowPopListProject() {
      if (this.isShowPopListProject) {
        // Đóng
        this.isShowPopListProject = false;
      } else {
        this.isShowPopListProject = true;
        this.getAllProjectByIdDepart();
      }
    },
    getAllProjectByIdDepart() {
      var dataUrl = this.getDataUrl();
      var dataLocal = this.getDataLocal();
      this.axios
        .get(
          `http://localhost:56428/api/v2/Project/getall-byid?id=${
            dataUrl.Departid
          }&domain=${
            dataUrl.State == ENUMSTATE.CaNhan
              ? dataLocal.Dbdomain
              : dataLocal.Dbcompany
          }`
        )
        .then((res) => {
          if (res.data) {
            this.listProject = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onSelecteProject(id, iddepart) {
      // Đóng
      this.isShowPopListProject = false;
      var dataUrl = this.getDataUrl();
      var dataLocal = this.getDataLocal();
      this.$router.push({
        path: "/project",
        query: {
          projectid: id,
          departid: iddepart,
          state: dataUrl.State,
        },
      });
      this.axios
        .get(
          `http://localhost:56428/api/v2/Project/getby-id?id=${id}&domaindb=${
            dataUrl.State == ENUMSTATE.CaNhan
              ? dataLocal.Dbdomain
              : dataLocal.Dbcompany
          }`
        )
        .then((res) => {
          if (res.data) {
            this.project = res.data;
            this.isLoadDataJob = !this.isLoadDataJob;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },

    onShowDialogDeleteProject() {
      // Hiển thị thông báo đăng nhập
      this.contentWaring = [];
      this.contentWaring.push(
        `Bạn có muốn xoá dự án ${this.project.ProjectName} không?`
      );
      this.titleWarning = "Thông báo";
      this.typeIcon = ENUMICON.Waring;
      this.isShowWarning = true;
      this.isShowSetting = false;
    },
    onDeleteProject() {
      var db =
        this.state == ENUMSTATE.CaNhan
          ? localStorage.getItem("domain-db")
          : localStorage.getItem("domain-company");

      this.isShowLoading = true;
      this.axios
        .delete(
          `http://localhost:56428/api/v2/Project/deleteby-id?id=${this.project.ProjectID}&db=${db}`
        )
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowWarning = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu đã được cập nhật.",
              ENUMTOAST.Success
            );
            this.$router.push("/");
          }, 500);
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Lỗi!",
              "Không thể cập nhật dữ liệu.",
              ENUMTOAST.Waring
            );
            console.log(res);
          }, 500);
        });
    },

    getProjectById() {
      var dataUrl = this.getDataUrl();
      var dataLocal = this.getDataLocal();
      this.axios
        .get(
          `http://localhost:56428/api/v2/Project/getby-id?id=${
            dataUrl.Projectid
          }&domaindb=${
            dataUrl.State == ENUMSTATE.CaNhan
              ? dataLocal.Dbdomain
              : dataLocal.Dbcompany
          }`
        )
        .then((res) => {
          if (res.data) {
            this.project = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    toggleTab(index) {
      if (index == 0) {
        this.isShowTable = true;
        this.isShowList = false;
        this.$refs.tabtable.classList.add("tab-active");
        this.$refs.tablist.classList.remove("tab-active");
      } else {
        this.isShowTable = false;
        this.isShowList = true;
        this.$refs.tabtable.classList.remove("tab-active");
        this.$refs.tablist.classList.add("tab-active");
      }
    },
  },
  data() {
    return {
      contentWaring: [],
      isShowProjectDetail: false,
      isShowSetting: false,
      isShowTable: true,
      isShowList: false,
      project: {},
      listProject: [],
      isShowPopListProject: false,
      textRole: {},
      keyRole: {},
      isShowLoading: false,
      state: ENUMSTATE.CaNhan,
      ENUMMODE,
      ENUMSTATE,
      ENUMTOAST,
      isShowWarning: false,
      titleWarning: "Thông báo",
      typeIcon: ENUMICON.Waring,
      isLoadDataJob: false,
      isShowAddJob: false,
      isShowJobDetail: false,
      ENUMSCREEN,
      RESAPI,
      isShowDeparDetail: false,
      projectNow: {},
      modeProjectNow: ENUMMODE.Add,
      isShowCompanyDetail: false,
    };
  },
};
</script>

<style scoped>
.cbb-setting {
  position: absolute;
  top: 48px;
  left: 0px;
  z-index: 20 !important;
}
.cbb-project {
  top: 48px;
  left: -70px;
  z-index: 20;
}
.my-task-content {
  width: calc(100% - 36px);
  height: auto !important;
  padding: 0px 16px 12px 16px;
}
.my-task-content {
  width: calc(100% - 36px);
  height: calc(100vh - 69px);
  padding: 0px 16px 12px 16px;
}
.char-pro + .char-pro {
  margin-top: 4px;
}
.i-description {
  margin-top: 6px;
  padding-left: 30px;
}
.text-name {
  font-size: 17px;
  font-weight: 700;
  margin-bottom: 6px;
}
.text-date {
  font-size: 13px;
  color: #666;
}
.info-avt {
  width: 30px;
  height: 30px;
  background-color: greenyellow;
  border-radius: 50px;
  margin-right: 8px;
}
.i-info {
  display: flex;
  align-items: center;
  justify-content: center;
}
.c-project {
  padding: 20px 0px 20px 0px;
  display: flex;
}
.item-projecr {
  width: 200px;
  height: 300px;
  background-color: #fff;
  border-radius: 8px;
  padding: 10px;
}
.item-projecr + .item-projecr {
  margin-left: 20px;
}
.chart-content {
  padding: 20px;
}
.i-text {
  font-size: 13px;
  color: #767676;
}
.item-chart {
  display: flex;
  align-items: center;
}

.i-color {
  width: 12px;
  height: 12px;
  border-radius: 2px;
  margin-right: 8px;
}
.chart-footer {
  display: flex;
  justify-content: space-around;
  padding: 20px;
}
.i-cbb {
  width: 250px;
  position: relative;
}
.chart-header {
  display: flex;
  justify-content: space-between;
}
.c-chart {
  background-color: #fff;
  border-radius: 8px;
  padding: 20px;
}
.point {
  left: 10px !important;
}
.i-filter {
  width: 250px;
}
.c-header {
  display: flex;
  padding: 20px 0px 20px 0px;
  justify-content: space-between;
}
.flex-con {
  display: flex;
  align-items: center;
  position: relative;
}
.my-task-content {
  background-color: #f1f1f1 !important;
}
.h-left-tab {
  align-items: center;
  position: relative;
}

.popup-deprt {
  top: 37px;
}

.cbb {
  background-image: url(./../../assets/img/cbb.svg);
  position: absolute;
  right: 10px;
}
.icon-setting-deprt {
  background-image: url(./../../assets/img/setting-deprt.svg);
  margin-left: 12px;
  cursor: pointer;
  user-select: none;
}
.icon-white {
  background-image: url(./../../assets/img/white.svg);
  width: 30px;
  height: 24px;
  background-size: 24px 24px;
  margin-left: 12px;
}
.btn-name-department {
  min-width: 10px !important;
  margin-top: 4px;
  width: auto;
  min-width: 78px;
  height: 32px;
  padding: 0px 16px 0px 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  position: relative;
  -webkit-user-select: none;
  -moz-user-select: none;
  user-select: none;
}
</style>
