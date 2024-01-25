using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class RetiroController : Controller
    {
        // GET: Retiro
        public ActionResult Form()
        {
            ML.Contadores contador = new ML.Contadores();

            return View(contador);
        }
        [HttpPost]
        public ActionResult Form(int Retiro) {
            int saldoActual  = (int)Convert.ToInt64(Session["SaldoActual"]);
            if(saldoActual >= Retiro)
            {
                int saldoCajero = BL.SaldoCajero.GetSaldoCajero();
                if(saldoCajero >= Retiro)
                {
                    int CuentaSesion = (int)Convert.ToInt64(Session["NoCuentaRegistrada"]);
                    int descuentoSaldo = saldoActual - Retiro;
                    ML.Contadores result = BL.SaldoCajero.Retiro(Retiro);
                    bool resulltActualizacioSaldo = BL.CuentaSaldo.UpdateSaldoCliente(CuentaSesion,descuentoSaldo);

                    
                    return PartialView("ModalRetiro", result);
                }
                else
                {
                    ViewBag.Mensaje = "Saldo insuficiente para el retiro";
                    return PartialView("Modal");

                } 
                
            }
            else
            {
                ViewBag.Mensaje = "Saldo insuficiente para el retiro";
                return PartialView("Modal");
            }
        }
        public ActionResult Retorno()
        {
            return RedirectToAction("GetAll", "SaldoCuenta");
        }
    }
}