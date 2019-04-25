using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAKE_BFF_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            
            var accessToken = HttpContext.Request.Headers["Authorization"].First().Split(" ").Last();
            
              HttpClient client = new HttpClient();
//            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/te/herbiedv0b2c.onmicrosoft.com/b2c_1_signinv2/oauth2/v2.0/token");
//            tokenRequest.Content = new FormUrlEncodedContent(new[]
//            {
//                new KeyValuePair<string, string>("client_id", "f4613dae-c0ab-4e7b-95fe-858665c6b1a0"),
//                new KeyValuePair<string, string>("client_info", "1"),
//                new KeyValuePair<string, string>("client_secret", "^;0zja2O4}iH287J50Uf]9R`"),
//                new KeyValuePair<string, string>("scope", "https://herbiedv0b2c.onmicrosoft.com/TelematicsServices/read_write offline_access openid profile"),
//                new KeyValuePair<string, string>("grant_type", "refresh_token"),
//                new KeyValuePair<string, string>("refresh_token", accessToken),
//            });*/

            //var tokenResult = await client.SendAsync(tokenRequest);
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://herbiedv0.swissre.com/platform/api/v1/users/me/sessions?Month=3&Year=2019");

            // Add token to the Authorization header and make the request
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.SendAsync(request);

            // Handle the response
            string responseString;
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    responseString = await response.Content.ReadAsStringAsync();
                    break;
                case HttpStatusCode.Unauthorized:
                    responseString = $"Please sign in again. {response.ReasonPhrase}";
                    break;
                default:
                    responseString = $"Error calling API. StatusCode=${response.StatusCode}";
                    break;
            }
            return new string[] {responseString, "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}