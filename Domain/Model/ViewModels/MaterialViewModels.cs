using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.ViewModels
{
    public class MaterialViewModels
    {
        [Display(Name ="Codigo")]
        public int Codigo { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Disponibles")]
        public int CantDisponible { get; set; }
        [Display(Name = "Total")]
        public int CantTotal { get; set; }
    }
}
