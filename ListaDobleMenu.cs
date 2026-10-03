using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoPagina
    {
        public int numeroPagina;
        public string contenidoTabla;
        public NodoPagina siguiente, anterior;

        public NodoPagina(int num, string contenido)
        {
            numeroPagina = num;
            contenidoTabla = contenido;
            siguiente = anterior = null;
        }
    }

    public class ListaDobleMenu
    {
        private NodoPagina cabeza, cola, actual;
        private int cantidad;

        public int Cantidad => cantidad;
        public NodoPagina Actual => actual;

        public ListaDobleMenu()
        {
            cabeza = cola = actual = null;
            cantidad = 0;
        }

        // Inserta una página de transacciones al final manteniendo el puntero cola en O(1)
        public void AgregarPagina(string contenido)
        {
            cantidad++;
            NodoPagina nuevo = new NodoPagina(cantidad, contenido);
            if (cabeza == null)
            {
                cabeza = cola = actual = nuevo;
            }
            else
            {
                cola.siguiente = nuevo;
                nuevo.anterior = cola;
                cola = nuevo;
            }
        }

        // Navegación bidireccional hacia adelante (Página Siguiente)
        public void IrSiguiente()
        {
            if (actual != null && actual.siguiente != null)
                actual = actual.siguiente;
        }

        // Navegación bidireccional hacia atrás (Página Anterior)
        public void IrAnterior()
        {
            if (actual != null && actual.anterior != null)
                actual = actual.anterior;
        }
    }
}
