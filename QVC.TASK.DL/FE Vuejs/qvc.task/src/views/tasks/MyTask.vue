<template>
  <div class="container my-task">
    <div class="my-task-header">
      <div class="mt-header-left">
        <div class="h-left-icon">
          <div class="icon icon-24 icon-toggle"></div>
          <router-link to="/" class="icon icon-24 icon-home"></router-link>
        </div>
        <div class="mt-layout"></div>
        <div class="h-left-tab">
          <div
            ref="tabitem"
            class="tab-item"
            :class="{ 'tab-active': index == 0 ? true : false }"
            v-for="(item, index) in tabItems"
            :key="index"
            @click="onSelectedTab(index, item.type, ENUMSORT.EndTimeASC)"
          >
            <div class="tab-item-text">{{ item.text }}</div>
            <div class="tab-item-quantity icon icon-25">
              <span>?</span>
            </div>
          </div>
          <div
            ref="tabassign"
            class="tab-item"
            v-if="isShowJobAssign"
            @click="
              onSelectedTab(3, ENUMJOBSTATUS.Assignment, ENUMSORT.EndTimeASC)
            "
          >
            <div class="tab-item-text">Việc giao cho tôi</div>
            <div class="tab-item-quantity icon icon-25">
              <span>?</span>
            </div>
          </div>

          <div class="btn-header btn-other" v-show="false">
            <span class="btn-h-text" style="color: #000">Khác</span>
            <div class="btn-add-plus">
              <div class="icon-h-drop drop-black"></div>
            </div>
          </div>
          <div class="popup-combobox cbb-other" v-if="false">
            <div class="arror arrow-top"></div>
            <div class="p-s-content">
              <div class="item-ccb">
                <span>Việc chờ duyệt</span>
                <div class="icon icon-cbb"></div>
              </div>
              <div class="item-ccb"><span>Việc tôi quản lý</span></div>
            </div>
          </div>
        </div>
      </div>
      <div class="mt-header-right">
        <div class="btn-header btn-layout">
          <div class="icon__btn-header icon-add"></div>
          <span class="btn-h-text">Thêm công việc</span>
          <div class="btn-h-layout"></div>
          <div class="btn-add-plus">
            <div class="icon-h-drop"></div>
          </div>
        </div>
      </div>
    </div>
    <div class="my-task-content">
      <div class="mt-content-body">
        <div class="c-body-header">
          <div class="b-header-left">
            <div class="bh-left-job-soon">
              <!-- <div class="job-soon job-text">Công việc sắp đến hạn</div>
              <div class="job-soon job-state" @click="isShowSortByDate = !isShowSortByDate">
                7 ngày gần nhất
              </div>
              <div class="job-soon job-icon" @click="isShowSortByDate = !isShowSortByDate">
                <div class="icon-h-drop drop-gray"></div>
              </div> -->
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
              <SortByDate v-if="isShowSortByDate"></SortByDate>
            </div>
          </div>
          <div class="b-header-right" style="position: relative">
            <button
              class="btn btn-with-icon btn-light btn-setup"
              v-show="false"
            >
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
            <div class="active-filter" v-show="isShowActiveFilter"></div>
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
                  <div
                    class="from-item"
                    style="margin-right: 12px; width: 100%"
                  >
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
                <div class="from-item item-pos" v-if="false">
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
                <button class="btn btn-success" @click="onFilter">
                  Áp dụng
                </button>
              </div>
            </div>
          </div>
        </div>
        <div class="c-body-content scrollbar">
          <table class="tbl">
            <thead>
              <tr>
                <th>Tên công việc</th>
                <th>
                  {{ textDepart }}
                </th>
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
                v-for="(mytask, index) in mytask"
                :key="index"
                class="job-row"
              >
                <td>
                  <div class="td-multiple">
                    <div
                      class="td-m-icon icon icon-24"
                      :class="{
                        iconComplete:
                          mytask.JobStatus == ENUMJOBSTATUS.Complete,
                        iconProgess:
                          mytask.JobStatus == ENUMJOBSTATUS.Processing,
                        iconTodo: mytask.JobStatus == ENUMJOBSTATUS.Todo,
                      }"
                    ></div>
                    <div
                      class="td-m-text"
                      :class="{
                        textdecoration:
                          mytask.JobStatus == ENUMJOBSTATUS.Complete,
                      }"
                    >
                      {{ mytask.JobName }}
                    </div>
                  </div>
                </td>
                <td>{{ mytask.DepartmentName }}</td>
                <td>
                  <div class="td-multiple">
                    <div class="td-m-icon icon icon-24 icon-group-mytask">
                      <div class="icon icon-16 icon-user"></div>
                    </div>
                    <div class="td-m-text">{{ mytask.ProjectName }}</div>
                  </div>
                </td>
                <td>
                  <div class="td-multiple">
                    <div class="td-m-icon icon icon-datepicker"></div>
                    <div class="td-m-text">
                      {{ formatDateTime(mytask.StartTime) }}
                    </div>
                  </div>
                </td>
                <td>
                  <div class="td-multiple">
                    <div
                      class="td-m-icon icon icon-24 icon-datepicker-yes"
                      :class="{
                        'icon-outofdate':
                          onGenTextJobStatus(
                            mytask.JobStatus,
                            mytask.EndTime
                          ) == 'Quá hạn'
                            ? true
                            : false,
                      }"
                    ></div>
                    <div
                      class="td-m-text"
                      :class="{
                        'text-outofdate':
                          onGenTextJobStatus(
                            mytask.JobStatus,
                            mytask.EndTime
                          ) == 'Quá hạn'
                            ? true
                            : false,
                      }"
                    >
                      {{ formatDateTime(mytask.EndTime) }}
                    </div>
                  </div>
                </td>
                <td>
                  <div class="td-multiple">
                    <div
                      class="td-m-icon icon icon-10 icon-status"
                      :class="
                        onBindingJobStatus(mytask.EndTime, mytask.JobStatus)
                      "
                    ></div>
                    <div class="td-m-text">
                      {{ onGenTextJobStatus(mytask.JobStatus, mytask.EndTime) }}
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
                        important: mytask.JobTag == ENUMJOBTAG.Important,
                        instant: mytask.JobTag == ENUMJOBTAG.Urgent,
                      }"
                    ></div>
                    <div class="td-m-text">
                      {{
                        mytask.JobTag == ENUMJOBTAG.Important
                          ? "Quan trọng"
                          : mytask.JobTag == ENUMJOBTAG.Urgent
                          ? "Khẩn cấp"
                          : ""
                      }}
                    </div>
                  </div>
                </td>
                <td>
                  <div class="td-multiple">
                    <div class="td-m-icon icon icon-performer"></div>
                    <div class="td-m-text">{{ mytask.EmployeeName }}</div>
                  </div>
                </td>
                <td v-if="false">
                  <div class="td-multiple">
                    <div class="td-m-icon icon icon-jobadd"></div>
                    <div class="td-m-text">Quách Văn Cảnh</div>
                  </div>
                </td>
                <td>{{ formatDateTime(mytask.CreatedDate) }}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
  <QvcLoading v-if="isShowLoading"></QvcLoading>
</template>

<script>
import SortByDate from "./../../components/popup/sort-by-date.vue";
import { ENUMJOBSTATUS } from "@/enum.js";
import { ENUMJOBTAG } from "@/enum.js";
import { ENUMSTATE } from "@/enum.js";
import { ENUMSORT } from "@/enum.js";
import { ENUMDATEOPTION } from "@/enum.js";
import QvcLoading from "./../../components/dialog/qvc-loading.vue";
export default {
  name: "MyTask",
  components: { SortByDate, QvcLoading },
  watch: {
    // Thay đổi title
    $route: {
      immediate: true,
      handler(to) {
        document.title = to.meta.title || "QVC | Việc của tôi";
      },
    },
  },
  mounted() {},
  created() {
    if (parseInt(localStorage.getItem("state")) == ENUMSTATE.CaNhan) {
      this.textDepart = "Danh mục công việc";
      this.isShowJobAssign = false;
    } else {
      this.textDepart = "Phòng ban";
      this.isShowJobAssign = true;
    }
    this.onGetAllTask(0, ENUMJOBSTATUS.Processing);
  },
  methods: {
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
    removeFilter() {
      this.isShowFilter = false;
      this.keySearch = null;
      this.dateOptionValue.Name = "Không chọn";
      this.dateOptionValue.Option = ENUMDATEOPTION.None;
      this.dateOptionStart = null;
      this.dateOptionEnd = null;
      this.onGetAllTask(
        this.indexTab,
        this.typeJob,
        this.typeSort,
        this.keySearch,
        this.dateOptionValue.Option,
        this.dateOptionStart,
        this.dateOptionEnd
      );
    },
    onFilter() {
      this.isShowFilter = false;
      this.onGetAllTask(
        this.indexTab,
        this.typeJob,
        this.typeSort,
        this.keySearch,
        this.dateOptionValue.Option,
        this.dateOptionStart,
        this.dateOptionEnd
      );
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
      this.onGetAllTask(
        this.indexTab,
        this.typeJob,
        typesort,
        this.keySearch,
        this.dateOptionValue.Option,
        this.dateOptionStart,
        this.dateOptionEnd
      );
    },
    onShowSort() {
      this.isShowSort = !this.isShowSort;
    },
    onSelectedTab(idx, type, typesort) {
      // Xoá trạng thái sort
      this.textSort = "Hạn hoàn thành (tăng dần)";
      // Xoá actic tab khác
      var all = this.$refs.tabitem;
      var tabass = this.$refs.tabassign;
      for (const i of all) {
        i.classList.remove("tab-active");
      }
      if (tabass) {
        tabass.classList.remove("tab-active");
      }

      // Active tab hiện tại
      if (idx == 3) {
        tabass.classList.add("tab-active");
      } else {
        all[idx].classList.add("tab-active");
      }
      this.onGetAllTask(idx, type, typesort);
    },
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

    onGetAllTask(
      idx,
      type,
      typesort = ENUMSORT.EndTimeASC,
      keySearch = null,
      dateOption = ENUMDATEOPTION.None,
      dateOptionStart = null,
      dateOptionEnd = null
    ) {
      //Build dữ liệu
      var data = {
        Id: localStorage.getItem("userid"),
        DBDomain: localStorage.getItem("domain-db"),
        DBCompany: localStorage.getItem("domain-company"),
        State: parseInt(localStorage.getItem("state")),
        Type: type,
        TypeSort: typesort,
        Search: keySearch,
        DateOption: dateOption,
        StartDate: dateOptionStart,
        EndDate: dateOptionEnd,
      };
      // Cập nhật lại giá trị index tab và loại công việc
      this.indexTab = idx;
      this.typeJob = type;
      this.isShowLoading = true;
      this.axios
        .post("http://localhost:56428/api/v2/Assign/GetAllMyTask", data)
        .then((res) => {
          if (res.data) {
            setTimeout(() => {
              this.isShowLoading = false;
              this.mytask = res.data;
              this.totalRow = res.data.length;
              if (idx == 3) {
                this.$refs.tabassign.lastChild.firstElementChild.innerHTML =
                  this.totalRow;
              } else {
                this.$refs.tabitem[idx].lastChild.firstElementChild.innerHTML =
                  this.totalRow;
              }
            }, 300);
          }
        })
        .catch((res) => {
          setTimeout(() => {
            this.isShowLoading = false;
            console.log(res);
          }, 300);
        });
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
      dateOptionStart: null,
      dateOptionEnd: null,
      dateOptionValue: {
        Name: "Không chọn",
        Option: ENUMDATEOPTION.None,
      },
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
      keySearch: "",
      textSort: "Hạn hoàn thành (tăng dần)",
      isShowActiveS0: true,
      isShowActiveS1: false,
      isShowActiveS2: false,
      isShowActiveS3: false,
      isShowActiveS4: false,
      isShowActiveS5: false,
      indexTab: 0,
      typeJob: ENUMJOBSTATUS.Processing,
      textDepart: "Phòng ban",
      totalRow: 0,
      isShowSortByDate: false,
      mytask: [],
      ENUMJOBSTATUS,
      ENUMJOBTAG,
      isShowJobAssign: false,
      isShowLoading: false,
      tabItems: [
        { text: "Việc đang làm", type: ENUMJOBSTATUS.Processing },
        { text: "Việc đã hoàn thành", type: ENUMJOBSTATUS.Complete },
        { text: "Việc quá hạn", type: ENUMJOBSTATUS.OutOfDate },
      ],
      isShowSort: false,
      ENUMSORT,
      isShowCbbEndtime: false,
      isShowCbbJobStatus: false,
      isShowFilter: false,
      typeSort: 0,
      ENUMDATEOPTION,
      isShowDateOther: false,
      isShowActiveFilter: false,
    };
  },
};
</script>

<style scoped>
.active-filter {
  width: 10px;
  height: 10px;
  background-color: red;
  border-radius: 50px;
  position: absolute;
  right: 15px;
  top: 11px;
}
.job-row {
  cursor: pointer;
}

.icon-outofdate {
  filter: invert(16%) sepia(93%) saturate(6936%) hue-rotate(360deg)
    brightness(104%) contrast(115%) !important;
}

.job-row:hover {
  background-color: #eef8ec;
}

.icon-group-mytask {
  border-radius: 56px;
  background-color: #50b83c !important;
  position: relative;
}

.textdecoration {
  text-decoration: line-through !important;
}

.text-outofdate {
  color: red;
  font-weight: 700;
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

.important {
  background-image: url(./../../assets/img/information-circle-orange.svg);
}

.instant {
  background-image: url(./../../assets/img/lightning-circle-red.svg);
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
</style>
