using Domain.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;


namespace Domain.DataAccess
{
    public class DAEstados
    {
        public List<SelectListItem> listaEstados()
        {
            try
            {
                var list = new List<SelectListItem>();
                using (var db = new EmpresaEntities1())
                {

                    foreach (var item in db.tbEstado)
                    {
                        list.Add(new SelectListItem { Value = item.id.ToString(), Text = item.Estado });
                    }
                       
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> listaEstados(int idSelected)
        {
            try
            {
                var list = new List<SelectListItem>();
                using (var db = new EmpresaEntities1())
                {
                    var h = db.tbEstado.ToList();
                    for (int i = idSelected - 2; i < idSelected +1; i++)
                    {


                        if (i>-1 && h.ElementAt(i).id == idSelected)
                        {
                            list.Add(new SelectListItem { Value = h.ElementAt(i).id.ToString(), Text = h.ElementAt(i).Estado, Selected = true });
                        }
                        else
                        {
                            if (i>-1 && i<h.Count)
                            {
                                list.Add(new SelectListItem { Value = h.ElementAt(i).id.ToString(), Text = h.ElementAt(i).Estado, Selected = false });
                            }
                            
                        }
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string FindNombreEstado(int idEstado)
        {
            try
            {
                string a = "";
                using (var db = new EmpresaEntities1())
                {
                    a = db.tbEstado.Find(idEstado).Estado;
                }
                return a;
            }
            catch (Exception)
            {

                throw;

            }
        }
        public int FindIdEstado(string Estado)
        {
            try
            {
                int a = 0;
                using (var db = new EmpresaEntities1())
                {
                    a = db.tbEstado.Where(d=>d.Estado==Estado)
                        .Select(d=>d.id).FirstOrDefault();
                }
                return a;
            }
            catch (Exception)
            {

                throw;

            }
        }
    }
            
    
    
}
