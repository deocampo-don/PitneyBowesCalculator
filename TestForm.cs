using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Packed_And_Ready;

namespace WindowsFormsApp1
{
    public partial class TestForm : Form
    {


        public TestForm()
        {
            InitializeComponent();

            PackedRowControl packedRow = new PackedRowControl();
            packedRow.Dock = DockStyle.Fill;
          

            this.Controls.Add(packedRow);

        }
    }
}
