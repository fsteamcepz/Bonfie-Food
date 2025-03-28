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
    public partial class MessageBoxAttention : Form
    {
        // делегат для передачі результату
        public bool UserConfirmed { get; private set; }

        public MessageBoxAttention(string message)
        {
            InitializeComponent();
            message_attention.Text = message;
        }
        public static bool Show(string message)
        {
            using (MessageBoxAttention attentionForm = new MessageBoxAttention(message))
            {
                attentionForm.ShowDialog(); // відкриваємо модальну форму
                return attentionForm.UserConfirmed; // повертаємо вибір користувача
            }
        }
        private void btnNO_Click(object sender, EventArgs e)
        {
            UserConfirmed = false;
            this.Close();
        }
        private void btnYES_Click(object sender, EventArgs e)
        {
            UserConfirmed = true;
            this.Close();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            SystemSounds.Hand.Play();
        }
    }
}