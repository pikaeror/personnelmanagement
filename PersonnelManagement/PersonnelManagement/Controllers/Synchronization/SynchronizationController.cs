using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;
using PersonnelManagement.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

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

        [HttpPost("front/a/{adress}/p/{port}")]
        public void sendThisDataBaseToServer([FromRoute] string adress, [FromRoute] string port)
        {
            Synchronization output = new Synchronization(repository.GetContext());
            send(output, string.Format("http://{0}:{1}/api/Synchronization/back", adress, port));
            var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
        }

        [NonSerialized]
        static readonly HttpClient client = new HttpClient();
        static async void send(Synchronization synchronization, string connection_string = "http://172.26.200.89:8080/api/Synchronization/back")
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(synchronization);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(connection_string, byteContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async void get(Synchronization synchronization, string connection_string = "http://172.26.200.89:8080/api/Synchronization/back")
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(synchronization);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(connection_string, byteContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        [HttpPost("back"), DisableRequestSizeLimit]
        public IActionResult getDataFromServer([FromBody] Synchronization data_for_sync)
        {
            try
            {
                int k = 0;
                //data_for_sync.update(repository);
            } catch
            {
                return new ObjectResult(Keys.ERROR_SHORT + "ошибка обновления");
            }
            return new ObjectResult(Keys.SUCCESS_SHORT + "обновление прошло успешно");
        }

        [HttpPost("send_to_server"), DisableRequestSizeLimit]
        public IActionResult sendDataFromServer([FromBody] Synchronization data_for_sync)
        {
            try
            {
                int k = 0;
                //data_for_sync.update(repository);
            }
            catch
            {
                return new ObjectResult(Keys.ERROR_SHORT + "ошибка обновления");
            }
            return new ObjectResult(Keys.SUCCESS_SHORT + "обновление прошло успешно");
        }

        [HttpPost("get_from_server"), DisableRequestSizeLimit]
        public IActionResult getfromDataFromServer([FromBody] Synchronization data_for_sync)
        {
            try
            {
                int k = 0;
                //data_for_sync.update(repository);
            }
            catch
            {
                return new ObjectResult(Keys.ERROR_SHORT + "ошибка обновления");
            }
            return new ObjectResult(Keys.SUCCESS_SHORT + "обновление прошло успешно");
        }
    }
}
