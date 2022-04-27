using GameListProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UserStoreApi.Services;

namespace UserStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService) =>
        _usersService = usersService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _usersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _usersService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _usersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _usersService.RemoveAsync(id);

        return NoContent();
    }

    [Route("teste/{nome}")]
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> GetNovoTesteComNome(string nome)
    {
        var user = await _usersService.GetAsyncByName(nome);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [Route("github")]
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<String>> githubRequest()
    {
        string url = "https://api.github.com/orgs/Projeto-Engenharia";
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("fernandox2000" , "1"));
            return await client.GetStringAsync(url);
        }
    }

}

