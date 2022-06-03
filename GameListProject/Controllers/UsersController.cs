using GameListProject.Models;
using GameStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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

    //rudney


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
        if(await _usersService.GetAsyncByName(newUser.Nome) is not null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuário esse nome já existe" });
        }


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
    public async Task<ActionResult<User>> AdicionarJogoAoUsuario(string idUser, string idGame)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        var game = await _gamesService.GetAsync(idGame);

        var verifyGame = user.Games.Find(x => x.Id == idGame);

        if (verifyGame is not null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuário já possui este jogo em sua lista." });
        }

        user.Games.Add(game);

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

    [Route("{idUser}/removeGame/{idGame}")]
    [HttpPut]
    public async Task<ActionResult<User>> DeletarJogoDoUsuario(string idUser, string idGame)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        var game = user.Games.Find(x => x.Id == idGame);

        if (game is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuário não possui este jogo" });
        }

        var newGameList = new List<Game>();

        for (int i = 0; i < user.Games.Count; i++)
        {
            if (user.Games[i].Id != idGame)
            {
                newGameList.Add(user.Games[i]);
            }
        }

        user.Games = newGameList;

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

    [Route("{idUser}/addFavorite/{idGame}")]
    [HttpPut]
    public async Task<ActionResult<User>> AdicionarJogoAoFavorito(string idUser, string idGame)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        var game = await _gamesService.GetAsync(idGame);

        var verifyGame = user.Favorites.Find(x => x.Id == idGame);

        if (verifyGame is not null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuário já possui este jogo em sua lista de favoritos." });
        }

        user.Favorites.Add(game);

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

    [Route("{idUser}/removeFavorite/{idGame}")]
    [HttpPut]
    public async Task<ActionResult<User>> DeletarJogoDoFavorito(string idUser, string idGame)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        var game = user.Favorites.Find(x => x.Id == idGame);

        if (game is null)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Usuário não possui este jogo" });
        }

        var newGameList = new List<Game>();

        for (int i = 0; i < user.Favorites.Count; i++)
        {
            if (user.Favorites[i].Id != idGame)
            {
                newGameList.Add(user.Favorites[i]);
            }
        }

        user.Favorites = newGameList;

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

    [Route("{idUser}/changeBio/{newBio}")]
    [HttpPost]
    public async Task<ActionResult<User>> ChangeBio(string idUser, string newBio)
    {
        var user = await _usersService.GetAsync(idUser);
        if (user is null)
        {
            return NotFound();
        }

        user.Bio = newBio;

        await _usersService.UpdateAsync(idUser, user);

        return user;
    }

}

