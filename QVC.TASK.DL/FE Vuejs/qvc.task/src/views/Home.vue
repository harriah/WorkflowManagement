<template>
  <div class="container dashboard">
    <div class="sidebar">
      <div class="s-logo">
        <div class="s-l-img"></div>
        <div class="s-l-name">Công việc</div>
      </div>
      <div class="s-menu">
        <div class="s-m-item">
          <div class="i__icon home"></div>
          <div class="i__content-menu">Tổng quan</div>
        </div>
        <router-link to="/mytask" class="s-m-item">
          <div class="i__icon my-job"></div>
          <div class="i__content-menu">Việc của tôi</div>
        </router-link>
        <div class="s-m-item">
          <div class="i__icon report"></div>
          <div class="i__content-menu">Báo cáo</div>
        </div>
      </div>
      <div class="s-option">
        <input placeholder="Tìm kiếm" class="s-o__filter" type="text" />
        <div class="s-o__setting"></div>
      </div>
      <div class="s-list scrollbar">
        <ListDepartment
          v-for="(item, index) in departments"
          :key="index"
          :name="item.DepartmentName"
          :id="item.DepartmentID"
          :companyid="item.CompanyID"
          :isloadproject="isLoadProject"
          @onShowProject="
            onShowProject(
              item.DepartmentID,
              item.DepartmentName,
              item.CompanyID
            )
          "
        ></ListDepartment>
      </div>
    </div>
    <div class="main">
      <div class="m-header">
        <div class="m-h-left">
          <div class="btn-header btn-custom">
            <div class="icon__btn-header icon-custom"></div>
            <span class="btn-h-text">Tuỳ chỉnh</span>
          </div>
        </div>
        <div class="m-h-right">
          <div class="btn-header" style="margin-right: 10px">
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
            <div class="popup-add-job" v-if="isShowAddJob">
              <div class="arror arrow-top"></div>
              <div class="p-a-item" v-if="false">
                <div class="item-icon icon icon-24 add-job"></div>
                <div class="item-text">Thêm công việc từ mẫu</div>
              </div>
              <div class="p-a-item">
                <div class="item-icon icon icon-24 job-column"></div>
                <div class="item-text">Thêm cột mốc công việc</div>
              </div>
              <div
                class="p-a-item"
                v-if="keyRole.isShowAddProject"
                @click="onShowProject(undefined, undefined, undefined)"
              >
                <div class="item-icon icon icon-24 add-project-group"></div>
                <div class="item-text">Thêm dự án/nhóm</div>
              </div>
              <div
                class="p-a-item"
                @click="onShowFormDeprt"
                v-if="keyRole.isShowAddDepart"
              >
                <div class="item-icon icon icon-24 add-deprt"></div>
                <div class="item-text">{{ textRole.depart }}</div>
              </div>
              <div
                class="p-a-item"
                @click="onShowCompanyDetail"
                v-if="keyRole.isShowAddCompay"
              >
                <div class="item-icon icon icon-24 add-uo"></div>
                <div class="item-text">Tạo tổ chức (công ty/doanh nghiệp)</div>
              </div>
              <div class="p-a-item" v-if="keyRole.isShowAddEmp">
                <div class="item-icon icon icon-24 add-user"></div>
                <div class="item-text">
                  {{ textRole.addemp }}
                </div>
              </div>
            </div>
          </div>
          <div
            class="btn-icon-header"
            v-if="false"
            @click.stop="isShowFilter = !isShowFilter"
          >
            <div class="icon-header icon-search"></div>
            <div class="popup-search" v-if="isShowFilter">
              <div class="arror arrow-top search-arrow"></div>
              <div class="p-n-input">
                <input
                  placeholder="Tìm theo tên công việc, mô tả công việc, thành viên, thẻ..."
                  class="input input-icon-left i-search-all"
                  type="text"
                />
                <div class="icon icon-16 search-all"></div>
              </div>
              <div class="p-n-content">
                <div class="p-n-c-background"></div>
                <div class="p-n-c-text">
                  Tìm kiếm công việc, thành viên, thẻ...
                </div>
              </div>
              <div class="p-n-footer">
                <div class="p-n-f-icon icon icon-16 icon-advanced"></div>
                <div class="p-n-f-text">Tìm kiếm công việc nâng cao</div>
              </div>
            </div>
          </div>
          <div
            v-if="false"
            class="btn-icon-header"
            @click.stop="this.isShowNotification = !this.isShowNotification"
          >
            <div class="icon-header icon-notification"></div>
            <div class="popup-notification" v-if="isShowNotification">
              <div class="arror arrow-top notification-arrow"></div>
              <div class="p-n-header">
                <div class="p-n-h-title">Thông báo</div>
                <div class="p-n-h-func">
                  <div
                    class="btn-nofiti"
                    @click.stop="isShowNotifiFilter = !isShowNotifiFilter"
                  >
                    <span>Tất cả</span>
                    <div class="icon-nofiti"></div>
                  </div>
                  <div
                    class="popup-notification-view-menu"
                    v-if="isShowNotifiFilter"
                  >
                    <div class="p-a-item">
                      <div class="item-text">Tất cả</div>
                    </div>
                    <div class="p-a-item">
                      <div class="item-text">Chưa đọc</div>
                    </div>
                  </div>
                  <div
                    class="btn-menu"
                    @click.stop="isShowNotifiSetting = !isShowNotifiSetting"
                  >
                    <div class="icon-menu"></div>
                  </div>
                  <div
                    class="popup-notification-view-setup"
                    v-if="isShowNotifiSetting"
                  >
                    <div class="p-a-item">
                      <div class="item-icon icon icon-16 icon-checkall"></div>
                      <div class="item-text">Đánh dấu đã đọc tất cả</div>
                    </div>
                    <div class="p-a-item">
                      <div
                        class="item-icon icon icon-16 icon-setting-nofiti"
                      ></div>
                      <div class="item-text">Thiết lập thông báo</div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="p-n-notification">
                <div class="p-n-n-icon"></div>
              </div>
            </div>
          </div>
          <div class="btn-icon-header" v-if="false">
            <div class="icon-header icon-message"></div>
          </div>
          <div
            v-if="false"
            class="btn-icon-header"
            @click="this.isShowSetting = !this.isShowSetting"
          >
            <div class="icon-header icon-setting"></div>
            <div class="popup-settings" v-if="isShowSetting">
              <div class="arror arrow-top settings-arrow"></div>
              <div class="p-s-header">Thiết lập chung</div>
              <div class="p-s-content">
                <div class="p-s-c-item"><span>Mẫu công việc</span></div>
                <div class="p-s-c-item"><span>Mẫu dự án</span></div>
                <div class="p-s-c-item"><span>Thông báo</span></div>
                <div class="p-s-c-item"><span>Tự động gửi báo cáo</span></div>
                <div class="p-s-c-item"><span>Bàn giao công việc</span></div>
                <div class="p-s-c-item"><span>Uỷ quyền</span></div>
                <div class="p-s-c-item"><span>Góp ý sản phẩm</span></div>
              </div>
            </div>
          </div>
          <div class="btn-icon-header btn-avatar"></div>
          <div class="text-logout" @click="onLogout">
            {{ fullName }}
          </div>
        </div>
      </div>
      <div class="m-content">
        <div class="container-h-option">
          <!-- Số lượng công việc hôm nay -->
          <div class="h-myjob-count">
            <div class="tite-h">Số lượng công việc tính đến hôm nay</div>
            <div class="body-h">
              <div class="item-h">
                <div class="item-left">
                  <div class="icon icon-24 icon-quahan"></div>
                  <div class="text-body">Quá hạn</div>
                </div>
                <div class="count-body">{{ jobOutOfDate }}</div>
              </div>
              <div class="item-h">
                <div class="item-left">
                  <div class="icon icon-24 icon-danglam"></div>
                  <div class="text-body">Đang làm</div>
                </div>
                <div class="count-body">{{ jobProcessing }}</div>
              </div>
              <div class="item-h">
                <div class="item-left">
                  <div class="icon icon-24 icon-dahoanthanh"></div>
                  <div class="text-body">Đã hoàn thành</div>
                </div>
                <div class="count-body">{{ jobComplete }}</div>
              </div>
              <div class="item-h">
                <div class="item-left">
                  <div class="icon icon-24 icon-giaochotoi"></div>
                  <div class="text-body">Giao cho tôi</div>
                </div>
                <div class="count-body">{{ jobAssign }}</div>
              </div>
            </div>
          </div>
          <!-- Số lượng công việc hôm nay -->

          <!-- Danh sách việc đang làm -->
          <div class="h-jobprogess">
            <div class="tite-h" style="position: sticky; top: 0">
              Danh sách công việc đang làm
            </div>
            <div class="body-h-list scrollbar">
              <div
                class="item-job"
                v-for="(item, index) in listJobProcessing"
                :key="index"
              >
                <div class="job-status">
                  <div
                    title="Ấn để hoàn thành công việc"
                    class="s-item icon icon-24 icon-Progress"
                    @click.stop="updateJob(item)"
                  ></div>
                  <div class="s-text" style="color: rgb(164, 207, 48)">
                    Đang thực hiện
                  </div>
                </div>
                <div class="job-name">{{ item.JobName }}</div>
                <div class="job-icon">
                  <div class="icon icon-24 icon-relevant-circle-dash-v2"></div>
                  <div class="icon icon-24 icon-no-date"></div>
                  <div class="date-end">{{ formatDateTime(item.EndTime) }}</div>
                </div>
              </div>
            </div>
          </div>
          <!-- Danh sách việc đang làm -->

          <!-- Danh sách việc giao cho tôi -->
          <div class="h-myjob">
            <div style="position: sticky; top: 0" class="tite-h">
              Danh sách công việc giao cho tôi
            </div>
            <div class="body-h-list scrollbar">
              <div
                class="item-job"
                v-for="(item, index) in listJobAssign"
                :key="index"
              >
                <div class="job-status">
                  <div
                    class="s-item icon icon-24 icon-dahoanthanh"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                        'Đã hoàn thành' ||
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                        'Hoàn thành quá hạn'
                        ? true
                        : false
                    "
                  ></div>
                  <div
                    class="s-item icon icon-24 icon-quahan"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Quá hạn'
                        ? true
                        : false
                    "
                  ></div>
                  <div
                    class="s-item icon icon-24 icon-canthuchien"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Cần thực hiện'
                        ? true
                        : false
                    "
                  ></div>
                  <div
                    class="s-item icon icon-24 icon-dangthuchien"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Đang thực hiện'
                        ? true
                        : false
                    "
                  ></div>
                  <div
                    class="text-complete"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Đã hoàn thành'
                        ? true
                        : false
                    "
                  >
                    {{ onGenTextJobStatus(item.JobStatus, item.EndTime) }}
                  </div>
                  <div
                    class="text-complete-out"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Hoàn thành quá hạn'
                        ? true
                        : false
                    "
                  >
                    {{ onGenTextJobStatus(item.JobStatus, item.EndTime) }}
                  </div>
                  <div
                    class="text-outofdate"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Quá hạn'
                        ? true
                        : false
                    "
                  >
                    {{ onGenTextJobStatus(item.JobStatus, item.EndTime) }}
                  </div>
                  <div
                    class="text-todo"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Cần thực hiện'
                        ? true
                        : false
                    "
                  >
                    {{ onGenTextJobStatus(item.JobStatus, item.EndTime) }}
                  </div>
                  <div
                    class="text-progress"
                    v-if="
                      onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                      'Đang thực hiện'
                        ? true
                        : false
                    "
                  >
                    {{ onGenTextJobStatus(item.JobStatus, item.EndTime) }}
                  </div>
                </div>
                <div class="job-name">{{ item.JobName }}</div>
                <div class="job-icon">
                  <div class="icon icon-24 icon-relevant-circle-dash-v2"></div>
                  <div class="icon icon-24 icon-no-date"></div>
                  <div class="date-end">{{ formatDateTime(item.EndTime) }}</div>
                </div>
              </div>
            </div>
          </div>
          <!-- Danh sách việc giao cho tôi -->
        </div>

        <div class="m-c-info">
          <div class="m-c-hello">
            <span class="hello-text">{{ hello }}</span>
          </div>
          <div class="m-c-datetime">
            <span class="datetime-text">{{ time }}</span>
          </div>
          <div class="m-c-saying">
            <span class="saying-text">{{ saying }}</span>
            <br />
            <br />
            <span class="saying-author">{{ author }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
  <PopupNotification
    v-if="isShowWaring"
    :title="titleWaring"
    :content="contentWaring"
    :typeicon="typeIcon"
    :showbtncancel="false"
    @onClose="isShowWaring = false"
    @onConfirm="onConfirm"
  ></PopupNotification>
  <DepartmentDetail
    v-if="isShowDepartment"
    @onClose="isShowDepartment = !isShowDepartment"
    @onCancel="isShowDepartment = !isShowDepartment"
    @onConfirm="insertDepartment"
  ></DepartmentDetail>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
  <ToastMessage ref="toast"></ToastMessage>
  <ProjectDetail
    v-if="isShowProjectDetail"
    @onClose="isShowProjectDetail = !isShowProjectDetail"
    @onCancel="isShowProjectDetail = !isShowProjectDetail"
    @onConfirm="insertProject"
    :iddeprt="this.idDeprt"
    :namedeprt="nameDeprt"
    :idcompany="idCompany"
  ></ProjectDetail>
  <JobDetail
    @onClose="isShowJobDetail = !isShowJobDetail"
    @onCancel="isShowJobDetail = !isShowJobDetail"
    @onConfirm="insertJob"
    :idproject="idProjectSelected"
    :nameproject="nameProjectSelected"
    :screen="EN"
    v-if="isShowJobDetail"
  ></JobDetail>
  <CompanyDetail
    v-if="isShowCompanyDetail"
    @onConfirm="insertCompany"
    @onClose="isShowCompanyDetail = false"
    @onCancel="isShowCompanyDetail = false"
  ></CompanyDetail>
</template>

<script>
import CompanyDetail from "./../components/form/company-detail.vue";
import PopupNotification from "./../components/popup/popup-notification.vue";
import ListDepartment from "./../components/menu/list-department.vue";
import DepartmentDetail from "./../components/form/department-detail.vue";
import QvcLoading from "./../components/dialog/qvc-loading.vue";
import ToastMessage from "./../components/toast/toast-message.vue";
import ProjectDetail from "./../components/form/project-detail.vue";
import JobDetail from "./../components/form/job-detail.vue";
import { ENUMICON } from "@/enum.js";
import { ENUMPOPUP } from "@/enum.js";
import { ENUMTOAST } from "@/enum.js";
import { ENUMSTATE } from "@/enum.js";
import { ENUMSCREEN, ENUMJOBSTATUS } from "@/enum.js";
import { RESAPI } from "@/res.js";
export default {
  name: "HomeTask",
  components: {
    PopupNotification,
    CompanyDetail,
    ListDepartment,
    DepartmentDetail,
    QvcLoading,
    ToastMessage,
    ProjectDetail,
    JobDetail,
  },
  created() {
    this.fullName = localStorage.getItem("full-name");
    // Kiểm tra xem đã login hay chưa?
    if (!localStorage.getItem("access-token")) {
      // Hiển thị thông báo đăng nhập
      this.contentWaring = [];
      this.contentWaring.push(
        "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại để tiếp tục."
      );
      this.typeIcon = ENUMICON.Waring;
      this.typePopup = ENUMPOPUP.Login;
      this.isShowWaring = true;
    } else {
      // Hiển thị thông tin người dùng
      this.setInfoSession();

      // Kiểm tra quyền để hiển thị chức năng
      this.checkLeve();
      // Kiểm tra là Cá nhân hay công ty
      this.checkState();
      // Hiển thị danh sách phòng ban
      this.getDepartment();
      this.showTime();
      this.getSaying();
      this.onGetTaskProcessing();
      this.onGetTaskOutOfDate();
      this.onGetTaskComplete();
      this.onGetTaskAssign();
    }
  },
  methods: {
    formatDateTime(dateString) {
      const date = new Date(dateString);
      const day = date.getDate().toString().padStart(2, "0");
      const month = (date.getMonth() + 1).toString().padStart(2, "0");
      const year = date.getFullYear().toString();
      const hours = date.getHours().toString().padStart(2, "0");
      const minutes = date.getMinutes().toString().padStart(2, "0");
      if (
        `${day}/${month}/${year}` == "01/01/1970" ||
        `${day}/${month}/${year}` == "01/01/1"
      ) {
        return ``;
      } else {
        return `${day}/${month}/${year} - ${hours}:${minutes}`;
      }
    },
    isDateGreaterThanToday(dateString) {
      const date = new Date(dateString);
      const today = new Date();
      if (!dateString) {
        return false;
      } else {
        return date < today;
      }
    },
    onGenTextJobStatus(jobstatus, endtime) {
      if (this.isDateGreaterThanToday(endtime)) {
        if (jobstatus == ENUMJOBSTATUS.Complete) {
          return "Hoàn thành quá hạn";
        } else {
          return "Quá hạn";
        }
      } else if (jobstatus == ENUMJOBSTATUS.Complete) {
        return "Đã hoàn thành";
      } else if (jobstatus == ENUMJOBSTATUS.Processing) {
        return "Đang thực hiện";
      } else {
        return "Cần thực hiện";
      }
    },
    onGetTaskComplete() {
      //Build dữ liệu
      var data = {
        Id: localStorage.getItem("userid"),
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
        Type: ENUMJOBSTATUS.Complete,
      };

      this.axios
        .post("http://localhost:56428/api/v2/Assign/GetAllMyTask", data)
        .then((res) => {
          if (res.data) {
            this.jobComplete = res.data.length;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onGetTaskAssign() {
      //Build dữ liệu
      var data = {
        Id: localStorage.getItem("userid"),
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
        Type: ENUMJOBSTATUS.Assignment,
      };

      this.axios
        .post("http://localhost:56428/api/v2/Assign/GetAllMyTask", data)
        .then((res) => {
          if (res.data) {
            this.jobAssign = res.data.length;
            this.listJobAssign = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onGetTaskProcessing() {
      //Build dữ liệu
      var data = {
        Id: localStorage.getItem("userid"),
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
        Type: ENUMJOBSTATUS.Processing,
      };

      this.axios
        .post("http://localhost:56428/api/v2/Assign/GetAllMyTask", data)
        .then((res) => {
          if (res.data) {
            this.jobProcessing = res.data.length;
            this.listJobProcessing = res.data;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onGetTaskOutOfDate() {
      //Build dữ liệu
      var data = {
        Id: localStorage.getItem("userid"),
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
        Type: ENUMJOBSTATUS.OutOfDate,
      };

      this.axios
        .post("http://localhost:56428/api/v2/Assign/GetAllMyTask", data)
        .then((res) => {
          if (res.data) {
            this.jobOutOfDate = res.data.length;
          }
        })
        .catch((res) => {
          console.log(res);
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
    checkState() {
      if (
        !localStorage.getItem("domain-company") ||
        localStorage.getItem("domain-company") == "null"
      ) {
        this.state = ENUMSTATE.CaNhan;
        // Dạng cá nhân
        this.textRole.addemp = "Thêm thành viên vào danh mục/dự án";
        this.textRole.depart = "Thêm danh mục công việc";
        this.keyRole.isShowAddEmp = false;
      } else {
        //Dạng công ty
        this.state = ENUMSTATE.CongTy;
        this.textRole.addemp = "Thêm nhanh thành viên vào phòng ban/dự án";
        this.textRole.depart = "Thêm phòng ban";
        this.keyRole.isShowAddEmp = true;
      }
    },
    checkLeve() {
      switch (parseInt(localStorage.getItem("role"))) {
        case 0:
          this.keyRole.isShowAddCompay =
            parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan
              ? true
              : false;
          break;
        case 1:
          this.keyRole.isShowAddCompay = false;
          break;
        case 2:
          this.keyRole.isShowAddProject = false;
          this.keyRole.isShowAddDepart = false;
          this.keyRole.isShowAddEmp = false;
          this.keyRole.isShowAddCompay = false;
          break;
      }
    },
    onShowCompanyDetail() {
      this.isShowAddJob = false;
      this.isShowCompanyDetail = true;
    },
    onLogout() {
      localStorage.clear();
      this.$router.push("/login");
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

    /**Sự kiện khi ấn đồng ý */
    onConfirm() {
      switch (this.typePopup) {
        case ENUMPOPUP.Login:
          this.goToLogin();
          break;
        default:
          break;
      }
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

    /**
     * Thực hiện hiển thị thông tin cá nhân khi vào trang home
     */
    setInfoSession() {
      // Hiển thị lời chào
      this.hello = "Xin chào " + localStorage.getItem("full-name");
    },

    /**
     * Đi tới trang login
     */
    goToLogin() {
      this.isShowWaring = false;
      this.$router.push("/login");
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
     * Hiển thị form thêm phòng ban
     */
    onShowFormDeprt() {
      this.isShowAddJob = false;
      this.isShowDepartment = true;
    },

    /**
     * Thực hiên thêm mới phòng ban
     * @param {*} deprt thông tin phòng ban
     * @param {*} listid danh sách id nhân viên
     */
    insertDepartment(deprt, emps, role) {
      // Bulid dữ liệu
      var data = {
        Data: {
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
            this.isShowDepartment = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu phòng ban đã được cập nhật.",
              ENUMTOAST.Success
            );
            //Lấy lại danh sách phòng ban
            this.getDepartment();
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

    /**
     * Lấy thời gian hiện tại
     * Author: QVCANH (28/12/2022)
     */
    showTime() {
      var date = new Date();
      var h = date.getHours(); // 0 - 23
      var m = date.getMinutes(); // 0 - 59
      var s = date.getSeconds(); // 0 - 59
      var session = "AM";

      if (h == 0) {
        h = 12;
      }

      if (h > 12) {
        h = h - 12;
        session = "PM";
      }

      h = h < 10 ? "0" + h : h;
      m = m < 10 ? "0" + m : m;
      s = s < 10 ? "0" + s : s;

      this.time = h + ":" + m + ":" + s + " " + session;

      // Sau 1 giây sẽ chạy lại hàm showTime
      setTimeout(this.showTime, 1000);
    },

    /**
     * Hiển thị form thêm dự án
     * @param {*} iddeprt id phòng ban
     */
    onShowProject(iddeprt, namedeprt, idcompany) {
      if (!iddeprt) {
        this.isShowAddJob = false;
        this.isShowProjectDetail = true;
      } else {
        this.isShowProjectDetail = true;
      }
      this.idDeprt = iddeprt;
      this.nameDeprt = namedeprt;
      this.idCompany = idcompany;
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
      console.log(listid);
    },

    /**
     * API lấy câu nói hay
     * Author: QVCANH (28/12/2022)
     */
    getSaying() {
      try {
        this.axios
          .get("https://type.fit/api/quotes")
          .then((res) => {
            var data = res.data[Math.floor(Math.random() * res.data.length)];
            this.saying = data.text;
            this.author = data.author;
          })
          .catch((res) => {
            console.log(res);
          });

        // Sau 1 phút sẽ chạy lại hàm getSaying
        setTimeout(this.getSaying, 60000);
      } catch (error) {
        console.log(error);
      }
    },
    onGenClassJobStatus(jobstatus, endtime) {
      if (this.isDateGreaterThanToday(endtime)) {
        if (jobstatus == ENUMJOBSTATUS.Complete) {
          return "text-complete-out";
        } else {
          return "text-outofdate";
        }
      } else if (jobstatus == ENUMJOBSTATUS.Complete) {
        return "text-complete";
      } else if (jobstatus == ENUMJOBSTATUS.Processing) {
        return "";
      } else {
        return "text-todo";
      }
    },
  },
  data() {
    return {
      fullName: "",
      ENUMSCREEN,
      keyRole: {
        isShowAddProject: true,
        isShowAddDepart: true,
        isShowAddCompay: true,
        isShowAddEmp: true,
      },
      textRole: {
        depart: "Thêm danh mục công việc",
        addemp: "Thêm thành viên vào danh mục/dự án",
      },
      idCompany: "",
      isShowCompanyDetail: false,
      /**Id dự án đang chọn */
      idProjectSelected: "",

      /**Tên dự án đang chọn */
      nameProjectSelected: "",

      // Thời gian hiện tại
      time: "",

      // Câu nói hay
      saying: "",

      // Tác giả câu nói
      author: "",

      /**Hiển thị thông báo */
      isShowWaring: false,

      /**Tiêu đề thông báo */
      titleWaring: "Thông báo",

      /**Nội dung thông báo */
      contentWaring: [],

      /**Loại icon thông báo */
      typeIcon: ENUMICON.Info,

      /**Loại thông báo */
      typePopup: ENUMPOPUP.Waring,

      /**Lời chào */
      hello: "",

      /**Hiển thị popup thiết lập chung */
      isShowSetting: false,

      /**Hiển thị popup thông báo */
      isShowNotification: false,

      /**Hiển thị menu lọc thông báo */
      isShowNotifiFilter: false,

      /**Hiển thị menu thiết lập thông báo */
      isShowNotifiSetting: false,

      /**Hiển thị popup tìm kiếm */
      isShowFilter: false,

      /**Hiển thị popup thêm công việc */
      isShowAddJob: false,

      /**Danh sách phòn ban */
      departments: [],

      /**Hiển thị dialog thêm phòng ban */
      isShowDepartment: false,

      /**Hiển thị loading */
      isShowLoading: false,

      /**Hiển thị form thêm dự án*/
      isShowProjectDetail: false,

      /**State lấy lại danh sách dự án */
      isLoadProject: false,

      /**ID phòng ban đang thao tác */
      idDeprt: "",

      /**Tên phòng ban đang tương tác */
      nameDeprt: "",

      /** Hiển thị form thêm công việc*/
      isShowJobDetail: false,
      state: ENUMSTATE.CaNhan,

      /**Dữ liệu enum */
      ENUMICON,
      ENUMPOPUP,
      ENUMTOAST,
      RESAPI,
      ENUMSTATE,
      jobProcessing: 0,
      jobOutOfDate: 0,
      jobComplete: 0,
      jobAssign: 0,
      listJobProcessing: [],
      listJobAssign: [],
    };
  },
};
</script>

<style scoped>
.text-complete {
  color: rgb(53, 190, 69) !important;
  font-weight: 600;
  text-decoration: line-through !important;
}
.text-complete-out {
  color: red !important;
  font-weight: 600;
  text-decoration: line-through !important;
}
.text-outofdate {
  color: red !important;
  font-weight: 600;
}
.text-todo {
  color: rgb(141, 163, 166) !important;
  font-weight: 600;
}
.text-progress {
  font-weight: 600;
  color: rgb(164, 207, 48);
}
.item-job + .item-job {
  margin-top: 12px;
}
.icon-Progress:hover {
  filter: invert(54%) sepia(80%) saturate(378%) hue-rotate(64deg)
    brightness(96%) contrast(94%);
}
.s-text {
  font-weight: 600;
}
.job-name {
  font-size: 15px;
  color: rgba(0, 0, 0, 0.87);
  margin-bottom: 24px;
}
.job-icon {
  display: flex;
  align-items: center;
}
.date-end {
  font-size: 12px;
  font-weight: 600;
  color: red;
}
.icon-relevant-circle-dash-v2 {
  background-image: url(./../assets/img/relevant-circle-dash-v2.svg);
  margin-right: 16px;
}
.icon-no-date {
  background-image: url(./../assets/img/no-date.svg);
  margin-right: 12px;
}
.icon-Progress {
  cursor: pointer;
  background-image: url(./../assets/img/dangthuchien.svg);
}
.s-item {
  margin-right: 10px;
}
.job-status {
  display: flex;
  align-items: center;
  border-bottom: 1px solid #c2c2c2;
  margin-bottom: 12px;
  padding-bottom: 10px;
}
.item-job {
  user-select: none;
  width: calc(100% - 24px);
  height: auto;
  background-color: #fff;
  border-radius: 8px;
  padding: 12px;
}
.text-logout {
  color: #fff;
  font-size: 13px;
  font-weight: 600;
  margin-top: 8px;
  padding-left: 18px;
  cursor: pointer;
  user-select: none;
}
.text-logout:hover {
  color: lightgreen;
}
.icon-canthuchien {
  background-image: url(./../assets/img/complete.svg);
}
.icon-dahoanthanh {
  background-image: url(./../assets/img/done-green.svg);
}
.icon-dangthuchien {
  background-image: url(./../assets/img/dangthuchien.svg);
}
.icon-quahan {
  background-image: url(./../assets/img/quahan.svg);
}
</style>
