using Microsoft.AspNetCore.Mvc;
using TeracromController;
using TeracromDatabase;
using TeracromModels;

namespace ParkingSystem.Controllers
{
    public class ParkingSystemController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public ParkingSystemController(IDatabaseService databaseService)
        {
           _databaseService = databaseService;
        }

        [HttpPost]
        public async Task<RespuestaJson> GetVehiculo()
        {
            RespuestaJson respuesta = await new Vehiculos(_databaseService).GetVehiculo();
            return respuesta;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<RespuestaJson> GetEntradasSalidas()
        {
            RespuestaJson respuestaES = await new Vehiculos(_databaseService).GetEntradasSalidas();
            return respuestaES;
        }

        [HttpPost]
        public async Task<RespuestaJson> GetUsuarios()
        {
            RespuestaJson respuestaUser = await new Vehiculos(_databaseService).GetUsuarios();
            return respuestaUser;
        }

        [HttpPost]
        public async Task<RespuestaJson> VerificarUsuario(string email, string password)
        {
            RespuestaJson respuestaUser = new RespuestaJson();
            try
            {
                respuestaUser = await new Vehiculos(_databaseService).VerificarUsuario(email, password);
            }
            catch (Exception ex)
            {
                respuestaUser.resultado = false;
                respuestaUser.mensaje = "Ocurrió un error inesperado: " + ex.Message;
                respuestaUser.error.Add(ex.Message); // Añadimos el error a la lista
            }

            return respuestaUser;
        }


        [HttpPost]
        public async Task<RespuestaJson> RegistrarUsuario([FromForm] Usuario usuario, IFormFile foto)
        {
            return await new Vehiculos(_databaseService).RegistrarUsuario(usuario, foto);
        }




    }
}
