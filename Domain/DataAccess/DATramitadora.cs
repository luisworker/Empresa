using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.DataAccess
{
    public class DATramitadora
	{
		public List<SelectListItem> Tramitadoras() {
			var list = new List<SelectListItem>();
			try 
			{	        
				using(var db= new EmpresaEntities1())
				{
					var f= db.tbTramitadora.ToList();
					foreach (var item in f)
					{
						list.Add(new SelectListItem { Value = item.id.ToString(), Text = item.Nombre });
					}
				
				}
				return list;
			}
			catch (global::System.Exception)
			{

			throw;
			}
		}
		public List<SelectListItem> Tramitadoras(int IdTramitadora)
		{
			var list = new List<SelectListItem>();
			try
			{
				using (var db = new EmpresaEntities1())
				{
					var f = db.tbTramitadora.ToList();
					foreach (var item in f)
					{
						if (item.id==IdTramitadora)
						{
							list.Add(new SelectListItem { Value = item.id.ToString(), Text = item.Nombre, Selected = true });
						}
						else
						{
							list.Add(new SelectListItem { Value = item.id.ToString(), Text = item.Nombre, Selected = false });
						}
						
					}

				}
				return list;
			}
			catch (global::System.Exception)
			{

				throw;
			}
		}
		public string FindNombreTramitadora(int idTramitadora)
		{
			try
			{
				string a = "";
				using (var db = new EmpresaEntities1())
				{
					a = db.tbTramitadora.Find(idTramitadora).Nombre;
				}
				return a;
			}
			catch (Exception)
			{

				throw;

			}
		}
		public int FindIdTramitadora(string Nombre)
		{
			try
			{
				int a = 0;
				using (var db = new EmpresaEntities1())
				{
					a = db.tbTramitadora.Where(d => d.Nombre == Nombre)
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
