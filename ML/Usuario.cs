using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int NoCuenta {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Nip { get; set; }
        public List<object> Usuarios { get; set;}
    }
}
