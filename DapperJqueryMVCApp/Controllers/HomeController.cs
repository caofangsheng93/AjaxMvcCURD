using DapperJqueryMVCApp.Models;
using DapperJqueryMVCApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperJqueryMVCApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Index()
        {
            EmpRepository emp = new EmpRepository();
            
            return Json(emp.GetAllEmployees(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddEmployee(EmpModel model)
        {
            try
            {
                EmpRepository emp = new EmpRepository();
                emp.AddEmployee(model);
                return Json("添加成功");
            }
            catch (Exception ex)
            {
                return Json("添加失败");
                
            }
        }

        public ActionResult EditEmployee(int? id)
        {
            EmpRepository emp = new EmpRepository();
           return View(emp.GetAllEmployees().Find(s => s.Id == id));
        }

        [HttpPost]
        public JsonResult EditEmployee(EmpModel model)
        {
            try
            {
                EmpRepository emp = new EmpRepository();
                emp.UpdateEmployee(model);
                return Json("修改成功");
            }
            catch (Exception ex)
            {
                 return Json("修改失败");
                
            }
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            try
            {
                EmpRepository emp = new EmpRepository();
                emp.DeleteEmployee(id);
                return Json("删除成功", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("删除失败");
               
            }

        }

        public PartialViewResult EmployeeDetail()
        {
            return PartialView("EmployeeDetail");
        }
    }
}