using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model.ViewModels;
using Domain.DataAccess;

namespace EmpresaWeb.Areas.Supervisor.Controllers
{
    public class SupervisorController : Controller
    {
        //    // GET: Supervisor/Supervisor
        [HttpGet]
        public ActionResult Index()
        {
            DATrabajador obTrabajador = new DATrabajador();
            var obEstados = new DAEstados();
            DATramitadora t = new DATramitadora();

            ViewBag.Estados = obEstados.listaEstados();
            ViewBag.Trabajadores = obTrabajador.ListTrabajadores();
            ViewBag.Tramitadora = t.Tramitadoras();

            if (TempData["mensaje"]!=null)
            {
                ViewBag.msj = TempData["mensaje"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string IdReporte, string Trabajadores, string Estado, string FAlta, string idTramitadora)
        {
            DATrabajador obTrabajador = new DATrabajador();
            var obEstados = new DAEstados();
            DATramitadora t = new DATramitadora();

            ViewBag.Estados = obEstados.listaEstados();
            ViewBag.Trabajadores = obTrabajador.ListTrabajadores();
            ViewBag.Tramitadora = t.Tramitadoras();

            var obDAservicios = new DAServicios();

            List<ServiciosViewModels> Serv = obDAservicios.getListaServicios(IdReporte, Trabajadores, Estado, FAlta, idTramitadora);
            TempData["mensaje"] = "";
            return View(Serv);


        }

        [HttpGet]
        public ActionResult Detalles(string IdReporte)
        {
            string rutaserver = Server.MapPath("~/");
            ServiciosViewModels Serv = new ServiciosViewModels();
            if (IdReporte != null)
            {
                var obServ = new ServiciosViewModels();

                IdReporte = IdReporte.Trim();

                DAServicios obDAservicios = new DAServicios();
                DATrabajador obTrabajador = new DATrabajador();
                ViewBag.Trabajadores = obTrabajador.ListTrabajadores();
                Serv = obDAservicios.Detalles(IdReporte,rutaserver);

            }
            else
            {
                Serv.Descripcion = "Error";
            }
            return View(Serv);


        }

        [HttpGet]
        public ActionResult CrearServicio()
        {
            DATramitadora t = new DATramitadora();
            DATrabajador c = new DATrabajador();
            DAEstados b = new DAEstados();
            ViewBag.Tramitadora = t.Tramitadoras();
            ViewBag.Trabajadores = c.ListTrabajadores();
            ViewBag.Estados = b.listaEstados(1);

            return View();
        }

        [HttpPost]
        public ActionResult CrearServicio(AddServicioViewModels a)
        {
            string rutaserver = Server.MapPath("~/");
            if (!ModelState.IsValid)
            {
                DATramitadora t = new DATramitadora();
                DATrabajador c = new DATrabajador();
                DAEstados b = new DAEstados();


                ViewBag.Tramitadora = t.Tramitadoras();
                ViewBag.Trabajadores = c.ListTrabajadores();
                ViewBag.Estados = b.listaEstados();
                return View(a);
            }
           
            var obDAServ = new DAServicios();
            obDAServ.CreateServ(a, rutaserver);
            TempData["mensaje"] = "Se Adicionó correctamente";
            return RedirectToAction("IndexServicio");
                
           

        }

        [HttpGet]
        public ActionResult EditarServicio(string idReporte)
        {
             
            var ADserv = new DAServicios();
            var oServicio = ADserv.getServicio(idReporte);



            DATrabajador c = new DATrabajador();
            DAEstados b = new DAEstados();

            ViewBag.Trabajadores = c.ListTrabajadores(oServicio.idTrabajador);
            ViewBag.Estados = b.listaEstados(oServicio.idEstado);
            
            oServicio.idReporte = idReporte;


            return View(oServicio);
        }

        [HttpPost]
        public ActionResult EditarServicio(EditarServViewModels a)
        {

            DATramitadora t = new DATramitadora();
            DATrabajador c = new DATrabajador();
            DAEstados b = new DAEstados();

            if (!ModelState.IsValid)
            {
                ViewBag.Trabajadores = c.ListTrabajadores(a.idTrabajador);
                ViewBag.Estados = b.listaEstados(a.idEstado);

                return View(a);

            }
            
            var obServ = new DAServicios();
            obServ.UpdateServicio(a);
            ViewBag.Estados =b.listaEstados();
            ViewBag.Trabajadores =c.ListTrabajadores();
            ViewBag.Tramitadora = t.Tramitadoras();
            TempData["mensaje"] = "Se ha actualizado el elemento";

            return RedirectToAction("IndexServicio");
        }

        [HttpPost]
        public ActionResult Eliminar(string idReporte)
        {
            idReporte = idReporte.Trim();
            try
            {
                DAServicios obDAServ = new DAServicios();
                //obDAServ.DellServicio(idReporte);

            }
            catch (Exception)
            {

                throw;
            }
            TempData["mensaje"] = "Correctamente Eliminado";

            return RedirectToAction("IndexServicio");
        }
    }
}