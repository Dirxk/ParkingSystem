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
    }
}
