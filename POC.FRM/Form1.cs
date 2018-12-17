using POC.DTO;
using POC.FRM.FilaDebitoService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POC.FRM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            using (var svc = new FilaDebitoService.FilaDebitoClient())
            {
                svc.Dispatch(1, "http://localhost:59865/FilaDebito/FilaDebito.svc");
            }

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Requisisao e resposta na fila de debitos realizados com sucesso");
        }
    }
}
