using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MyDataAnotations
{
    public class ExistElementAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            bool existe = false;

            using (var db = new EmpresaEntities1())
            {
                switch (validationContext.MemberName)
                {
                    case "idReporte":
                        var f = db.tbServicio.Where(d => d.idReporte == value.ToString()).FirstOrDefault();
                        if ( f!= null) 
                            existe = true;
                        break;
                    case "NIF":
                        if (db.tbTrabajador.Where(d => d.NIF == value.ToString()).FirstOrDefault() != null)
                            existe = true;
                        break;
                    case "Nombre":
                        if (db.tbTramitadora.Where(d => d.Nombre == value.ToString()).FirstOrDefault() != null)
                            existe = true;
                        break;
                    case "Estado1":
                        if (db.tbEstado.Where(d => d.Estado == value.ToString()).FirstOrDefault() != null)
                            existe = true;
                        break;
                    default:
                        existe = false;
                        break;

                }

            }
            if (existe)
            {
                return new ValidationResult("Ya Existe");
            }
            return ValidationResult.Success;
        }
    }
}
