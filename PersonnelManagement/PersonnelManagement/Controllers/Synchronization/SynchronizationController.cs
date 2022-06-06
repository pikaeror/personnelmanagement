using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PersonnelManagement.Controllers
{

    [Produces("application/json")]
    [Route("api/Synchronization")]
    public class SynchronizationController : Controller
    {

        private Repository repository;
        public SynchronizationController(Repository repo)
        {
            repository = repo;
        }

        [HttpPost("front/a{address}p{port}")]
        public IActionResult sendThisDataBaseToServer([FromRoute] string address, [FromRoute] string port)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(string.Format("http://{0}:{1}/", address, port));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.PostAsync("api/Synchronization/back", HttpPostAttribute);
            //HttpResponseMessage httpResponse = client.PostAsJsonAsync();
            return new ObjectResult("");
        }

        [HttpPost("back")]
        public bool getDataFromServer([FromBody] Object data_for_sync)
        {

            return false;
        }
    }
}
