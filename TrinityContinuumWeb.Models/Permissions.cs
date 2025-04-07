using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrinityContinuum.Models;

public class Permissions
{
    public IEnumerable<Role> Roles { get; set; } = Enumerable.Empty<Role>();
    public IEnumerable<Player> Players { get; set; } = Enumerable.Empty<Player>();
}

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

}

public class  Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public Guid[] Roles { get; set; } = Array.Empty<Guid>();
}
