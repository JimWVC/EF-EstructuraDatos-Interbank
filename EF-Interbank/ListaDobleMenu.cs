using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoPagina
    {
        public int numeroPagina; public string contenidoTabla;
        public NodoPagina siguiente, anterior;
        public NodoPagina(int num, string contenido)
        { // Nodo de lista doble para paginación
            numeroPagina = num; contenidoTabla = contenido; siguiente = anterior = null;
        }
    }
    public class ListaDobleMenu
    {
        private NodoPagina cabeza, cola, actual; private int cantidad = 0;
        public int Cantidad => cantidad; public NodoPagina Actual => actual;
        public ListaDobleMenu() { cabeza = cola = actual = null; } // Inicializa la lista doble vacía
        public void AgregarPagina(string contenido)
        { // Inserta página al final manteniendo cola en O(1)
            cantidad++; NodoPagina nuevo = new NodoPagina(cantidad, contenido);
            if (cabeza == null) cabeza = cola = actual = nuevo;
            else { cola.siguiente = nuevo; nuevo.anterior = cola; cola = nuevo; }
        }
        public void IrSiguiente() { if (actual?.siguiente != null) actual = actual.siguiente; } // Navega hacia adelante
        public void IrAnterior() { if (actual?.anterior != null) actual = actual.anterior; } // Navega hacia atrás
    }
}
