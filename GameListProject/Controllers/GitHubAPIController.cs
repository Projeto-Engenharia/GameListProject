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
    public async Task<ActionResult<string>> githubOrgReposRequest(string organization)
    {
        //Desafio-Stags-2022
        string url = "https://api.github.com/orgs/"+organization+"/repos";
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("fernandox2000", "1"));
            return await client.GetStringAsync(url);
        }
    }

    [Route("repos")]
    [HttpGet]
    public async Task<ActionResult<string>> githubRepoBranchesRequest(string organization, string repo)
    {
        // Desafio-Stags-2022
        // transformation-notifier-api
        string url = "https://api.github.com/repos/"+organization+"/"+repo+"/branches";
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("fernandox2000", "1"));
            return await client.GetStringAsync(url);
        }
    }
}

