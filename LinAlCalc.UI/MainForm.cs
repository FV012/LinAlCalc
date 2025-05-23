using LinAlCalc.Controller;
using LinAlCalc.FileIO;
using LinAlCalc.Solver;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace LinAlCalc.UI
{
    public partial class MainForm : Form
    {
        private readonly LinearSystemController _controller = new();
        private readonly FileManager _fileManager = new();

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            InitializeMatrixGrid();
        }

        private void InitializeMatrixGrid()
        {
            MatrixGrid.Columns.Clear();
            MatrixGrid.Rows.Clear();
            for (int j = 0; j < 3; j++)
            {
                MatrixGrid.Columns.Add($"col{j}", $"x{j + 1}");
                MatrixGrid.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                MatrixGrid.Columns[j].ValueType = typeof(string);
            }
            MatrixGrid.Columns.Add("col_b", "b");
            MatrixGrid.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            MatrixGrid.Columns[3].ValueType = typeof(string);
            MatrixGrid.RowCount = 3;
            MatrixGrid.Refresh();
        }

        private async void LoadButton_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog.Title = "Открыть файл с системой уравнений";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (InputTabControl.SelectedTab == TextInputTab)
                    {
                        string input = await FileManager.ReadInputAsync(openFileDialog.FileName);
                        InputTextBox.Text = input;
                        OutputListView.Items.Clear();
                    }
                    else if (InputTabControl.SelectedTab == MatrixInputTab)
                    {
                        var (coefficients, constants) = await FileManager.ReadMatrixInputAsync(openFileDialog.FileName);
                        int rowCount = coefficients.GetLength(0);
                        int colCount = coefficients.GetLength(1);

                        RowCountUpDown.ValueChanged -= RowCountUpDown_ValueChanged!;
                        ColumnCountUpDown.ValueChanged -= ColumnCountUpDown_ValueChanged!;

                        try
                        {
                            MatrixGrid.Columns.Clear();
                            MatrixGrid.Rows.Clear();
                            for (int j = 0; j < colCount; j++)
                            {
                                MatrixGrid.Columns.Add($"col{j}", $"x{j + 1}");
                                MatrixGrid.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                                MatrixGrid.Columns[j].ValueType = typeof(string);
                            }
                            MatrixGrid.Columns.Add("col_b", "b");
                            MatrixGrid.Columns[colCount].SortMode = DataGridViewColumnSortMode.NotSortable;
                            MatrixGrid.Columns[colCount].ValueType = typeof(string);
                            MatrixGrid.RowCount = rowCount;

                            for (int i = 0; i < rowCount; i++)
                            {
                                for (int j = 0; j < colCount; j++)
                                {
                                    MatrixGrid[j, i].Value = coefficients[i, j].ToString(CultureInfo.InvariantCulture);
                                }
                                MatrixGrid[colCount, i].Value = constants[i].ToString(CultureInfo.InvariantCulture);
                            }

                            RowCountUpDown.Value = rowCount;
                            ColumnCountUpDown.Value = colCount;

                            MatrixGrid.Refresh();
                        }
                        finally
                        {
                            RowCountUpDown.ValueChanged += RowCountUpDown_ValueChanged!;
                            ColumnCountUpDown.ValueChanged += ColumnCountUpDown_ValueChanged!;
                        }

                        OutputListView.Items.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SolveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SolutionResult? result = null;

                if (InputTabControl.SelectedTab == TextInputTab)
                {
                    string input = InputTextBox.Text;
                    if (!LinearSystemController.ValidateInput(input, out string errorMessage))
                    {
                        MessageBox.Show($"Ошибка: {errorMessage}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = LinearSystemController.SolveSystem(input);
                }
                else if (InputTabControl.SelectedTab == MatrixInputTab)
                {
                    int rowCount = (int)RowCountUpDown.Value;
                    int colCount = (int)ColumnCountUpDown.Value;
                    var coefficients = new double[rowCount, colCount];
                    var constants = new double[rowCount];

                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < colCount; j++)
                        {
                            string value = MatrixGrid[j, i].Value?.ToString()!;
                            if (string.IsNullOrEmpty(value) || !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedValue))
                            {
                                MessageBox.Show($"Некорректное значение в ячейке [{i + 1}, {j + 1}]", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            coefficients[i, j] = parsedValue;
                        }
                        string constantValue = MatrixGrid[colCount, i].Value?.ToString()!;
                        if (string.IsNullOrEmpty(constantValue) || !double.TryParse(constantValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedConstant))
                        {
                            MessageBox.Show($"Некорректное значение константы в строке {i + 1}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        constants[i] = parsedConstant;
                    }

                    StringBuilder inputBuilder = new();
                    for (int i = 0; i < rowCount; i++)
                    {
                        StringBuilder equation = new();
                        bool firstTerm = true;
                        for (int j = 0; j < colCount; j++)
                        {
                            double coeff = coefficients[i, j];
                            if (Math.Abs(coeff) < 1e-10)
                                continue;
                            if (!firstTerm && coeff > 0)
                                equation.Append('+');
                            if (Math.Abs(coeff) == 1.0)
                                equation.Append(coeff < 0 ? "-" : "");
                            else if (Math.Abs(coeff) == -1.0)
                                equation.Append('-');
                            else
                                equation.Append(coeff.ToString(CultureInfo.InvariantCulture));
                            equation.Append($"x{j + 1}");
                            firstTerm = false;
                        }
                        if (firstTerm)
                            equation.Append('0');
                        equation.Append($" = {constants[i].ToString(CultureInfo.InvariantCulture)}");
                        inputBuilder.AppendLine(equation.ToString());
                    }

                    string input = inputBuilder.ToString();
                    if (!LinearSystemController.ValidateInput(input, out string errorMessage))
                    {
                        MessageBox.Show($"Ошибка: {errorMessage}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = LinearSystemController.SolveSystem(input);
                }

                OutputListView.Items.Clear();

                if (result!.Status == SolutionStatus.NoSolution)
                {
                    OutputListView.Items.Add(new ListViewItem(["Статус", "Система не имеет решений"]));
                }
                else if (result.Status == SolutionStatus.InfiniteSolutions)
                {
                    OutputListView.Items.Add(new ListViewItem(["Статус", "Система имеет бесконечно много решений"]));
                }
                else if (result.Status == SolutionStatus.UniqueSolution)
                {
                    foreach (var solution in result.Solutions)
                    {
                        OutputListView.Items.Add(new ListViewItem([solution.Key, solution.Value]));
                    }
                    OutputListView.Items.Add(new ListViewItem(["Погрешность", $"{result.ResidualNorm.ToString(CultureInfo.InvariantCulture)}"]));
                }
                else
                {
                    OutputListView.Items.Add(new ListViewItem(["Статус", "Статус решения не определён"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (InputTabControl.SelectedTab == TextInputTab)
            {
                InputTextBox.Text = string.Empty;
            }
            else if (InputTabControl.SelectedTab == MatrixInputTab)
            {
                RowCountUpDown.ValueChanged -= RowCountUpDown_ValueChanged!;
                ColumnCountUpDown.ValueChanged -= ColumnCountUpDown_ValueChanged!;

                try
                {
                    MatrixGrid.Columns.Clear();
                    MatrixGrid.Rows.Clear();
                    for (int j = 0; j < 3; j++)
                    {
                        MatrixGrid.Columns.Add($"col{j}", $"x{j + 1}");
                        MatrixGrid.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                        MatrixGrid.Columns[j].ValueType = typeof(string);
                    }
                    MatrixGrid.Columns.Add("col_b", "b");
                    MatrixGrid.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    MatrixGrid.Columns[3].ValueType = typeof(string);
                    MatrixGrid.RowCount = 3;
                    RowCountUpDown.Value = 3;
                    ColumnCountUpDown.Value = 3;
                    MatrixGrid.Refresh();
                }
                finally
                {
                    RowCountUpDown.ValueChanged += RowCountUpDown_ValueChanged!;
                    ColumnCountUpDown.ValueChanged += ColumnCountUpDown_ValueChanged!;
                }
            }
            OutputListView.Items.Clear();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "Сохранить решение системы уравнений";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (OutputListView.Items.Count == 0)
                    {
                        MessageBox.Show("Нет решения для сохранения. Сначала решите систему.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    StringBuilder csvBuilder = new();
                    csvBuilder.AppendLine("Variable,Value");
                    foreach (ListViewItem item in OutputListView.Items)
                    {
                        string variable = item.SubItems[0].Text;
                        string value = item.SubItems[1].Text;
                        if (variable == "Статус")
                        {
                            string status;
                            if (value == "Система не имеет решений")
                                status = "NoSolution";
                            else if (value == "Система имеет бесконечно много решений")
                                status = "InfiniteSolutions";
                            else
                                status = "Undefined";
                            csvBuilder.Clear();
                            csvBuilder.AppendLine($"Status,{status}");
                            break;
                        }
                        else
                        {
                            variable = variable.Replace(",", "\\,");
                            value = value.Replace(",", "\\,");
                            csvBuilder.AppendLine($"{variable},{value}");
                        }
                    }
                    // Вот тут используем await!
                    await FileManager.SaveInputAsync(csvBuilder.ToString(), saveFileDialog.FileName);
                    MessageBox.Show("Решение успешно сохранено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ProcessStartInfo processStartInfo = new()
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
                    section = "vvod_sistemy_cherez_pole_tekstovogo_vvoda.htm";
                }
                else
                {
                    section = "vvod_sistemy_cherez_tablitsu_koehffitsientov.htm";
                }

                Help.ShowHelp(this, helpFilePath, HelpNavigator.Topic, section);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии справки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RowCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (InputTabControl.SelectedTab == MatrixInputTab)
            {
                int newRowCount = (int)RowCountUpDown.Value;
                if (newRowCount >= 1)
                {
                    MatrixGrid.AllowUserToAddRows = false;
                    MatrixGrid.RowCount = newRowCount;
                    MatrixGrid.Refresh();
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                HelpButton_Click(sender, e);
            }
        }

        private void ColumnCountUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (InputTabControl.SelectedTab == MatrixInputTab)
            {
                int newColCount = (int)ColumnCountUpDown.Value;
                if (newColCount >= 1)
                {
                    int rowCount = (int)RowCountUpDown.Value;
                    var oldValues = new Dictionary<(int, int), string>();
                    for (int i = 0; i < MatrixGrid.RowCount; i++)
                    {
                        for (int j = 0; j < MatrixGrid.ColumnCount; j++)
                        {
                            if (MatrixGrid[j, i].Value != null)
                            {
                                oldValues[(i, j)] = MatrixGrid[j, i].Value.ToString()!;
                            }
                        }
                    }

                    MatrixGrid.Columns.Clear();
                    MatrixGrid.AllowUserToAddRows = false;
                    for (int j = 0; j < newColCount; j++)
                    {
                        MatrixGrid.Columns.Add($"col{j}", $"x{j + 1}");
                        MatrixGrid.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                        MatrixGrid.Columns[j].ValueType = typeof(string);
                    }
                    MatrixGrid.Columns.Add("col_b", "b");
                    MatrixGrid.Columns[newColCount].SortMode = DataGridViewColumnSortMode.NotSortable;
                    MatrixGrid.Columns[newColCount].ValueType = typeof(string);

                    MatrixGrid.RowCount = rowCount;

                    for (int i = 0; i < rowCount; i++)
                    {
                        for (int j = 0; j < newColCount + 1; j++)
                        {
                            if (oldValues.TryGetValue((i, j), out string? value))
                            {
                                MatrixGrid[j, i].Value = value;
                            }
                        }
                    }

                    MatrixGrid.Refresh();
                }
            }
        }
    }
}