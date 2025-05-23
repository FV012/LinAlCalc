namespace LinAlCalc.UI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            InputTabControl = new TabControl();
            TextInputTab = new TabPage();
            InputLabel = new Label();
            InputTextBox = new TextBox();
            MatrixInputTab = new TabPage();
            MatrixGrid = new DataGridView();
            RowCountLabel = new Label();
            RowCountUpDown = new NumericUpDown();
            ColumnCountLabel = new Label();
            ColumnCountUpDown = new NumericUpDown();
            OutputLabel = new Label();
            OutputListView = new ListView();
            SolveButton = new Button();
            ClearButton = new Button();
            LoadButton = new Button();
            SaveButton = new Button();
            DocumentationButton = new Button();
            HelpButton = new Button();
            InputTabControl.SuspendLayout();
            TextInputTab.SuspendLayout();
            MatrixInputTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RowCountUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ColumnCountUpDown).BeginInit();
            SuspendLayout();
            // 
            // InputTabControl
            // 
            InputTabControl.Controls.Add(TextInputTab);
            InputTabControl.Controls.Add(MatrixInputTab);
            InputTabControl.Location = new Point(14, 14);
            InputTabControl.Margin = new Padding(4, 3, 4, 3);
            InputTabControl.Name = "InputTabControl";
            InputTabControl.SelectedIndex = 0;
            InputTabControl.Size = new Size(887, 300);
            InputTabControl.TabIndex = 0;
            // 
            // TextInputTab
            // 
            TextInputTab.Controls.Add(InputLabel);
            TextInputTab.Controls.Add(InputTextBox);
            TextInputTab.Location = new Point(4, 24);
            TextInputTab.Margin = new Padding(4, 3, 4, 3);
            TextInputTab.Name = "TextInputTab";
            TextInputTab.Padding = new Padding(4, 3, 4, 3);
            TextInputTab.Size = new Size(879, 272);
            TextInputTab.TabIndex = 0;
            TextInputTab.Text = "Текстовый ввод";
            TextInputTab.UseVisualStyleBackColor = true;
            // 
            // InputLabel
            // 
            InputLabel.AutoSize = true;
            InputLabel.Location = new Point(7, 7);
            InputLabel.Margin = new Padding(4, 0, 4, 0);
            InputLabel.Name = "InputLabel";
            InputLabel.Size = new Size(353, 15);
            InputLabel.TabIndex = 0;
            InputLabel.Text = "Введите систему линейных уравнений (например, 2x1 + x2 = 5)";
            // 
            // InputTextBox
            // 
            InputTextBox.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            InputTextBox.Location = new Point(7, 25);
            InputTextBox.Margin = new Padding(4, 3, 4, 3);
            InputTextBox.Multiline = true;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.ScrollBars = ScrollBars.Both;
            InputTextBox.Size = new Size(863, 230);
            InputTextBox.TabIndex = 1;
            // 
            // MatrixInputTab
            // 
            MatrixInputTab.Controls.Add(MatrixGrid);
            MatrixInputTab.Controls.Add(RowCountLabel);
            MatrixInputTab.Controls.Add(RowCountUpDown);
            MatrixInputTab.Controls.Add(ColumnCountLabel);
            MatrixInputTab.Controls.Add(ColumnCountUpDown);
            MatrixInputTab.Location = new Point(4, 24);
            MatrixInputTab.Margin = new Padding(4, 3, 4, 3);
            MatrixInputTab.Name = "MatrixInputTab";
            MatrixInputTab.Padding = new Padding(4, 3, 4, 3);
            MatrixInputTab.Size = new Size(879, 272);
            MatrixInputTab.TabIndex = 1;
            MatrixInputTab.Text = "Матричный ввод";
            MatrixInputTab.UseVisualStyleBackColor = true;
            // 
            // MatrixGrid
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            MatrixGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            MatrixGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            MatrixGrid.DefaultCellStyle = dataGridViewCellStyle2;
            MatrixGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            MatrixGrid.Location = new Point(7, 7);
            MatrixGrid.Margin = new Padding(4, 3, 4, 3);
            MatrixGrid.Name = "MatrixGrid";
            MatrixGrid.RowHeadersVisible = false;
            MatrixGrid.Size = new Size(863, 208);
            MatrixGrid.TabIndex = 0;
            // 
            // RowCountLabel
            // 
            RowCountLabel.AutoSize = true;
            RowCountLabel.Location = new Point(7, 222);
            RowCountLabel.Margin = new Padding(4, 0, 4, 0);
            RowCountLabel.Name = "RowCountLabel";
            RowCountLabel.Size = new Size(43, 15);
            RowCountLabel.TabIndex = 1;
            RowCountLabel.Text = "Строк:";
            // 
            // RowCountUpDown
            // 
            RowCountUpDown.Location = new Point(61, 219);
            RowCountUpDown.Margin = new Padding(4, 3, 4, 3);
            RowCountUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            RowCountUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            RowCountUpDown.Name = "RowCountUpDown";
            RowCountUpDown.Size = new Size(58, 23);
            RowCountUpDown.TabIndex = 2;
            RowCountUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            RowCountUpDown.ValueChanged += RowCountUpDown_ValueChanged;
            // 
            // ColumnCountLabel
            // 
            ColumnCountLabel.AutoSize = true;
            ColumnCountLabel.Location = new Point(126, 222);
            ColumnCountLabel.Margin = new Padding(4, 0, 4, 0);
            ColumnCountLabel.Name = "ColumnCountLabel";
            ColumnCountLabel.Size = new Size(64, 15);
            ColumnCountLabel.TabIndex = 3;
            ColumnCountLabel.Text = "Столбцов:";
            // 
            // ColumnCountUpDown
            // 
            ColumnCountUpDown.Location = new Point(198, 219);
            ColumnCountUpDown.Margin = new Padding(4, 3, 4, 3);
            ColumnCountUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            ColumnCountUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ColumnCountUpDown.Name = "ColumnCountUpDown";
            ColumnCountUpDown.Size = new Size(58, 23);
            ColumnCountUpDown.TabIndex = 4;
            ColumnCountUpDown.Value = new decimal(new int[] { 3, 0, 0, 0 });
            ColumnCountUpDown.ValueChanged += ColumnCountUpDown_ValueChanged;
            // 
            // OutputLabel
            // 
            OutputLabel.AutoSize = true;
            OutputLabel.Location = new Point(14, 321);
            OutputLabel.Margin = new Padding(4, 0, 4, 0);
            OutputLabel.Name = "OutputLabel";
            OutputLabel.Size = new Size(63, 15);
            OutputLabel.TabIndex = 2;
            OutputLabel.Text = "Результат:";
            // 
            // OutputListView
            // 
            OutputListView.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            OutputListView.FullRowSelect = true;
            OutputListView.GridLines = true;
            OutputListView.Location = new Point(14, 339);
            OutputListView.Margin = new Padding(4, 3, 4, 3);
            OutputListView.Name = "OutputListView";
            OutputListView.Size = new Size(886, 230);
            OutputListView.TabIndex = 3;
            OutputListView.UseCompatibleStateImageBehavior = false;
            OutputListView.View = View.Details;
            // 
            // SolveButton
            // 
            SolveButton.Location = new Point(14, 577);
            SolveButton.Margin = new Padding(4, 3, 4, 3);
            SolveButton.Name = "SolveButton";
            SolveButton.Size = new Size(117, 35);
            SolveButton.TabIndex = 4;
            SolveButton.Text = "Решить";
            SolveButton.UseVisualStyleBackColor = true;
            SolveButton.Click += SolveButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(138, 577);
            ClearButton.Margin = new Padding(4, 3, 4, 3);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(117, 35);
            ClearButton.TabIndex = 5;
            ClearButton.Text = "Очистить";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(261, 577);
            LoadButton.Margin = new Padding(4, 3, 4, 3);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(117, 35);
            LoadButton.TabIndex = 6;
            LoadButton.Text = "Загрузить";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(385, 577);
            SaveButton.Margin = new Padding(4, 3, 4, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(117, 35);
            SaveButton.TabIndex = 7;
            SaveButton.Text = "Сохранить";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // DocumentationButton
            // 
            DocumentationButton.Location = new Point(509, 577);
            DocumentationButton.Margin = new Padding(4, 3, 4, 3);
            DocumentationButton.Name = "DocumentationButton";
            DocumentationButton.Size = new Size(117, 35);
            DocumentationButton.TabIndex = 8;
            DocumentationButton.Text = "Документация";
            DocumentationButton.UseVisualStyleBackColor = true;
            DocumentationButton.Click += DocumentationButton_Click;
            // 
            // HelpButton
            // 
            HelpButton.Location = new Point(632, 577);
            HelpButton.Margin = new Padding(4, 3, 4, 3);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(117, 35);
            HelpButton.TabIndex = 9;
            HelpButton.Text = "Помощь";
            HelpButton.UseVisualStyleBackColor = true;
            HelpButton.Click += HelpButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 624);
            Controls.Add(InputTabControl);
            Controls.Add(OutputLabel);
            Controls.Add(OutputListView);
            Controls.Add(SolveButton);
            Controls.Add(ClearButton);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(DocumentationButton);
            Controls.Add(HelpButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimumSize = new Size(931, 663);
            Name = "MainForm";
            Text = "LinAlCalc - Калькулятор СЛАУ";
            KeyDown += MainForm_KeyDown;
            InputTabControl.ResumeLayout(false);
            TextInputTab.ResumeLayout(false);
            TextInputTab.PerformLayout();
            MatrixInputTab.ResumeLayout(false);
            MatrixInputTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MatrixGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)RowCountUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)ColumnCountUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl InputTabControl;
        private System.Windows.Forms.TabPage TextInputTab;
        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.TabPage MatrixInputTab;
        private System.Windows.Forms.DataGridView MatrixGrid;
        private System.Windows.Forms.Label RowCountLabel;
        private System.Windows.Forms.NumericUpDown RowCountUpDown;
        private System.Windows.Forms.Label ColumnCountLabel;
        private System.Windows.Forms.NumericUpDown ColumnCountUpDown;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.ListView OutputListView;
        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DocumentationButton;
        private new System.Windows.Forms.Button HelpButton;
    }
}