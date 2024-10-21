using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DAL.Models
{
    public class Medicos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Matricula { get; set; }

        [Required] 
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Apellido { get; set; }

        [Required] 
        private string passwordHash { get; set; }

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


        public int EspecialidadId { get; set; }
        public Especialidades Especialidad { get; set; }
    }
}
