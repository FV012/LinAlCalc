using System;
using System.Windows.Forms;
using LinAlCalc.Controller;
using LinAlCalc.FileIO;
using LinAlCalc.DataProcessing;
using System.Text;
using System.Diagnostics;
using System.IO;

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
            InitializeMatrixGrid();
            this.KeyPreview = true; // Разрешаем форме перехватывать события клавиш
            this.KeyDown += new KeyEventHandler(MainForm_KeyDown); // Подписываемся на событие KeyDown
        }

        private void InitializeMatrixGrid()
        {
            try
            {
                MatrixGrid.AllowUserToAddRows = false;
                MatrixGrid.RowCount = (int)RowCountUpDown.Value;
                MatrixGrid.ColumnCount = (int)ColumnCountUpDown.Value + 1;

                for (int i = 0; i < (int)ColumnCountUpDown.Value; i++)
                {
                    MatrixGrid.Columns[i].HeaderText = $"x{i + 1}";
                    MatrixGrid.Columns[i].Width = 60;
                }
                MatrixGrid.Columns[(int)ColumnCountUpDown.Value].HeaderText = "=";
                MatrixGrid.Columns[(int)ColumnCountUpDown.Value].Width = 60;

                for (int i = 0; i < MatrixGrid.RowCount; i++)
                {
                    for (int j = 0; j < MatrixGrid.ColumnCount; j++)
                    {
                        MatrixGrid[j, i].Value = "0";
                    }
                }

                MatrixGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации таблицы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string resultText;
                if (InputTabControl.SelectedTab == TextInputTab)
                {
                    string input = InputTextBox.Text;
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        MessageBox.Show("Пожалуйста, введите систему уравнений.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!_controller.ValidateInput(input, out string errorMessage))
                    {
                        MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var result = _controller.SolveSystem(input);
                    resultText = result.ToString();
                }
                else
                {
                    var system = ReadMatrixInput();
                    if (system == null)
                    {
                        MessageBox.Show("Пожалуйста, заполните все ячейки корректными числами.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DataProcessor.ValidateInput(system);
                    var A = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(system.Coefficients);
                    var b = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(system.Constants);
                    var result = LinAlCalc.Solver.LinearSystemSolver.Solve(A, b);
                    resultText = result.ToString();
                }

                if (!resultText.Contains("\n"))
                {
                    MessageBox.Show("Warning: No newline characters detected in result text.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                OutputTextBox.Text = resultText;
                OutputTextBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataProcessor.LinearSystem ReadMatrixInput()
        {
            int rows = (int)RowCountUpDown.Value;
            int cols = (int)ColumnCountUpDown.Value;
            var coefficients = new double[rows, cols];
            var constants = new double[rows];
            string errorMessage = string.Empty;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var cellValue = MatrixGrid[j, i].Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        errorMessage = $"Ячейка ({i + 1}, x{j + 1}) пуста.";
                        MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }

                    if (!double.TryParse(cellValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
                    {
                        errorMessage = $"Некорректное число в ячейке ({i + 1}, x{j + 1}): '{cellValue}'.";
                        MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    coefficients[i, j] = value;
                }

                var constantValue = MatrixGrid[cols, i].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(constantValue))
                {
                    errorMessage = $"Ячейка свободного члена в строке {i + 1} пуста.";
                    MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                if (!double.TryParse(constantValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double constant))
                {
                    errorMessage = $"Некорректное число в ячейке свободного члена строки {i + 1}: '{constantValue}'.";
                    MessageBox.Show(errorMessage, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                constants[i] = constant;
            }

            return new DataProcessor.LinearSystem
            {
                Coefficients = coefficients,
                Constants = constants
            };
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (InputTabControl.SelectedTab == TextInputTab)
            {
                InputTextBox.Clear();
                OutputTextBox.Clear();
            }
            else
            {
                for (int i = 0; i < MatrixGrid.RowCount; i++)
                {
                    for (int j = 0; j < MatrixGrid.ColumnCount; j++)
                    {
                        MatrixGrid[j, i].Value = "0";
                    }
                }
                OutputTextBox.Clear();
            }
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            if (InputTabControl.SelectedTab != TextInputTab)
            {
                MessageBox.Show("Загрузка из файла доступна только в режиме текстового ввода.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new OpenFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Выберите файл с системой уравнений"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string input = await _fileManager.ReadInputAsync(dialog.FileName);
                    InputTextBox.Text = input;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OutputTextBox.Text))
            {
                MessageBox.Show("Нет результатов для сохранения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Title = "Сохранить результат"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _fileManager.SaveResultAsync(OutputTextBox.Text, dialog.FileName);
                    MessageBox.Show("Результат успешно сохранён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DocumentationButton_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFilePath = Path.Combine(Application.StartupPath, "Documentation.chm");
                if (File.Exists(helpFilePath))
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo
                    {
                        FileName = helpFilePath,
                        UseShellExecute = true
                    };
                    Process.Start(processStartInfo);
                }
                else
                {
                    MessageBox.Show("Файл документации 'Documentation.chm' не найден. Убедитесь, что файл находится в папке приложения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии документации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            try
            {
                string helpFilePath = Path.Combine(Application.StartupPath, "Help.chm");
                if (!File.Exists(helpFilePath))
                {
                    MessageBox.Show("Файл справки 'Help.chm' не найден. Убедитесь, что файл находится в папке приложения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string section;
                if (InputTabControl.SelectedTab == TextInputTab)
                {
                    section = "vvod_sistemy_cherez_pole_tekstovogo_vvoda.htm"; // Раздел для текстового ввода
                }
                else
                {
                    section = "vvod_sistemy_cherez_tablitsu_koehffitsientov.htm"; // Раздел для матричного ввода
                }

                Help.ShowHelp(this, helpFilePath, HelpNavigator.Topic, section);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии справки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true; // Отмечаем, что событие обработано
                HelpButton_Click(sender, e); // Вызываем логику кнопки "Помочь"
            }
        }

        private void RowCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int newRows = (int)RowCountUpDown.Value;
                MatrixGrid.RowCount = newRows;
                for (int i = 0; i < newRows; i++)
                {
                    for (int j = 0; j < MatrixGrid.ColumnCount; j++)
                    {
                        if (MatrixGrid[j, i].Value == null)
                        {
                            MatrixGrid[j, i].Value = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения количества строк: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColumnCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int newCols = (int)ColumnCountUpDown.Value + 1;
                MatrixGrid.ColumnCount = newCols;
                for (int i = 0; i < (int)ColumnCountUpDown.Value; i++)
                {
                    MatrixGrid.Columns[i].HeaderText = $"x{i + 1}";
                    MatrixGrid.Columns[i].Width = 60;
                }
                MatrixGrid.Columns[(int)ColumnCountUpDown.Value].HeaderText = "=";
                MatrixGrid.Columns[(int)ColumnCountUpDown.Value].Width = 60;

                for (int i = 0; i < MatrixGrid.RowCount; i++)
                {
                    for (int j = 0; j < newCols; j++)
                    {
                        if (MatrixGrid[j, i].Value == null)
                        {
                            MatrixGrid[j, i].Value = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения количества столбцов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}