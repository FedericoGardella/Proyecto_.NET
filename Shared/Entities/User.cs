namespace Shared.Entities
{
    public class User
    {
        public string Id { get; set; }
        public bool Activo { get; set; }
        public Persona Persona { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}