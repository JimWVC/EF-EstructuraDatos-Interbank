using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo para manejar los elementos de la pila de reintentos
    public class NodoPila
    {
        public Transaccion dato;
        public NodoPila siguiente;

        public NodoPila(Transaccion t)
        {
            this.dato = t;
            this.siguiente = null;
        }
    }

    // Pila (LIFO): La última transacción fallida que entra es la primera en reintentarse
    public class PilaReintentos
    {
        public NodoPila cima;
        public int tamanio;

        public PilaReintentos()
        {
            this.cima = null;
            this.tamanio = 0;
        }

        // Agrega una transferencia fallida a la cima de la pila para su reintento
        public void Push(Transaccion t)
        {
            NodoPila nuevo = new NodoPila(t);
            nuevo.siguiente = cima;
            cima = nuevo;
            tamanio++;
            t.estado = "En Reintento";
        }

        // Extrae la última transacción ingresada para procesar su reintento
        public Transaccion Pop()
        {
            if (cima == null)
            {
                return null; // La pila se encuentra vacía
            }
            Transaccion t = cima.dato;
            cima = cima.siguiente;
            tamanio--;
            return t;
        }

        // Comprueba si la pila está vacía
        public bool EstaVacia()
        {
            return cima == null;
        }
    }
}
