using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoAuditoria
    {
        public Transaccion dato; public NodoAuditoria siguiente;
        public NodoAuditoria(Transaccion t) { dato = t; siguiente = null; } // Nodo del historial de auditoría
    }
    // Lista simple enlazada para mantener el registro general sin límite fijo
    public class ListaAuditoria
    {
        public NodoAuditoria cabeza;
        public ListaAuditoria() { cabeza = null; } // Inicializa la lista vacía
        public void Registrar(Transaccion t)
        { // Inserta la transacción al final del historial lineal
            NodoAuditoria nuevo = new NodoAuditoria(t);
            if (cabeza == null) cabeza = nuevo;
            else
            {
                NodoAuditoria actual = cabeza;
                while (actual.siguiente != null) actual = actual.siguiente;
                actual.siguiente = nuevo;
            }
        }
    }
}
