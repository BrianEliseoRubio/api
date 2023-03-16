namespace Webapi.Modelo
{
    public class Mempleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal Salario { get; set; }
        public int IdRol { get; set; }
    }
}
