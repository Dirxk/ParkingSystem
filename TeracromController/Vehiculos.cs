using TeracromDatabase;
using TeracromModels;

namespace TeracromController
{
    public class Vehiculos
    {
        private readonly IDatabaseService _databaseService;
        public Vehiculos(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<RespuestaJson> GetUsuarios()
        {

            RespuestaJson respuestaUser = new RespuestaJson();
            try
            {
                string sql = "SELECT IdUsuario, Nombre, Apellido, Correo, Contrasena, Imagen FROM Usuarios;";
                var User = await _databaseService.QueryAsync<dynamic>(sql);
                respuestaUser.resultado = true;
                respuestaUser.data = User.Select(s => new Vehiculo
                {
                    IdVehiculo = s.IdVehiculo,
                    Placas = s.Placas,
                    Tipo = s.Tipo,
                }).ToList();
            }
            catch (Exception ex)
            {
                respuestaUser.mensaje = "Ocurrió un error al obtener los datos del vehículo." + ex.Message;
            }
            return respuestaUser;
        }


        public async Task<RespuestaJson> VerificarUsuario(string email, string password)
        {
            RespuestaJson respuestaUser = new RespuestaJson();
            try
            {
                // Consulta SQL para buscar el usuario por correo
                string sql = "SELECT IdUsuario, Nombre, Apellido, Correo, Contrasena FROM Usuarios WHERE Correo = @Correo";
                var user = await _databaseService.QueryFirstOrDefaultAsync<dynamic>(sql, new { Correo = email });

                // Validar si el usuario existe
                if (user == null)
                {
                    respuestaUser.resultado = false;
                    respuestaUser.mensaje = "El usuario no existe.";
                    return respuestaUser;
                }

                // Verificar contraseña (puedes usar hashing aquí si es necesario)
                if (user.Contrasena != password)
                {
                    respuestaUser.resultado = false;
                    respuestaUser.mensaje = "Contraseña incorrecta.";
                    return respuestaUser;
                }

                // Si las credenciales son válidas
                respuestaUser.resultado = true;
                respuestaUser.mensaje = "Inicio de sesión exitoso.";
                respuestaUser.data = new
                {
                    IdUsuario = user.IdUsuario,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Correo = user.Correo
                };
                respuestaUser.data2 = "Información adicional si es necesaria"; // Ejemplo, puedes adaptarlo según tu caso
            }
            catch (Exception ex)
            {
                respuestaUser.resultado = false;
                respuestaUser.mensaje = "Ocurrió un error al verificar las credenciales.";
                respuestaUser.error.Add(ex.Message); // Añadir detalles del error
            }

            return respuestaUser;
        }





        public async Task<RespuestaJson> GetVehiculo()
        {
            
            RespuestaJson respuesta = new RespuestaJson();
            try
            {
                string sql = "SELECT IdVehiculo, Placas,Tipo FROM Vehiculo;";
                var vehiculo = await _databaseService.QueryAsync<dynamic>(sql);
                respuesta.resultado = true;
                respuesta.data = vehiculo.Select(s => new Vehiculo
                {
                    IdVehiculo = s.IdVehiculo,
                    Placas = s.Placas,
                    Tipo = s.Tipo,
                }).ToList();
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Ocurrió un error al obtener los datos del vehículo." + ex.Message;
            }
            return respuesta;
        }

        public async Task<RespuestaJson> GetEntradasSalidas()
        {

            RespuestaJson respuestaES = new RespuestaJson();
            try
            {
                string sql = "SELECT IdEntrada_Salida,FechaEntrada, FechaSalida, PuertaAcceso, PuertaSalida, Folio, TotalPagar, UsuarioEntrada, UsuarioSalida FROM Entradas_Salidas;";
                var EntradasSalidas = await _databaseService.QueryAsync<dynamic>(sql);
                respuestaES.resultado = true;
                respuestaES.data = EntradasSalidas.Select(s => new EntradasSalidas
                {
                    IdEntrada_Salida = s.IdEntrada_Salida,
                    FechaEntrada = s.FechaEntrada,
                    FechaSalida = s.FechaSalida,
                    PuertaAcceso = s.PuertaAcceso,
                    PuertaSalida = s.PuertaSalida,
                    Folio = s.Folio,
                    TotalPagar = s.TotalPagar,
                    UsuarioEntrada = s.UsuarioEntrada,
                    UsuarioSalida = s.UsuarioSalida,
                }).ToList();
            }
            catch (Exception ex)
            {
                respuestaES.mensaje = "Ocurrió un error al obtener los datos de las entradas y salidas." + ex.Message;
            }
            return respuestaES;
        }

    }
}
