using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Interbank
{
    // Nodo para la lista circular que representa a los servidores del backend
    public class NodoServidor
    {
        public string nombreServidor;
        public int capacidadCarga;
        public NodoServidor siguiente;

        public NodoServidor(string nombre, int capacidad)
        {
            this.nombreServidor = nombre;
            this.capacidadCarga = capacidad;
            this.siguiente = null;
        }
    }

    // Lista Circular: Distribuye la carga de peticiones de forma rotativa entre los servidores
    public class ListaCircularServidores
    {
        public NodoServidor cabeza;
        public NodoServidor actual;

        public ListaCircularServidores()
        {
            this.cabeza = null;
            this.actual = null;
        }

        // Agrega un servidor a la estructura circular
        public void AgregarServidor(string nombre, int capacidad)
        {
            NodoServidor nuevo = new NodoServidor(nombre, capacidad);
            if (cabeza == null)
            {
                cabeza = nuevo;
                cabeza.siguiente = cabeza; // Se apunta a sí mismo para cerrar el ciclo
                actual = cabeza;
            }
            else
            {
                NodoServidor temp = cabeza;
                while (temp.siguiente != cabeza)
                {
                    temp = temp.siguiente;
                }
                temp.siguiente = nuevo;
                nuevo.siguiente = cabeza;
            }
        }

        // Selecciona el siguiente servidor de forma rotativa para atender una solicitud
        public string ObtenerSiguienteServidor()
        {
            if (cabeza == null)
            {
                return "No hay servidores disponibles";
            }
            string servidorAsignado = actual.nombreServidor;
            actual = actual.siguiente; // Avanza al siguiente servidor en el ciclo
            return servidorAsignado;
        }
    }
}
