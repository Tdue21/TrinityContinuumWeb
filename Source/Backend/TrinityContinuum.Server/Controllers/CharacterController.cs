using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Server.Attributes;
using TrinityContinuum.Services;

namespace TrinityContinuum.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiKey]
public class CharacterController(ICharacterService characterService, ILogger<CharacterController> logger) : ControllerBase
{
    private readonly ICharacterService _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
    private readonly ILogger<CharacterController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpGet("{id}")]
    [ProducesResponseType<Character>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
            _logger.LogError(ex, "Error retrieving character with ID {Id}", id);
            return BadRequest(ex);
        }
    }

    [HttpGet("list")]
    [ProducesResponseType<IEnumerable<Character>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListCharacters()
    {
        try
        {
            var result = await _characterService.GetCharacterList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogError(ex, "Character directory not found.");
            return BadRequest(new ProblemDetails
            {
                Title = $"Exception: {ex.GetType()}",
                Detail = "Unable to locate character directory.",
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing characters.");
            return BadRequest(new ProblemDetails
            {
                Title = $"Exception: {ex.GetType()}",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest
            });
        }
    }

    [HttpPost("{id}")]
    [ProducesResponseType<Character>(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SaveCharacter(int id, [FromBody] Character character, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _characterService.SaveCharacter(id, character, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving character with ID {Id}", id);
            return BadRequest(ex);
        }
    }
}
