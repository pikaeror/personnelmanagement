using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;
using PersonnelManagement.Services;

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
        public Synchronization sendThisDataBaseToServer([FromRoute] string address, [FromRoute] string port)
        {
            Synchronization output = new Synchronization(repository.GetContext());
            return output;
            /*WebRequest request = WebRequest.Create(string.Format("http://{0}:{1}/", address, port));
            request.Method = "POST";
            //request.

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(string.Format("http://{0}:{1}/", address, port));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));*/
            //client.PostAsync("api/Synchronization/back");
            //HttpResponseMessage httpResponse = client.PostAsJsonAsync();
            //return new ObjectResult("");
        }

        [HttpPost("back")]
        public IActionResult getDataFromServer([FromBody] Synchronization data_for_sync)
        {

            return new ObjectResult("");
        }
    }
}
