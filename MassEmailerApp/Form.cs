using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //test data
            string[] a51 = { "tom hanks", "tomhanks@hotmail.com", "macho@gmail.com", "rachel1" };

            string[][] contactArray = {a51};

            for (int i = 0; i < contactArray.Length; i++ )
            {
                sendEmail(contactArray[i]);
            }
        }

        private void sendEmail(String[] reciepientArray)
        {
            String rName = reciepientArray[0];
            String rEmail = reciepientArray[1];
            String rUsername = reciepientArray[2];
            String rPassword = reciepientArray[3];

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("<email>");
                mail.To.Add(rEmail);
                mail.Subject = "Azure Access Key";
                mail.Body = 
                    "Dear " + rName
                    + " Sincerely, \r\n -Phuc";
                //MessageBox.Show(mail.Body.ToString());
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("<email>", "<Password>");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
                textBox1.Text += "Email sent to... " + rName + " \r\n";
            }
            catch (Exception ex)
            {
                textBox1.Text += "Email failed send to... " + rName + ". Error: " + ex + " \r\n";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
