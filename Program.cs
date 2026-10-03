using System;
using System.Diagnostics;

namespace EF_Interbank
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;

            do
            {
                while (Console.KeyAvailable)
                    Console.ReadKey(true);

                Console.Clear();
                Console.WriteLine("==================================================================================================");
                Console.WriteLine("        INTERBANK - PROCESAMIENTO MASIVO CON ARQUITECTURA DINÁMICA ANTI-COLAPSO          ");
                Console.WriteLine("==================================================================================================\n");

                Console.WriteLine("Seleccione el escenario de prueba según el caso de alta demanda:");
                Console.WriteLine(" [1] Prueba rápida (3 solicitudes, límite estático de 2)");
                Console.WriteLine(" [2] Prueba mediana (110 solicitudes, límite estático de 100)");
                Console.WriteLine(" [3] Prueba masiva real (5020 solicitudes, superando el tope crítico de 5000)");
                Console.WriteLine(" [4] Salir del sistema");
                Console.Write("\nIngrese una opción (1-4): ");

                string opcion = Console.ReadLine()?.Trim();
                int limiteEstatico = 10;
                int nUsuarios = 12;
                int tamanoPagina = 10;

                if (opcion == "1") { limiteEstatico = 2; nUsuarios = 3; tamanoPagina = 2; }
                else if (opcion == "2") { limiteEstatico = 100; nUsuarios = 110; tamanoPagina = 20; }
                else if (opcion == "3") { limiteEstatico = 5000; nUsuarios = 5020; tamanoPagina = 25; }
                else if (opcion == "4" || string.IsNullOrEmpty(opcion)) break;
                else continue;

                Console.Clear();
                Console.WriteLine("Ejecutando motor masivo y estructurando páginas de resultados...");

                Stopwatch stopwatch = Stopwatch.StartNew();
                Transaccion[] listaTransacciones = Transaccion.ObtenerLoteTransacciones(nUsuarios);

                // Instanciación de las 5 estructuras dinámicas
                ListaAuditoria historialAuditoria = new ListaAuditoria();
                ColaTransferencias colaTransferencias = new ColaTransferencias();
                PilaReintentos pilaReintentos = new PilaReintentos();
                ListaDobleMenu paginadorTransacciones = new ListaDobleMenu();
                ListaCircularTransaccion lineaTiempoCircular = new ListaCircularTransaccion();

                int contadorAceptadas = 0;
                int contadorExcedentes = 0;

                foreach (Transaccion tx in listaTransacciones)
                {
                    historialAuditoria.Registrar(tx);

                    if (contadorAceptadas < limiteEstatico)
                    {
                        tx.estado = "Procesada";
                        colaTransferencias.Enqueue(tx);
                        contadorAceptadas++;
                    }
                    else
                    {
                        tx.estado = "Rechazada por Límite";
                        tx.motivoFalla = "Supera tope de 5000 (Pila LIFO de Resguardo)";
                        pilaReintentos.Push(tx);
                        contadorExcedentes++;
                    }
                }
                stopwatch.Stop();

                string bufferPagina = "";
                int contadorEnPagina = 0;

                while (!colaTransferencias.EstaVacia())
                {
                    Transaccion actual = colaTransferencias.Dequeue();

                    lineaTiempoCircular.InsertarAlFinal(actual.idTransaccion, actual.fechaHora);

                    bufferPagina += string.Format("{0,-14} | {1,-22} | S/. {2,-10:F2} | {3,-22} | {4,-8}\n",
                        actual.idTransaccion, actual.nombreTitularDestino, actual.monto, actual.estado, actual.fechaHora.Substring(11, 8));

                    contadorEnPagina++;

                    if (contadorEnPagina == tamanoPagina)
                    {
                        paginadorTransacciones.AgregarPagina(bufferPagina);
                        bufferPagina = "";
                        contadorEnPagina = 0;
                    }
                }
                if (contadorEnPagina > 0)
                {
                    paginadorTransacciones.AgregarPagina(bufferPagina);
                }

                // Paginación (Lista Doble)
                string tecla = "";
                while (tecla != "S" && paginadorTransacciones.Cantidad > 0)
                {
                    Console.Clear();
                    Console.WriteLine("==================================================================================================");
                    Console.WriteLine($"      REPORTE PAGINADO DE TRANSFERENCIAS VÁLIDAS (Total Aceptadas: {contadorAceptadas})         ");
                    Console.WriteLine("==================================================================================================\n");

                    NodoPagina paginaActual = paginadorTransacciones.Actual;
                    Console.WriteLine($"[ PÁGINA {paginaActual.numeroPagina} de {paginadorTransacciones.Cantidad} ]\n");
                    Console.WriteLine(string.Format("{0,-14} | {1,-22} | {2,-14} | {3,-22} | {4,-8}", "ID TX", "TITULAR DESTINO", "MONTO", "ESTADO", "HORA"));
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                    Console.WriteLine(paginaActual.contenidoTabla);
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");
                    Console.WriteLine(" Controles de Paginación Bidireccional (Lista Doble):");
                    Console.WriteLine("   [ D ] -> Página Siguiente (siguiente nodo)");
                    Console.WriteLine("   [ A ] <- Página Anterior (nodo anterior)");
                    Console.WriteLine("   [ S ] Salir de la paginación y ver Pila de Excedentes y Línea de Tiempo");
                    Console.WriteLine("==================================================================================================");
                    Console.Write(" Ingrese comando (A / D / S): ");

                    var input = Console.ReadKey(false);
                    tecla = input.Key.ToString().ToUpper();

                    if (tecla == "D") paginadorTransacciones.IrSiguiente();
                    else if (tecla == "A") paginadorTransacciones.IrAnterior();
                }

                // Pila de Excedentes y Lista Circular
                Console.Clear();
                Console.WriteLine("==================================================================================================");
                Console.WriteLine($"--- PILA DE RESGUARDO Y EXCEDENTES LIFO ({contadorExcedentes} rechazados por límite) ---");
                Console.WriteLine("==================================================================================================\n");
                Console.WriteLine(string.Format("{0,-14} | {1,-24} | {2,-20} | {3,-30}", "ID TX", "TITULAR DESTINO", "ESTADO", "MOTIVO DE FALLA"));
                Console.WriteLine("--------------------------------------------------------------------------------------------------");

                while (!pilaReintentos.EstaVacia())
                {
                    Transaccion reintento = pilaReintentos.Pop();
                    Console.WriteLine(string.Format("{0,-14} | {1,-24} | {2,-20} | {3,-30}",
                        reintento.idTransaccion, reintento.nombreTitularDestino, reintento.estado, reintento.motivoFalla));
                }
                Console.WriteLine("--------------------------------------------------------------------------------------------------\n");

                // Línea de Tiempo (Lista Circular)
                Console.WriteLine("--- LÍNEA DE TIEMPO CRONOLÓGICA (Lista Circular - Recorrido con do-while) ---");
                Console.WriteLine(lineaTiempoCircular.MostrarLoteCircular());
                Console.WriteLine("--------------------------------------------------------------------------------------------------\n");

                Console.WriteLine("==================================================================================================");
                Console.WriteLine($" [ÉXITO] Lote de {nUsuarios} procesado correctamente en {stopwatch.ElapsedMilliseconds} ms.");
                Console.WriteLine("==================================================================================================");

                Console.WriteLine("\n¿Desea realizar otra simulación? (S/N): ");
                string respuesta = Console.ReadLine()?.Trim();
                if (respuesta == null || respuesta.ToUpper() != "S")
                {
                    continuar = false;
                }

            } while (continuar);
        }
    }
}