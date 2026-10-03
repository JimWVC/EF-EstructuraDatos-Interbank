using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoCola
    {
        public Transaccion dato;
        public NodoCola siguiente;
        public NodoCola(Transaccion t) { this.dato = t; this.siguiente = null; }
    }
    // Estructura de Cola (FIFO): Atiende las transferencias en orden estricto de llegada
    public class ColaTransferencias
    {
        public NodoCola frente, atras;
        public int tamanio;
        public ColaTransferencias() { frente = atras = null; tamanio = 0; }

        public void Enqueue(Transaccion t)
        {
            NodoCola nuevo = new NodoCola(t);
            if (atras == null) { frente = atras = nuevo; }
            else { atras.siguiente = nuevo; atras = nuevo; }
            tamanio++;
        }
        public Transaccion Dequeue()
        {
            if (frente == null) { return null; }
            Transaccion t = frente.dato;
            frente = frente.siguiente;
            if (frente == null) { atras = null; }
            tamanio--;
            t.estado = "Procesada";
            return t;
        }
        public bool EstaVacia() { return frente == null; }
    }
}