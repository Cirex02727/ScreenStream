namespace StreamServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.devicesL = new System.Windows.Forms.Label();
            this.devicesC = new System.Windows.Forms.ComboBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.time_looper = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // devicesL
            // 
            this.devicesL.AutoSize = true;
            this.devicesL.Location = new System.Drawing.Point(12, 9);
            this.devicesL.Name = "devicesL";
            this.devicesL.Size = new System.Drawing.Size(50, 15);
            this.devicesL.TabIndex = 0;
            this.devicesL.Text = "Devices:";
            // 
            // devicesC
            // 
            this.devicesC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicesC.FormattingEnabled = true;
            this.devicesC.Location = new System.Drawing.Point(68, 6);
            this.devicesC.Name = "devicesC";
            this.devicesC.Size = new System.Drawing.Size(352, 23);
            this.devicesC.TabIndex = 1;
            this.devicesC.SelectedValueChanged += new System.EventHandler(this.devicesC_SelectedValueChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(0, 35);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(432, 203);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // time_looper
            // 
            this.time_looper.Enabled = true;
            this.time_looper.Interval = 33;
            this.time_looper.Tick += new System.EventHandler(this.Time_looper_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 238);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.devicesC);
            this.Controls.Add(this.devicesL);
            this.MinimumSize = new System.Drawing.Size(448, 277);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label devicesL;
        private ComboBox devicesC;
        private PictureBox pictureBox;
        private System.Windows.Forms.Timer time_looper;
    }
}