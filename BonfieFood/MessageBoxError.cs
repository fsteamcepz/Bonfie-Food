using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonfieFood
{
    public partial class MessageBoxError : Form
    {
        public MessageBoxError(string message)
        {
            InitializeComponent();
            message_error.Text = message;
        }
        public static void Show(string message)
        {
            MessageBoxError errorForm = new MessageBoxError(message);

            errorForm.ShowDialog();
        }
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        // відтворення звуку при кліку поза межами форми
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SystemSounds.Hand.Play();
        }
    }
}
