using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoPila
    {
        public Transaccion dato;
        public NodoPila siguiente;
        public NodoPila(Transaccion t) { this.dato = t; this.siguiente = null; }
    }
    // Estructura de Pila (LIFO): Gestiona las transacciones fallidas
    public class PilaReintentos
    {
        public NodoPila cima;
        public int tamanio;
        public PilaReintentos() { cima = null; tamanio = 0; }

        public void Push(Transaccion t)
        {
            NodoPila nuevo = new NodoPila(t);
            nuevo.siguiente = cima;
            cima = nuevo;
            tamanio++;
            t.estado = "En Reintento";
        }
        public Transaccion Pop()
        {
            if (cima == null) { return null; }
            Transaccion t = cima.dato;
            cima = cima.siguiente;
            tamanio--;
            return t;
        }
        public bool EstaVacia() { return cima == null; }
    }
}
