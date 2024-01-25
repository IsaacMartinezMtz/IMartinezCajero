using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CuentaSaldo
    {
        public int IdCuenta {  get; set; }
        public int SaldoActual { get;set; }
        public ML.Usuario Cuenta { get; set; }
        public List<object> Cuentas { get; set;}
    }
}
