using EF_Interbank;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Diseño
{
    public partial class FrmDashboard : Form
    {
        // PASO 1: Variables globales aquí (Línea 15)
        private ListaDobleMenu paginador;
        private PilaReintentos pila;
        private ListaCircularTransaccion lineaTiempo;
        private ListaAuditoria auditoria;

        // Variable global para almacenar el tamaño de página activo
        private int tamPaginaActual = 2;
        public FrmDashboard()
        {
        InitializeComponent();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            this.Text = "Interbank - Dashboard de Procesamiento Masivo";

            // Carga de opciones en el ComboBox
            cmbEscenario.Items.Clear();
            cmbEscenario.Items.Add("Prueba Rápida (3 solicitudes)");
            cmbEscenario.Items.Add("Prueba Mediana (110 solicitudes)");
            cmbEscenario.Items.Add("Prueba Masiva (5020 solicitudes)");
            cmbEscenario.SelectedIndex = 0;
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvTransferencias.Columns.Clear();
            dgvTransferencias.Columns.Add("id", "ID TX");
            dgvTransferencias.Columns.Add("titular", "Titular Destino");
            dgvTransferencias.Columns.Add("monto", "Monto");
            dgvTransferencias.Columns.Add("estado", "Estado");
            dgvTransferencias.Columns.Add("hora", "Hora");
        }
        private void MostrarPagina()
        {
            if (paginador == null || paginador.Cantidad == 0) return;

            dgvTransferencias.Rows.Clear();
            NodoPagina paginaActual = paginador.Actual;

            string[] filas = paginaActual.contenidoTabla.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string fila in filas)
            {
                string[] datos = fila.Split(';');
                if (datos.Length == 5)
                {
                    dgvTransferencias.Rows.Add(datos[0], datos[1], datos[2], datos[3], datos[4]);
                }
            }

            // Cálculo exacto de rangos basados en la página actual
            int registrosPaginaActual = filas.Length;
            int desde = ((paginaActual.numeroPagina - 1) * tamPaginaActual) + 1;
            int hasta = desde + registrosPaginaActual - 1;

            // Muestra el total de registros en el paginador
            lblPagina.Text = $"Mostrando {desde} a {hasta} registros | Página {paginaActual.numeroPagina} de {paginador.Cantidad}";
        }
        private void EjecutarSimulacion(int limite, int usuarios, int tamPagina)
        {
            // Instanciación limpia de estructuras para el nuevo lote
            auditoria = new ListaAuditoria();
            ColaTransferencias cola = new ColaTransferencias();
            pila = new PilaReintentos();
            lineaTiempo = new ListaCircularTransaccion();
            paginador = new ListaDobleMenu();

            Transaccion[] listaTransacciones = Transaccion.ObtenerLoteTransacciones(usuarios);
            int contadorAceptadas = 0;

            // Enrutamiento condicional (Lista Simple, Cola FIFO y Pila LIFO)
            foreach (Transaccion tx in listaTransacciones)
            {
                auditoria.Registrar(tx); // Registro histórico en Lista Simple

                if (contadorAceptadas < limite)
                {
                    tx.estado = "Procesada";
                    cola.Enqueue(tx); // Encola en FIFO
                    contadorAceptadas++;
                }
                else
                {
                    tx.estado = "Rechazada por Límite";
                    tx.motivoFalla = "Supera tope de " + limite + " (Pila LIFO)";
                    pila.Push(tx); // Apila en LIFO
                }
            }

            // Desencolado FIFO y empaquetado para Lista Doble y Lista Circular
            string bufferPagina = "";
            int contadorEnPagina = 0;

            while (!cola.EstaVacia())
            {
                Transaccion actual = cola.Dequeue();
                lineaTiempo.InsertarAlFinal(actual.idTransaccion, actual.fechaHora);

                string horaCorta = actual.fechaHora.Length >= 19 ? actual.fechaHora.Substring(11, 8) : actual.fechaHora;
                bufferPagina += $"{actual.idTransaccion};{actual.nombreTitularDestino};S/. {actual.monto:F2};{actual.estado};{horaCorta}|";
                contadorEnPagina++;

                if (contadorEnPagina == tamPagina)
                {
                    paginador.AgregarPagina(bufferPagina);
                    bufferPagina = "";
                    contadorEnPagina = 0;
                }
            }

            if (contadorEnPagina > 0)
            {
                paginador.AgregarPagina(bufferPagina);
            }

            MostrarPagina();
        }

        private void dgvTransferencias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            int limite = 0;
            int usuarios = 0;
            int tamPagina = 0;

            switch (cmbEscenario.SelectedIndex)
            {
                case 0: // Prueba Rápida
                    limite = 2;
                    usuarios = 3;
                    tamPaginaActual = 2;
                    break;

                case 1: // Prueba Mediana
                    limite = 100;
                    usuarios = 110;
                    tamPaginaActual = 20;
                    break;

                case 2: // Prueba Masiva Real
                    limite = 5000;
                    usuarios = 5020;
                    tamPaginaActual = 25;
                    break;
            }

            EjecutarSimulacion(limite, usuarios, tamPaginaActual);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginador != null)
            {
                paginador.IrSiguiente();
                MostrarPagina();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginador != null)
            {
                paginador.IrAnterior();
                MostrarPagina();
            }
        }

        private void btnExcedentes_Click(object sender, EventArgs e)
        {
            if (pila != null)
            {
                FrmExcedentes frm = new FrmExcedentes(pila);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Elija primera un escenario y ejecute la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLineaTiempo_Click(object sender, EventArgs e)
        {
  
            if (lineaTiempo != null)
            {
                FrmLineaTiempo frm = new FrmLineaTiempo(lineaTiempo);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Elija primera un escenario y ejecute la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
  
        }
    }
}
