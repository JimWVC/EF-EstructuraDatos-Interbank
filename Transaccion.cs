using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class Transaccion
    {
        // Datos básicos de la transferencia bancaria
        public string idTransaccion;
        public string cuentaOrigen;
        public string cuentaDestino;
        public decimal monto;
        public string fechaHora;
        public string estado;

        // Constructor para asignar los valores al crear una nueva transferencia
        public Transaccion(string id, string origen, string destino, decimal monto)
        {
            this.idTransaccion = id;
            this.cuentaOrigen = origen;
            this.cuentaDestino = destino;
            this.monto = monto;
            this.fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.estado = "Pendiente"; // Se define como pendiente por defecto
        }
    }
}
