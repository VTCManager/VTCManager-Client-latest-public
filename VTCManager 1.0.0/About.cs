using System;
using System.Windows.Forms;

namespace VTCManager_1._0._0
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Utilities util = new Utilities();
            util.Reg_Schreiben("Version", Main.labelRevision, "TruckersMP_Autorun");
        }
    }
}
