using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace DAL.Models

{
    public class Pacientes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(150)]
        public string Apellidos { get; set; }

        [Required]
        [MaxLength(8),MinLength(8)]
        public string Documento { get; set; }

        [Required]
        private string passwordHash;


        public void SetPassword(string password)
        {
            var hasLetterAndDigit = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)");
            var hasSpecialChar = new Regex(@"[!@#$%^&*(),.?""{}|<>]");
            if (password.Length == 8 && hasLetterAndDigit.IsMatch(password) && hasSpecialChar.IsMatch(password))
            { 
                passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            }
            else
            {
                throw new Exception("Formato de Password incorrecto");
            }
        }


        public bool VerificarPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
