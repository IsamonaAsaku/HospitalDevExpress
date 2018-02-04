using HospitalDevexpress.Service_Hospital;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Windows.Forms;
namespace HospitalDevexpress
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm, IHospitalServiceCallback
    {
        private string clientId;
        private HospitalServiceClient hospitalServiceClient;
        private InstanceContext instanceContext;
        public FrmMain()
        {
            InitializeComponent();
            clientId = new Guid().ToString();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            instanceContext = new InstanceContext(this);
            hospitalServiceClient = new HospitalServiceClient(instanceContext);
            hospitalServiceClient.ClientCredentials.ClientCertificate = System.Net.CredentialCache.DefaultCredentials;
            try
            {
                hospitalServiceClient.Subscribe(clientId);
                txtID.Text = clientId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            System.Timers.Timer timer = new System.Timers.Timer(3000);
            timer.Elapsed +=
            (
                (object o, ElapsedEventArgs args) =>
                {
                    try
                    {
                        if (hospitalServiceClient.State == CommunicationState.Faulted)
                        {
                            hospitalServiceClient.Abort();
                            hospitalServiceClient = new HospitalServiceClient(instanceContext);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            );
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hospitalServiceClient != null)
            {
                try
                {
                    if (hospitalServiceClient.State != CommunicationState.Faulted)
                    {
                        hospitalServiceClient.Unsubscribe(clientId);
                        hospitalServiceClient.Close();
                    }
                }
                catch
                {
                    hospitalServiceClient.Abort();
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    if (hospitalServiceClient.State == CommunicationState.Faulted)
                    {
                        hospitalServiceClient.Abort();
                        hospitalServiceClient = new HospitalServiceClient(instanceContext);
                    }
                    hospitalServiceClient.SendMessage(clientId, message, txtReciever.Text);
                    txtMessage.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Notification(string message)
        {
            if (!string.IsNullOrEmpty(txtChat.Text))
            {
                txtChat.Text += Environment.NewLine;
            }
            txtChat.Text += message;
        }
    }
}
