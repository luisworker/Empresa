using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;


namespace Domain.DataAccess
{
    public class DAServicios
    {
        public List<ServiciosViewModels> getListaServicios(string IdReporte, string Trabajadores, string Estado, string FAlta, string Tramitadora)
        {
            int Estado1 =0;
            List<ServiciosViewModels> list;
            
            List<ServiciosViewModels> listSalida=new List<ServiciosViewModels>();
            using (var db = new EmpresaEntities1())
            {
                //var query = from person in people
                //            join pet in pets on person equals pet.Owner into gj
                //            from subpet in gj.DefaultIfEmpty()
                //            select new { person.FirstName, PetName = subpet?.Name ?? String.Empty };


                list = (from k in db.tbServicio
                        join h in db.tbTrabajador on k.idTrabajador equals h.id into gp from temp in gp.DefaultIfEmpty()
                        join l in db.tbEstado on k.idEstado equals l.id
                        join f in db.tbTramitadora on k.idTramitadora equals f.id
                        select new ServiciosViewModels
                        {
                            Id = k.id,
                            IdEstado = l.id,
                            IdReporte = k.idReporte,
                            IdTrabajador = temp.id,
                            TextTrabajador = temp.Nombre,
                            TextEstado = l.Estado,
                            FAlta = k.FAlta,
                            FModifEstado = k.FModifEstado,
                            Descripcion = k.Descripcion,
                            TextTramitadora = f.Nombre,
                            Vitacora = k.Vitacora,
                            idTramitadora=k.idTramitadora

                        }).ToList();
            }
            if (string.IsNullOrEmpty(IdReporte))
            {
                IdReporte = "";
            }

            if (string.IsNullOrEmpty(Trabajadores))
            {
                Trabajadores = "0";
            }

            int Trabajadores1 = Convert.ToInt32(Trabajadores);
            if (string.IsNullOrEmpty(Estado))
            {
                Estado = "0";
                 
            }
            Estado1 = int.Parse(Estado);

            if (string.IsNullOrEmpty(FAlta))
            {
               FAlta = null;
            }
            
            if (string.IsNullOrEmpty(Tramitadora))
            {
                Tramitadora = "0";
            }
            int Tramitadora1 = int.Parse(Tramitadora);
            if (list.Count() > 0)
            {
                if ((IdReporte == "") && (Estado1 == 0) && (Trabajadores1 == 0) && (FAlta == null) && (Tramitadora1 == 0))
                {
                    listSalida = list;
                    
                    return listSalida;
                }
                else
                {
                    
                    if (IdReporte != "")
                    {
                        var a = (list.Where(ñ => ñ.IdReporte == IdReporte).Select(ñ=>ñ)).FirstOrDefault();
                        if (a!=null)
                        {
                            listSalida.Add(a);
                        }
                       
                        return listSalida;                        
                    }
                    listSalida = list;
                    if (Estado1 != 0)
                    {
                       
                        var a = listSalida.Where(ñ => ñ.IdEstado == Estado1).Select(ñ => ñ).ToList();

                        listSalida=a;
               
                    }
                    if (Trabajadores1 != 0)
                    {
                        
                        var a = listSalida.Where(ñ => ñ.IdTrabajador ==Trabajadores1 ).Select(ñ => ñ).ToList();
                        listSalida = a;
                        
                    }
                    if (FAlta != null)
                    {
                       
                        var a = listSalida.Where(ñ => ñ.FAlta == Convert.ToDateTime(FAlta)).Select(ñ => ñ).ToList();
                        listSalida = a;
                        
                    }
                    if (Tramitadora1 != 0)
                    {
                        
                        var a = listSalida.Where(ñ => ñ.idTramitadora == Tramitadora1).Select(ñ => ñ).ToList();
                        listSalida = a;
                        
                    }

                    return listSalida;
                }
            }

            
            return list;

        }

        public EditarServViewModels getServicio(string IdReporte)
        {
            try
            {
                EditarServViewModels a;
                using (var db=new EmpresaEntities1())
                {
                     a = (from d in db.tbServicio
                          where d.idReporte == IdReporte
                           select new EditarServViewModels
                           {
                               idEstado=d.idEstado,
                               idReporte=d.idReporte,
                               idTrabajador = d.idTrabajador,
                               Descripcion =d.Descripcion
                           }).FirstOrDefault();
                    
                }
                return a;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateServ(AddServicioViewModels a,string rutaserver)
        {
            
            string archivo = a.idReporte + "-" + (DateTime.Now.Date).ToString("yyyy-MM-dd")+".pdf" ;
            
            Stream myStream = a.RutaDocumento.InputStream;
            
            
            byte[] documento = null;
           
            using (MemoryStream ms=new MemoryStream())
            {
                myStream.CopyTo(ms);
                documento=ms.ToArray();
            }
            try
            {   int idTram = int.Parse(a.idTramitadora);
                if (a.IdTrabajadorSII !=null)
                {
                    int idestad =int.Parse(a.idEstadoSII);
                }
               
                using (var db = new EmpresaEntities1())
                {
                    

                    var serv = new tbServicio();
                    serv.idReporte = a.idReporte;
                    if (a.IdTrabajadorSII != null)
                    {
                        serv.idTrabajador = int.Parse(a.IdTrabajadorSII);
                    }
                    
                    serv.Documento = documento;

                    serv.idEstado = int.Parse(a.idEstadoSII);
                  
                    serv.idTramitadora = int.Parse(a.idTramitadora);
                    serv.FAlta = DateTime.Now;
                    serv.Vitacora = "Sin Asignar:" + DateTime.Now.Date.ToString("yyyy-MM-dd")+"\n";
                    if (a.idEstadoSII== "2")
                    {
                        serv.FModifEstado = DateTime.Now.Date;
                        serv.Vitacora += "Asignado:" + DateTime.Now.Date.ToString("yyyy-MM-dd")+"\n";
                    }
                    serv.Descripcion = a.Descripcion;
                    db.tbServicio.Add(serv);
                    db.SaveChanges();

                }
          
               
                
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ServiciosViewModels Detalles(string id,string rutaserver)
        {
            try
            {
                rutaserver = Path.Combine(rutaserver, "Files/Ejemplo.pdf");
                byte[] documento = null;
               
                List<ServiciosViewModels> d = getListaServicios(id,"","","","");
                using (var db = new EmpresaEntities1())
                {
                    documento = (from s in db.tbServicio where s.idReporte == id select s.Documento).FirstOrDefault();
                }
                if (!System.IO.File.Exists(rutaserver))
                {
                    System.IO.File.Create(rutaserver);
                    
                }
                System.IO.File.GetAccessControl(rutaserver);
                System.IO.File.WriteAllBytes(rutaserver, documento);
                return d.First();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
       
        public void UpdateServicio(EditarServViewModels a)
        {
            try
            {
                string vitacora="";
                using (var db=new EmpresaEntities1())
                {
                    var Serv = (from d in db.tbServicio
                                where d.idReporte == a.idReporte
                                   select d).FirstOrDefault();
                    
                    
                    Serv.Descripcion = a.Descripcion;
                    
                    switch (a.idEstado)
                    {
                        case 1: vitacora = "Sin Asignar";
                            break;
                        case 2:
                            vitacora = "Asignado";
                            break;
                        case 3:
                            vitacora = "Realizado";
                            break;
                        case 4:
                            vitacora = "Reclamacion";
                            break;
                        case 5:
                            vitacora = "Pendiente";
                            break;
                        default:
                            break;
                    }
                    if (a.idEstado!=Serv.idEstado)
                    {
                        Serv.FModifEstado = DateTime.Now.Date;
                        Serv.Vitacora += vitacora +":"+ DateTime.Now.Date.ToString("yyyy-MM-dd")+"\n";
                    }
                    if (a.idTrabajador != Serv.idTrabajador)
                    {
                        if (a.idTrabajador!=null)
                        {
                            var ob = new DATrabajador();
                            vitacora = "Trabajador:" + ob.FindNombreTrabajador(a.idTrabajador);
                            Serv.FModifEstado = DateTime.Now.Date;
                            Serv.Vitacora += vitacora + ":" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\n";
                        }
                       
                    }

                    Serv.idEstado = a.idEstado;
                    Serv.idTrabajador = a.idTrabajador;
                    db.Entry(Serv).State=System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DellServicio(string idReporte) 
        {
            using (var db=new EmpresaEntities1())
            {
                var a = (from d in db.tbServicio where d.idReporte == idReporte select d).FirstOrDefault();
                db.tbServicio.Remove(a);
                db.SaveChanges();
            }
        }



    }
 } 
