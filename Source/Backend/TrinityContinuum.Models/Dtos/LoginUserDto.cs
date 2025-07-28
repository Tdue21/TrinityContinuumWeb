using System.ComponentModel.DataAnnotations;

namespace TrinityContinuum.Models.Dtos;

public record LoginUserDto([Required] string Email, [Required] string Password) 
{
    public LoginUserDto() : this(string.Empty, string.Empty)
    {
    }
}

