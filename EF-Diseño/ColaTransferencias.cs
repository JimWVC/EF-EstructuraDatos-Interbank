using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    public class NodoCola
    {
        public Transaccion dato; public NodoCola siguiente;
        public NodoCola(Transaccion t) { dato = t; siguiente = null; } // Nodo encapsulador
    }
    // Estructura de Cola (FIFO): Atiende transferencias en orden de llegada
    public class ColaTransferencias
    {
        public NodoCola frente, atras; public int tamanio = 0;
        public ColaTransferencias() { frente = atras = null; } // Inicializa cola vacía
        public void Enqueue(Transaccion t)
        { // Inserta al final (FIFO)
            NodoCola nuevo = new NodoCola(t);
            if (atras == null) frente = atras = nuevo; else { atras.siguiente = nuevo; atras = nuevo; }
            tamanio++;
        }
        public Transaccion Dequeue()
        { // Extrae del frente y marca procesada
            if (frente == null) return null;
            Transaccion t = frente.dato; frente = frente.siguiente;
            if (frente == null) atras = null;
            tamanio--; t.estado = "Procesada"; return t;
        }
        public bool EstaVacia() { return frente == null; } // Valida si está vacía
    }
}