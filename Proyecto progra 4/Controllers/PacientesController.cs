using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_progra_4.Controllers
{
    public class PacientesController : Controller
    {
        ProyectoEntities bd = new ProyectoEntities(); 
        // GET: Pacientes
        public ActionResult Index()
        {
            List<Models.PacientesModel> List = new List<Models.PacientesModel>();
            List = (from p in bd.Pacientes
                    where p.Eliminado == false
                    select new Models.PacientesModel {
                    IdPacientes =  p.IdPaciente,
                    NombrePaciente = p.NombrePaciente,
                    Cedula = p.Cedula,
                    Tipo_de_Sangre = p.Tipo_de_sangre
                    }).ToList();

            return View(List);
        }


        public ActionResult _Detalle(int id)
        {
                Models.PacientesModel paciente = (from p in bd.Pacientes where p.IdPaciente == id 
                                                  select new Models.PacientesModel { 
                                                  IdPacientes = p.IdPaciente,
                                                  NombrePaciente = p.NombrePaciente,
                                                  Tipo_de_Sangre = p.Tipo_de_sangre,
                                                  Cedula =  p.Cedula
                                                  }).FirstOrDefault();
            return PartialView(paciente);
        }

        public List<Models.ListaModel> PacientesLista()
        {
            List<Models.ListaModel> lista = new List<Models.ListaModel>();
            lista = (from p in bd.Pacientes
                     select new Models.ListaModel
                     {
                         IdPaciente = p.IdPaciente,
                         NombrePaciente = p.NombrePaciente
                     }).ToList();
            return lista;

        }

            public ActionResult Edit(int id)
        {

            Models.PacientesModel Paciente = new Models.PacientesModel();

           Paciente = (from p in bd.Pacientes
                        where p.IdPaciente == id
                        select new Models.PacientesModel
                        {
                            IdPacientes= p.IdPaciente,
                            NombrePaciente= p.NombrePaciente,
                            Cedula=p.Cedula,
                            Tipo_de_Sangre= p.Tipo_de_sangre,   
                 
                        }).FirstOrDefault();

            ViewBag.paciente = PacientesLista();

            return View(Paciente);
        }

        [HttpPost]
        public ActionResult Edit(Models.PacientesModel model)
        {
            if (ModelState.IsValid)
            {
                //GUARDAR EN LA BASE DE DATOS
                //CONSULTA EN LA BASE DE DATOS
                Pacientes paciente = (from a in bd.Pacientes
                                      where a.IdPaciente == model.IdPacientes
                                      select a).FirstOrDefault();
                //MODIFICA LOS ATRIBUTOS
                paciente.NombrePaciente = model.NombrePaciente;
                paciente.Cedula = model.Cedula;
                paciente.Tipo_de_sangre = model.Tipo_de_Sangre;
                paciente.IdCita = model.IdCita; 
                //GUARDA EN LA BASE DE DATOS
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            //RETORNAR VISTA CON MODELO
            ViewBag.Paciente = PacientesLista();
            return View(model);
        }



        public ActionResult Create()
        {
            ViewBag.Pacientes = PacientesLista();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.PacientesModel model)
        {
            if (ModelState.IsValid)
            {
                //GUARDA
                Pacientes paciente = new Pacientes
                {
                    NombrePaciente = model.NombrePaciente,
                    Tipo_de_sangre = model.Tipo_de_Sangre,
                    Cedula = model.Cedula,
                    IdCita = model.IdCita,

                };
                bd.Pacientes.Add(paciente);
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = PacientesLista();
                return View(model);
            }
        }


        [HttpPost]
        public String Delete(int id)
        {
            //TRAE ARTICULO DE LA BASE DE DATOS
            Pacientes paciente = (from a in bd.Pacientes
                                  where a.IdPaciente == id
                                  select a).FirstOrDefault();
            //CAMBAR EL ATRIBUTO DE ELIMINADO
            paciente.Eliminado = true;
            //GUARDA EN LA BASE DE DATOS
            bd.SaveChanges();
            //RETORNA OK
            return ("Index");
        }

    }


}