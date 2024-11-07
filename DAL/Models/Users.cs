using Microsoft.AspNetCore.Identity;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Users : IdentityUser
    {
        public bool Activo { get; set; }

        [ForeignKey("Personas")]
        public long PersonasId { get; set; }

        [Required]
        public Personas Personas { get; set; }

        [NotMapped]
        public string Username
        {
            get => UserName;  
            set => UserName = value; 
        }

        [NotMapped]
        public List<string> Roles { get; set; } = new List<string>();

        public User GetEntity(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, bool addRoles)
        {
            var usuario = new User
            {
                Id = Id,
                Persona = Personas.GetEntity(),  
                Username = UserName ?? "",  
                Email = Email ?? "",
                Activo = Activo
            };

            if (addRoles)
            {
                usuario.Roles = userManager?.GetRolesAsync(this).Result.ToList() ?? new List<string>();
            }

            return usuario;
        }
    }
}
