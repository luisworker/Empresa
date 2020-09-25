using Domain.MyDataAnotations;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Domain.Model
{
    public class ServiciosViewModels
    {
        public int Id { get; set; }
        [DisplayName("No. Reporte")]
        [ExistElement]
        public string IdReporte { get; set; }
        public Nullable<int> IdTrabajador { get; set; }
        public int IdEstado { get; set; }
        public int Idtramitadora { get; set; }
        [DisplayName("Estado")]
        public string TextEstado { get; set; }
        [DisplayName("Tramitadora")]
        public string TextTramitadora { get; set; }
        public Nullable<System.DateTime> FAlta { get; set; }
        public Nullable<System.DateTime> FModifEstado { get; set; }
        [DisplayName("Operario")]
        public string TextTrabajador { get; set; }
        [DisplayName("Descripción")]
        public  string Descripcion { get; set; }
        
        public Nullable<int> idTramitadora { get; set; }
        public string Vitacora { get; set; }
    }
}
