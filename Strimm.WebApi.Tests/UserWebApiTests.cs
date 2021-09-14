using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using Strimm.WebApi.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Strimm.Model;
using System.Web.Http;
using System.Threading;

namespace Strimm.WebApi.Tests
{
    [TestClass]
    public class UserWebApiTests
    {
        [TestProperty("ControllerTest", "UserController")]
        [TestMethod]
        public async Task GetAllUsersControllerTest()
        {
            var controller = new UserController();
            var results = await controller.GetAllUsersAsync() as IEnumerable<User>;
        }

        [TestProperty("WebApiTest", "UserController")]
        [TestMethod]
        public async Task GetAllUsersWebApiTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4001");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("/api/user/all");
                string usersJson = String.Empty;

                if (response.IsSuccessStatusCode)
                {
                    usersJson = await response.Content.ReadAsStringAsync();
                }

                Assert.IsTrue(response.IsSuccessStatusCode, "Server request failed");
                Assert.IsTrue(!String.IsNullOrEmpty(usersJson), "Unable to retrieve any users");
            }
        }

        [TestMethod]
        public async Task GetAllUsersHttpServer()
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            HttpServer server = new HttpServer(config);
            using (HttpMessageInvoker client = new HttpMessageInvoker(server))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:4002/api/user/"))
                {
                    HttpResponseMessage response = await client.SendAsync(request, CancellationToken.None);
                    var statusCode = response.StatusCode;
                }
            }
        }

    }
}
