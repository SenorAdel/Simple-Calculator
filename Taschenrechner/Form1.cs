using System;
using System.Data;           // for DataTable
using System.Linq;
using System.Windows.Forms;

namespace Taschenrechner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
        }

        private void TextToResult(string text)
        {
            if (txtResult.Text == "Error")
                txtResult.Text = "";

            txtResult.Text += text;
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            TextToResult(btn.Text);
        }

        private void OperatorButton_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            txtResult.Text += " " + btn.Text + " ";
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (!txtResult.Text.Contains("."))
                txtResult.Text += ".";
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            try
            {
                var expr = txtResult.Text
                                .Replace('÷', '/')
                                .Replace('×', '*');

                var table = new DataTable();
                var result = table.Compute(expr, "");

                txtResult.Text = result?.ToString() ?? "Error";
            }
            catch
            {
                txtResult.Text = "Error";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Length > 0)
            {
                txtResult.Text = txtResult.Text.Substring(0, txtResult.Text.Length - 1);
            }
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtResult.Text, out double n))
            {
                if (n >= 0)
                    txtResult.Text = Math.Sqrt(n).ToString();
                else
                    MessageBox.Show("Cannot calculate square root of a negative number.");
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid number.");
            }
        }

        private void btnPercentage_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtResult.Text, out double n))
                txtResult.Text = (n / 100).ToString();
            else
                MessageBox.Show("Invalid input. Please enter a valid number.");
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                TextToResult(e.KeyChar.ToString());
            }
            else if ("+-*/".Contains(e.KeyChar))
            {
                txtResult.Text += " " + e.KeyChar + " ";
            }
            else if (e.KeyChar == '.')
            {
                btnDecimal.PerformClick();
            }
            else if (e.KeyChar == '=' || e.KeyChar == '\r')
            {
                btnEquals.PerformClick();
            }
            else if (e.KeyChar == '\b')
            {
                btnBackspace.PerformClick();
            }
            else if (e.KeyChar == '%')
            {
                btnPercentage.PerformClick();
            }
        }
    }
}
