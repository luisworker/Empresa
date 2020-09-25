using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class TrabajadorViewModels
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NIF { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Roll { get; set; }
    }
}
