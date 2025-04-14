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
            // Добавим подсказку в текстовое поле
            InputTextBox.Text = "Пример ввода:\n1/2x1 + 3x2 = 4\nx1 - x2 = 1";
            InputTextBox.ForeColor = System.Drawing.Color.Gray;
            InputTextBox.GotFocus += (s, e) => { if (InputTextBox.Text.StartsWith("Пример")) InputTextBox.Text = ""; InputTextBox.ForeColor = System.Drawing.Color.Black; };
        }

        // Обработчик кнопки "Решить"
        private void SolveButton_Click(object sender, EventArgs e)
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input) || input.StartsWith("Пример"))
            {
                OutputTextBox.Text = "Ошибка: Введите систему уравнений, например:\n1/2x1 + 3x2 = 4\nx1 - x2 = 1";
                return;
            }

            try
            {
                // Проверяем корректность входных данных
                if (!_controller.ValidateInput(input, out string errorMessage))
                {
                    OutputTextBox.Text = $"Ошибка: {errorMessage}";
                    return;
                }

                var result = _controller.SolveSystem(input);
                OutputTextBox.Text = result.ToString();
            }
            catch (ArgumentException ex)
            {
                OutputTextBox.Text = $"Ошибка: {ex.Message}";
            }
            catch (Exception ex)
            {
                OutputTextBox.Text = $"Произошла неизвестная ошибка: {ex.Message}";
            }
        }

        // Обработчик кнопки "Очистить"
        private void ClearButton_Click(object sender, EventArgs e)
        {
            InputTextBox.Text = string.Empty;
            OutputTextBox.Text = string.Empty;
        }

        // Обработчик кнопки "Загрузить из файла"
        private async void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл с системой уравнений";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string input = await _fileManager.ReadInputAsync(openFileDialog.FileName);
                        InputTextBox.Text = input;
                        OutputTextBox.Text = "Файл успешно загружен. Нажмите 'Решить' для обработки.";
                    }
                    catch (Exception ex)
                    {
                        OutputTextBox.Text = $"Ошибка при загрузке файла: {ex.Message}";
                    }
                }
            }
        }

        // Обработчик кнопки "Сохранить результат"
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OutputTextBox.Text))
            {
                OutputTextBox.Text = "Ошибка: Нет результата для сохранения.";
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить результат";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _fileManager.SaveResultAsync(OutputTextBox.Text, saveFileDialog.FileName);
                        OutputTextBox.Text += $"\r\nРезультат сохранён в {saveFileDialog.FileName}";
                    }
                    catch (Exception ex)
                    {
                        OutputTextBox.Text = $"Ошибка при сохранении файла: {ex.Message}";
                    }
                }
            }
        }

        // Обработчик кнопки "Документация" (заглушка)
        private void DocumentationButton_Click(object sender, EventArgs e)
        {
            OutputTextBox.Text = "Документация находится в разработке.";
        }
    }
}