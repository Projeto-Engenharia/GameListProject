using GameListProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UserStoreApi.Services;

namespace UserStoreApi.Controllers;

[ApiController]
[Route("api/github")]
public class GitHubAPIController : ControllerBase
{

    [Route("org")]
    [HttpGet]
    public async Task<ActionResult<string>> githubRequest()
    {
        string url = "https://api.github.com/orgs/Projeto-Engenharia";
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("fernandox2000", "1"));
            return await client.GetStringAsync(url);
        }
    }
}

