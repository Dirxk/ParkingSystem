using Dapper;
using Microsoft.AspNetCore.Http;
using System.Data;
using TeracromDatabase;
using TeracromModels;
using static System.Net.Mime.MediaTypeNames;

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
                respuestaUser.data = User.Select(s => new Usuario
                {
                    IdUsuario = s.IdUsuario,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Correo = s.Correo,
                    Contrasena = s.Contrasena,
                    Imagen = s.Imagen
                }).ToList();
            }
            catch (Exception ex)
            {
                respuestaUser.mensaje = "Ocurrió un error al obtener los datos del Usuario." + ex.Message;
            }
            return respuestaUser;
        }

        public async Task<RespuestaJson> VerificarUsuario(string email, string password)
        {
            RespuestaJson respuestaUser = new RespuestaJson();
            try
            {
                // Consulta SQL para validar usuario con hash de la contraseña
                string sql = @"
            SELECT IdUsuario, Nombre, Apellido, Correo 
            FROM Usuarios 
            WHERE Correo = @Correo 
            AND Contrasena = HASHBYTES('SHA2_256', CONVERT(NVARCHAR(250), @Contrasena))";

                var user = await _databaseService.QueryFirstOrDefaultAsync<dynamic>(sql, new { Correo = email, Contrasena = password });

                // Validar si el usuario existe
                if (user == null)
                {
                    respuestaUser.resultado = false;
                    respuestaUser.mensaje = "Usuario o contraseña incorrectos.";
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
            }
            catch (Exception ex)
            {
                respuestaUser.resultado = false;
                respuestaUser.mensaje = "Ocurrió un error al verificar las credenciales.";
                respuestaUser.error.Add(ex.Message);
            }

            return respuestaUser;
        }




        public async Task<RespuestaJson> RegistrarUsuario(Usuario usuario, IFormFile foto)
        {
            RespuestaJson respuesta = new RespuestaJson();

            try
            {
                byte[] imagenBytes = null;

                if (foto != null && foto.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await foto.CopyToAsync(ms);
                        imagenBytes = ms.ToArray();
                    }
                }

                string sql = @"
                INSERT INTO Usuarios (Nombre, Apellido, Correo, Contrasena, Imagen) 
                VALUES (@Nombre, @Apellido, @Correo, 
                        HASHBYTES('SHA2_256', CONVERT(NVARCHAR(250), @Contrasena)), @Imagen);";

                var parametros = new DynamicParameters();
                parametros.Add("@Nombre", usuario.Nombre, DbType.String);
                parametros.Add("@Apellido", usuario.Apellido, DbType.String);
                parametros.Add("@Correo", usuario.Correo, DbType.String);
                parametros.Add("@Contrasena", usuario.Contrasena, DbType.String);
                parametros.Add("@Imagen", imagenBytes, DbType.Binary);

                int filasAfectadas = await _databaseService.ExecuteAsync(sql, parametros);

                if (filasAfectadas > 0)
                {
                    respuesta.resultado = true;
                    respuesta.mensaje = "Usuario registrado exitosamente.";
                }
                else
                {
                    respuesta.mensaje = "No se pudo registrar el usuario.";
                }
            }
            catch (Exception ex)
            {
                respuesta.mensaje = "Ocurrió un error al registrar el usuario: " + ex.Message;
            }

            return respuesta;
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
