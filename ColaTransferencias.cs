using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo para manejar los elementos dentro de la cola
    public class NodoCola
    {
        public Transaccion dato;
        public NodoCola siguiente;

        public NodoCola(Transaccion t)
        {
            this.dato = t;
            this.siguiente = null;
        }
    }

    // Cola (FIFO): Las transferencias que llegan primero se atienden primero
    public class ColaTransferencias
    {
        public NodoCola frente;
        public NodoCola atras;
        public int tamanio;

        public ColaTransferencias()
        {
            this.frente = null;
            this.atras = null;
            this.tamanio = 0;
        }

        // Agrega una nueva transferencia al final de la cola
        public void Enqueue(Transaccion t)
        {
            NodoCola nuevo = new NodoCola(t);

            if (atras == null)
            {
                frente = atras = nuevo;
            }
            else
            {
                atras.siguiente = nuevo;
                atras = nuevo;
            }
            tamanio++;
        }

        // Extrae y atiende la primera transferencia que llegó
        public Transaccion Dequeue()
        {
            if (frente == null)
            {
                return null;
            }

            Transaccion t = frente.dato;
            frente = frente.siguiente;

            if (frente == null)
            {
                atras = null;
            }

            tamanio--;
            t.estado = "Procesada";
            return t;
        }

        // Verifica si la cola se encuentra vacía
        public bool EstaVacia()
        {
            return frente == null;
        }
    }
}
