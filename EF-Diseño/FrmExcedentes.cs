using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EF_Interbank;

namespace EF_Diseño
{
    public partial class FrmExcedentes : Form
    {
        private PilaReintentos pilaRef;

        public FrmExcedentes(PilaReintentos pilaExcedentes)
        {
            InitializeComponent();
            pilaRef = pilaExcedentes;
        }

        private void FrmExcedentes_Load(object sender, EventArgs e)
        {
            this.Text = "Interbank - Módulo de Resguardo y Excedentes (Pila LIFO)";

            // 1. Configuración de columnas del DataGridView
            dgvExcedentes.Columns.Clear();
            dgvExcedentes.Columns.Add("id", "ID TX");
            dgvExcedentes.Columns.Add("titular", "Titular Destino");
            dgvExcedentes.Columns.Add("monto", "Monto");
            dgvExcedentes.Columns.Add("estado", "Estado");
            dgvExcedentes.Columns.Add("motivo", "Motivo de Falla / Rechazo");

            // Opcional: Ajustar ancho automático para que se lean bien los motivos
            dgvExcedentes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Carga y desapilado LIFO hacia la tabla
            dgvExcedentes.Rows.Clear();

            if (pilaRef == null || pilaRef.EstaVacia())
            {
                MessageBox.Show("No se registraron transacciones excedentes en este escenario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Recorre la Pila LIFO desapilando cada nodo
            while (!pilaRef.EstaVacia())
            {
                Transaccion reintento = pilaRef.Pop();

                dgvExcedentes.Rows.Add(
                    reintento.idTransaccion,
                    reintento.nombreTitularDestino,
                    $"S/. {reintento.monto:F2}",
                    reintento.estado,
                    reintento.motivoFalla
                );
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario flotante y regresa al Dashboard
        }
    }
}
