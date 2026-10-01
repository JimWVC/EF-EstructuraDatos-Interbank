using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo para almacenar la transacción y el puntero al siguiente elemento
    public class NodoAuditoria
    {
        public Transaccion dato;
        public NodoAuditoria siguiente;

        public NodoAuditoria(Transaccion t)
        {
            this.dato = t;
            this.siguiente = null;
        }
    }

    // Lista simple enlazada para llevar el historial completo de operaciones
    public class ListaAuditoria
    {
        public NodoAuditoria cabeza;

        public ListaAuditoria()
        {
            this.cabeza = null;
        }

        // Agrega una nueva transacción al final de la lista
        public void Registrar(Transaccion t)
        {
            NodoAuditoria nuevo = new NodoAuditoria(t);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoAuditoria actual = cabeza;
                while (actual.siguiente != null)
                {
                    actual = actual.siguiente;
                }
                actual.siguiente = nuevo;
            }
        }
    }
}
