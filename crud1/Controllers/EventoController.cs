using crud1.Dao;
using crud1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud1.Controllers
{
    public class EventoController : Controller
    {
        public EventoDao EDao = new EventoDao();
        public UsuarioDao Udao = new UsuarioDao();

        // GET: Agenda
        public ActionResult List(string data)
        {
            List<Evento> eventoList;
            eventoList = EDao.GetEventoList();
            ViewBag.eventoList = eventoList;
            return View();
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            Evento e = EDao.GetEventosById(id);
            List<Usuario> userList = Udao.getUserListByEventoId(id);
            ViewBag.Evento = e;
            ViewBag.UserList = userList;
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update(long id)
        {
            Evento e = EDao.GetEventosById(id);
            ViewBag.Evento = e;
            return View();
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            EDao.deleteEvento(id);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Save(FormCollection form)
        {

            Evento e = new Evento();
            Categoria c = new Categoria();
            string d = form["Data"] != null ? form["Data"] : null;
            string horas = ((int)Int32.Parse(form["Hora"])) < 10 ? "0" + form["Hora"] : form["Hora"];
            string min = (form["zero"] != null && !form["zero"].Equals("")) ? form["zero"] : form["trinta"];
            e.Descricao = form["Descricao"];
            e.Observacao = form["Observacao"];
            c.Id = (int)Int32.Parse(form["CategoriaId"]);
            e.Categoria = c;
            if (min == null) min = "00";
            if (d != null && !d.Equals(""))
            {
                string date = d + " "+ horas + ":" + min;
                DateTime dt = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm", null);
                e.Data = dt;
            }

            if (form["Id"] != "")
            {
                int id = (int)Int32.Parse(form["Id"]);
                if (id != 0)
                {
                    e.Id = (int)Int32.Parse(form["Id"]);
                    EDao.updateEvento(e);
                }
            }
            else
            {
                EDao.insertEvento(e);
            }

            return RedirectToAction("List");
        }

    }
}