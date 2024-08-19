using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Concepto
    {

        private CD_Concepto objcd_Concepto = new CD_Concepto();

        public List<Concepto> Listar()
        {
            return objcd_Concepto.Listar();
        }

        public int Registrar(Concepto objConcepto, out string mensaje)
        {
            mensaje = string.Empty;

            if (objConcepto.descripcion == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Concepto\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Concepto.Registrar(objConcepto, out mensaje);
            }
        }

        public bool Editar(Concepto objConcepto, out string mensaje)
        {
            mensaje = string.Empty;

            if (objConcepto.descripcion == "")
            {
                mensaje = mensaje + "Es necesario el nombre del Concepto\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Concepto.Editar(objConcepto, out mensaje);
            }
        }

        public bool Eliminar(Concepto objConcepto, out string mensaje)
        {
            return objcd_Concepto.Eliminar(objConcepto, out mensaje);
        }
    }
}
