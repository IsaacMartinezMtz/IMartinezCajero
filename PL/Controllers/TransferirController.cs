using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class TransferirController : Controller
    {
        // GET: Transferir
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Form(int monto, int cuentaReceptora)
        {
            ML.Tranferencia tranferencia = new ML.Tranferencia();
            ML.CuentaSaldo cuentaSaldo = new ML.CuentaSaldo();
            int NoCuentaSesion = (int)Convert.ToInt64(Session["NoCuentaRegistrada"]);
            int saldoActual = (int)Convert.ToInt64(Session["SaldoActual"]);
            if(saldoActual >= monto)
            {
                object resultGet = BL.Transferencia.GetCuentaReceptora(cuentaReceptora);
                if(resultGet != null)
                {
                    cuentaSaldo = (ML.CuentaSaldo)resultGet;
                    tranferencia.CuentaEmisora = NoCuentaSesion;
                    tranferencia.CuentaReceptora = cuentaReceptora;
                    tranferencia.saldoReceptor = cuentaSaldo.SaldoActual + monto;
                    tranferencia.SaldoEmnisor = saldoActual - monto;
                    bool Correct = BL.Transferencia.ActulizacionSaldos(tranferencia);
                    if (Correct)
                    {
                        ViewBag.Mensaje = "Operacion Exitosa!!!";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error al realzar la operacion, vuelava intentar";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se encontro Numero de cuenta";
                }
            }
            else
            {
                ViewBag.Mensaje = "Saldo insuficiente para el retiro";
            }
            return PartialView("Modal");
        }
        public ActionResult Regreso()
        {
            return RedirectToAction("GetAll", "SaldoCuenta");
        }
    }
}