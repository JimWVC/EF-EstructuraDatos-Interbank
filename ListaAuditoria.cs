using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo para almacenar cada transacción en el historial persistente
    public class NodoAuditoria
    {
        public Transaccion dato;
        public NodoAuditoria siguiente;
        public NodoAuditoria(Transaccion t) { this.dato = t; this.siguiente = null; }
    }
    // Lista simple enlazada para mantener el registro general sin límite fijo
    public class ListaAuditoria
    {
        public NodoAuditoria cabeza;
        public ListaAuditoria() { cabeza = null; }

        public void Registrar(Transaccion t)
        {
            NodoAuditoria nuevo = new NodoAuditoria(t);
            if (cabeza == null) { cabeza = nuevo; }
            else
            {
                NodoAuditoria actual = cabeza;
                while (actual.siguiente != null) { actual = actual.siguiente; }
                actual.siguiente = nuevo;
            }
        }
    }
}
