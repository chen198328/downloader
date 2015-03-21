using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JCRDownload.Code;
namespace JCRDownload
{
    public partial class ViewData : Form
    {
        public List<Journal> Journals { set; get; }
        public ViewData()
        {
            InitializeComponent();
        }

        private void ViewData_Load(object sender, EventArgs e)
        {
            if (Journals != null)
            {
                dataGridView1.DataSource = Journals;
            }
        }
    }
}
