using GameListProject.Models;
using GameStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UserStoreApi.Services;

namespace UserStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;
    private readonly GamesService _gamesService;
    public UsersController(UsersService usersService, GamesService gamesService)
    {
        _usersService = usersService;
        _gamesService = gamesService;
    } 






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

    [Route("{idUser}/addGame/{idGame}")]
    [HttpPut]
    public async Task<ActionResult<User>> GetNovoTesteComNome(string idUser, string idGame)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        var game = await _gamesService.GetAsync(idGame);

        var entity = user.Games.Find(document => document.Id == "idGame");

        user.Games.Add(game);

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

}

