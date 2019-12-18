namespace movable_2dmap
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.foodButton = new System.Windows.Forms.Button();
            this.GraphicsToggle = new System.Windows.Forms.Button();
            this.GameSpeedSlider = new System.Windows.Forms.TrackBar();
            this.TrackingToggle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GameSpeedSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.FileName = "demo.txt";
            this.saveFileDialog1.Filter = "\"Text files (*.txt)|*.txt\"";
            this.saveFileDialog1.InitialDirectory = "C:\\Users\\User\\source\\repos";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.Title = "Save file to...";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog1_FileOk);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // foodButton
            // 
            this.foodButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodButton.Location = new System.Drawing.Point(422, 87);
            this.foodButton.Name = "foodButton";
            this.foodButton.Size = new System.Drawing.Size(107, 31);
            this.foodButton.TabIndex = 1;
            this.foodButton.TabStop = false;
            this.foodButton.Text = "Spawn food";
            this.foodButton.UseVisualStyleBackColor = true;
            this.foodButton.Click += new System.EventHandler(this.FoodButton_Click);
            // 
            // GraphicsToggle
            // 
            this.GraphicsToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GraphicsToggle.Location = new System.Drawing.Point(422, 124);
            this.GraphicsToggle.Name = "GraphicsToggle";
            this.GraphicsToggle.Size = new System.Drawing.Size(107, 31);
            this.GraphicsToggle.TabIndex = 2;
            this.GraphicsToggle.TabStop = false;
            this.GraphicsToggle.Text = "Graphics";
            this.GraphicsToggle.UseVisualStyleBackColor = true;
            this.GraphicsToggle.Click += new System.EventHandler(this.GraphicsToggle_Click);
            // 
            // GameSpeedSlider
            // 
            this.GameSpeedSlider.Location = new System.Drawing.Point(422, 197);
            this.GameSpeedSlider.Margin = new System.Windows.Forms.Padding(2);
            this.GameSpeedSlider.Name = "GameSpeedSlider";
            this.GameSpeedSlider.Size = new System.Drawing.Size(107, 45);
            this.GameSpeedSlider.TabIndex = 4;
            this.GameSpeedSlider.Scroll += new System.EventHandler(this.GameSpeedSlider_Scroll);
            // 
            // TrackingToggle
            // 
            this.TrackingToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackingToggle.Location = new System.Drawing.Point(422, 162);
            this.TrackingToggle.Name = "TrackingToggle";
            this.TrackingToggle.Size = new System.Drawing.Size(107, 31);
            this.TrackingToggle.TabIndex = 5;
            this.TrackingToggle.TabStop = false;
            this.TrackingToggle.Text = "Track";
            this.TrackingToggle.UseVisualStyleBackColor = true;
            this.TrackingToggle.Click += new System.EventHandler(this.TrackingToggle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 461);
            this.Controls.Add(this.TrackingToggle);
            this.Controls.Add(this.GameSpeedSlider);
            this.Controls.Add(this.GraphicsToggle);
            this.Controls.Add(this.foodButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.GameSpeedSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button foodButton;
        private System.Windows.Forms.Button GraphicsToggle;
        private System.Windows.Forms.TrackBar GameSpeedSlider;
        private System.Windows.Forms.Button TrackingToggle;
        public System.Windows.Forms.Timer timer1;
    }
}