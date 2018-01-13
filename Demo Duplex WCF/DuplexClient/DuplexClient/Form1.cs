using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
namespace DuplexClient
{
    public partial class Form1 : Form, ReportServiceReference.IReportServiceCallback
    {
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        public void Process(int percentangeCompleted)
        {
            textBox1.Text = String.Format("{0} % completed", percentangeCompleted);
        }

        private void btnProcessReport_Click(object sender, EventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            ReportServiceReference.ReportServiceClient client = new ReportServiceReference.ReportServiceClient(instanceContext);
            client.ProcessReport();
        }
    }
}
