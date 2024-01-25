using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario) {
            int NoCuenta  = usuario.NoCuenta;
            int Nip = usuario.Nip;

            object result = BL.Usuario.GetCuenta(NoCuenta);


            if (result != null)
            {
                usuario = (ML.Usuario)result;
                if (usuario.Nip == Nip)
                {
                    Session["NoCuentaRegistrada"] = usuario.NoCuenta;
                    return RedirectToAction("GetAll", "SaldoCuenta");
                }
                else
                {
                    ViewBag.valido = true;
                    ViewBag.Mensaje = "Error Nip incorrecto";
                }
            }
            else
            {
                ViewBag.Mensaje = "Error cuenta incorrecta";
                ViewBag.valido = true;
            }
            return PartialView("Modal");
        }
    }
}