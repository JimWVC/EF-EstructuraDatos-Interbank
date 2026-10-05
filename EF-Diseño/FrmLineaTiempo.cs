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
    public partial class FrmLineaTiempo : Form
    {
        private ListaCircularTransaccion linea;
        public FrmLineaTiempo(ListaCircularTransaccion lineaTiempo)
        {
            InitializeComponent();
            linea = lineaTiempo;
        }

        private void FrmLineaTiempo_Load(object sender, EventArgs e)
        {
            this.Text = "Interbank - Auditoría Cronológica (Lista Circular)";

            if (linea != null)
            {
                // Invoca el método de la estructura que recorre el anillo de nodos
                txtLineaTiempo.Text = linea.MostrarLoteCircular();
            }
            else
            {
                txtLineaTiempo.Text = "No hay datos registrados en la línea de tiempo.";
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra esta ventana flotante
        }
    }
}
