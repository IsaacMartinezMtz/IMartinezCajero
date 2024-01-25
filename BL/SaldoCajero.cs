using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SaldoCajero
    {
        public static int GetSaldoCajero()
        {
            int total = 0;
            try
            {
                using (DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.SaldoTotalCajero().ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            total = item.Value;
                        }
                    }
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return total;
        }
        public static ML.Contadores Retiro(int retiro)
        {
            ML.Contadores contador = new ML.Contadores();
            
            int veinte = 0;
            int cincuenta = 0;
            int cien = 0;
            int docientos = 0;
            int quinientos = 0;
            int mil = 0;

            int contadorveinte = 0;
            int contadorcincuenta = 0;
            int contadorcien = 0;
            int contadordocientos =0;
            int contadorquinientos = 0;
            int contadormil = 0;
            

            try
            {
                using (DL.CajeroEntities context = new DL.CajeroEntities())
                {
                    var query = context.GetALLCajero().ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            if (item.Denominacion == 20)
                            {
                                veinte = item.CantidadBilletes;
                            }
                            else if (item.Denominacion == 50)
                            {
                                cincuenta = item.CantidadBilletes;
                            } else if (item.Denominacion == 100)
                            {
                                cien = item.CantidadBilletes;
                            } else if (item.Denominacion == 200)
                            {
                                docientos = item.CantidadBilletes;
                            } else if (item.Denominacion == 500)
                            {
                                quinientos = item.CantidadBilletes;
                            }
                            else if (item.Denominacion == 1000)
                            {
                                mil = item.CantidadBilletes;
                            }
                        }
                        do
                        {
                            
                            if (retiro >= 1000)
                            {
                                if(mil > 0) { 
                                    retiro -= 1000;
                                    contadormil++;
                                    mil -= 1;
                                    context.UpdateSaldoCajero(6, mil);
                                }
                            }
                            else if (retiro >= 500)
                            {
                                if(quinientos > 0)
                                {
                                    retiro -= 500;
                                    contadorquinientos++;
                                    quinientos -= 1;
                                    context.UpdateSaldoCajero(5, quinientos);
                                }
                            }
                            else if (retiro >= 200)
                            {
                                if(docientos > 0)
                                {
                                    retiro -= 200;
                                    contadordocientos++;
                                    docientos -= 1;
                                    context.UpdateSaldoCajero(4, docientos);
                                }
                            }
                            else if (retiro >= 100)
                            {
                                if(cien > 0)
                                {
                                    retiro -= 100;
                                    contadorcien++;
                                    cien -= 1;
                                    context.UpdateSaldoCajero(3, cien);
                                }
                            }
                            else if (retiro >= 50)
                            {
                                if (cincuenta > 0)
                                {
                                    retiro -= 50;
                                    contadorcincuenta++;
                                    cincuenta -= 1;
                                    context.UpdateSaldoCajero(2, cincuenta);
                                }
                            }
                            else if (retiro >= 20)
                            {
                                if(veinte > 0)
                                {
                                    retiro -= 20;
                                    contadorveinte++;
                                    veinte -= 1;
                                    context.UpdateSaldoCajero(1, veinte);
                                }
                            }

                        } while (1 < retiro);
                        contador.ContadorVenite = contadorveinte;
                        contador.ContadorCincuenta = contadorcincuenta;
                        contador.ContadorCien = contadorcien;
                        contador.ContadorDocientos = contadordocientos;
                        contador.ContadorQuinientos = contadorquinientos;
                        contador.ContadorMil = contadormil; 
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (contador);

        }
    }
}
