namespace Autenticacion.Models
{
    public class LoginResponse
    {
        public bool StatusOk { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
        public string IdUsuario { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; }
    }
}
