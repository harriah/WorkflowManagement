import { createApp } from "vue";
import App from "./App.vue";
import axios from "axios";
import VueAxios from "vue-axios";

import HomeTask from "./views/Home.vue";
import LoginAccount from "./views/Login.vue";
import RegisterAccount from "./views/Register.vue";
import MyTask from "./views/tasks/MyTask.vue";
import DepartmentTask from "./views/tasks/Department.vue";
import ProjectTask from "./views/tasks/Project.vue";
import { createRouter, createWebHistory } from "vue-router";

const routers = [
  {
    path: "/",
    name: "HomeTask",
    component: HomeTask,
  },
  {
    path: "/login",
    name: "LoginAccount",
    component: LoginAccount,
  },
  {
    path: "/register",
    name: "RegisterAccount",
    component: RegisterAccount,
  },
  {
    path: "/mytask",
    name: "MyTask",
    component: MyTask,
  },
  {
    path: "/department",
    name: "DepartmentTask",
    component: DepartmentTask,
  },
  {
    path: "/project",
    name: "ProjectTask",
    component: ProjectTask,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});

const app = createApp(App);
app.config.globalProperties.$common = {
  getNameDB(idcompany) {
    var state = parseInt(localStorage.getItem("state"));
    // Cá nhân
    if (state == 0) {
      return localStorage.getItem("domain-db");
    } else {
      // Công ty
      if (!idcompany || idcompany === "00000000-0000-0000-0000-000000000000") {
        return localStorage.getItem("domain-db");
      } else {
        return localStorage.getItem("domain-company");
      }
    }
  },
};

app.use(VueAxios, axios).use(router).mount("#app");
