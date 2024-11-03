﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Entities;

namespace DAL.Models
{
    public class Users : IdentityUser
    {

        public bool Activo { get; set; }

        [ForeignKey("Personas")]
        public long PersonaId { get; set; }

        [Required]
        public Personas Personas { get; set; }

        public User GetEntity(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, bool addRoles)
        {
            User usuario = new User
            {
                Id = Id,
                Persona = Personas.GetEntity(),
                Username = UserName ?? "",
                Email = Email ?? "",
                Activo = Activo
            };

            if (addRoles)
                usuario.Roles = userManager?.GetRolesAsync(this).Result.ToList() ?? new List<string>();

            return usuario;
        }
    }
}