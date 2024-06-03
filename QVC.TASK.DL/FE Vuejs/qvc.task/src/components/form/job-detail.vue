<template>
  <!-- The Modal -->
  <div class="modal">
    <!-- Modal Content -->
    <div class="modal-content">
      <div class="form-header header-line">
        <div class="header-left">
          <!-- Modal header -->
          <div class="title">{{ textRole.title }}</div>
        </div>
        <div class="header-right">
          <div class="icon icon-24 icon-list-job"></div>
          <div class="icon icon-24 icon-attr-file"></div>
          <div class="icon icon-24 icon-tag"></div>
          <div
            class="icon icon-24 icon-bacham"
            v-if="isShowIconBaCham"
            @click="isShowOption = !isShowOption"
          ></div>
          <span class="icon icon-24 close" @click="onClose"></span>
          <div
            class="popup-combobox popup-select-status"
            style="top: 30px"
            v-if="isShowOption"
          >
            <div class="arror arrow-top" style="right: 74px"></div>
            <div class="p-s-content">
              <div class="item-ccb" @click="onShowDeleteJob">
                <span>Xoá công việc</span>
                <div class="icon icon-cbb"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- Phần nội dung form -->
      <div class="form-body">
        <div class="f-body-left">
          <div
            class="f-body-item item-group"
            style="margin-top: 5px; justify-content: space-between"
          >
            <div class="item-left">
              <div
                class="item-project"
                @click="onShowProject"
                :objid="job.ProjectID"
              >
                <div class="icon-project-cbb">
                  <div class="icon icon-16 icon-leaf"></div>
                </div>
                <div class="item-text">{{ job.ProjectName }}</div>
                <div class="icon icon-16 cbb"></div>
              </div>
              <SelectProject
                v-if="isShowSelectProject"
                @onSelectProject="onSelectProject"
              ></SelectProject>

              <div
                class="item-status"
                @click="isShowSelectStatusJob = !isShowSelectStatusJob"
              >
                <div class="item-text">
                  {{
                    job.JobStatus == ENUMJOBSTATUS.Complete
                      ? "Đã hoàn thành"
                      : job.JobStatus == ENUMJOBSTATUS.Processing
                      ? "Đang thực hiện"
                      : "Cần thực hiện"
                  }}
                </div>
                <div class="icon icon-16 cbb"></div>
              </div>
              <SelectStatusJob
                v-if="isShowSelectStatusJob"
                @onSelectJobStatus="onSelectJobStatus"
              ></SelectStatusJob>
            </div>
            <div class="item-right">
              <div
                ref="important"
                id="important"
                class="icon icon-16 icon-important"
                selected="false"
                @click="onSelectTag($event, ENUMJOBTAG.Important)"
              ></div>
              <span
                v-if="isShowJobImportant"
                style="font-size: 12px; color: #fd612c; margin-right: 12px"
                >Quan trọng</span
              >
              <div
                ref="instant"
                class="icon icon-16 icon-instant"
                selected="false"
                @click="onSelectTag($event, ENUMJOBTAG.Urgent)"
              ></div>
              <span
                v-if="isShowJobUrgent"
                style="font-size: 12px; color: #e8384f; margin-right: 12px"
                >Khẩn cấp</span
              >
            </div>
          </div>
          <div class="f-body-item">
            <input
              ref="namejob"
              placeholder="Nhập tên công việc"
              type="text"
              class="input input-job"
              v-model="job.JobName"
            />
          </div>
          <div class="f-body-item" style="margin-top: 5px">
            <div
              class="select-option"
              :class="{ hide: hideSelectPeople }"
              v-if="isShowOptionAssign"
              @click="isShowSelectPopoverAssign = !isShowSelectPopoverAssign"
            >
              <div class="assign-item" v-if="isShowPeopleAssign">
                <div class="icon icon-32 icon-question-circle"></div>
                <div class="select-text">Chọn người thực hiện</div>
              </div>
              <div class="assign-item" v-if="isShowSelectAssign">
                <div class="bgr-user">
                  <div class="icon icon-16 icon-user-v2"></div>
                </div>
                <div>
                  <div class="select-text">Người thực hiện</div>
                  <div
                    class="select-text"
                    style="font-weight: 600; margin-top: 6px"
                  >
                    {{ job.EmployeeName }}
                  </div>
                </div>
              </div>
            </div>
            <SelectPopoverAssign
              v-if="isShowSelectPopoverAssign"
              @onSelectEmployee="onSelectEmployee"
            ></SelectPopoverAssign>
            <div
              class="select-option"
              @click="isShowSelectDueDate = !isShowSelectDueDate"
            >
              <div class="assign-item" v-if="isShowTimeDefault">
                <div class="icon icon-32 icon-calendar-circle"></div>
                <div class="select-text">Chọn hạn hoàn thành</div>
              </div>
              <div class="assign-item" v-if="isShowTimeSetup">
                <div class="bgr-user" style="background-color: #96a7b5">
                  <div class="icon icon-32 icon-calendar-small-white"></div>
                </div>
                <div>
                  <div
                    class="select-text"
                    style="font-weight: 600; margin-bottom: 6px"
                  >
                    Từ:&emsp;<span style="font-weight: normal">{{
                      timeJob.StartDate + "&ensp;" + timeJob.StartTime
                    }}</span>
                  </div>
                  <div class="select-text" style="font-weight: 600">
                    Đến:&emsp;<span style="font-weight: normal">{{
                      timeJob.EndDate + "&ensp;" + timeJob.EndTime
                    }}</span>
                  </div>
                </div>
              </div>
            </div>
            <SelectDueDate
              v-show="isShowSelectDueDate"
              @onConfirm="onSelectTimeJob"
            ></SelectDueDate>
          </div>
          <div
            class="f-body-item item-plus"
            v-if="isShowBtnAddDescription"
            @click="onShowAddDescription"
            style="height: 40px; margin-top: 5px"
          >
            <div class="icon icon-24 icon-add-description"></div>
            <div class="option-plus-text">Nhập mô tả</div>
          </div>

          <div
            class="f-body-item"
            v-if="isShowAddDescription"
            style="display: flex; flex-direction: column; height: 80px"
          >
            <div class="title-description">
              <div class="icon icon-24 icon-description-v2"></div>
              <span style="font-size: 17px; font-weight: 600">Mô tả:</span>
            </div>
            <textarea
              class="input"
              placeholder="Nhập mô tả"
              @blur="onBlurDescription"
              v-model="job.Description"
              ref="description"
            ></textarea>
          </div>

          <div
            class="f-body-item item-plus"
            v-if="isShowBtnJobChild"
            @click="onShowJobChild"
            style="height: 40px"
          >
            <div class="icon icon-24 icon-plus-blue"></div>
            <div class="option-plus-text">Thêm công việc con</div>
          </div>
          <div
            class="f-body-item"
            style="flex-direction: column; height: auto"
            v-if="isShowJobChild"
          >
            <div class="title-description">
              <div class="icon icon-24 icon-sub-task-v2"></div>
              <span style="font-size: 17px; font-weight: 600"
                >Công việc con:</span
              >
            </div>
            <div
              class="job-child"
              v-for="(item, index) in countJobChild"
              :key="index"
            >
              <input
                ref="jobchild"
                type="text"
                class="input input-job-child"
                @blur="getListJobChild"
              />
              <div class="icon icon-24 icon-relevant-circle-dash"></div>
              <div class="icon icon-24 icon-calendar-circle-dash"></div>
            </div>
            <div
              class="f-body-item item-plus"
              style="height: 40px"
              @click="addJobChild"
            >
              <div class="icon icon-24 icon-plus-blue"></div>
              <div class="option-plus-text">Thêm việc</div>
            </div>
          </div>
        </div>
        <div class="f-body-right" v-if="isShowMenu">
          <div class="item-option">
            <div class="icon icon-24 icon-approval"></div>
            <div class="option-text">Phê duyệt</div>
          </div>
          <div class="item-option">
            <div class="icon icon-24 icon-repeat"></div>
            <div class="option-text">Lặp lại</div>
          </div>
          <div class="item-option">
            <div class="icon icon-24 icon-notify"></div>
            <div class="option-text">Nhắc việc</div>
          </div>
          <div class="item-option">
            <div class="icon icon-24 icon-relevant"></div>
            <div class="option-text">Người liên quan</div>
          </div>
        </div>
      </div>
      <!-- Phần footer của form -->
      <div class="modal-foodter">
        <button class="btn btn-light" @click="onCancel">Huỷ bỏ</button>
        <button class="btn btn-success" @click="onConfirm">Lưu</button>
      </div>
    </div>
  </div>
  <PopupNotification
    :title="titleWarning"
    :content="contentWaring"
    :typeicon="typeIcon"
    :showbtncancel="true"
    @onClose="isShowWarning = false"
    @onConfirm="onDeleteJob"
    @onCancel="isShowWarning = false"
    v-if="isShowWarning"
  ></PopupNotification>
  <ToastMessage ref="toast"></ToastMessage>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>

<script>
import SelectProject from "./../popup/select-project.vue";
import SelectStatusJob from "./../popup/select-status-job.vue";
import SelectPopoverAssign from "./../popup/select-popover-assign.vue";
import SelectDueDate from "./../popup/select-due-date.vue";
import { ENUMJOBSTATUS } from "@/enum.js";
import { ENUMJOBTAG } from "@/enum.js";
import { ENUMMODE } from "@/enum.js";
import { ENUMSTATE } from "@/enum.js";
const { v4: uuidv4 } = require("uuid");
import { ENUMICON } from "@/enum.js";
import { ENUMTOAST } from "@/enum.js";
import { ENUMSCREEN } from "@/enum.js";
import QvcLoading from "./../dialog/qvc-loading.vue";
import PopupNotification from "./../popup/popup-notification.vue";
import ToastMessage from "./../toast/toast-message.vue";
export default {
  name: "JobDetail",
  props: [
    "idcompany",
    "iddepart",
    "idproject",
    "nameproject",
    "data",
    "state",
    "mode",
    "screen",
  ],
  emits: ["onClose", "onCancel", "onConfirm", "onCloseDetail"],
  components: {
    SelectProject,
    SelectStatusJob,
    SelectPopoverAssign,
    SelectDueDate,
    PopupNotification,
    ToastMessage,
    QvcLoading,
  },
  watch: {
    countJobChild() {
      if (this.countJobChild <= 0) {
        this.isShowJobChild = false;
        this.isShowBtnJobChild = true;
      }
    },
    "job.ProjectID": function () {
      if (!this.job.CompanyID) {
        this.isShowOptionAssign = false;
        this.job.EmployeeID = null;
        this.job.EmployeeName = null;
      } else {
        this.isShowOptionAssign = true;
      }
    },
  },
  mounted() {
    if (this.mode == ENUMMODE.Edit) {
      if (this.job.JobTag == ENUMJOBTAG.Important) {
        this.isShowJobImportant = true;
        var el = this.$refs.important;
        el.attributes["selected"].value = "true";
        el.classList.remove("icon-important");
        el.classList.add("icon-information-circle-orange");
      } else if (this.job.JobTag == ENUMJOBTAG.Urgent) {
        this.isShowJobUrgent = true;
        var ele = this.$refs.instant;
        ele.attributes["selected"].value = "true";
        ele.classList.remove("icon-instant");
        ele.classList.add("icon-lightning-circle-red");
      }
    }
  },
  created() {
    // Thiết lập khi ở màn hình phòng ban
    if (this.screen == ENUMSCREEN.Department) {
      if (!this.idcompany) {
        this.getProjectByIdDepartment(
          this.iddepart,
          localStorage.getItem("domain-db")
        );
      } else {
        this.job.CompanyID = this.idcompany;
        this.getProjectByIdDepartment(
          this.iddepart,
          localStorage.getItem("domain-company")
        );
      }
    } else if (this.screen == ENUMSCREEN.Project) {
      this.job.CompanyID = this.idcompany;
      this.job.ProjectID = this.idproject;
      this.job.ProjectName = this.nameproject;
    } else if (this.screen == ENUMSCREEN.Home) {
      if (!this.idproject) {
        // Thiết lập dự án thêm công việc mặc định
        this.getAllProject();
      }
    }

    if (parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan) {
      this.isShowMenu = false;
    } else {
      this.isShowMenu = true;
    }
    if (this.mode == ENUMMODE.Edit) {
      this.textRole.title = "Chỉnh sửa công việc";
      this.job = this.data;
      this.job.ProjectName = this.nameproject;
      this.isShowBtnJobChild = false;
      this.isShowIconBaCham = true;
      // Binding tag công việc
      if (!this.job.StartTime && !this.job.EndDate) {
        this.isShowTimeDefault = true;
      } else {
        this.isShowTimeDefault = false;
        this.isShowTimeSetup = true;
        this.onBindingDateTime(this.job.StartTime, this.job.EndTime);
        if (this.job.Description) {
          this.isShowAddDescription = true;
        }
      }
      // Binding người thực hiện
      if (!this.job.EmpAssign) {
        this.isShowPeopleAssign = true;
        this.isShowSelectAssign = false;
      } else {
        this.isShowPeopleAssign = false;
        this.isShowSelectAssign = true;
        this.job.EmployeeName = this.job.EmpAssign;
        this.hideSelectPeople = true;
      }
    } else {
      this.textRole.title = "Thêm mới công việc";
    }
    this.$nextTick(() => this.$refs.namejob.focus());
  },
  methods: {
    getProjectByIdDepartment(id, db) {
      this.axios
        .get(
          `http://localhost:56428/api/v2/Project/getall-byid?id=${id}&domain=${db}
`
        )
        .then((res) => {
          if (res.data) {
            this.job.ProjectID = res.data[0].ProjectID;
            this.job.ProjectName = res.data[0].ProjectName;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    onDeleteJob() {
      this.isShowWarning = false;
      var db = "";
      if (this.state == ENUMSTATE.CaNhan) {
        db = localStorage.getItem("domain-db");
      } else {
        db = localStorage.getItem("domain-company");
      }
      this.isShowLoading = true;
      this.axios
        .delete(
          `http://localhost:56428/api/v2/Job/deleteby-id?id=${this.job.JobID}&db=${db}`
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
            this.$emit("onCloseDetail");
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

    onShowDeleteJob() {
      // Hiển thị thông báo đăng nhập
      this.contentWaring = [];

      this.contentWaring.push(
        `Bạn có muốn xoá công việc ${this.job.JobName} không?`
      );
      this.titleWarning = "Thông báo";
      this.typeIcon = ENUMICON.Waring;
      this.isShowWarning = true;
      this.isShowOption = false;
    },
    /**
     * Thực hiện lấy danh sách công việc con
     */
    getListJobChild() {
      var jobchilds = this.$refs.jobchild;
      this.jobChilds = [];
      for (const item of jobchilds) {
        if (item.value) {
          var job = {
            JobName: item.value,
            ProjectID: this.job.ProjectID,
            JobStatus: ENUMJOBSTATUS.Todo,
          };
          this.jobChilds.push(job);
        }
      }
    },

    getAllProject() {
      var data = {
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
      };
      this.axios
        .post("http://localhost:56428/api/v2/Project/getall-project", data)
        .then((res) => {
          if (res.data) {
            const filterArr = res.data.filter(
              (item) => item.ProjectCode === "CONGVIECCANHAN"
            );
            this.job.ProjectID = filterArr[0].ProjectID;
            this.job.ProjectName = filterArr[0].ProjectName;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * Thực hiện chọn tag công việc
     * @param {*} e phần tử đang tương tác
     * @param {*} tag tag công việc
     */
    onSelectTag(e, tag) {
      this.isShowJobImportant = false;
      this.isShowJobUrgent = false;
      switch (tag) {
        case ENUMJOBTAG.Important:
          // Kiểm tra xem có đang chọn không
          var sel = e.target.attributes["selected"].value;
          if (sel == "false") {
            // Chọn
            e.target.classList.remove("icon-important");
            e.target.classList.add("icon-information-circle-orange");
            e.target.attributes["selected"].value = "true";
            this.isShowJobImportant = true;
            //Cập nhật dữ liệu
            this.job.JobTag = ENUMJOBTAG.Important;
          } else {
            // Bỏ chọn
            e.target.classList.remove("icon-information-circle-orange");
            e.target.classList.add("icon-important");
            e.target.attributes["selected"].value = "false";
            this.isShowJobImportant = false;
            //Cập nhật dữ liệu
            this.job.JobTag = null;
          }
          this.$refs.instant.classList.remove("icon-lightning-circle-red");
          this.$refs.instant.classList.add("icon-instant");
          this.$refs.instant.attributes["selected"].value = "false";
          break;
        case ENUMJOBTAG.Urgent:
          // Kiểm tra xem có đang chọn không
          var sel2 = e.target.attributes["selected"].value;
          if (sel2 == "false") {
            // Chọn
            e.target.classList.remove("icon-instant");
            e.target.classList.add("icon-lightning-circle-red");
            e.target.attributes["selected"].value = "true";
            this.isShowJobUrgent = true;
            //Cập nhật dữ liệu
            this.job.JobTag = ENUMJOBTAG.Urgent;
          } else {
            // Bỏ chọn
            e.target.classList.remove("icon-lightning-circle-red");
            e.target.classList.add("icon-instant");
            e.target.attributes["selected"].value = "false";
            this.isShowJobUrgent = false;
            //Cập nhật dữ liệu
            this.job.JobTag = null;
          }
          this.$refs.important.classList.remove(
            "icon-information-circle-orange"
          );
          this.$refs.important.classList.add("icon-important");
          this.$refs.important.attributes["selected"].value = "false";
          break;
      }
    },
    removeJobChild(e) {
      console.log(e);
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
     * Sự kiện khi ấn vào thêm việc
     */
    addJobChild() {
      this.countJobChild++;
    },

    /**
     * Thực hiện chọn thời gian công việc
     * @param {*} data dũ liệu thời gian công việc
     */
    onSelectTimeJob(data) {
      // Đóng popup
      this.isShowSelectDueDate = false;
      if (
        (data.StartDate && data.StartTime) ||
        (data.EndDate && data.EndTime)
      ) {
        // Gán lại dữ liệu
        this.timeJob = data;
        if (!this.timeJob.StartDate) {
          this.timeJob.StartDate = "";
        }
        if (!this.timeJob.StartTime) {
          this.timeJob.StartTime = "";
        }
        if (!this.timeJob.EndDate) {
          this.timeJob.EndDate = "";
        }
        if (!this.timeJob.EndTime) {
          this.timeJob.EndTime = "";
        }
        this.isShowTimeDefault = false;
        this.isShowTimeSetup = true;
      }
      // Ghép dữ liệu ngày và giờ
      if (this.timeJob.StartDate && this.timeJob.StartTime) {
        this.job.StartTime = new Date(
          this.timeJob.StartDate + "T" + this.timeJob.StartTime + ":00"
        );
      }
      if (this.timeJob.EndDate && this.timeJob.EndTime) {
        this.job.EndTime = new Date(
          this.timeJob.EndDate + "T" + this.timeJob.EndTime + ":00"
        );
      }
    },

    onBindingDateTime(start, end) {
      const startobj = new Date(start);

      const year = startobj.getFullYear();
      const month = startobj.getMonth() + 1;
      const day = startobj.getDate();

      const hours = startobj.getHours();
      const minutes = startobj.getMinutes();
      if (
        `${day}/${month}/${year}` == "1/1/1" ||
        `${day}/${month}/${year}` == "1/1/1970"
      ) {
        this.timeJob.StartDate = ``;
        this.timeJob.StartTime = ``;
      } else {
        // Ghép dữ liệu ngày và giờ
        this.timeJob.StartDate = `${day}/${month}/${year}`;
        this.timeJob.StartTime = `${hours}:${minutes}`;
      }

      const endobj = new Date(end);
      const eyear = endobj.getFullYear();
      const emonth = endobj.getMonth() + 1;
      const eday = endobj.getDate();

      const ehours = endobj.getHours();
      const eminutes = endobj.getMinutes();

      if (
        `${eday}/${emonth}/${eyear}` == "1/1/1" ||
        `${eday}/${emonth}/${eyear}` == "1/1/1970"
      ) {
        this.timeJob.EndDate = ``;
        this.timeJob.EndTime = ``;
      } else {
        // Ghép dữ liệu ngày và giờ
        this.timeJob.EndDate = `${eday}/${emonth}/${eyear}`;
        this.timeJob.EndTime = `${ehours}:${eminutes}`;
      }
    },

    /**
     * Thực hiện sự kiện khi chọn người thực hiện công việc
     * @param {*} data thông tin người dùng
     */
    onSelectEmployee(data) {
      // Đóng popup
      this.isShowSelectPopoverAssign = false;
      if (!this.job.EmployeeID && !this.job.EmployeeName) {
        // Gán lại dữ liệu
        this.job.EmployeeID = data.EmployeeID;
        this.job.EmployeeName = data.EmployeeName;
        this.isShowPeopleAssign = false;
        this.isShowSelectAssign = true;
      } else {
        // Gán lại dữ liệu
        this.job.EmployeeID = data.EmployeeID;
        this.job.EmployeeName = data.EmployeeName;
        this.isShowPeopleAssign = false;
        this.isShowSelectAssign = true;
      }
    },

    /**
     * Thực hiện sự kiện khi chọn trạng thái công việc
     * @param {*} type Loại status
     */
    onSelectJobStatus(type) {
      // Đóng popup
      this.isShowSelectStatusJob = false;
      // Gán lại dữ liệu
      this.job.JobStatus = type;
    },

    /**
     * Thực hiện sự kiện khi chọn dự án
     * @param {*} data dữ liệu dự án
     */
    onSelectProject(data, idCompany) {
      // Ẩn popup
      this.isShowSelectProject = false;
      this.job.ProjectID = data.ProjectID;
      this.job.ProjectName = data.ProjectName;
      this.job.DepartmentID = data.DepartmentID;
      this.job.CompanyID = idCompany;
    },

    /**
     * Thực hiện sự kiện khi click vào chọn dự án
     */
    onShowProject() {
      // Thực hiện gọi API lấy phòng ban
      this.isShowSelectProject = !this.isShowSelectProject;
    },

    /**
     * Xử lý khi ấn vào thêm công việc con
     */
    onShowJobChild() {
      if (!this.isShowJobChild) {
        this.isShowBtnJobChild = false;
        this.isShowJobChild = true;
        // Tăng dòng việc con
        this.countJobChild++;
      }
    },

    /**
     * Xử lý khi click vào nhập mô tả
     */
    onShowAddDescription() {
      if (!this.isShowAddDescription) {
        this.isShowBtnAddDescription = false;
        this.isShowAddDescription = true;
        this.$nextTick(() => this.$refs.description.focus());
      }
    },

    /**
     * Xử lý khi blue khỏi ô mô tả
     */
    onBlurDescription() {
      if (!this.job.Description) {
        this.isShowAddDescription = false;
        this.isShowBtnAddDescription = true;
      }
    },

    /**
     * Sự kiện ẩn popup
     */
    onClose() {
      this.$emit("onClose");
    },

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
      if (this.mode == ENUMMODE.Edit) {
        // Gán lại id công việc cha
        for (const job of this.jobChilds) {
          job.ParentID = this.job.JobID;
          job.JobCode = this.generateCodeDeprt("JB");
        }
      } else {
        // Tại id công việc
        this.job.JobID = uuidv4();
        // Tạo mã công việc
        this.job.JobCode = this.generateCodeDeprt("JB");
        // Gán lại id công việc cha
        for (const job of this.jobChilds) {
          job.ParentID = this.job.JobID;
          job.JobCode = this.generateCodeDeprt("JB");
        }
      }
      this.$emit("onConfirm", this.job, this.jobChilds);
    },
  },
  data() {
    return {
      isShowLoading: false,
      isShowOption: false,
      contentWaring: [],
      textRole: {},
      keyRole: {},
      /**Hiển thị thời gian mặc đinh */
      isShowTimeDefault: true,

      /**Hiển thị thời gian đã chọn */
      isShowTimeSetup: false,

      /**Thông tin thời gian công việc */
      timeJob: {},

      /**Thông tin công việc */
      job: {
        ProjectID: "00000000-0000-0000-0000-000000000000",
        ProjectName: "Công việc cá nhân",
        JobTag: null,
        JobStatus: ENUMJOBSTATUS.Todo,
      },

      /**Hiển thị popup chọn dự án */
      isShowSelectProject: false,

      /** Hiển thị popup chọn nhóm công việc */
      isShowSelectStatusJob: false,

      /**Hiển thị popup chọn người thực hiện */
      isShowSelectPopoverAssign: false,

      /**Hiển thị popup chọn hạn hoàn thành */
      isShowSelectDueDate: false,

      /**Hiển thị nút nhập mô tả */
      isShowBtnAddDescription: true,

      /**Hiển thị ô nhập mô tả */
      isShowAddDescription: false,

      isShowOptionAssign: true,
      /**Hiển thị nút thêm công việc con */
      isShowBtnJobChild: true,

      /**Hiển thị công việc con */
      isShowJobChild: false,

      /**Hiển thị mặc định chọn người thực hiện */
      isShowPeopleAssign: true,

      /**Hiển thị thông tin người chọn thực hiện */
      isShowSelectAssign: false,

      /**Số lượng công việc con */
      countJobChild: 0,

      /**Danh sách công việc con */
      jobChilds: [],

      /**Hiển thị tag quan trọng */
      isShowJobImportant: false,

      /**Hiển thị tag khẩn cấp */
      isShowJobUrgent: false,
      isShowMenu: false,

      /**Dữ liệu enum */
      ENUMJOBSTATUS,
      ENUMJOBTAG,
      ENUMMODE,
      ENUMSTATE,
      isShowIconBaCham: false,
      isShowWarning: false,
      typeIcon: ENUMICON.Waring,
      ENUMSCREEN,
      hideSelectPeople: false,
    };
  },
};
</script>
<style scoped>
.icon-bacham {
  background-image: url(./../../assets/img/bacham.svg);
}
.assign-item {
  display: flex;
  align-items: center;
}
.bgr-user {
  width: 32px;
  height: 32px;
  background-color: rgb(255, 145, 248);
  border-radius: 50px;
  margin-right: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-left: 12px;
}

.icon-user-v2 {
  background-image: url(./../../assets/img/user-v2.svg);
  filter: brightness(0) invert(1);
}
/* The Modal (background) */
.modal {
  position: fixed; /* Stay in place */
  z-index: 10; /* Sit on top */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0, 0, 0); /* Fallback color */
  background-color: rgba(0, 0, 0, 0.4); /* Black w/ opacity */
}

/* Modal Content */
.modal-content {
  position: absolute;
  left: 50%; /* Change from left: 0; */
  top: 50%; /* Change from top: 0; */
  transform: translate(
    -50%,
    -50%
  ); /* Center the popup vertically and horizontally */
  background-color: #fefefe;
  margin: auto;
  padding: 32px 20px 20px 20px;
  border: 1px solid #888;
  min-width: 850px;
  width: auto;
  border-radius: 8px;
}
.option-plus-text {
  color: #2196f3;
  font-size: 15px;
}

.job-child {
  display: flex;
  width: calc(100% - 20px);
  position: relative;
}

.icon-calendar-small-white {
  background-image: url(./../../assets/img/calendar-small-white.svg);
}

.icon-calendar-circle-dash {
  background-image: url(./../../assets/img/calendar-circle-dash.svg);
  position: absolute;
  right: 0px;
  top: 11px;
  cursor: pointer;
}

.icon-relevant-circle-dash {
  background-image: url(./../../assets/img/relevant-circle-dash.svg);
  position: absolute;
  right: 32px;
  top: 11px;
  cursor: pointer;
}

.hide {
  opacity: 50%;
  pointer-events: none;
  cursor: no-drop !important;
}

textarea {
  height: calc(100% - 18px);
  width: calc(100% - 32px);
}

textarea::placeholder {
  font-style: italic;
}

.item-plus {
  display: flex;
  align-items: center;
  cursor: pointer;
  user-select: none;
}

.item-text {
  font-size: 15px;
  color: #1f1f1f;
  font-weight: 400;
}

.title-description {
  display: flex;
  align-items: center;
  margin-bottom: 5px;
  margin-top: 5px;
}

.input-job-child {
  border-radius: 0px;
  border-left-color: transparent;
  border-right-color: transparent;
  height: 29px;
}

.icon-description-v2 {
  background-image: url(./../../assets/img/description-v2.svg);
  margin-right: 12px;
}
.icon-approval {
  background-image: url(./../../assets/img/Approval.svg);
  margin-left: 12px;
}

.icon-repeat {
  background-image: url(./../../assets/img/Repeat.svg);
  margin-left: 12px;
}

.icon-notify {
  background-image: url(./../../assets/img/notify.svg);
  margin-left: 12px;
}

.icon-relevant {
  background-image: url(./../../assets/img/relevant.svg);
  margin-left: 12px;
}

.form-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.title {
  font-size: 24px;
  font-weight: 600;
}

.cbb {
  background-image: url(./../../assets/img/cbb.svg);
  margin-left: 10px;
}

.item-left {
  display: flex;
  position: relative;
}

.item-option {
  width: 100%;
  height: 50px;
  background: linear-gradient(to left, #ffffff, #f1f1f1);
  /* background-color: #f1f1f1; */
  border-bottom: 1px solid #e0e0e0;
  border-bottom: 1px solid #e0e0e0;
  display: flex;
  align-items: center;
  cursor: pointer;
}

.input-job {
  width: 500px;
  height: 40px;
  font-size: 20px;
  font-weight: 700;
  border: unset;
}
.input-job::placeholder {
  color: #a8a8a8;
  font-size: 20px;
  font-weight: 700;
}

.form-body {
  border-top: 1px solid #e0e0e0;
  border-bottom: 1px solid #e0e0e0;
  margin-top: 32px;
  margin-bottom: 20px;
  display: flex;
  height: auto;
}

.icon-project-cbb {
  width: 32px;
  height: 32px;
  border-radius: 50px;
  background-color: rgb(255, 145, 248);
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 8px;
}

.f-body-left {
  flex: 3;
  border-right: 1px solid #e0e0e0;
}

.f-body-right {
  flex: 1;
}

.option-text {
  margin-left: 12px;
  color: #1f1f1f;
  font-size: 15px;
}

.modal-foodter {
  display: flex;
  justify-content: flex-end;
}

.select-option {
  width: 280px;
  height: 100%;
  background-color: #f5f5f5;
  border-radius: 8px;
  display: flex;
  align-items: center;
  cursor: pointer;
  user-select: none;
}
.select-option:first-child {
  margin-right: 20px;
}

.item-right {
  display: flex;
  align-items: center;
}

/* The Close Button */
.close {
  float: right;
  background-image: url(./../../assets/img/Close.svg);
}

.icon-sub-task-v2 {
  background-image: url(./../../assets/img/sub-task-v2.svg);
  margin-right: 8px;
}

.icon-plus-blue {
  background-image: url(./../../assets/img/plus-blue.svg);
  margin-right: 8px;
}

.icon-add-description {
  background-image: url(./../../assets/img/add-description.svg);
  margin-right: 8px;
}

.icon-calendar-circle {
  margin-left: 12px;
  background-image: url(./../../assets/img/calendar-circle.svg);
  margin-right: 12px;
}

.icon-question-circle {
  margin-left: 12px;
  background-image: url(./../../assets/img/question-circle.svg);
  margin-right: 12px;
}
.icon-important {
  background-image: url(./../../assets/img/important.svg);
  cursor: pointer;
}

.icon-information-circle-orange {
  background-image: url(./../../assets/img/information-circle-orange.svg);
  cursor: pointer;
}

.icon-lightning-circle-red {
  background-image: url(./../../assets/img/lightning-circle-red.svg);
  cursor: pointer;
}
.icon-instant {
  background-image: url(./../../assets/img/instant.svg);
  cursor: pointer;
  margin-right: 12px;
}

.icon + .icon {
  margin-left: 16px;
}

.item-project,
.item-status {
  display: flex;
  align-items: center;
  cursor: pointer;
  position: relative;
  user-select: none;
}

.item-status {
  margin-left: 24px;
}

.close:hover,
.close:focus,
.icon-list-job:hover,
.icon-list-job:focus,
.icon-attr-file:hover,
.icon-attr-file:focus,
.icon-tag:hover,
.icon-tag:focus,
.icon-bacham:hover,
.icon-bacham:focus {
  filter: invert(57%) sepia(50%) saturate(596%) hue-rotate(64deg)
    brightness(97%) contrast(91%);
  text-decoration: none;
  cursor: pointer;
}

.f-body-item {
  width: 100%;
  height: 53px;
  display: flex;
  position: relative;
}

.icon-list-job {
  background-image: url(./../../assets/img/list-job.svg);
}

.icon-attr-file {
  background-image: url(./../../assets/img/attr-file.svg);
}

.icon-tag {
  background-image: url(./../../assets/img/tag.svg);
}

.header-right {
  display: flex;
  align-items: center;
  position: relative;
}
</style>
