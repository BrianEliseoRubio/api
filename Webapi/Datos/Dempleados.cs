using System.Data;
using System.Data.SqlClient;
using Webapi.Conexion;
using Webapi.Modelo;

namespace Webapi.Datos
{
    public class Dempleados
    {
        Conexionbd cn  = new Conexionbd();

        public decimal total_sueldo_out { get;set; }
        public decimal total_x_entregas_out { get; set; }
        public decimal total_x_bonos_out { get; set; }
        public decimal isr_out { get; set; }
        public decimal vales_out { get; set; }

        public async Task<List<Mempleado>> Mostrarempleados()
        {
            var lista  = new List<Mempleado>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("spObtenerEmpleados", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while(await item.ReadAsync())
                        {
                            var mempleado = new Mempleado();
                            mempleado.Telefono = (string)item["Telefono"];
                            mempleado.Direccion = (string)item["Direccion"];
                            mempleado.Apellido = (string)item["Apellido"];
                            mempleado.Nombre = (string)item["Nombre"];
                            mempleado.Apellido = (string)item["Apellido"];
                            mempleado.IdEmpleado = (int)item["idEmpleado"];
                            lista.Add(mempleado);
                        }
                    }
                }
            }
            return lista;
        }


        public async Task insertarEmpleados(Mempleado parametros)
        {
            using(var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("spInsertarEmpleados", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", parametros.Apellido);
                    cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", parametros.Telefono);
                    cmd.Parameters.AddWithValue("@salario", parametros.Salario);
                    cmd.Parameters.AddWithValue("@idrol", parametros.IdRol);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task updateEmpleados(Mempleado parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("spActualizarEmpleados", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", parametros.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", parametros.Apellido);
                    cmd.Parameters.AddWithValue("@Direccion", parametros.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", parametros.Telefono);
                    cmd.Parameters.AddWithValue("@salario", parametros.Salario);
                    cmd.Parameters.AddWithValue("@idrol", parametros.IdRol);
                    cmd.Parameters.AddWithValue("@idEmpleado", parametros.IdEmpleado);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<List<Mnomina>> obtenerNomida(Mempleado parametros)
        {
            var lista = new List<Mnomina>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("spGeneraNomina", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idEmpleado", parametros.IdEmpleado);
                    cmd.Parameters.AddWithValue("@entregas", parametros.IdRol);
                    // Agregar parámetros de salida para obtener los valores generados por el SP
                    cmd.Parameters.Add("@total_sueldo_out", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@total_x_entregas_out", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@total_x_bonos_out", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@isr_out", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@vales_out", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    // Obtener los valores generados por el SP después de la ejecución
                    total_sueldo_out = (decimal)cmd.Parameters["@total_sueldo_out"].Value;
                    total_x_entregas_out = (decimal)cmd.Parameters["@total_x_entregas_out"].Value;
                    total_x_bonos_out = (decimal)cmd.Parameters["@total_x_bonos_out"].Value;
                    isr_out = (decimal)cmd.Parameters["@isr_out"].Value;
                    vales_out = (decimal)cmd.Parameters["@vales_out"].Value;


                    var mnomina = new Mnomina();
                    mnomina.total_sueldo_out = total_sueldo_out;
                    mnomina.total_x_entregas_out = total_x_entregas_out;
                    mnomina.total_x_bonos_out = total_x_bonos_out;
                    mnomina.isr_out = isr_out;
                    mnomina.vales_out = vales_out;
                    lista.Add(mnomina);

                }
            }
            return lista;
        }
    }
}
