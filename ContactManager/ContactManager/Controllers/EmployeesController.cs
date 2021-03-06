﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Threading.Tasks;
using ContactManager.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using System.Net.Http;
using System.Windows.Forms;

namespace ContactManager.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeesController : Controller
    {
        [ActionName("Index")]
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var username = User.Identity.Name;
                var user = context.Users.FirstOrDefault(p => p.UserName == username);
                if (user != null)
                {
                    WebRequest request = WebRequest.Create("https://fidelitefunctionapp.azurewebsites.net/api/GetTopTenEmployees");
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

                    var first = responseFromServer.Replace("\"", "'");
                    var ranking = JsonConvert.DeserializeObject<List<Rank>>(first);


                    //var employees = DocumentDBRepository<Employees>.getEmployeesActivities();
                    //var e = new Employees();
                    ////will only enter once
                    //foreach (Employees emp in employees)
                    //{
                    //    e = emp;
                    //}
                    //var logged = new Employee();
                    ////string [] comp = User.Identity.Name.ToLower().Split('.', (char)2, (char)1);
                    ////logged = e.employees.FirstOrDefault(p => p.firstname.ToLower() == comp[0] && p.lastname.ToLower() == comp[1]);
                    //logged = e.employees.FirstOrDefault(p => p.screen_name == username);


                    using (var client = new WebClient())
                    {
                        string data = "{\"screenname\":\"" + username + "\"}";
                        var values = new NameValueCollection();
                        values["screenname"] = username;

                        client.Headers.Add("content-type", "application/json");
                        client.QueryString = values;
                        var responseEmployee = client.UploadData("https://fidelitefunctionapp.azurewebsites.net/api/GetConnectedEmployee", "post", Encoding.Default.GetBytes(data));
                        var responseString = Encoding.Default.GetString(responseEmployee);

                        var firstConv = responseString.Replace("\"", "'");
                        var second = firstConv.Replace("\\", string.Empty);
                        var third = second.Replace("'{", "{");
                        var fourth = third.Replace("}'", "}");
                        var employee = JsonConvert.DeserializeObject<Employee>(fourth);



                        if (ranking == null)
                        {
                            return View("Error");
                        }
                        else
                        {
                            ViewData["ranking"] = ranking;
                            return View(employee);
                        }
                    }
                }
            }
            return View("Error");
        }

        [Authorize(Roles = "Employee")]
        [ActionName("Convert")]
        public ActionResult Convert()
        {
            using (var context = new ApplicationDbContext())
            {
                WebRequest request = WebRequest.Create("https://fidelitefunctionapp.azurewebsites.net/api/GetRewards");
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

                var first = responseFromServer.Replace("\"", "'");
                var second = first.Replace("\\", string.Empty);
                var third = second.Replace("'[", "[");
                var fourth = third.Replace("]'", "]");
                var rewards = JsonConvert.DeserializeObject<List<Reward>>(fourth);
                ViewData["rewards"] = rewards;

                //   WebRequest request = WebRequest.Create(
                //"https://fidelitefunctionapp.azurewebsites.net/api/GetRewards?code=7LSGHFqUi34TX/7Vq243akHhUKSpdYmC5RqnkEB09D12n/8kfsCNDw==");
                //   // Get the response.  
                //   WebResponse response = request.GetResponse();
                //   // Display the status.  
                //   Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                //   // Get the stream containing content returned by the server.  
                //   Stream dataStream = response.GetResponseStream();
                //   // Open the stream using a StreamReader for easy access.  
                //   StreamReader reader = new StreamReader(dataStream);
                //   // Read the content.  
                //   string responseFromServer = reader.ReadToEnd();
                //   // Display the content.  
                //   Console.WriteLine(responseFromServer);
                //   // Clean up the streams and the response.  
                //   reader.Close();
                //   response.Close();

                //   var first = responseFromServer.Replace("\"", "'");
                //   var second = first.Replace("\\", string.Empty);
                //   var third = second.Replace("'{", "{");
                //   var fourth = third.Replace("}'", "}");
                //   var rewards = JsonConvert.DeserializeObject<Rewards>(fourth);
                //ViewData["rewards"] = rewards;
            }

            var username = User.Identity.Name;
            if (username != null)
            {
                using (var client = new WebClient())
                {
                    string data = "{\"screenname\":\"" + username + "\"}";
                    var values = new NameValueCollection();
                    values["screenname"] = username;
                    client.Headers.Add("content-type", "application/json");
                    client.QueryString = values;
                    var response = client.UploadData("https://fidelitefunctionapp.azurewebsites.net/api/GetConnectedEmployee", "post", Encoding.Default.GetBytes(data));
                    var responseString = Encoding.Default.GetString(response);
                    var first = responseString.Replace("\"", "'");
                    var second = first.Replace("\\", string.Empty);
                    var third = second.Replace("'{", "{");
                    var fourth = third.Replace("}'", "}");
                    var employee = JsonConvert.DeserializeObject<Employee>(fourth);

                    return View(employee);
                }

            }
            return View("Error");
        }


        [ActionName("SendConversion")]
        public ActionResult SendConversion()
        {
            using (var client = new WebClient())
            {
                string username = User.Identity.Name;
                string data = "{\"screenname\":\"" + username + "\"}";
                var values = new NameValueCollection();
                values["screenname"] = username;

                client.Headers.Add("content-type", "application/json");
                client.QueryString = values;
                var response = client.UploadData("https://fidelitefunctionapp.azurewebsites.net/api/GetConnectedEmployee", "post", Encoding.Default.GetBytes(data));
                var responseString = Encoding.Default.GetString(response);

                var first = responseString.Replace("\"", "'");
                var second = first.Replace("\\", string.Empty);
                var third = second.Replace("'{", "{");
                var fourth = third.Replace("}'", "}");
                var employee = JsonConvert.DeserializeObject<Employee>(fourth);

                return View(employee);
            }


            //var request = (HttpWebRequest)WebRequest.Create("https://fidelitefunctionapp.azurewebsites.net/api/GetConnectedEmployee");

            ////var postData = $"screenname=marcnohra";
            ////var data = Encoding.ASCII.GetBytes(postData);
            //byte[] data = Encoding.ASCII.GetBytes("screenname=marcnohra");

            //request.Method = "POST";
            ////request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json";
            ////request.MediaType = "application/xml";
            //request.ContentLength = data.Length;


            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}

            //var response = (HttpWebResponse)request.GetResponse();

            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();










            //using (var client = new HttpClient())
            //{
            //    Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //    dictionary.Add("screenname", "marcnohra");

            //    string json = JsonConvert.SerializeObject(dictionary);
            //    var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            //    //var response = client.PostAsync(String.Format("url"), requestData);
            //    var response = client.PostAsync("https://fidelitefunctionapp.azurewebsites.net/api/GetConnectedEmployee", requestData);
            //    var result = response.Result.Content.ReadAsStringAsync();

            //}




            //// Create a request using a URL that can receive a post.   
            //WebRequest request = WebRequest.Create("https://fidelitefunctionapp.azurewebsites.net/api/HttpTriggerCSharp/name/");
            //// Set the Method property of the request to POST.  
            //request.Method = "POST";
            //// Create POST data and convert it to a byte array.  
            //string postName = "screenname=marcnohra";
            //byte[] screenname = Encoding.UTF8.GetBytes(postName);
            //// Set the ContentType property of the WebRequest.  
            //request.ContentType = "application/x-www-form-urlencoded";
            //// Set the ContentLength property of the WebRequest.  
            //request.ContentLength = screenname.Length;



            //// Get the request stream.  
            //Stream dataStream = request.GetRequestStream();
            //// Write the data to the request stream.  
            //dataStream.Write(screenname, 0, screenname.Length);
            //// Close the Stream object.  
            //dataStream.Close();
            //// Get the response.  
            //WebResponse response = request.GetResponse();
            //// Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            //// Get the stream containing content returned by the server.  
            //dataStream = response.GetResponseStream();
            //// Open the stream using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);
            //// Read the content.  
            //string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);
            //// Clean up the streams.  
            //reader.Close();
            //dataStream.Close();
            //response.Close();





            //using (var client = new WebClient())
            //{
            //    var values = new NameValueCollection();
            //    values["screenname"] = "marcnohra";

            //    var response = client.UploadValues("https://fidelitefunctionapp.azurewebsites.net/api/HttpTriggerCSharp/name/", values);

            //    var responseString = Encoding.Default.GetString(response);
            //}
        }


        [HttpPost]
        [ActionName("ConvertPoints")]
        public ActionResult ConvertPoints()
        {
            string rewardName = Request["Rewards"].ToString();
            using (var client = new WebClient())
            {
                string username = User.Identity.Name;
                string data = "[{\"screenname\":\"" + username + "\"},{\"rewardtype\":\"" + rewardName + "\"}]";
                var values = new NameValueCollection();
                values["screenname"] = username;
                values["rewardtype"] = rewardName;

                client.Headers.Add("content-type", "application/json");
                client.QueryString = values;
                try
                {
                    var response = client.UploadData("https://fidelitefunctionapp.azurewebsites.net/api/RewardEmployee", "post", Encoding.Default.GetBytes(data));

                    var responseString = Encoding.Default.GetString(response);
                    responseString = responseString.Replace("\"", "");
                    responseString = responseString.Replace('"', '\0');

                    if (responseString.Equals("Successfully Converted Points to Reward "))
                    {
                        //MessageBox.Show("Congratulations", "Conversion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return RedirectToAction("Convert");
                    }
                    else
                    {
                        //MessageBox.Show("Not enough points", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return RedirectToAction("Convert");
                    }

                }
                catch (Exception)
                {
                    //MessageBox.Show("Not enough points", "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return RedirectToAction("Convert");
                }
            }
        }
    }
}