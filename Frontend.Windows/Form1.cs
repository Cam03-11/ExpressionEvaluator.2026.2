using Backend;
using System.Globalization;

namespace Frontend.Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                AddText(button.Text);
            }
        }

        private void AddText(string text)
        {
            if (txtDisplay.Text.Contains('='))
            {
                txtDisplay.Text = string.Empty;
            }
            txtDisplay.Text += text;
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text[..^1];
            }
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtDisplay.Text = string.Empty;
        }

        private void btnEquals_Click(object? sender, EventArgs e)
        {
            var infix = txtDisplay.Text;
            if (infix.Length == 0 || infix.Contains('='))
            {
                return;
            }

            try
            {
                var result = ExpressionEvaluator.Evalute(infix);
                txtDisplay.Text = $"{infix}={result.ToString(CultureInfo.InvariantCulture)}";
            }
            catch
            {
                MessageBox.Show("Invalid expression.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if ("0123456789.()+-*/^".Contains(e.KeyChar))
            {
                AddText(e.KeyChar.ToString());
            }
            else if (e.KeyChar == '=')
            {
                btnEquals_Click(sender, e);
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                btnDelete_Click(sender, e);
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                btnClear_Click(sender, e);
            }
            e.Handled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnEquals_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
