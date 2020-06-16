namespace Masinsko_Ucenje
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.RegressionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tbE = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnDBScan = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.tbK = new System.Windows.Forms.NumericUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnKmeans = new System.Windows.Forms.Button();
            this.btnLinearRegression = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tbErr = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ClusteringChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.RegressionChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbK)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClusteringChart)).BeginInit();
            this.SuspendLayout();
            // 
            // RegressionChart
            // 
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX2.IsStartedFromZero = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY2.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea1";
            this.RegressionChart.ChartAreas.Add(chartArea1);
            this.RegressionChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            legend1.TextWrapThreshold = 150;
            this.RegressionChart.Legends.Add(legend1);
            this.RegressionChart.Location = new System.Drawing.Point(0, 0);
            this.RegressionChart.Name = "RegressionChart";
            this.RegressionChart.Size = new System.Drawing.Size(623, 611);
            this.RegressionChart.TabIndex = 0;
            this.RegressionChart.Text = "chart1";
            // 
            // tbE
            // 
            this.tbE.Location = new System.Drawing.Point(51, 394);
            this.tbE.Name = "tbE";
            this.tbE.Size = new System.Drawing.Size(87, 20);
            this.tbE.TabIndex = 19;
            this.tbE.Text = "1";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(26, 397);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(23, 13);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Ɛ =";
            // 
            // btnDBScan
            // 
            this.btnDBScan.Location = new System.Drawing.Point(51, 421);
            this.btnDBScan.Name = "btnDBScan";
            this.btnDBScan.Size = new System.Drawing.Size(129, 30);
            this.btnDBScan.TabIndex = 17;
            this.btnDBScan.Text = "DBScan algoritam";
            this.btnDBScan.UseVisualStyleBackColor = true;
            this.btnDBScan.Click += new System.EventHandler(this.btnDBScan_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(26, 253);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(22, 13);
            this.Label3.TabIndex = 16;
            this.Label3.Text = "k =";
            // 
            // tbK
            // 
            this.tbK.Location = new System.Drawing.Point(60, 251);
            this.tbK.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.tbK.Name = "tbK";
            this.tbK.Size = new System.Drawing.Size(87, 20);
            this.tbK.TabIndex = 15;
            this.tbK.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(21, 209);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(104, 20);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "Klasterovanje";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(21, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(145, 20);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "Regresiona analiza";
            // 
            // btnKmeans
            // 
            this.btnKmeans.Location = new System.Drawing.Point(51, 313);
            this.btnKmeans.Name = "btnKmeans";
            this.btnKmeans.Size = new System.Drawing.Size(129, 30);
            this.btnKmeans.TabIndex = 12;
            this.btnKmeans.Text = "KMeans algoritam";
            this.btnKmeans.UseVisualStyleBackColor = true;
            this.btnKmeans.Click += new System.EventHandler(this.btnKmeans_Click);
            // 
            // btnLinearRegression
            // 
            this.btnLinearRegression.Location = new System.Drawing.Point(51, 55);
            this.btnLinearRegression.Name = "btnLinearRegression";
            this.btnLinearRegression.Size = new System.Drawing.Size(129, 30);
            this.btnLinearRegression.TabIndex = 20;
            this.btnLinearRegression.Text = "Linearna regresija";
            this.btnLinearRegression.UseVisualStyleBackColor = true;
            this.btnLinearRegression.Click += new System.EventHandler(this.btnLinearRegression_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbErr);
            this.panel1.Controls.Add(this.btnLinearRegression);
            this.panel1.Controls.Add(this.btnKmeans);
            this.panel1.Controls.Add(this.tbE);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Label4);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.btnDBScan);
            this.panel1.Controls.Add(this.tbK);
            this.panel1.Controls.Add(this.Label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(623, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 611);
            this.panel1.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "err =";
            // 
            // tbErr
            // 
            this.tbErr.Location = new System.Drawing.Point(60, 277);
            this.tbErr.Name = "tbErr";
            this.tbErr.Size = new System.Drawing.Size(87, 20);
            this.tbErr.TabIndex = 21;
            this.tbErr.Text = "0.01";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ClusteringChart);
            this.panel2.Controls.Add(this.RegressionChart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(623, 611);
            this.panel2.TabIndex = 22;
            // 
            // ClusteringChart
            // 
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisX2.IsStartedFromZero = false;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY2.IsStartedFromZero = false;
            chartArea2.Name = "ChartArea1";
            this.ClusteringChart.ChartAreas.Add(chartArea2);
            this.ClusteringChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Name = "Legend1";
            legend2.TextWrapThreshold = 150;
            this.ClusteringChart.Legends.Add(legend2);
            this.ClusteringChart.Location = new System.Drawing.Point(0, 0);
            this.ClusteringChart.Name = "ClusteringChart";
            this.ClusteringChart.Size = new System.Drawing.Size(623, 611);
            this.ClusteringChart.TabIndex = 1;
            this.ClusteringChart.Text = "chart1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.RegressionChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbK)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClusteringChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart RegressionChart;
        internal System.Windows.Forms.TextBox tbE;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button btnDBScan;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.NumericUpDown tbK;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnKmeans;
        internal System.Windows.Forms.Button btnLinearRegression;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ClusteringChart;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox tbErr;
    }
}

