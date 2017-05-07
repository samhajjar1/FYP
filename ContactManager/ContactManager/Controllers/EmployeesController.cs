using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Threading.Tasks;
using ContactManager.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;


namespace ContactManager.Controllers
{
    public class EmployeesController : Controller
    {
        //// GET: Employees
        //public ActionResult Index()
        //{
        //    return View();
        //}



        //[ActionName("Index")]
        //[AllowAnonymous]
        //public async Task<ActionResult> IndexAsync()
        //{
        //    var items = await DocumentDBRepository<Employees>.GetItemsAsync();
        //    Console.WriteLine(items);
        //    return View(items);
        //}
        

        [ActionName("Index")]
        [Authorize(Roles = "Employee")]
        public ActionResult Index(){
            using (var context = new ApplicationDbContext())
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(p => p.UserName == username);
                if (user != null)
                {
                    var employees = DocumentDBRepository<Employees>.getEmployeesActivities();
                    var e = new Employees();
                    //will only enter once
                    foreach (Employees emp in employees)
                    {
                        e = emp;
                    }
                    var logged = new Employee();
                    //string [] comp = User.Identity.Name.ToLower().Split('.', (char)2, (char)1);
                    //logged = e.employees.FirstOrDefault(p => p.firstname.ToLower() == comp[0] && p.lastname.ToLower() == comp[1]);
                    logged = e.employees.FirstOrDefault(p => p.screen_name == username);
                    if (logged == null)
                    {
                        return View("Error");
                    }
                    else
                    {
                        ViewData["user"] = logged;
                        return View(e);
                    }
                }
            }
            return View("Error");
        }

        [ActionName("Convert")]
        [Authorize(Roles = "Employee")]
        public ActionResult Convert()
        {
            using (var context = new ApplicationDbContext())
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(p => p.UserName == username);
                if(user != null){
                    var fixedScore = DocumentDBRepository<Employees>.getConnectedEmployeeFixedScore(username);
                }
            }
            return View();
        }

    }
}