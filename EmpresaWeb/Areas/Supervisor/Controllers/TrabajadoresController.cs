using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpresaWeb.Areas.Supervisor.Controllers
{
    public class TrabajadoresController : Controller
    {
        // GET: Supervisor/Trabajadores
        public ActionResult Index()
        {
            return View();
        }
    }
}