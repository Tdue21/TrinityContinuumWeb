using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services;

namespace TrinityContinuum.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PowersController(IPowersService powersService) : ControllerBase
{
    private readonly IPowersService _powersService = powersService ?? throw new ArgumentNullException(nameof(powersService));

    [HttpGet("list")]
    [ProducesResponseType<IEnumerable<PsiPower>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ListPowers()
    {
        try
        {
            var result = await _powersService.GetPsiPowers();
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
}
