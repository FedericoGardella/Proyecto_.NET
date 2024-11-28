namespace Autenticacion.Models
{
    public class RegisterMedicoModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Matricula { get; set; }
        public List<long> EspecialidadesIds { get; set; } = new List<long>();
    }
}
