using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoradoMessias
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent(); this.Icon = new Icon("if_Jesus_362064.ico");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Você não pode enviar um email sem texto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                try
                {
                    DateTime today = DateTime.Today;

                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("#######@gmail.com", "########");
                    MailMessage mm = new MailMessage("#########@gmail.com", "#########@gmail.com", "Reporta Bug - ", textBox1.Text);
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.Send(mm);
                }
                catch (Exception)
                {

                    MessageBox.Show("Algum problema foi encontrado no meio do caminho.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                MessageBox.Show("Mensagem Enviada com sucesso.", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
        }

        private void report_Load(object sender, EventArgs e)
        {

        }
    }
}
