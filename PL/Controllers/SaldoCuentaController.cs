using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SaldoCuentaController : Controller
    {
        // GET: SaldoCuenta
        public ActionResult GetAll()
        {
            ML.CuentaSaldo cuentaSaldo = new ML.CuentaSaldo();

            int NoCuentaSesion = (int)Convert.ToInt64(Session["NoCuentaRegistrada"]);
            object result = BL.CuentaSaldo.GetSaldo(NoCuentaSesion);
            if(result != null)
            {
                cuentaSaldo = (ML.CuentaSaldo)result;
                Session["SaldoActual"] = cuentaSaldo.SaldoActual;
                return View(cuentaSaldo);
            }
            else
            {
                return View();
            } 
        }
    }
}