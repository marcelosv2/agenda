using crud1.Dao;
using crud1.Models;
using crud1.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud1.Controllers
{
    public class AgendaController : Controller
    {
        public EventoDao EDao= new EventoDao();
        public UsuarioDao Udao = new UsuarioDao();

        // GET: Agenda
        public ActionResult Index(string data)
        {
            List<Evento> eventoList;
            Dictionary<string, Evento> EventosHash = new Dictionary<string, Evento>();
            for (int i = 0; i < 24; i++)
            {
                string key1 = (i < 10) ? "0"+ i + ":00" : i + ":00";
                string key2 = (i < 10) ? "0" + i + ":30" : i + ":30";
                EventosHash[key1] = null;
                EventosHash[key2] = null;
            }

            if (data != null && !data.Equals("")) {
                eventoList = EDao.GetEventosByData(data);
                foreach (Evento e in eventoList) {
                   EventosHash[String.Format("{0:t}", e.Data)] = e;
                }
                ViewBag.EventoList = eventoList;
            }
            ViewBag.EventosHash = EventosHash;
            return View();
        }

        [HttpPost]
        public ActionResult Mail(FormCollection form) {
            try {
                string ids = form["usuarioId"];
                long eventoId = Convert.ToInt64(form["EventoId"]) ;
                Evento e = EDao.GetEventosById(eventoId);
                List<Usuario> userList = Udao.getUserListByIds(ids);
                SendMail sm = new SendMail();
                sm.send(userList, e);
                UsuarioEventoDao UVDao = new UsuarioEventoDao();
                foreach (Usuario u in userList) {
                    UVDao.insertUsuarioEvento(u.Id,eventoId);
                }
            } catch (Exception e) {
                //e.StackTrace();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Show(long id)
        {
            Evento e = EDao.GetEventosById(id);
            List<Usuario> userList = Udao.getUserListByEventoId(id);
            List<Usuario> userNcList = Udao.getUserListNaoConvidadosByEventoId(id);
            ViewBag.Evento = e;
            ViewBag.UserList = userList;
            ViewBag.UserNcList = userNcList;
            return View();
        }

        [HttpPost]
        public ActionResult Save(FormCollection form) {

            Evento e = new Evento();
            Categoria c = new Categoria();
            string d = form["Data"] != null ? form["Data"] : null;
            e.Descricao = form["Descricao"];
            e.Observacao = form["Observacao"];
            c.Id = (int)Int32.Parse(form["CategoriaId"]);
            e.Categoria = c;

            if (d != null && !d.Equals(""))
            {
                DateTime dObj = DateTime.Parse(d);
                e.Data = dObj;
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

            return RedirectToAction("Index");
        }

    }
}