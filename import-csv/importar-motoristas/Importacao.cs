using import_csv.Models;
using importar_motoristas.Business;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace importar_motoristas
{
    public partial class Importacao : Form
    {
        public Importacao()
        {
            InitializeComponent();
        }

        private void btnDiretorio_Click(object sender, EventArgs e)
        {
            if(openFile.ShowDialog() ==  DialogResult.OK)
            {
                txtPath.Text = openFile.FileName;
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Aguarde... Processando";
            Application.DoEvents();
            ImportarMotoristas importarMotoristas = new ImportarMotoristas();
            importarMotoristas.Importar(openFile.FileName);
            lblStatus.Text = "";
            Application.DoEvents();
        }
    }
}
