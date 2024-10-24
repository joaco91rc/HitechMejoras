using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Compra
    {
        public int idCompra { get; set; }
        public int idNegocio { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
        public string tipoDocumento { get; set; }
        public string nroDocumento { get; set; }
        public decimal montoTotal { get; set; }
        public List<DetalleCompra> oDetalleCompra { get; set; }
        public string fechaRegistro { get; set; }
        public string observaciones { get; set; }
        // Nuevos campos
        public string formaPago { get; set; }
        public string formaPago2 { get; set; }
        public string formaPago3 { get; set; }
        public string formaPago4 { get; set; }
        public decimal montoFP1 { get; set; }
        public decimal montoFP2 { get; set; }
        public decimal montoFP3 { get; set; }
        public decimal montoFP4 { get; set; }
        public decimal montoPago { get; set; }
        public decimal montoPagoFP2 { get; set; }
        public decimal montoPagoFP3 { get; set; }
        public decimal montoPagoFP4 { get; set; }



    }
}
