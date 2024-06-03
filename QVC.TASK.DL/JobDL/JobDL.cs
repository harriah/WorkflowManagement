using Dapper;
using MySqlConnector;
using QVC.TASK.Common;
using QVC.TASK.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QVC.TASK.DL
{
    public class JobDL : BaseDL<Job>, IJobDL
    {
        /// <summary>
        /// thực hiện thêm mới nhiều công việc
        /// </summary>
        /// <param name="jobs">danh sách công việc</param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
    /*    public bool insertjobchild(list<job> jobs, idbconnection connection, idbtransaction transaction)
        {
            // chuẩn bị tên stored procedure
            string storedprocedurename = "proc_inserts_job";

            // chuẩn bị danh sách tham số đầu vào cho sở thích phục vụ
            var parameters = new dynamicparameters();
            string values = "";
            foreach (var item in jobs)
            {
                guid newid = guid.newguid();
                string value = "'" + item.jobid + "','" + item.jobcode + "','" + item.jobname + "','" + item.projectid + "','" + item.jobstatus+ "','"+item.jobtag+ "','"+item.starttime+ "','"+item.endtime+ "','"+item.description+ "','"+item.parentid+ "','"+                 "',now(),'',now(),''";
                values = values + "(" + value + ")" + ",";
            }

        //    // thêm tham số đầu vào cho parameters
            parameters.add("@values", values.remove(values.length - 1));
        }*/
        public List<Job> GetJobsComplete(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Complete_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<JobOutput> GetAllJobByIdProject(Guid id, string dbdomanin)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format("Proc_GetAll_Job_ById_Project");

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<JobOutput> allTasks = new List<JobOutput>();

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, dbdomanin)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                allTasks = mySqlConnection.Query<JobOutput>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return allTasks;
        }

        public List<Job> GetJobsOutOfDate(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_OutOfDate_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<Job> GetJobsProcessing(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Processing_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }

        public List<Job> GetJobsToDo(Guid id, string domaindb)
        {
            // Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_GetJob_Todo_ByID";

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            // Khai báo đối tượng muốn lấy
            List<Job> record;

            // Khởi tạo kết nối tới Database
            using (var mySqlConnection = new MySqlConnection(String.Format(Database.DBDomain, domaindb)))
            {
                // Mở kết nối
                OpenConnection(mySqlConnection);

                // Thực hiện gọi vào Database để chạy stored procedure
                record = mySqlConnection.Query<Job>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Đóng kết nối
                CloseConnection(mySqlConnection);
            }

            //Trả về đối tượng Employee
            return record;
        }
    }
}
