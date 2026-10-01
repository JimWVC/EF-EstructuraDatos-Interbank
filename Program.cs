using System;

namespace EF_Interbank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("   SISTEMA INTERBANK - PROCESAMIENTO Y FALLOS     ");
            Console.WriteLine("==================================================\n");

            // 1. Instanciar estructuras
            ListaAuditoria historialAuditoria = new ListaAuditoria();
            ColaTransferencias colaTransferencias = new ColaTransferencias();
            PilaReintentos pilaReintentos = new PilaReintentos();

            // 2. Simular N usuarios (ej. 5 clientes) llenando formularios en el buzón
            int totalClientes = 5;
            Console.WriteLine($"[+] Recibiendo formularios de {totalClientes} clientes en el buzón...");

            for (int i = 1; i <= totalClientes; i++)
            {
                Transaccion tx = new Transaccion($"TX-2026-0{i}", $"Cliente-0{i}", "Destino-Interbank", i * 200.00m);

                // Todo queda registrado en el historial general (Auditoría)
                historialAuditoria.Registrar(tx);

                // Se van acumulando en la cola FIFO del buzón
                colaTransferencias.Enqueue(tx);
            }
            Console.WriteLine($"[+] Total de operaciones acumuladas en cola: {colaTransferencias.tamanio}\n");

            // 3. Procesamiento parcial del buzón (Simulando que el sistema se satura o se corta el día)
            // que de las 5, el sistema solo procesa las primeras 3, dejando las demás en la cola.
            Console.WriteLine("--- PROCESANDO LOTE DE TRANSACCIONES ---");
            int procesadasExito = 0;
            int limiteProcesamiento = 3;

            while (!colaTransferencias.EstaVacia() && procesadasExito < limiteProcesamiento)
            {
                Transaccion actual = colaTransferencias.Dequeue();

                // una de las procesadas falla por error de conexión o saldo
                if (actual.idTransaccion == "TX-2026-02")
                {
                    actual.estado = "Fallida";
                    pilaReintentos.Push(actual); // Se va a la pila de reintentos (LIFO)
                    Console.WriteLine($"[X] Error en {actual.idTransaccion}. Movida a Pila de Reintentos.");
                }
                else
                {
                    Console.WriteLine($"[Procesada] ID: {actual.idTransaccion} | Monto: S/. {actual.monto} | Estado: {actual.estado}");
                }
                procesadasExito++;
            }

            // 4. Mostrar lo que **se quedó en la cola sin procesarse**
            Console.WriteLine($"\n[!] Corte de proceso. Transacciones que **se quedan en la cola** sin ejecutarse: {colaTransferencias.tamanio}");
            NodoCola nodoPendiente = colaTransferencias.frente;
            while (nodoPendiente != null)
            {
                Console.WriteLine($"    -> Pendiente en Cola | ID: {nodoPendiente.dato.idTransaccion} | Monto: S/. {nodoPendiente.dato.monto}");
                nodoPendiente = nodoPendiente.siguiente;
            }

            // 5. Gestionar los reintentos que cayeron en la pila
            Console.WriteLine("\n--- EJECUTANDO REINTENTOS DE ÚLTIMO MOMENTO (PILA LIFO) ---");
            while (!pilaReintentos.EstaVacia())
            {
                Transaccion txReintento = pilaReintentos.Pop();
                txReintento.estado = "Procesada en Reintento";
                Console.WriteLine($"[v] Reintentando transacción fallida: {txReintento.idTransaccion} | Nuevo Estado: {txReintento.estado}");
            }
            Console.ReadKey();
        }
    }
}