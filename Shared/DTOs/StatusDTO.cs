namespace Shared.DTOs
{
    public class StatusDTO
    {
        public StatusDTO() { }

        public StatusDTO(bool status, string mensaje)
        {
            Status = status;
            Mensaje = mensaje;
        }

        public bool Status { get; set; }
        public string Mensaje { get; set; }
    }
}
