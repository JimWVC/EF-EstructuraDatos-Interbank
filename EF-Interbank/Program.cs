using EF_Interbank;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        //Mantener el ciclo del menú interactivo de la consola
        bool continuar = true;

        do
        {
            // Limpia el búfer de teclado para evitar saltos o entradas vacias
            while (Console.KeyAvailable)
                Console.ReadKey(true);

            // Empieza la interfaz principal del sistema de alta demanda
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

            // Configuración de los menus según la opción elegida por el usuario
            if (opcion == "1") { limiteEstatico = 2; nUsuarios = 3; tamanoPagina = 2; }
            else if (opcion == "2") { limiteEstatico = 100; nUsuarios = 110; tamanoPagina = 20; }
            else if (opcion == "3") { limiteEstatico = 5000; nUsuarios = 5020; tamanoPagina = 25; }
            else if (opcion == "4" || string.IsNullOrEmpty(opcion)) break;
            else continue;

            Console.Clear();
            Console.WriteLine("Ejecutando motor masivo y estructurando páginas de resultados...");

            // Inicialización del tiempo para medir la eficiencia temporal del procesamiento masivo
            Stopwatch stopwatch = Stopwatch.StartNew();
            Transaccion[] listaTransacciones = Transaccion.ObtenerLoteTransacciones(nUsuarios);

            // Instanciación de las 5 estructuras dinámicas clave para la gestión de datos
            ListaAuditoria historialAuditoria = new ListaAuditoria();       // Historial lineal completo
            ColaTransferencias colaTransferencias = new ColaTransferencias(); // Cola FIFO para operaciones válidas
            PilaReintentos pilaReintentos = new PilaReintentos();             // Pila LIFO para excedentes y resguardo
            ListaDobleMenu paginadorTransacciones = new ListaDobleMenu();     // Lista doble para paginación bidireccional
            ListaCircularTransaccion lineaTiempoCircular = new ListaCircularTransaccion(); // Línea de tiempo circular

            int contadorAceptadas = 0;
            int contadorExcedentes = 0;

            //Distribución y clasificación del lote masivo de transacciones
            foreach (Transaccion tx in listaTransacciones)
            {
                // Registra todas las transacciones para fines de auditoría general
                historialAuditoria.Registrar(tx);

                // Control de validación contra el límite estático del sistema
                if (contadorAceptadas < limiteEstatico)
                {
                    tx.estado = "Procesada";
                    colaTransferencias.Enqueue(tx); // Encola ordenadamente (FIFO)
                    contadorAceptadas++;
                }
                else
                {
                    tx.estado = "Rechazada por Límite";
                    tx.motivoFalla = "Supera tope de 5000 (Pila LIFO de Resguardo)";
                    pilaReintentos.Push(tx); // Apila el excedente de forma inversa (LIFO)
                    contadorExcedentes++;
                }
            }
            stopwatch.Stop(); // Detiene la medición de tiempo del procesamiento

            string bufferPagina = "";
            int contadorEnPagina = 0;

            // Procesamiento de la cola FIFO para poblar la línea de tiempo y las páginas de visualización
            while (!colaTransferencias.EstaVacia())
            {
                Transaccion actual = colaTransferencias.Dequeue();

                // Inserta la transacción procesada en la lista circular cronológica
                lineaTiempoCircular.InsertarAlFinal(actual.idTransaccion, actual.fechaHora);

                // Formatea los datos tabulados para el búfer de la página actual
                bufferPagina += string.Format("{0,-14} | {1,-22} | S/. {2,-10:F2} | {3,-22} | {4,-8}\n",
                    actual.idTransaccion, actual.nombreTitularDestino, actual.monto, actual.estado, actual.fechaHora.Substring(11, 8));

                contadorEnPagina++;

                // Agrupa los registros según el tamaño de página definido al alcanzar el límite parcial
                if (contadorEnPagina == tamanoPagina)
                {
                    paginadorTransacciones.AgregarPagina(bufferPagina);
                    bufferPagina = "";
                    contadorEnPagina = 0;
                }
            }
            // Agrega los elementos restantes si el búfer final no completó el tamaño exacto de página
            if (contadorEnPagina > 0)
            {
                paginadorTransacciones.AgregarPagina(bufferPagina);
            }

            // Módulo Interactivo de Paginación Bidireccional (utilizando la Lista Doble Enlazada)
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
                Console.WriteLine("    [ D ] -> Página Siguiente (siguiente nodo)");
                Console.WriteLine("    [ A ] <- Página Anterior (nodo anterior)");
                Console.WriteLine("    [ S ] Salir de la paginación y ver Pila de Excedentes y Línea de Tiempo");
                Console.WriteLine("==================================================================================================");
                Console.Write(" Ingrese comando (A / D / S): ");

                var input = Console.ReadKey(false);
                tecla = input.Key.ToString().ToUpper();

                // Manejo de punteros hacia adelante y atrás en memoria mediante la lista doble
                if (tecla == "D") paginadorTransacciones.IrSiguiente();
                else if (tecla == "A") paginadorTransacciones.IrAnterior();
            }

            // Visualización de la Pila de Excedentes (Desapilado LIFO)
            Console.Clear();
            Console.WriteLine("==================================================================================================");
            Console.WriteLine($"--- PILA DE RESGUARDO Y EXCEDENTES LIFO ({contadorExcedentes} rechazados por límite) ---");
            Console.WriteLine("==================================================================================================\n");
            Console.WriteLine(string.Format("{0,-14} | {1,-24} | {2,-20} | {3,-30}", "ID TX", "TITULAR DESTINO", "ESTADO", "MOTIVO DE FALLA"));
            Console.WriteLine("--------------------------------------------------------------------------------------------------");

            // Extrae y muestra los elementos en orden inverso estricto de entrada (LIFO)
            while (!pilaReintentos.EstaVacia())
            {
                Transaccion reintento = pilaReintentos.Pop();
                Console.WriteLine(string.Format("{0,-14} | {1,-24} | {2,-20} | {3,-30}",
                    reintento.idTransaccion, reintento.nombreTitularDestino, reintento.estado, reintento.motivoFalla));
            }
            Console.WriteLine("--------------------------------------------------------------------------------------------------\n");

            // Visualización de la Línea de Tiempo Cronológica (Lista Circular)
            Console.WriteLine("--- LÍNEA DE TIEMPO (Lista Circular - Recorrido) ---");
            Console.WriteLine(lineaTiempoCircular.MostrarLoteCircular());
            Console.WriteLine("--------------------------------------------------------------------------------------------------\n");

            // Resumen de rendimiento y tiempo de ejecución global
            Console.WriteLine("==================================================================================================");
            Console.WriteLine($" [ÉXITO] Lote de {nUsuarios} procesado correctamente en {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine("==================================================================================================");

            // Consulta final para repetir o finalizar la ejecución del programa
            Console.WriteLine("\n¿Desea realizar otra simulación? (S/N): ");
            string respuesta = Console.ReadLine()?.Trim();
            if (respuesta == null || respuesta.ToUpper() != "S")
            {
                continuar = false;
            }
        } while (continuar);
    }
}