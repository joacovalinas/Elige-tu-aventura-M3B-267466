namespace EscogeAventura.Cliente.Models
{
    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string token { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
    }
}
