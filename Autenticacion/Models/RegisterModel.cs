using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El documento es requerido"), MinLength(3), MaxLength(128)]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer nombre es requerido"), MinLength(3), MaxLength(128)]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido es requerido"), MinLength(3), MaxLength(128)]
        public string Apellidos { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido"), MinLength(4), MaxLength(128)]
        public string Email { get; set; } = string.Empty;

        public bool Activo { get; set; }
    }
}
