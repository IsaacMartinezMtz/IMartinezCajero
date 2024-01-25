using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static object GetCuenta(int NoCuenta)
        {
            object cuenta = new object();
            try
            {
                using(DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.GetCuenta(NoCuenta);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.NoCuenta = item.NoCuenta;
                            usuario.Nip = item.Nip;
                            cuenta = usuario;
                        }
                    }
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cuenta;
        }
    }
}
