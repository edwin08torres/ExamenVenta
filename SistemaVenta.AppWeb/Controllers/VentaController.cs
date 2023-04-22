using Microsoft.AspNetCore.Mvc;

namespace SistemaVentas.AppWeb.Controllers
{

    using AutoMapper;
    using SistemaVentas.AppWeb.Models.ViewModels;
    using SistemaVentas.AppWeb.Utilidades.Response;
    using SistemaVentas.BLL.Interfaces;
    using SistemaVentas.Entity;

    public class VentaController : Controller
    {
        private readonly ITipoDocumentoVentaService _documentoVentaService;
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;

        public VentaController(ITipoDocumentoVentaService documentoVentaService, IVentaService ventaService, IMapper mapper)
        {
            _documentoVentaService = documentoVentaService;
            _ventaService = ventaService;
            _mapper = mapper;
        }

        //public IActionResult NuevaVenta()
        //{
        //    return View();
        //}

        //public IActionResult HistorialVenta()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoVenta> vmListaTipoDocumento = _mapper.Map<List<VMTipoDocumentoVenta>>(await _documentoVentaService.Lista());
            return StatusCode(StatusCodes.Status200OK, vmListaTipoDocumento);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper.Map<List<VMProducto>>(await _ventaService.ObtenerProducto(busqueda));
            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody]VMVenta modelo)
        {
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();

            try
            {
                modelo.IdUsuario = 22;

                Venta venta_creada = await _ventaService.Registrar(_mapper.Map<Venta>(modelo));
                modelo = _mapper.Map<VMVenta>(venta_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch(Exception ex)
            {
                gResponse.Estado = false;
                //gResponse.Mensaje = ex.Message;
                gResponse.Mensaje = ex.InnerException.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);

        }

        [HttpGet]
        public async Task<IActionResult> Historial(string numeroVenta, string fechaInicio, string fechaFin)
        {
           
            List<VMVenta> vmHistorialVenta = _mapper.Map<List<VMVenta>>(await _ventaService.Historial(numeroVenta, fechaInicio, fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialVenta);

        }

    }

}

