using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El nombre de Usuario es requerido")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = string.Empty;
    }
}
