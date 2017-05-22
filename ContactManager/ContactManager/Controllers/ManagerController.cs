using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using ContactManager.Models;
using System.Net;
using System.IO;

namespace ContactManager.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            WebRequest request = WebRequest.Create("https://fidelitefunctionapp.azurewebsites.net/api/GetAllEmployeesActivities");
            request.Method = "POST";
            request.ContentType = "application/json";

            Stream dataStream = request.GetRequestStream();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            var activities = JsonConvert.DeserializeObject<List<Employee>>(responseFromServer);
            return View(activities);
        }
    }
}