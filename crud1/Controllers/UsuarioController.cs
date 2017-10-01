using crud1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crud1.Dao;
using crud1.Util;

namespace crud1.Controllers
{
    public class UsuarioController : Controller
    {

        public UsuarioDao Udao = new UsuarioDao();
        
        // GET: Usuario
        public ActionResult List(string nome,  string page)
        {
            if (nome != null)
            {
                //List<Usuario> userList = Udao.getUserListByName(nome);
                //return View(userList);
                int max = Udao.getUserListByNamePageCount(nome);
                int offset = 0;
                if (page != null)
                {
                    offset = (int)Int32.Parse(page) != 0 ? ((int)Int32.Parse(page) - 1) * Constants.N_ROWS : 0;
                }


                List<Usuario> userList = Udao.getUserListByNamePage(offset,nome);
                int nofpages = max / Constants.N_ROWS;
                int resto = max % Constants.N_ROWS;
                if (resto > 0) nofpages++;
                ViewBag.Nome = nome;
                ViewBag.Max = max;
                ViewBag.NOfPages = nofpages;
                ViewBag.Page = page != null ? (int)Int32.Parse(page) : 0;
                return View(userList);

            }
            else {
                //List<Usuario> userList = Udao.getUserList();
                //return View(userList);

                int max = Udao.getUserListPageCount();
                int offset = 0;
                if (page != null)
                {
                    offset = (int)Int32.Parse(page) != 0 ? ((int)Int32.Parse(page) - 1) * Constants.N_ROWS : 0;
                }
                
                List<Usuario> userList = Udao.getUserListPage(offset);
                int nofpages = max / Constants.N_ROWS;
                int resto = max % Constants.N_ROWS;
                if (resto > 0) nofpages++;
                ViewBag.Nome = null;
                ViewBag.Max = max;
                ViewBag.NOfPages = nofpages;
                ViewBag.Page = page != null ? (int)Int32.Parse(page) : 0;
                return View(userList);
            }
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            Usuario user = Udao.getUserById(id);
            ViewBag.User = user;
            return View(user);
        }

        [HttpGet]
        public ActionResult Create()
        {
            //string id = Request.QueryString["id"];
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                Usuario user = Udao.getUserById(id);
                ViewBag.User = user;
            }
            return View();
        }

        public bool validate() {
            return true;
        }

        [HttpPost]
        public ActionResult Save(FormCollection form)
        {
            Usuario user = new Usuario();
            user.Nome = form["Nome"];
            user.Endereco = form["Endereco"];
            user.Numero = form["Numero"];
            user.Cidade = form["Cidade"];
            user.Telefone = form["Telefone"];
            user.Celular = form["Celular"];
            user.Email = form["Email"];
            user.Password = form["Password"];

            string nascimento = form["nascimento"];
            if (nascimento != null && !nascimento.Equals("")) {
                DateTime nascimentoObj = DateTime.Parse(nascimento);
                user.Nascimento = nascimentoObj;
            }

            if (form["Id"] != "") {
                int id = (int)Int32.Parse(form["Id"]);
                if (id != 0)
                {
                    user.Id = (int)Int32.Parse(form["Id"]);
                    Udao.updateUser(user);
                }
            } else {
                Udao.insertUser(user);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            Udao.deleteUsuario(id);
            return RedirectToAction("List");
        }
    }
}