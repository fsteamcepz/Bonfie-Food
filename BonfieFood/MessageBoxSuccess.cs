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
    public partial class MessageBoxSuccess : Form
    {
        public MessageBoxSuccess(string message)
        {
            InitializeComponent();
            message_success.Text = message;
        }
        public static void Show(string message)
        {
            MessageBoxSuccess successForm = new MessageBoxSuccess(message);

            successForm.ShowDialog();
        }
        private void btnOK_Click(object sender, EventArgs e)
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
