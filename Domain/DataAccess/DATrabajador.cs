using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.DataAccess
{
    public class DATrabajador
    {
        public List<SelectListItem> ListTrabajadores()
        {
			try
			{
				//var asd;
				List<SelectListItem> list = new List<SelectListItem>();
				using (var db=new EmpresaEntities1())
				{
					var trabaja = db.tbTrabajador.ToList();
					
					foreach (var item in trabaja)
					{

						list.Add(new SelectListItem { Value = item.id.ToString(), Text = (item.Nombre + " " + item.Apellido) });

					}
				}
				return list;

			}
			catch (Exception)
			{

				throw;
			}
        }
		public List<SelectListItem> ListTrabajadores(int? idSelected)
		{
			try
			{
				List<SelectListItem> list = new List<SelectListItem>();
				using (var db = new EmpresaEntities1())
				{
					var trabaja = db.tbTrabajador.ToList();

					foreach (var item in trabaja)
					{
						if (item.id == idSelected)
						{
							list.Add(new SelectListItem { Value = item.id.ToString(), Text = (item.Nombre + " " + item.Apellido), Selected = true });
						}
						else
						{
							list.Add(new SelectListItem { Value = item.id.ToString(), Text = (item.Nombre + " " + item.Apellido), Selected = false });
						}
					}
				}
				return list;

			}
			catch (Exception)
			{

				throw;
			}
		}
		public string FindNombreTrabajador(int? idTrabajador)
		{
			try
			{
				string a = "";
				using (var db = new EmpresaEntities1())
				{
					a = db.tbTrabajador.Find(idTrabajador).Nombre;
				}
				return a;
			}
			catch (Exception)
			{

				throw;

			}
		}
		public int FindIdTrabajador(string Nombre)
		{
			try
			{
				int a = 0;
				using (var db = new EmpresaEntities1())
				{
					a = db.tbTrabajador.Where(d => d.Nombre == Nombre)
						.Select(d => d.id).FirstOrDefault();
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
