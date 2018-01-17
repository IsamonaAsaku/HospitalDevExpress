using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace HospitalDevexpress
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        InstanceContext instanceContext;
        HospitalService.HospitalServiceClient client;
        public FrmMain()
        {
            InitializeComponent();
            instanceContext = new InstanceContext(this);
            client = new HospitalService.HospitalServiceClient();
            client.ClientCredentials.UserName.UserName = "far.ultimate@gmail.com";
            client.ClientCredentials.UserName.Password = "Thuan123.";
            CheckForIllegalCrossThreadCalls = false;
        }

        public void Process(int percentangeCompleted)
        {
            textEdit1.Text = String.Format("{0} % completed", percentangeCompleted);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(client.GetMessage("Thuận"));
        }
    }
}
