namespace ChartApp
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.sysChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.diskButton = new System.Windows.Forms.Button();
            this.memoryButton = new System.Windows.Forms.Button();
            this.cpuButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sysChart)).BeginInit();
            this.SuspendLayout();
            // 
            // sysChart
            // 
            chartArea3.Name = "ChartArea1";
            this.sysChart.ChartAreas.Add(chartArea3);
            this.sysChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.sysChart.Legends.Add(legend3);
            this.sysChart.Location = new System.Drawing.Point(0, 0);
            this.sysChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sysChart.Name = "sysChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.sysChart.Series.Add(series3);
            this.sysChart.Size = new System.Drawing.Size(1026, 686);
            this.sysChart.TabIndex = 0;
            this.sysChart.Text = "sysChart";
            // 
            // diskButton
            // 
            this.diskButton.Location = new System.Drawing.Point(861, 585);
            this.diskButton.Name = "diskButton";
            this.diskButton.Size = new System.Drawing.Size(140, 52);
            this.diskButton.TabIndex = 1;
            this.diskButton.Text = "Disk (Off)";
            this.diskButton.UseVisualStyleBackColor = true;
            this.diskButton.Click += new System.EventHandler(this.diskButton_Click);
            // 
            // memoryButton
            // 
            this.memoryButton.Location = new System.Drawing.Point(861, 525);
            this.memoryButton.Name = "memoryButton";
            this.memoryButton.Size = new System.Drawing.Size(140, 52);
            this.memoryButton.TabIndex = 2;
            this.memoryButton.Text = "Memory (Off)";
            this.memoryButton.UseVisualStyleBackColor = true;
            this.memoryButton.Click += new System.EventHandler(this.memoryButton_Click);
            // 
            // cpuButton
            // 
            this.cpuButton.Location = new System.Drawing.Point(861, 467);
            this.cpuButton.Name = "cpuButton";
            this.cpuButton.Size = new System.Drawing.Size(140, 52);
            this.cpuButton.TabIndex = 3;
            this.cpuButton.Text = "CPU (On)";
            this.cpuButton.UseVisualStyleBackColor = true;
            this.cpuButton.Click += new System.EventHandler(this.cpuButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(861, 347);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(140, 52);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause ||";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 686);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.cpuButton);
            this.Controls.Add(this.memoryButton);
            this.Controls.Add(this.diskButton);
            this.Controls.Add(this.sysChart);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.Text = "System Metrics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sysChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart sysChart;
        private System.Windows.Forms.Button diskButton;
        private System.Windows.Forms.Button memoryButton;
        private System.Windows.Forms.Button cpuButton;
        private System.Windows.Forms.Button pauseButton;
    }
}

