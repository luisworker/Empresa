using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model.ViewModels;
using Domain.DataAccess;
using Newtonsoft.Json;

namespace EmpresaWeb.Areas.Supervisor.Controllers
{
    public class MaterialesController : Controller
    {
        // GET: Supervisor/Materiales
        [HttpGet]
        public ActionResult Index()
        {
            var obDAM = new DAMateriales();
            List<MaterialViewModels> lst = obDAM.lstMateriales("", "");
            ViewBag.msj = TempData["mensaje"];
            return View(lst);

        }
        [HttpPost]
        public ActionResult Index(string txtCodigo, string txtNombre)
        {
            var obDAM = new DAMateriales();

            List<MaterialViewModels> lst = obDAM.lstMateriales(txtCodigo, txtNombre);

            return View(lst);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddMatrialViewModels a)
        {
            if (!ModelState.IsValid)
            {
                return View(a);
            }
            var obDAM = new DAMateriales();
            string msj = obDAM.AddMateriales(a);
            @TempData["mensaje"] = msj;
            return RedirectToAction("Index");
        }
        public ActionResult Adicionar(int Codigo)
        {
            var obDAM = new DAMateriales();
            MaterialViewModels Material = obDAM.lstMateriales("" + Codigo, "").First();

            return View(Material);
        }
        [HttpPost]
        public ActionResult Adicionar(MaterialViewModels a, int txtNuevaCant)
        {
            var obDAM = new DAMateriales();
            string msj = obDAM.AddUnidadesMateriales(a, txtNuevaCant);
            TempData["mensaje"] = msj;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Asignar(int Codigo)
        {

            var obDAM = new DAMateriales();
            MaterialViewModels Material = obDAM.lstMateriales("" + Codigo, "").First();

            var obTrab = new DATrabajador();
            List<SelectListItem> lst = obTrab.ListTrabajadores();


            AsignarMatrialViewModels a = new AsignarMatrialViewModels();
            a.Material = Material;
            ViewBag.Trabajadores = lst;

            return View(a);
        }
        [HttpPost]
        public ActionResult Asignar(int Codigo, string sTrabajadores, int cantidad)
        {

            try
            {
                int idtrabajador = int.Parse(sTrabajadores);
                var obDAM = new DAMateriales();
                string msj = obDAM.AsinarMatrial(Codigo, idtrabajador, cantidad);
                TempData["mensaje"] = msj;
                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }

        }
        [HttpPost]
        public ActionResult Retirar(int Codigo, string sTrabajadores, int cantidad)
        {
            try
            {
                int idtrabajador = int.Parse(sTrabajadores);
                var obDAM = new DAMateriales();
                string msj = obDAM.RetirarMatrial(Codigo, idtrabajador, cantidad);
                TempData["mensaje"] = msj;

                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");

            }

        }
        public ActionResult CantidadMaterial(int Codigo, string Trabajador)
        {
            if (Trabajador != "")
            {
                var obTrab = new DATrabajador();
                int id = int.Parse(Trabajador);
                var obDAM = new DAMateriales();
                return Content(obDAM.MaterialAsignado(id, Codigo));


            }

            
            return Content("----");
        }
        [HttpGet]
        public ActionResult Detalles(int Codigo)
        {
            var obDAM = new DAMateriales();
            MaterialViewModels Material = obDAM.lstMateriales("" + Codigo, "").FirstOrDefault();
            
            return View(Material);
        }
        [HttpPost]
        public ActionResult Distribucion(int Codigo)
        {
            var obDAM = new DAMateriales();
            List<TrabajadorMaterialViewModels> Material = obDAM.DistribucionMaterial(Codigo);
            string sJson = JsonConvert.SerializeObject(Material);
            return Content(sJson);
        }
    }
}