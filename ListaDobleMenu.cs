using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo doble para recorrer las transacciones hacia adelante y hacia atrás
    public class NodoDoble
    {
        public Transaccion dato;
        public NodoDoble siguiente;
        public NodoDoble anterior;

        public NodoDoble(Transaccion t)
        {
            this.dato = t;
            this.siguiente = null;
            this.anterior = null;
        }
    }

    // Lista Doble: Permite navegar el historial de transacciones en ambas direcciones
    public class ListaDobleMenu
    {
        public NodoDoble cabeza;
        public NodoDoble cola;

        public ListaDobleMenu()
        {
            this.cabeza = null;
            this.cola = null;
        }

        // Inserta una transacción al final de la lista doble
        public void Insertar(Transaccion t)
        {
            NodoDoble nuevo = new NodoDoble(t);
            if (cabeza == null)
            {
                cabeza = cola = nuevo;
            }
            else
            {
                cola.siguiente = nuevo;
                nuevo.anterior = cola;
                cola = nuevo;
            }
        }
    }
}
