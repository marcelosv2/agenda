using crud1.Dao;
using crud1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace crud1.Controllers
{
    public class LoginController : Controller
    {

        UsuarioDao UDao = new UsuarioDao();
        [HttpPost]
        public ActionResult Check (FormCollection form)
        {
            string l = form["login"];
            string pass = form["pwd"];

            if (l != null && pass != null) {
                try {
                    Usuario u = UDao.getUsuerByLoginSenha(l, pass);
                    if (u != null && u.Id != 0)
                    {
                        // HttpContext context = ;
                        HttpContext.Session["id"] = u.Id;
                        HttpContext.Session["senha"] = u.Id;
                        HttpContext.Session["nome"] = u.Nome;
                        return RedirectToAction("../Usuario/List");
                    }
                } catch (Exception e) {

                }
               
            }
            return RedirectToAction("Login");
        }

        public ActionResult Login() {
            return View();
        }

        public ActionResult Deslogar()
        {
            HttpContext.Session["id"] = null;
            HttpContext.Session["senha"] = null;
            HttpContext.Session["nome"] = null;
            return RedirectToAction("Login");
        }
    }
}