using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DapperJqueryMVCApp.Models;
using Dapper;


namespace DapperJqueryMVCApp.Repository
{
    public class EmpRepository
    {
        SqlConnection con;

        /// <summary>
        /// 创建连接对象
        /// </summary>
        private void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(connectionString);
        }

        #region 添加雇员
        /// <summary>
        /// 添加雇员
        /// </summary>
        /// <param name="model"></param>
        public void AddEmployee(EmpModel model)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Name", model.Name);
                parameter.Add("@City", model.City);
                parameter.Add("Address", model.Address);

                //1.创建连接对象
                Connection();

                //2.打开连接
                con.Open();
                con.Execute("AddNewEmpDetails", parameter, commandType: CommandType.StoredProcedure);   //命名参数
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        } 
        #endregion

        #region 获取雇员信息
        /// <summary>
        /// 获取雇员信息
        /// </summary>
        /// <returns></returns>
        public List<EmpModel> GetAllEmployees()
        {
            try
            {
                Connection();
                con.Open();
                IList<EmpModel> lstEmp = SqlMapper.Query<EmpModel>(con, "GetEmployees").ToList();
                con.Close();
                return lstEmp.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region 更新雇员信息
        /// <summary>
        /// 更新雇员信息
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEmployee(EmpModel model)
        {
            try
            {

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@EmpId", model.Id);
                parameter.Add("@Name", model.Name);
                parameter.Add("@City", model.City);
                parameter.Add("@Address", model.Address);

                //创建连接对象
                Connection();
                con.Open();//打开连接
                con.Execute("UpdateEmpDetails", parameter, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }


        } 
        #endregion

        #region 删除雇员信息
        /// <summary>
        /// 删除雇员信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(int id)
        {

            try
            {
                Connection();//创建连接对象
                con.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@EmpId", id);
                con.Execute("DeleteEmpById", parameter, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        } 
        #endregion
    }
}