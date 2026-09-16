namespace Historia.LogicaNegocios.Entidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public Rol()
        {
            Usuarios = new List<Usuario>();
        }
    }
}
