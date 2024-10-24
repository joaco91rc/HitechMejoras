using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string documento { get; set; }
        public string nombreCompleto { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }

        public string ciudad { get; set; }


        public string direccion { get; set; }
        public bool estado { get; set; }
        public string fechaRegistro { get; set; }
        public string hitech1 { get; set; }
        public string hitech2 { get; set; }
        public string appleStore { get; set; }
        public string appleCafe { get; set; }



    }
}
