<template>
  <div class="project-task-content">
    <div class="mt-content-body">
      <div class="c-body-header">
        <div class="b-header-left">
          <div class="bh-left-job-soon">
            <!-- ? -->
            <div class="popup-combobox popup-sort" v-show="isShowSort">
              <div class="arror arrow-top"></div>
              <div class="p-s-content">
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.EndTimeASC)"
                >
                  <span>Hạn hoàn thành (tăng dần)</span>
                  <div v-if="isShowActiveS0" class="icon icon-cbb"></div>
                </div>
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.EndTimeDESC)"
                >
                  <span>Hạn hoàn thành (giảm dần)</span>
                  <div v-if="isShowActiveS1" class="icon icon-cbb"></div>
                </div>
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.StartTimeASC)"
                >
                  <span>Ngày bắt đầu (tăng dần)</span>
                  <div v-if="isShowActiveS2" class="icon icon-cbb"></div>
                </div>
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.StartTimeDESC)"
                >
                  <span>Ngày bắt đầu (giảm dần)</span>
                  <div v-if="isShowActiveS3" class="icon icon-cbb"></div>
                </div>
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.CreatedDateASC)"
                >
                  <span>Ngày tạo (tăng dần)</span>
                  <div v-if="isShowActiveS4" class="icon icon-cbb"></div>
                </div>
                <div
                  class="item-ccb"
                  @click="onSelectSort(ENUMSORT.CreatedDateDESC)"
                >
                  <span>Ngày tạo (giảm dần)</span>
                  <div v-if="isShowActiveS5" class="icon icon-cbb"></div>
                </div>
              </div>
            </div>
            <!-- ? -->
          </div>
          <div class="bh-left-sort">
            <div class="job-sort sort-icon">
              <div class="icon-sort"></div>
            </div>
            <div class="job-sort sort-text" @click="onShowSort">
              Sắp xếp công việc
            </div>
            <div class="job-sort sort-state" @click="onShowSort">
              {{ textSort }}
            </div>
            <div class="job-sort sort-icon" @click="onShowSort">
              <div class="icon-h-drop drop-gray"></div>
            </div>
          </div>
        </div>
        <div class="b-header-right">
          <button class="btn btn-with-icon btn-light btn-setup" v-if="false">
            <div class="icon__button icon-setup"></div>
            <span>Thiết lập</span>
          </button>
          <div class="popup popup-setup" v-if="false">
            <div class="arror arrow-top arrow-setup"></div>
            <div class="popup-header">
              <div class="p-header-title">Tuỳ chọn dữ liệu</div>
              <div class="icon icon-pos-16 icon-close"></div>
            </div>
            <div class="popup-body body-setup">
              <div class="from-item item-pos">
                <input
                  placeholder="Tìm kiếm"
                  class="input i-search-left"
                  type="text"
                  name=""
                  id=""
                />
                <div class="icon icon-pos-16 i-search-input"></div>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-checked"></div>
                <div class="icon icon-18 icon-hidelist"></div>
                <span>Chọn tất cả</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-depart"></div>
                <div class="icon icon-18 icon-hidelist"></div>
                <span>Cá nhân</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-project"></div>
                <div class="i-group gr-user">
                  <div class="icon icon-16 icon-member"></div>
                </div>
                <span>Công việc cá nhân</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-checked item-cbb-depart"></div>
                <div class="icon icon-18 icon-hidelist"></div>
                <span>Phòng ban 1</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-project"></div>
                <div class="i-group gr-leaf">
                  <div class="icon icon-16 icon-leaf"></div>
                </div>
                <span>Dự án 1</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-project"></div>
                <div class="i-group gr-star">
                  <div class="icon icon-16 icon-star"></div>
                </div>
                <span>Dự án 2</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-project"></div>
                <div class="i-group gr-page">
                  <div class="icon icon-16 icon-page"></div>
                </div>
                <span>Dự án 3</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-check item-cbb-depart"></div>
                <div class="icon icon-18 icon-showlist"></div>
                <span>Phòng ban 2</span>
              </div>
              <div class="item-ccb item-checkbox">
                <div class="icon icon-20 icon-checked item-cbb-depart"></div>
                <div class="icon icon-18 icon-showlist"></div>
                <span>Phòng ban 3</span>
              </div>
            </div>
          </div>
          <button class="btn btn-with-icon btn-light">
            <div class="icon__button icon-export"></div>
            <span>Xuất khẩu</span>
          </button>
          <div class="bh-layout"></div>
          <div
            class="icon icon-24 icon-filter"
            @click="this.isShowFilter = !this.isShowFilter"
          ></div>
          <!-- <div class="popup popup-filter">
            <div class="arror arrow-top arrow-filter"></div>
            <div class="popup-header">
              <div class="p-header-title">Lọc công việc</div>
              <div class="icon icon-pos-16 icon-close"></div>
            </div>
            <div class="popup-body">
              <div class="from-item item-pos">
                <input
                  placeholder="Tìm kiếm công việc"
                  class="input i-search-left"
                  type="text"
                  name=""
                  id=""
                />
                <div class="icon icon-pos-16 i-search-input"></div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title">Hạn hoàn thành</div>
                <input class="input i-cbb-right" type="text" name="" id="" />
                <div class="icon icon-pos-16 icon-dropdown"></div>
                <div class="popup-combobox">
                  <div class="p-s-content">
                    <div class="item-ccb">
                      <span>Không chọn</span>
                      <div class="icon icon-cbb"></div>
                    </div>
                    <div class="item-ccb"><span>Hôm nay</span></div>
                    <div class="item-ccb"><span>Tuần này</span></div>
                    <div class="item-ccb"><span>Tuần trước</span></div>
                    <div class="item-ccb"><span>Tháng này</span></div>
                    <div class="item-ccb"><span>Tháng trước</span></div>
                    <div class="item-ccb"><span>Tuỳ chọn</span></div>
                  </div>
                </div>
              </div>
              <div class="from-item item-group">
                <div class="from-item" style="margin-right: 12px; width: 100%">
                  <input
                    placeholder="Từ ngày"
                    class="input"
                    type="date"
                    name=""
                    id=""
                  />
                </div>
                <div class="from-item" style="width: 100%">
                  <input
                    placeholder="Đến ngày"
                    class="input"
                    type="date"
                    name=""
                    id=""
                  />
                </div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title">Trạng thái hoàn thành</div>
                <input class="input i-cbb-right" type="text" name="" id="" />
                <div class="icon icon-pos-16 icon-dropdown"></div>
                <div class="popup-combobox">
                  <div class="p-s-content">
                    <div class="item-ccb">
                      <span>Tất cả</span>
                      <div class="icon icon-cbb"></div>
                    </div>
                    <div class="item-ccb"><span>Chưa hoàn thành</span></div>
                    <div class="item-ccb"><span>Đã hoàn thành</span></div>
                  </div>
                </div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title">Trạng thái hoạt động</div>
                <input class="input i-cbb-right" type="text" name="" id="" />
                <div class="icon icon-pos-16 icon-dropdown"></div>
                <div class="popup-combobox">
                  <div class="p-s-content">
                    <div class="item-ccb">
                      <span>Tất cả</span>
                      <div class="icon icon-cbb"></div>
                    </div>
                    <div class="item-ccb"><span>Đang hoạt động</span></div>
                    <div class="item-ccb"><span>Đã lưu trữ</span></div>
                  </div>
                </div>
              </div>
              <div class="from-item">
                <div class="f-i-title">Người thực hiện</div>
                <div class="icon icon-pos-32 icon-plus-circle"></div>
              </div>
            </div>
            <div class="popup-footer">
              <button class="btn btn-light">Bỏ lọc</button>
              <button class="btn btn-success">Áp dụng</button>
            </div>
          </div> -->
          <div class="popup popup-filter" v-if="isShowFilter">
            <div class="arror arrow-top arrow-filter"></div>
            <div class="popup-header">
              <div class="p-header-title">Lọc công việc</div>
              <div
                class="icon icon-pos-16 icon-close"
                style="cursor: pointer"
                @click="isShowFilter = !isShowFilter"
              ></div>
            </div>
            <div class="popup-body">
              <div class="from-item item-pos">
                <input
                  placeholder="Tìm kiếm công việc"
                  class="input i-search-left"
                  type="text"
                  v-model="keySearch"
                />
                <div class="icon icon-pos-16 i-search-input"></div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title">Hạn hoàn thành</div>
                <input
                  class="input i-cbb-right"
                  type="text"
                  :option="dateOptionValue.Option"
                  v-model="dateOptionValue.Name"
                  readonly
                  @click="onShowCbbDateOption"
                />
                <div class="icon icon-pos-16 icon-dropdown"></div>
                <div class="popup-combobox" v-show="isShowCbbEndtime">
                  <div class="p-s-content">
                    <div
                      class="item-ccb"
                      :option="item.Option"
                      v-for="(item, index) in dateOption"
                      :key="index"
                    >
                      <span
                        @click.stop="onSelectDateOption($event, item)"
                        style="width: 100%; height: 100%; line-height: 36px"
                        >{{ item.Name }}</span
                      >
                      <div
                        ref="cbbdateoption"
                        class="icon"
                        :class="{
                          'icon-cbb': index == 0 ? true : false,
                        }"
                        :option="item.Option"
                      ></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="from-item item-group">
                <div class="from-item" style="margin-right: 12px; width: 100%">
                  <input
                    placeholder="Từ ngày"
                    class="input"
                    type="date"
                    v-model="dateOptionStart"
                    v-show="isShowDateOther"
                  />
                </div>
                <div class="from-item" style="width: 100%">
                  <input
                    placeholder="Đến ngày"
                    class="input"
                    type="date"
                    v-model="dateOptionEnd"
                    v-show="isShowDateOther"
                  />
                </div>
              </div>
              <div class="from-item item-pos">
                <div class="f-i-title">Trạng thái hoàn thành</div>
                <input
                  class="input i-cbb-right"
                  type="text"
                  name=""
                  id=""
                  @click="this.isShowCbbJobStatus = !this.isShowCbbJobStatus"
                />
                <div class="icon icon-pos-16 icon-dropdown"></div>
                <div class="popup-combobox" v-if="isShowCbbJobStatus">
                  <div class="p-s-content">
                    <div class="item-ccb">
                      <span>Tất cả</span>
                      <div class="icon icon-cbb"></div>
                    </div>
                    <div class="item-ccb"><span>Chưa hoàn thành</span></div>
                    <div class="item-ccb"><span>Đã hoàn thành</span></div>
                  </div>
                </div>
              </div>
              <!-- <div class="from-item item-pos">
                  <div class="f-i-title">Trạng thái hoạt động</div>
                  <input class="input i-cbb-right" type="text" name="" id="" />
                  <div class="icon icon-pos-16 icon-dropdown"></div>
                  <div class="popup-combobox">
                    <div class="p-s-content">
                      <div class="item-ccb">
                        <span>Tất cả</span>
                        <div class="icon icon-cbb"></div>
                      </div>
                      <div class="item-ccb"><span>Đang hoạt động</span></div>
                      <div class="item-ccb"><span>Đã lưu trữ</span></div>
                    </div>
                  </div>
                </div>
                <div class="from-item">
                  <div class="f-i-title">Người thực hiện</div>
                  <div class="icon icon-pos-32 icon-plus-circle"></div>
                </div> -->
            </div>
            <div class="popup-footer">
              <button class="btn btn-light" @click="removeFilter">
                Bỏ lọc
              </button>
              <button class="btn btn-success" @click="onFilter">Áp dụng</button>
            </div>
          </div>
        </div>
      </div>
      <div class="c-body-content scrollbar">
        <table class="tbl">
          <thead>
            <tr>
              <th>Tên công việc</th>
              <th v-if="false">Phòng ban</th>
              <th>Dự án/Nhóm</th>
              <th>Thời điểm bắt đầu</th>
              <th>Hạn hoàn thành</th>
              <th>Tình trạng</th>
              <th v-if="false">Trạng thái hoạt động</th>
              <th v-if="false">Tiến độ</th>
              <th>Thẻ</th>
              <th>Người thực hiện</th>
              <th v-if="false">Người liên quan</th>
              <th>Ngày tạo</th>
            </tr>
          </thead>
          <tbody>
            <tr
              class="job-row"
              v-for="(item, index) in jobs"
              :key="index"
              @dblclick="onDBClickRow(item)"
            >
              <td>
                <div class="td-multiple">
                  <div
                    class="td-m-icon icon icon-24"
                    :class="{
                      iconComplete: item.JobStatus == ENUMJOBSTATUS.Complete,
                      iconProgess: item.JobStatus == ENUMJOBSTATUS.Processing,
                      iconTodo: item.JobStatus == ENUMJOBSTATUS.Todo,
                    }"
                  ></div>
                  <div
                    class="td-m-text"
                    :class="{
                      textdecoration: item.JobStatus == ENUMJOBSTATUS.Complete,
                    }"
                  >
                    {{ item.JobName }}
                  </div>
                </div>
              </td>
              <td v-if="false">Cá nhân</td>
              <td>
                <div class="td-multiple">
                  <div class="td-m-icon icon icon-24 icon-group">
                    <div class="icon icon-16 icon-leaf"></div>
                  </div>
                  <div class="td-m-text">{{ nameproject }}</div>
                </div>
              </td>
              <td>
                <div class="td-multiple">
                  <div class="td-m-icon icon icon-datepicker"></div>
                  <div class="td-m-text">
                    {{ formatDateTime(item.StartTime) }}
                  </div>
                </div>
              </td>
              <td>
                <div class="td-multiple">
                  <div
                    class="td-m-icon icon icon-24 icon-datepicker-yes"
                    :class="{
                      'icon-outofdate':
                        onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                        'Quá hạn'
                          ? true
                          : false,
                    }"
                  ></div>
                  <div
                    class="td-m-text"
                    :class="{
                      'text-outofdate':
                        onGenTextJobStatus(item.JobStatus, item.EndTime) ==
                        'Quá hạn'
                          ? true
                          : false,
                    }"
                  >
                    {{ formatDateTime(item.EndTime) }}
                  </div>
                </div>
              </td>
              <td>
                <div class="td-multiple">
                  <div
                    class="td-m-icon icon icon-10 icon-status"
                    :class="onBindingJobStatus(item.EndTime, item.JobStatus)"
                  ></div>
                  <div class="td-m-text">
                    {{
                      isDateGreaterThanToday(item.EndTime)
                        ? "Quá hạn"
                        : item.JobStatus == ENUMJOBSTATUS.Complete
                        ? "Đã hoàn thành"
                        : item.JobStatus == ENUMJOBSTATUS.Processing
                        ? "Đang thực hiện"
                        : item.JobStatus == ENUMJOBSTATUS.Todo
                        ? "Cần thực hiện"
                        : ""
                    }}
                  </div>
                </div>
              </td>
              <td v-if="false">Đang hoạt động</td>
              <td v-if="false">Gần xong</td>
              <td>
                <div class="td-multiple">
                  <div
                    class="td-m-icon icon icon-16"
                    :class="{
                      important: item.JobTag == ENUMJOBTAG.Important,
                      instant: item.JobTag == ENUMJOBTAG.Urgent,
                    }"
                  ></div>
                  <div class="td-m-text">
                    {{
                      item.JobTag == ENUMJOBTAG.Important
                        ? "Quan trọng"
                        : item.JobTag == ENUMJOBTAG.Urgent
                        ? "Khẩn cấp"
                        : ""
                    }}
                  </div>
                </div>
              </td>
              <td>
                <div class="td-multiple">
                  <div class="td-m-icon icon icon-performer"></div>
                  <div class="td-m-text">{{ item.EmpAssign }}</div>
                </div>
              </td>
              <td v-if="false">
                <div class="td-multiple">
                  <div class="td-m-icon icon icon-jobadd"></div>
                  <div class="td-m-text">Quách Văn Cảnh</div>
                </div>
              </td>
              <td>{{ formatDateTime(item.CreatedDate) }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
  <JobDetail
    :data="job"
    :state="state"
    :mode="ENUMMODE.Edit"
    :nameproject="nameproject"
    @onCancel="isShowJobDetail = false"
    @onClose="isShowJobDetail = false"
    @onConfirm="updateJob"
    @onCloseDetail="onCloseDetail"
    v-if="isShowJobDetail"
  ></JobDetail>
  <ToastMessage ref="toast"></ToastMessage>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>
<script>
import { ENUMSTATE } from "@/enum";
import { ENUMJOBTAG } from "@/enum";
import { ENUMMODE } from "@/enum";
import { ENUMTOAST } from "@/enum";
import { ENUMJOBSTATUS } from "@/enum";
import { ENUMSORT, ENUMDATEOPTION } from "@/enum.js";
import JobDetail from "./../form/job-detail.vue";
import ToastMessage from "./../../components/toast/toast-message.vue";
import QvcLoading from "./../../components/dialog/qvc-loading.vue";
export default {
  name: "ProjectList",
  components: { JobDetail, ToastMessage, QvcLoading },
  emits: [],
  props: ["id", "state", "isload", "nameproject"],
  watch: {
    isload() {
      this.getAllJob(this.id, this.state);
    },
  },
  mounted() {
    this.getAllJob(this.id, this.state);
  },
  created() {},
  methods: {
    removeFilter() {
      this.isShowFilter = false;
      this.keySearch = null;
      this.dateOptionValue.Name = "Không chọn";
      this.dateOptionValue.Option = ENUMDATEOPTION.None;
      this.dateOptionStart = null;
      this.dateOptionEnd = null;
      this.getAllJob(
        this.id,
        this.state,
        this.typeSort,
        this.keySearch,
        this.dateOptionValue.Option,
        this.dateOptionStart,
        this.dateOptionEnd
      );
      // this.onGetAllTask(
      //   this.indexTab,
      //   this.typeJob,
      //   this.typeSort,
      //   this.keySearch,
      //   this.dateOptionValue.Option,
      //   this.dateOptionStart,
      //   this.dateOptionEnd
      // );
    },
    onFilter() {
      this.isShowFilter = false;
      this.getAllJob(
        this.id,
        this.state,
        this.typeSort,
        this.keySearch,
        this.dateOptionValue.Option,
        this.dateOptionStart,
        this.dateOptionEnd
      );
      // this.onGetAllTask(
      //   this.indexTab,
      //   this.typeJob,
      //   this.typeSort,
      //   this.keySearch,
      //   this.dateOptionValue.Option,
      //   this.dateOptionStart,
      //   this.dateOptionEnd
      // );
    },
    onSelectDateOption(e, item) {
      // Xoá toàn bộ active của các item
      var elcbb = this.$refs.cbbdateoption;
      for (const i of elcbb) {
        i.classList.remove("icon-cbb");
        if (parseInt(i.attributes["option"].value) == item.Option) {
          i.classList.add("icon-cbb");
        }
      }

      this.dateOptionValue.Name = item.Name;
      this.dateOptionValue.Option = item.Option;
      this.isShowCbbEndtime = false;

      if (item.Option == ENUMDATEOPTION.Other) {
        this.isShowDateOther = true;
        if (!this.dateOptionStart) {
          this.dateOptionStart = null;
        }
        if (!this.dateOptionEnd) {
          this.dateOptionEnd = null;
        }
      } else {
        this.isShowDateOther = false;
      }
    },
    onShowCbbDateOption() {
      this.isShowCbbEndtime = !this.isShowCbbEndtime;
      var elcbb = this.$refs.cbbdateoption;
      for (const i of elcbb) {
        i.classList.remove("icon-cbb");
        if (
          parseInt(i.attributes["option"].value) == this.dateOptionValue.Option
        ) {
          i.classList.add("icon-cbb");
        }
      }
    },
    onSelectSort(typesort) {
      switch (typesort) {
        case ENUMSORT.EndTimeASC:
          this.isShowActiveS0 = true;
          this.isShowActiveS1 = false;
          this.isShowActiveS2 = false;
          this.isShowActiveS3 = false;
          this.isShowActiveS4 = false;
          this.isShowActiveS5 = false;
          this.textSort = "Hạn hoàn thành (tăng dần)";
          break;
        case ENUMSORT.EndTimeDESC:
          this.isShowActiveS0 = false;
          this.isShowActiveS1 = true;
          this.isShowActiveS2 = false;
          this.isShowActiveS3 = false;
          this.isShowActiveS4 = false;
          this.isShowActiveS5 = false;
          this.textSort = "Hạn hoàn thành (giảm dần)";
          break;
        case ENUMSORT.StartTimeASC:
          this.isShowActiveS0 = false;
          this.isShowActiveS1 = false;
          this.isShowActiveS2 = true;
          this.isShowActiveS3 = false;
          this.isShowActiveS4 = false;
          this.isShowActiveS5 = false;
          this.textSort = "Ngày bắt đầu (tăng dần)";
          break;
        case ENUMSORT.StartTimeDESC:
          this.isShowActiveS0 = false;
          this.isShowActiveS1 = false;
          this.isShowActiveS2 = false;
          this.isShowActiveS3 = true;
          this.isShowActiveS4 = false;
          this.isShowActiveS5 = false;
          this.textSort = "Ngày bắt đầu (giảm dần)";
          break;
        case ENUMSORT.CreatedDateASC:
          this.isShowActiveS0 = false;
          this.isShowActiveS1 = false;
          this.isShowActiveS2 = false;
          this.isShowActiveS3 = false;
          this.isShowActiveS4 = true;
          this.isShowActiveS5 = false;
          this.textSort = "Ngày tạo (tăng dần)";
          break;
        case ENUMSORT.CreatedDateDESC:
          this.isShowActiveS0 = false;
          this.isShowActiveS1 = false;
          this.isShowActiveS2 = false;
          this.isShowActiveS3 = false;
          this.isShowActiveS4 = false;
          this.isShowActiveS5 = true;
          this.textSort = "Ngày tạo (giảm dần)";
          break;

        default:
          break;
      }
      this.isShowSort = false;
      this.typeSort = typesort;
      this.getAllJob(this.id, this.state, typesort);
    },
    onShowSort() {
      this.isShowSort = !this.isShowSort;
    },
    onCloseDetail() {
      this.isShowJobDetail = false;
      this.getAllJob(this.id, this.state);
    },
    updateJob(job, listjobChild) {
      //Dữ liệu
      var data = {
        Data: job,
        DBDomain:
          this.state == ENUMSTATE.CaNhan
            ? localStorage.getItem("domain-db")
            : localStorage.getItem("domain-company"),
      };
      data.Data.ModifiedBy = localStorage.getItem("full-name");
      this.isShowLoading = true;
      this.axios
        .post("http://localhost:56428/api/v2/Job/updateby-id", data)
        .then(() => {
          setTimeout(() => {
            this.isShowLoading = false;
            this.isShowJobDetail = false;
            // Hiển thị toast
            this.$refs.toast.show(
              "Thành công!",
              "Dữ liệu đã được cập nhật.",
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
            this.insertAssign(assigns, data.DBDomain);
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
              this.insertAssign(ass, data.DBDomain);
            }
          }
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
      console.log(listjobChild);
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
    onDBClickRow(data) {
      this.job = data;
      this.job.CompanyID = this.$route.query.companyid;
      this.isShowJobDetail = true;
      console.log(this.job);
    },
    getAllJob(
      id,
      state,
      typesort = ENUMSORT.EndTimeASC,
      keySearch = null,
      dateOption = ENUMDATEOPTION.None,
      dateOptionStart = null,
      dateOptionEnd = null
    ) {
      //Build dữ liệu
      var data = {
        Id: id,
        DBDomain:
          state == ENUMSTATE.CaNhan
            ? localStorage.getItem("domain-db")
            : localStorage.getItem("domain-company"),
        TypeSort: typesort,
        Search: keySearch,
        DateOption: dateOption,
        StartDate: dateOptionStart,
        EndDate: dateOptionEnd,
      };
      this.axios
        .post("http://localhost:56428/api/v2/Job/GetAllJobByIdProject", data)
        .then((res) => {
          this.jobs = res.data;
          console.log(this.jobs);
        })
        .catch((res) => {
          console.log(res);
        });
    },
    // getAllJob(id, state) {
    //   var db =
    //     state == ENUMSTATE.CaNhan
    //       ? localStorage.getItem("domain-db")
    //       : localStorage.getItem("domain-company");
    //   this.axios
    //     .get(
    //       `http://localhost:56428/api/v2/Job/getall-byid?id=${id}&domain=${db}`
    //     )
    //     .then((res) => {
    //       this.jobs = res.data;
    //       console.log(this.jobs);
    //     })
    //     .catch((res) => {
    //       console.log(res);
    //     });
    // },
    onBindingJobStatus(time, status) {
      var clas = "";
      if (this.isDateGreaterThanToday(time)) {
        clas = "outofdate";
      } else {
        if (status == ENUMJOBSTATUS.Processing) {
          clas = "progess";
        }
        if (status == ENUMJOBSTATUS.Todo) {
          clas = "todo";
        }
        if (status == ENUMJOBSTATUS.Complete) {
          clas = "complete";
        }
      }
      return clas;
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
  },
  data() {
    return {
      /**Danh sách công việc*/
      jobs: [],
      ENUMSTATE,
      ENUMJOBSTATUS,
      ENUMJOBTAG,
      isShowJobDetail: false,
      job: {},
      ENUMMODE,
      isShowLoading: false,
      ENUMSORT,
      textSort: "Hạn hoàn thành (tăng dần)",
      isShowActiveS0: true,
      isShowActiveS1: false,
      isShowActiveS2: false,
      isShowActiveS3: false,
      isShowActiveS4: false,
      isShowActiveS5: false,
      isShowSort: false,
      typeSort: 0,
      isShowFilter: false,
      keySearch: "",
      dateOptionValue: {
        Name: "Không chọn",
        Option: ENUMDATEOPTION.None,
      },
      isShowCbbEndtime: false,
      dateOption: [
        {
          Name: "Không chọn",
          Option: ENUMDATEOPTION.None,
        },
        {
          Name: "Hôm nay",
          Option: ENUMDATEOPTION.ToDay,
        },
        {
          Name: "Tuần này",
          Option: ENUMDATEOPTION.ThisWeek,
        },
        {
          Name: "Tuần trước",
          Option: ENUMDATEOPTION.LastWeek,
        },
        {
          Name: "Tháng này",
          Option: ENUMDATEOPTION.ThisMonth,
        },
        {
          Name: "Tháng trước",
          Option: ENUMDATEOPTION.LastMonth,
        },
        {
          Name: "Tuỳ chọn",
          Option: ENUMDATEOPTION.Other,
        },
      ],
      dateOptionStart: null,
      dateOptionEnd: null,
      isShowDateOther: false,
      isShowCbbJobStatus: false,
    };
  },
};
</script>

<style scoped>
.icon-outofdate {
  filter: invert(16%) sepia(93%) saturate(6936%) hue-rotate(360deg)
    brightness(104%) contrast(115%) !important;
}
.text-outofdate {
  color: red;
  font-weight: 700;
}
.popup-sort {
  width: 250px;
  min-width: 200px;
  top: 123px;
  left: 71px;
  z-index: 15;
}
.icon-group {
  display: flex;
  align-items: center;
  justify-content: center;
}
.job-row {
  cursor: pointer;
}
.job-row:hover {
  background-color: #eef8ec;
}

.important {
  background-image: url(./../../assets/img/information-circle-orange.svg);
}
.instant {
  background-image: url(./../../assets/img/lightning-circle-red.svg);
}
.outofdate {
  background-color: red !important;
  border-color: red;
}
.complete {
  background-color: #50b83c !important;
  border-color: #50b83c !important;
}
.todo {
  /* background-color: rgb(141, 163, 166) !important;
  border-color: rgb(141, 163, 166) !important; */
}
.progess {
  background-color: coral !important;
  border-color: coral !important;
}
.textdecoration {
  text-decoration: line-through !important;
}
.iconTodo {
  background-image: url(./../../assets/img/complete.svg);
}
.iconProgess {
  background-image: url(./../../assets/img/dangthuchien.svg);
}
.iconComplete {
  background-image: url(./../../assets/img/done-green.svg);
}
.project-task-content {
  width: calc(100% - 40px);
  height: calc(100vh - 96px);
  padding: 20px;
  display: flex;
}
</style>
