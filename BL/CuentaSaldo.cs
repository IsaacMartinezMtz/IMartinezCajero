using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CuentaSaldo
    {
        public static object GetSaldo(int NumeroCuenta)
        {
            object Saldo = new object();
            try
            {
                using(DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.GetSaldo(NumeroCuenta);
                    if (query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.CuentaSaldo cuentaSaldo = new ML.CuentaSaldo();
                            cuentaSaldo.Cuenta = new ML.Usuario();
                            cuentaSaldo.IdCuenta = item.IdCuentaSaldo;
                            cuentaSaldo.Cuenta.NoCuenta = (int)item.NumeroCuenta;
                            cuentaSaldo.SaldoActual = (int)item.SaldoActual;
                            cuentaSaldo.Cuenta.Nombre = item.Nombre;
                            cuentaSaldo.Cuenta.Apellido = item.ApellidoPaterno;
                            Saldo = cuentaSaldo;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Saldo;
        }
        public static bool UpdateSaldoCliente(int numeroCuenta, int saldoActual)
        {
            bool Correct = false;
            try
            {
                using(DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.UpdateSaldoCliente(numeroCuenta, saldoActual);
                    if (query > 0)
                    {
                        Correct = true;
                    }
                    else
                    {
                        Correct = false;
                    }
                }

            }catch (Exception ex)
            {
                Correct = false;
            }
            return Correct;
        }
    }
}