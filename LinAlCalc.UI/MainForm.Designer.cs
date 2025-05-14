namespace LinAlCalc.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InputTabControl = new System.Windows.Forms.TabControl();
            this.TextInputTab = new System.Windows.Forms.TabPage();
            this.InputLabel = new System.Windows.Forms.Label();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.MatrixInputTab = new System.Windows.Forms.TabPage();
            this.MatrixGrid = new System.Windows.Forms.DataGridView();
            this.RowCountLabel = new System.Windows.Forms.Label();
            this.RowCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.ColumnCountLabel = new System.Windows.Forms.Label();
            this.ColumnCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.SolveButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DocumentationButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MatrixGrid)).BeginInit();
            this.TextInputTab.SuspendLayout();
            this.MatrixInputTab.SuspendLayout();
            this.InputTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputTabControl
            // 
            this.InputTabControl.Controls.Add(this.TextInputTab);
            this.InputTabControl.Controls.Add(this.MatrixInputTab);
            this.InputTabControl.Location = new System.Drawing.Point(12, 12);
            this.InputTabControl.Name = "InputTabControl";
            this.InputTabControl.SelectedIndex = 0;
            this.InputTabControl.Size = new System.Drawing.Size(760, 260);
            this.InputTabControl.TabIndex = 0;
            // 
            // TextInputTab
            // 
            this.TextInputTab.Controls.Add(this.InputLabel);
            this.TextInputTab.Controls.Add(this.InputTextBox);
            this.TextInputTab.Location = new System.Drawing.Point(4, 22);
            this.TextInputTab.Name = "TextInputTab";
            this.TextInputTab.Padding = new System.Windows.Forms.Padding(3);
            this.TextInputTab.Size = new System.Drawing.Size(752, 234);
            this.TextInputTab.TabIndex = 0;
            this.TextInputTab.Text = "Текстовый ввод";
            this.TextInputTab.UseVisualStyleBackColor = true;
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.Location = new System.Drawing.Point(6, 6);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(300, 13);
            this.InputLabel.TabIndex = 0;
            this.InputLabel.Text = "Введите систему линейных уравнений (например, 2x1 + x2 = 5)";
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(6, 22);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputTextBox.Size = new System.Drawing.Size(740, 200);
            this.InputTextBox.TabIndex = 1;
            this.InputTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // MatrixInputTab
            // 
            this.MatrixInputTab.Controls.Add(this.MatrixGrid);
            this.MatrixInputTab.Controls.Add(this.RowCountLabel);
            this.MatrixInputTab.Controls.Add(this.RowCountUpDown);
            this.MatrixInputTab.Controls.Add(this.ColumnCountLabel);
            this.MatrixInputTab.Controls.Add(this.ColumnCountUpDown);
            this.MatrixInputTab.Location = new System.Drawing.Point(4, 22);
            this.MatrixInputTab.Name = "MatrixInputTab";
            this.MatrixInputTab.Padding = new System.Windows.Forms.Padding(3);
            this.MatrixInputTab.Size = new System.Drawing.Size(752, 234);
            this.MatrixInputTab.TabIndex = 1;
            this.MatrixInputTab.Text = "Матричный ввод";
            this.MatrixInputTab.UseVisualStyleBackColor = true;
            // 
            // MatrixGrid
            // 
            this.MatrixGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MatrixGrid.Location = new System.Drawing.Point(6, 6);
            this.MatrixGrid.Name = "MatrixGrid";
            this.MatrixGrid.RowHeadersVisible = false;
            this.MatrixGrid.Size = new System.Drawing.Size(740, 180);
            this.MatrixGrid.TabIndex = 0;
            this.MatrixGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MatrixGrid.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.MatrixGrid.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.MatrixGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.MatrixGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MatrixGrid.EnableHeadersVisualStyles = true;
            this.MatrixGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            // 
            // RowCountLabel
            // 
            this.RowCountLabel.AutoSize = true;
            this.RowCountLabel.Location = new System.Drawing.Point(6, 192);
            this.RowCountLabel.Name = "RowCountLabel";
            this.RowCountLabel.Size = new System.Drawing.Size(40, 13);
            this.RowCountLabel.TabIndex = 1;
            this.RowCountLabel.Text = "Строк:";
            // 
            // RowCountUpDown
            // 
            this.RowCountUpDown.Location = new System.Drawing.Point(52, 190);
            this.RowCountUpDown.Minimum = 1;
            this.RowCountUpDown.Maximum = 10;
            this.RowCountUpDown.Value = 3;
            this.RowCountUpDown.Name = "RowCountUpDown";
            this.RowCountUpDown.Size = new System.Drawing.Size(50, 20);
            this.RowCountUpDown.TabIndex = 2;
            this.RowCountUpDown.ValueChanged += new System.EventHandler(this.RowCountUpDown_ValueChanged);
            // 
            // ColumnCountLabel
            // 
            this.ColumnCountLabel.AutoSize = true;
            this.ColumnCountLabel.Location = new System.Drawing.Point(108, 192);
            this.ColumnCountLabel.Name = "ColumnCountLabel";
            this.ColumnCountLabel.Size = new System.Drawing.Size(56, 13);
            this.ColumnCountLabel.TabIndex = 3;
            this.ColumnCountLabel.Text = "Столбцов:";
            // 
            // ColumnCountUpDown
            // 
            this.ColumnCountUpDown.Location = new System.Drawing.Point(170, 190);
            this.ColumnCountUpDown.Minimum = 1;
            this.ColumnCountUpDown.Maximum = 10;
            this.ColumnCountUpDown.Value = 3;
            this.ColumnCountUpDown.Name = "ColumnCountUpDown";
            this.ColumnCountUpDown.Size = new System.Drawing.Size(50, 20);
            this.ColumnCountUpDown.TabIndex = 4;
            this.ColumnCountUpDown.ValueChanged += new System.EventHandler(this.ColumnCountUpDown_ValueChanged);
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(12, 278);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(60, 13);
            this.OutputLabel.TabIndex = 2;
            this.OutputLabel.Text = "Результат:";
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Location = new System.Drawing.Point(12, 294);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputTextBox.Size = new System.Drawing.Size(760, 200);
            this.OutputTextBox.TabIndex = 3;
            this.OutputTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputTextBox.WordWrap = false;
            this.OutputTextBox.AcceptsReturn = true;
            // 
            // SolveButton
            // 
            this.SolveButton.Location = new System.Drawing.Point(12, 500);
            this.SolveButton.Name = "SolveButton";
            this.SolveButton.Size = new System.Drawing.Size(100, 30);
            this.SolveButton.TabIndex = 4;
            this.SolveButton.Text = "Решить";
            this.SolveButton.UseVisualStyleBackColor = true;
            this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(118, 500);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(100, 30);
            this.ClearButton.TabIndex = 5;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(224, 500);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(100, 30);
            this.LoadButton.TabIndex = 6;
            this.LoadButton.Text = "Загрузить";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(330, 500);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 30);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DocumentationButton
            // 
            this.DocumentationButton.Location = new System.Drawing.Point(436, 500);
            this.DocumentationButton.Name = "DocumentationButton";
            this.DocumentationButton.Size = new System.Drawing.Size(100, 30);
            this.DocumentationButton.TabIndex = 8;
            this.DocumentationButton.Text = "Документация";
            this.DocumentationButton.UseVisualStyleBackColor = true;
            this.DocumentationButton.Click += new System.EventHandler(this.DocumentationButton_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(542, 500);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(100, 30);
            this.HelpButton.TabIndex = 9;
            this.HelpButton.Text = "Помощь";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 540);
            this.Controls.Add(this.InputTabControl);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.SolveButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DocumentationButton);
            this.Controls.Add(this.HelpButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 580);
            this.Name = "MainForm";
            this.Text = "LinAlCalc - Калькулятор СЛАУ";
            ((System.ComponentModel.ISupportInitialize)(this.MatrixGrid)).EndInit();
            this.TextInputTab.ResumeLayout(false);
            this.TextInputTab.PerformLayout();
            this.MatrixInputTab.ResumeLayout(false);
            this.MatrixInputTab.PerformLayout();
            this.InputTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
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
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button SolveButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DocumentationButton;
        private System.Windows.Forms.Button HelpButton;
    }
}