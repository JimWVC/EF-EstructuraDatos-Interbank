using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class Transaccion
    {
        // Atributos basados en los formularios de los N usuarios
        public string idTransaccion;
        public string cuentaOrigen;
        public string cuentaDestino;
        public string nombreTitularDestino;
        public decimal monto;
        public string fechaHora;
        public string estado;
        public string motivoFalla;

        // Constructor para inicializar cada transferencia
        public Transaccion(string id, string origen, string destino, string titularDestino, decimal monto)
        {
            this.idTransaccion = id;
            this.cuentaOrigen = origen;
            this.cuentaDestino = destino;
            this.nombreTitularDestino = titularDestino;
            this.monto = monto;
            this.fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            this.estado = "Pendiente";
            this.motivoFalla = "Ninguno";
        }

        // Método estático para generar el lote masivo correlativo de 1 a N
        public static Transaccion[] ObtenerLoteTransacciones(int nUsuarios)
        {
            Transaccion[] listaTransacciones = new Transaccion[nUsuarios];

            for (int i = 0; i < nUsuarios; i++)
            {
                listaTransacciones[i] = new Transaccion(
                    id: $"TX-2026-{i + 1:D5}", // Genera IDs correlativos: TX-2026-00001, 00002, etc.
                    origen: $"4001-000-{i + 1}",
                    destino: $"5002-999-{i + 1}",
                    titularDestino: $"Cliente Destino {i + 1}",
                    monto: (i + 1) * 125.50m
                );
            }

            return listaTransacciones;
        }
    }
}