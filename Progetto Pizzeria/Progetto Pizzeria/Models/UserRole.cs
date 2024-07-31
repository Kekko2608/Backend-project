using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Progetto_Pizzeria.Models
{
    [Index("UserId", "RoleId", Name = "UK_UsersRoles", IsUnique = true)]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
