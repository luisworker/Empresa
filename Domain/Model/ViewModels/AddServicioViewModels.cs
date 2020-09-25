using Domain.MyDataAnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Domain.Model
{
    public class AddServicioViewModels
    {
        public int id { get; set; }
        [Required]
        [ExistElement]
        [Display(Name ="No. Reporte")]
        public string idReporte { get; set; }
        [Display(Name = "Trabajador")]
        public string IdTrabajadorSII { get; set; }
        [Required]
        [Display(Name = "Documento")]
        public HttpPostedFileBase RutaDocumento { get; set; }
        [Display(Name = "Estado")]
        public string idEstadoSII { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime FAlta { get; set; }
        [DataType(DataType.Date)]
        //[Compare("FAlta","ttrer")]
        public Nullable<System.DateTime> FModifEstado { get; set; }
        public Nullable<int> idTrabImg { get; set; }
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        [Required]
        public string idTramitadora { get; set; }
        
        public string Vitacora { get; set; }
    }
}
