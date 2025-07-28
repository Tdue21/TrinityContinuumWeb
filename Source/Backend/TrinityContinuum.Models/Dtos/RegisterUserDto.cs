using System.ComponentModel.DataAnnotations;

namespace TrinityContinuum.Models.Dtos;

public record RegisterUserDto([Required] string Email, [Required] string Password);

