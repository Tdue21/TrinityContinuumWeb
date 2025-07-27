using System.ComponentModel.DataAnnotations;

namespace TrinityContinuum.Identity.Models;

public record RegisterUserDto([Required] string Email, [Required] string Password);
public record LoginUserDto([Required] string Email, [Required] string Password);

