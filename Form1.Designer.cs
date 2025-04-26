using System;
using System.Windows.Forms;

namespace KinectMapping
{
    partial class MainWindow
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
            this.kinect_display = new System.Windows.Forms.PictureBox();
            this.yawSlider = new System.Windows.Forms.HScrollBar();
            this.pitchSlider = new System.Windows.Forms.HScrollBar();
            this.rollSlider = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pointCloudControl = new OpenTK.GLControl();
            ((System.ComponentModel.ISupportInitialize)(this.kinect_display)).BeginInit();
            this.SuspendLayout();
            // 
            // kinect_display
            // 
            this.kinect_display.Location = new System.Drawing.Point(65, 24);
            this.kinect_display.Margin = new System.Windows.Forms.Padding(2);
            this.kinect_display.Name = "kinect_display";
            this.kinect_display.Size = new System.Drawing.Size(720, 440);
            this.kinect_display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kinect_display.TabIndex = 0;
            this.kinect_display.TabStop = false;
            // 
            // yawSlider
            // 
            this.yawSlider.Location = new System.Drawing.Point(800, 504);
            this.yawSlider.Maximum = 360;
            this.yawSlider.Minimum = -360;
            this.yawSlider.Name = "yawSlider";
            this.yawSlider.Size = new System.Drawing.Size(412, 21);
            this.yawSlider.SmallChange = 5;
            this.yawSlider.TabIndex = 2;
            // 
            // pitchSlider
            // 
            this.pitchSlider.Location = new System.Drawing.Point(800, 570);
            this.pitchSlider.Maximum = 360;
            this.pitchSlider.Minimum = -360;
            this.pitchSlider.Name = "pitchSlider";
            this.pitchSlider.Size = new System.Drawing.Size(412, 21);
            this.pitchSlider.SmallChange = 5;
            this.pitchSlider.TabIndex = 3;
            // 
            // rollSlider
            // 
            this.rollSlider.Location = new System.Drawing.Point(800, 634);
            this.rollSlider.Maximum = 360;
            this.rollSlider.Minimum = -360;
            this.rollSlider.Name = "rollSlider";
            this.rollSlider.Size = new System.Drawing.Size(412, 21);
            this.rollSlider.SmallChange = 5;
            this.rollSlider.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(989, 483);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Yaw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(989, 550);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(994, 613);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Roll";
            // 
            // pointCloudControl
            // 
            this.pointCloudControl.BackColor = System.Drawing.Color.Black;
            this.pointCloudControl.Location = new System.Drawing.Point(836, 201);
            this.pointCloudControl.Name = "pointCloudControl";
            this.pointCloudControl.Size = new System.Drawing.Size(360, 220);
            this.pointCloudControl.TabIndex = 8;
            this.pointCloudControl.VSync = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 839);
            this.Controls.Add(this.pointCloudControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rollSlider);
            this.Controls.Add(this.pitchSlider);
            this.Controls.Add(this.yawSlider);
            this.Controls.Add(this.kinect_display);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Kinect Mapping";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kinect_display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox kinect_display;
        private HScrollBar yawSlider;
        private HScrollBar pitchSlider;
        private HScrollBar rollSlider;
        private Label label1;
        private Label label2;
        private Label label3;
        private OpenTK.GLControl pointCloudControl;
    }
}

