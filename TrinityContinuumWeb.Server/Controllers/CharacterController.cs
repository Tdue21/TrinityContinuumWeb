using Microsoft.AspNetCore.Mvc;
using TrinityContinuumWeb.Models;
using TrinityContinuumWeb.Services;

namespace TrinityContinuumWeb.Server.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CharacterController(ICharacterService characterService) : ControllerBase
{
    private readonly ICharacterService _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));

    [HttpGet("{id}")]
    [ProducesResponseType<Character>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCharacter(int id)
    {
        try
        {
            var result = await _characterService.GetCharacterFromId(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("list")]
    [ProducesResponseType<IEnumerable<Character>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListCharacters()
    {
        var result = await _characterService.GetCharacterList();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
