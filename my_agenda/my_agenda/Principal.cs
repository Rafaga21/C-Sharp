using System;
using System.Data;
using System.Windows.Forms;

namespace my_agenda
{
    public partial class Principal : Form
    {
        private DataTable dataTable;

        public Principal()
        {
            InitializeComponent();
            ///////////////////////////////////////////////////
            start();///////////////////////////////////////////
            ///////////////////////////////////////////////////
        }

        private void start()
        {
            refresDGV();
        }

        private void refresDGV()
        {
            dgvAgenda.DataSource = dataTable = new Agenda().consultar();
            isEnable(false);
            txtClear();
        }

        private void isEnable(Boolean answer)
        {
            btnEliminar.Enabled = answer;
            btnModificar.Enabled = answer;
        }

        private void txtClear()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtFiltrar.Clear();
        }

        private void btnRegistrar_Click(object sender, System.EventArgs e)
        {
            string nombre = txtNombre.Text.Length > 0 ? txtNombre.Text : null;
            string telefono = txtTelefono.Text.Length > 0 ? txtTelefono.Text : null;
            if (nombre != null && telefono != null)
            {
                if(new Agenda(nombre, telefono).insertar())
                {
                    refresDGV();
                    txtClear();
                    MessageBox.Show(String.Format("Contacto {0} Agregado.", nombre), "Guardado!");
                }
                else
                {
                    MessageBox.Show("No se Pudo Guardar el Contacto.", "Erro!");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                String.Format(
                    "¿Actualizar Contacto?\n\nNombre: \n{0} -> {1}\n\nTeléfono: \n{2} -> {3}\n",
                    dgvAgenda.CurrentRow.Cells["nombre"].Value.ToString(),
                    txtNombre.Text,
                    dgvAgenda.CurrentRow.Cells["telefono"].Value.ToString(),
                    txtTelefono.Text
                ),
                "Actualizar",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation
            );
            int id = Convert.ToInt32(dgvAgenda.CurrentRow.Cells["id"].Value);
            if (dr == DialogResult.OK)
            {
                if (new Agenda(id, txtNombre.Text, txtTelefono.Text).actualizar())
                {
                    refresDGV();
                    MessageBox.Show("Contacto Actualizado!", "Exito!");
                }
                else
                {
                    MessageBox.Show("No se Pudo Actualizar el Contacto.", "Error!");
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                String.Format("¿Eliminar Contacto {0}?", dgvAgenda.CurrentRow.Cells["nombre"].Value.ToString()),
                "Eliminar",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation
            );
            if (dr == DialogResult.OK)
            {
                if (new Agenda(Convert.ToInt32(dgvAgenda.CurrentRow.Cells["id"].Value)).eliminar())
                {
                    refresDGV();
                    MessageBox.Show("Contacto Eliminado!");
                }
                else
                {
                    MessageBox.Show("No se Pudo Eliminar el Contacto.", "Error!");
                }
            }
        }

        private void txtFiltrar_TextChanged(object sender, System.EventArgs e)
        {
            dataTable.DefaultView.RowFilter = string.Format("nombre LIKE '%{0}%' OR telefono LIKE '%{0}%'", txtFiltrar.Text);
        }

        private void dgvAgenda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtNombre.Text = dgvAgenda.CurrentRow.Cells["nombre"].Value.ToString();
            txtTelefono.Text = dgvAgenda.CurrentRow.Cells["telefono"].Value.ToString();
            isEnable(true);
        }
    }
}
