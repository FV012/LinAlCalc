using LinAlCalc.Controller;
using LinAlCalc.FileIO;
using System;
using System.Windows.Forms;

namespace LinAlCalc.UI
{
    public partial class MainForm : Form
    {
        private readonly LinearSystemController _controller;
        private readonly FileManager _fileManager;

        public MainForm()
        {
            InitializeComponent();
            _controller = new LinearSystemController();
            _fileManager = new FileManager();
            // ������� ��������� � ��������� ����
            InputTextBox.Text = "������ �����:\n1/2x1 + 3x2 = 4\nx1 - x2 = 1";
            InputTextBox.ForeColor = System.Drawing.Color.Gray;
            InputTextBox.GotFocus += (s, e) => { if (InputTextBox.Text.StartsWith("������")) InputTextBox.Text = ""; InputTextBox.ForeColor = System.Drawing.Color.Black; };
        }

        // ���������� ������ "������"
        private void SolveButton_Click(object sender, EventArgs e)
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input) || input.StartsWith("������"))
            {
                OutputTextBox.Text = "������: ������� ������� ���������, ��������:\n1/2x1 + 3x2 = 4\nx1 - x2 = 1";
                return;
            }

            try
            {
                // ��������� ������������ ������� ������
                if (!_controller.ValidateInput(input, out string errorMessage))
                {
                    OutputTextBox.Text = $"������: {errorMessage}";
                    return;
                }

                var result = _controller.SolveSystem(input);
                OutputTextBox.Text = result.ToString();
            }
            catch (ArgumentException ex)
            {
                OutputTextBox.Text = $"������: {ex.Message}";
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"��������� ����������� ������: {ex.Message}";
            }
        }

        // ���������� ������ "��������"
        private void ClearButton_Click(object sender, EventArgs e)
        {
            InputTextBox.Text = string.Empty;
            OutputTextBox.Text = string.Empty;
        }

        // ���������� ������ "��������� �� �����"
        private async void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*";
                openFileDialog.Title = "�������� ���� � �������� ���������";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string input = await _fileManager.ReadInputAsync(openFileDialog.FileName);
                        InputTextBox.Text = input;
                        OutputTextBox.Text = "���� ������� ��������. ������� '������' ��� ���������.";
                    }
                    catch (Exception ex)
                    {
                        OutputTextBox.Text = $"������ ��� �������� �����: {ex.Message}";
                    }
                }
            }
        }

        // ���������� ������ "��������� ���������"
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OutputTextBox.Text))
            {
                OutputTextBox.Text = "������: ��� ���������� ��� ����������.";
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*";
                saveFileDialog.Title = "��������� ���������";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _fileManager.SaveResultAsync(OutputTextBox.Text, saveFileDialog.FileName);
                        OutputTextBox.Text += $"\r\n��������� ������� � {saveFileDialog.FileName}";
                    }
                    catch (Exception ex)
                    {
                        OutputTextBox.Text = $"������ ��� ���������� �����: {ex.Message}";
                    }
                }
            }
        }

        // ���������� ������ "������������" (��������)
        private void DocumentationButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = "������������ ��������� � ����������.";
        }
    }
}