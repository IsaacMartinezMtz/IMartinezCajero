using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Transferencia
    {
        public static object GetCuentaReceptora(int NumeroReceptor)
        {
            object cuenta = new object();
            try
            {
                using(DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.GetCuentaTranferir(NumeroReceptor);
                    if(query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.CuentaSaldo cuentaSaldo = new ML.CuentaSaldo();
                            cuentaSaldo.SaldoActual = (int)item.SaldoActual;
                            cuenta = cuentaSaldo;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return cuenta;
        }
        public static bool ActulizacionSaldos(ML.Tranferencia tranferencia)
        {
            bool Correct = false;
            try
            {
                using(DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.Transferencia(tranferencia.CuentaEmisora, tranferencia.CuentaReceptora, tranferencia.SaldoEmnisor, tranferencia.saldoReceptor);
                    if(query > 0)
                    {
                        Correct = true;
                    }
                    else{
                        Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Correct = false;
            }
            return Correct;
        }
    }
    
}
