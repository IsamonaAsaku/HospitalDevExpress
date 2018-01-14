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
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm, HospitalServiceReference.IServiceHospitalCallback
    {
        InstanceContext instanceContext;
        HospitalServiceReference.ServiceHospitalClient client;
        public FrmMain()
        {
            InitializeComponent();
            instanceContext = new InstanceContext(this);
            client = new HospitalServiceReference.ServiceHospitalClient(instanceContext);
            CheckForIllegalCrossThreadCalls = false;
        }

        public void Process(int percentangeCompleted)
        {
            textEdit1.Text = String.Format("{0} % completed", percentangeCompleted);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            client.ProcessReport();
        }
    }
}
