using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace Using_HttpClientFactory.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientController(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult> GetVerb()
        {
            using HttpClient client = _httpClientFactory.CreateClient("JsonPlaceholderApi" ?? "");

            try
            {
                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    return Ok(content);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Erro: {response.ReasonPhrase}. Detalhes: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
