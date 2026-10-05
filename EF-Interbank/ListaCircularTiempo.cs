using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoTransaccion
    {
        public string idTransaccion, fechaHora; public NodoTransaccion siguiente;
        public NodoTransaccion(string id, string fh)
        { // Nodo para lista circular temporal
            idTransaccion = id; fechaHora = fh; siguiente = null;
        }
    }
    public class ListaCircularTransaccion
    {
        private NodoTransaccion cabeza, cola; private int cantidad = 0;
        public int Cantidad => cantidad; public bool EstaVacia => cabeza == null;
        public void InsertarAlFinal(string id, string fh)
        { // Inserta y cierra el anillo circular en O(1)
            NodoTransaccion nuevo = new NodoTransaccion(id, fh);
            if (cabeza == null) { cabeza = cola = nuevo; nuevo.siguiente = nuevo; }
            else { cola.siguiente = nuevo; nuevo.siguiente = cabeza; cola = nuevo; }
            cantidad++;
        }
        public bool EliminarPorId(string idBuscado)
        { // Elimina nodo buscando por ID en el anillo
            if (cabeza == null) return false;
            NodoTransaccion actual = cabeza, anterior = cola; int intentados = 0;
            do
            {
                if (actual.idTransaccion == idBuscado)
                {
                    if (actual == cabeza) cabeza = actual.siguiente;
                    if (actual == cola) cola = anterior;
                    anterior.siguiente = actual.siguiente; cantidad--;
                    if (cantidad == 0) { cabeza = null; cola = null; }
                    return true;
                }
                anterior = actual; actual = actual.siguiente; intentados++;
            } while (intentados < cantidad);
            return false;
        }
        public string MostrarLoteCircular(int maxInicial = 8, int nodosExtra = 3)
        { // Recorrido cíclico con do-while
            if (cabeza == null) return "(vacia)";
            string s = ""; NodoTransaccion actual = cabeza; int contador = 0;
            do
            {
                if (contador < maxInicial) s += $"[{actual.fechaHora.Substring(11, 8)} ({actual.idTransaccion})] -> ";
                else if (contador == maxInicial && contador < cantidad - 3)
                {
                    s += "... [omitiendo] ... -> ";
                    int saltos = cantidad - contador - 3;
                    for (int i = 0; i < saltos; i++) { actual = actual.siguiente; contador++; }
                    continue;
                }
                else if (contador >= cantidad - 3) s += $"[{actual.fechaHora.Substring(11, 8)} ({actual.idTransaccion})] -> ";
                actual = actual.siguiente; contador++;
            } while (actual != cabeza);
            s += " -> [Vuelve a iniciar]: "; NodoTransaccion tempExtra = cabeza;
            for (int i = 0; i < nodosExtra; i++)
            {
                s += $"[{tempExtra.fechaHora.Substring(11, 8)} ({tempExtra.idTransaccion})] -> ";
                tempExtra = tempExtra.siguiente;
            }
            return s + "... (Flujo Continua)";
        }
    }
}
