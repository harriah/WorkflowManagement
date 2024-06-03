export const RESAPI = {
    /**API thêm mới 1 công việc */
    InsertJob: "http://localhost:56428/api/v2/Job/insert-job",

    /**API lấyy danh sách phòng ban */
    GetDepartment: (domain) => `http://localhost:56428/api/v2/Department/${domain}`,

    /**API lấy danh sách dự án theo id phòng ban */
    GetProjectById:(id,domain)=>`http://localhost:56428/api/v2/Project/getall-byid?id=${id}&domain=${domain}`
};