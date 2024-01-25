using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CajeroSaldo
    {
        public int IdBillete { get; set; }
        public int Denominacion { get; set; }
        public int CantidadBilletes { get; set; }
        public int total {  get; set; }
        public List<object> Cajeros {  get; set; }
    }
}
