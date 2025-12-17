using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebFormsCalculator
{
    public partial class FrmCalculator : Form
    {
        private string expression = "";
        private bool isResultShown = false;
        private double firstValue = 0;
        private string operation = "";
        private bool isNewEntry = false;
        public FrmCalculator()
        {
            InitializeComponent();
        }

        private void BtnNumber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (isResultShown)
            {
                txtDisplay.Text = "";
                expression = "";
                isResultShown = false;
            }

            txtDisplay.Text += btn.Text;
            expression += btn.Text;
            lblTitle.Text += btn.Text;
        }

        private void BtnOperator_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (string.IsNullOrEmpty(expression))
                return;

            char lastChar = expression[expression.Length - 1];

            // Art arda operatör engeli
            if ("+-*/".Contains(lastChar))
                return;

            txtDisplay.Text += " " + btn.Text + " ";
            lblTitle.Text += " " + btn.Text + " ";
            expression += btn.Text;
        }

        private void BtnEqual_Click(object sender, EventArgs e)
        {
            try
            {
                var result = new DataTable().Compute(expression, null);
                txtDisplay.Text = result.ToString();
                lblTitle.Text = result.ToString();
                expression = result.ToString();
                isResultShown = true;
            }
            catch
            {
                MessageBox.Show("Geçersiz işlem", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
            lblTitle.Text = "";
            expression = "";
            isResultShown = false;
        }

        private void BtnClearEntry_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
            lblTitle.Text = "";
            expression = "";
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(expression))
                return;

            // Expression'dan son karakteri sil
            expression = expression.Substring(0, expression.Length - 1);

            // Display'i expression'a göre güncelle
            txtDisplay.Text = expression
                .Replace("+", " + ")
                .Replace("-", " - ")
                .Replace("*", " * ")
                .Replace("/", " / ");

            txtDisplay.Text = System.Text.RegularExpressions.Regex
                .Replace(txtDisplay.Text, @"\s+", " ")
                .Trim();
            
            lblTitle.Text = expression
                .Replace("+", " + ")
                .Replace("-", " - ")
                .Replace("*", " * ")
                .Replace("/", " / ");

            lblTitle.Text = System.Text.RegularExpressions.Regex
                .Replace(txtDisplay.Text, @"\s+", " ")
                .Trim();

            isResultShown = false;
        }
    }
}
