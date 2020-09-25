using Domain.MyDataAnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TramitadoraViewModels
    {
        public int id { get; set; }
        [ExistElement]
        public string Nombre { get; set; }
    }
}
