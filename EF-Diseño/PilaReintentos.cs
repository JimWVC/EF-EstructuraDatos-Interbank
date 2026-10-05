using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoPila
    {
        public Transaccion dato; public NodoPila siguiente;
        public NodoPila(Transaccion t) { dato = t; siguiente = null; } // Nodo de la pila
    }
    // Estructura de Pila (LIFO): Gestiona las transacciones para reintentos
    public class PilaReintentos
    {
        public NodoPila cima; public int tamanio = 0;
        public PilaReintentos() { cima = null; } // Inicializa la pila vacía
        public void Push(Transaccion t)
        { // Apila elemento (LIFO) y actualiza estado
            NodoPila nuevo = new NodoPila(t);
            nuevo.siguiente = cima; cima = nuevo;
            tamanio++; t.estado = "En Reintento";
        }
        public Transaccion Pop()
        { // Desapila y retorna el elemento superior
            if (cima == null) return null;
            Transaccion t = cima.dato; cima = cima.siguiente;
            tamanio--; return t;
        }
        public bool EstaVacia() { return cima == null; } // Valida si la pila está vacía
    }
}
