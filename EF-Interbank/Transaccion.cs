using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class Transaccion
    {
        public string idTransaccion, cuentaOrigen, cuentaDestino, nombreTitularDestino, fechaHora, estado, motivoFalla;
        public decimal monto; // Atributos principales de la transferencia
        public Transaccion(string id, string origen, string destino, string titular, decimal m)
        {
            idTransaccion = id; cuentaOrigen = origen; cuentaDestino = destino; // Inicializa datos básicos
            nombreTitularDestino = titular; monto = m;
            fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); // Marca temporal actual
            estado = "Pendiente"; motivoFalla = "Ninguno"; // Estados iniciales por defecto
        }
        public static Transaccion[] ObtenerLoteTransacciones(int nUsuarios)
        { // Generador de lote masivo de 1 a N
            Transaccion[] lista = new Transaccion[nUsuarios];
            for (int i = 0; i < nUsuarios; i++)
                lista[i] = new Transaccion($"TX-2026-{i + 1:D5}", $"4001-000-{i + 1}", $"5002-999-{i + 1}", $"Cliente Destino {i + 1}", (i + 1) * 125.50m);
            return lista;
        }
    }
}