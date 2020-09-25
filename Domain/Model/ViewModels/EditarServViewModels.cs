using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class EditarServViewModels
    {
        

            public string idReporte { get; set; }
            public Nullable<int> idTrabajador { get; set; }
            public int idEstado { get; set; }
            public string Descripcion { get; set; }
        
    }
}
