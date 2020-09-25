using Domain.Model;
using Domain.Model.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public class DAMateriales
    {
        public List<MaterialViewModels> lstMateriales(string codigo, string nombreMaterial) 
        {
            List<MaterialViewModels> lst = new List<MaterialViewModels>();
            try
            {
                using (var db=new EmpresaEntities1())
                {
                    lst = (from d in db.tbMaterialesAsignados

                           group d by d.idMaterial into fp
                           select new MaterialViewModels
                           {
                               
                               Codigo= (db.tbMateriales.Where(f => f.id == fp.Key).Select(f => f.Codigo)).FirstOrDefault(),
                               Nombre = (db.tbMateriales.Where(f => f.id == fp.Key).Select(f => f.Nombre)).FirstOrDefault(),
                               CantDisponible = fp.Where(x => x.idTrabajador == null).Select(x => x.Cantidad).FirstOrDefault() + 0,
                               Descripcion = (db.tbMateriales.Where(f => f.id == fp.Key).Select(f => f.Descripcion)).FirstOrDefault(),

                               CantTotal = fp.Sum(x => x.Cantidad)
                           }

                           ).ToList();
                }

                if (string.IsNullOrEmpty(codigo))
                {
                    codigo = "0";
                }
                int Id = int.Parse(codigo);
                if (string.IsNullOrEmpty(nombreMaterial))
                {
                    nombreMaterial = "";
                }


                if (Id!=0||nombreMaterial!="")
                { 
                    if (Id!=0)
                    {
                        lst = (from d in lst where d.Codigo == Id select d).ToList();
                        return lst;
                    }
                    if (nombreMaterial!="")
                    {
                        lst=(from d in lst where d.Nombre.ToLower().Contains(nombreMaterial.ToLower())|| d.Descripcion.ToLower().Contains(nombreMaterial.ToLower()) select d).ToList();
                        return lst;
                    }

                }
                return lst;
                
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public string AddUnidadesMateriales(MaterialViewModels material, int cantidad)
        {
            try
            {
                using (var db = new EmpresaEntities1())
                {
                    var a = (from d in db.tbMateriales
                             join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                             where d.Codigo == material.Codigo && f.idTrabajador == null
                             select f).FirstOrDefault();
                    a.Cantidad += cantidad;
                    a.Fecha = DateTime.Now.Date;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return "Se adicionó material correctamente";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RetirarMatrial(int codigo, int idtrabajador, int cantidad)
        {
            
            try
            {
                using (var db = new EmpresaEntities1())
                {
                    var a = (from d in db.tbMateriales
                             join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                             where d.Codigo == codigo && f.idTrabajador == idtrabajador
                             select f).FirstOrDefault();
                    var b = (from d in db.tbMateriales
                            join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                            where d.Codigo == codigo && f.idTrabajador == null
                            select f).FirstOrDefault();
                    
                    a.Cantidad -= cantidad;
                    b.Cantidad += cantidad;
                    a.Fecha = DateTime.Now.Date;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                return "Se asignó material correctamente";
            }
            catch (Exception)
            {
                return "Problemas al retirar material";
                throw;
            }
        }

        public List<TrabajadorMaterialViewModels> DistribucionMaterial(int codigo)
        {
            List<TrabajadorMaterialViewModels> a;
            try
            {
                using (var db = new EmpresaEntities1())
                {
                    a = (from d in db.tbMateriales
                         join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                         join j in db.tbTrabajador on f.idTrabajador equals j.id
                         where d.Codigo == codigo
                         orderby j.Nombre
                         select new TrabajadorMaterialViewModels {
                             Nombre = j.Nombre,
                             Cantidad = f.Cantidad

                         }).ToList();
                }
                
                
            }
            catch
            {
                
                throw;
            }
            return a;
        }

        public string AsinarMatrial(int codigo, int trabajador, int cantidad)
        {
            tbMaterialesAsignados g;
            try
            {
                using (var db = new EmpresaEntities1())
                {
                    var a = (from d in db.tbMateriales
                             join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                             where d.Codigo == codigo && f.idTrabajador == null
                             select f).FirstOrDefault();
                    var b= from d in db.tbMateriales
                            join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                            where d.Codigo == codigo && f.idTrabajador == trabajador
                            select f;
                    if (b.Count()>0)
                    {
                        g = b.FirstOrDefault();
                        g.Cantidad += cantidad;

                    }
                    else
                    {
                        g = new tbMaterialesAsignados();
                        g.Cantidad = cantidad;
                        g.Fecha = DateTime.Now.Date;
                        g.idMaterial = a.idMaterial;
                        g.idTrabajador = trabajador;
                        
                        db.tbMaterialesAsignados.Add(g);
                    }
                    a.Cantidad -= cantidad;
                    a.Fecha = DateTime.Now.Date;
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                   
                }
                return "Se asignó material correctamente";
            }
            catch (Exception)
            {
                return "Problemas al asignar material";
                throw;
            }
        }

        public string AddMateriales(AddMatrialViewModels a)
        {
            try
            {
                using (var db=new EmpresaEntities1())
                {
                    tbMaterialesAsignados matAsig = new tbMaterialesAsignados();
                    tbMateriales mat = new tbMateriales();
                    mat.Codigo = a.Codigo;
                    mat.Descripcion = a.Descripcion;
                    mat.Nombre = a.Nombre;
                    db.tbMateriales.Add(mat);
                    db.SaveChanges();
                    matAsig.Fecha = DateTime.Now.Date;
                    matAsig.Cantidad = a.Cantidad;
                    matAsig.idMaterial = db.tbMateriales.Where(f => f.Codigo == a.Codigo).Select(f => f.id).FirstOrDefault();
                    db.tbMaterialesAsignados.Add(matAsig);
                    db.SaveChanges();
                    return "Se adicionó un nuevo material correctamente";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string MaterialAsignado(int idTRabajador,int codigo) 
        {
            int a = -1;
            try
            {
                using (var db=new EmpresaEntities1())
                {
                    a = (from d in db.tbMateriales
                             join f in db.tbMaterialesAsignados on d.id equals f.idMaterial
                             where d.Codigo == codigo && f.idTrabajador == idTRabajador
                             select f.Cantidad).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return a.ToString();
        }
    }
}
