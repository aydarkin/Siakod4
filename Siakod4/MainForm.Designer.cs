namespace Siakod4
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.graphPanel = new System.Windows.Forms.Panel();
            this.runObhod = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.joinBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.deselectBtn = new System.Windows.Forms.Button();
            this.statusObhod = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.TextBox();
            this.eCyclBtn = new System.Windows.Forms.Button();
            this.shortestPathBtn = new System.Windows.Forms.Button();
            this.weightNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightNum)).BeginInit();
            this.SuspendLayout();
            // 
            // graphPanel
            // 
            this.graphPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Location = new System.Drawing.Point(14, 15);
            this.graphPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(490, 543);
            this.graphPanel.TabIndex = 0;
            this.graphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPanel_Paint);
            this.graphPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseClick);
            // 
            // runObhod
            // 
            this.runObhod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.runObhod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runObhod.Location = new System.Drawing.Point(510, 598);
            this.runObhod.Name = "runObhod";
            this.runObhod.Size = new System.Drawing.Size(290, 27);
            this.runObhod.TabIndex = 6;
            this.runObhod.Text = "Обход в глубину";
            this.runObhod.UseVisualStyleBackColor = true;
            this.runObhod.Click += new System.EventHandler(this.runObhod_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid.BackgroundColor = System.Drawing.Color.White;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.Location = new System.Drawing.Point(510, 15);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGrid.Size = new System.Drawing.Size(290, 212);
            this.dataGrid.TabIndex = 3;
            this.dataGrid.TabStop = false;
            // 
            // joinBtn
            // 
            this.joinBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.joinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.joinBtn.Location = new System.Drawing.Point(510, 279);
            this.joinBtn.Name = "joinBtn";
            this.joinBtn.Size = new System.Drawing.Size(213, 29);
            this.joinBtn.TabIndex = 0;
            this.joinBtn.Text = "Соединить";
            this.joinBtn.UseVisualStyleBackColor = true;
            this.joinBtn.Click += new System.EventHandler(this.joinBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteBtn.Location = new System.Drawing.Point(510, 314);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(290, 29);
            this.deleteBtn.TabIndex = 2;
            this.deleteBtn.Text = "Удалить";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // deselectBtn
            // 
            this.deselectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deselectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deselectBtn.Location = new System.Drawing.Point(510, 416);
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.Size = new System.Drawing.Size(290, 29);
            this.deselectBtn.TabIndex = 3;
            this.deselectBtn.Text = "Убрать выделение всем";
            this.deselectBtn.UseVisualStyleBackColor = true;
            this.deselectBtn.Click += new System.EventHandler(this.deselectBtn_Click);
            // 
            // statusObhod
            // 
            this.statusObhod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusObhod.BackColor = System.Drawing.Color.White;
            this.statusObhod.Location = new System.Drawing.Point(14, 598);
            this.statusObhod.Multiline = true;
            this.statusObhod.Name = "statusObhod";
            this.statusObhod.ReadOnly = true;
            this.statusObhod.Size = new System.Drawing.Size(490, 27);
            this.statusObhod.TabIndex = 8;
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.status.BackColor = System.Drawing.Color.White;
            this.status.Location = new System.Drawing.Point(14, 565);
            this.status.Multiline = true;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Size = new System.Drawing.Size(490, 27);
            this.status.TabIndex = 7;
            // 
            // eCyclBtn
            // 
            this.eCyclBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.eCyclBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eCyclBtn.Location = new System.Drawing.Point(510, 565);
            this.eCyclBtn.Name = "eCyclBtn";
            this.eCyclBtn.Size = new System.Drawing.Size(290, 27);
            this.eCyclBtn.TabIndex = 5;
            this.eCyclBtn.Text = "Поиск эйлерова цикла";
            this.eCyclBtn.UseVisualStyleBackColor = true;
            this.eCyclBtn.Click += new System.EventHandler(this.eCyclBtn_Click);
            // 
            // shortestPathBtn
            // 
            this.shortestPathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shortestPathBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shortestPathBtn.Location = new System.Drawing.Point(510, 531);
            this.shortestPathBtn.Name = "shortestPathBtn";
            this.shortestPathBtn.Size = new System.Drawing.Size(290, 27);
            this.shortestPathBtn.TabIndex = 4;
            this.shortestPathBtn.Text = "Кратчайший путь";
            this.shortestPathBtn.UseVisualStyleBackColor = true;
            this.shortestPathBtn.Click += new System.EventHandler(this.shortestPathBtn_Click);
            // 
            // weightNum
            // 
            this.weightNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weightNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weightNum.Location = new System.Drawing.Point(729, 283);
            this.weightNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.weightNum.Name = "weightNum";
            this.weightNum.Size = new System.Drawing.Size(71, 22);
            this.weightNum.TabIndex = 1;
            this.weightNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(812, 642);
            this.Controls.Add(this.weightNum);
            this.Controls.Add(this.status);
            this.Controls.Add(this.statusObhod);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.deselectBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.joinBtn);
            this.Controls.Add(this.shortestPathBtn);
            this.Controls.Add(this.eCyclBtn);
            this.Controls.Add(this.runObhod);
            this.Controls.Add(this.graphPanel);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Граф";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weightNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.Button runObhod;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button joinBtn;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button deselectBtn;
        private System.Windows.Forms.TextBox statusObhod;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.Button eCyclBtn;
        private System.Windows.Forms.Button shortestPathBtn;
        private System.Windows.Forms.NumericUpDown weightNum;
    }
}

