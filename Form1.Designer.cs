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
            this.glControl1 = new OpenGL.GlControl();
            this.yawSlider = new System.Windows.Forms.HScrollBar();
            this.pitchSlider = new System.Windows.Forms.HScrollBar();
            this.rollSlider = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kinect_display)).BeginInit();
            this.SuspendLayout();
            // 
            // kinect_display
            // 
            this.kinect_display.Location = new System.Drawing.Point(87, 30);
            this.kinect_display.Name = "kinect_display";
            this.kinect_display.Size = new System.Drawing.Size(960, 540);
            this.kinect_display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kinect_display.TabIndex = 0;
            this.kinect_display.TabStop = false;
            // 
            // glControl1
            // 
            this.glControl1.Animation = true;
            this.glControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.glControl1.ColorBits = ((uint)(24u));
            this.glControl1.DepthBits = ((uint)(0u));
            this.glControl1.Location = new System.Drawing.Point(1066, 30);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.glControl1.MultisampleBits = ((uint)(0u));
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(550, 550);
            this.glControl1.StencilBits = ((uint)(0u));
            this.glControl1.TabIndex = 1;
            this.glControl1.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_ContextCreated);
            this.glControl1.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.RenderControl_Render);
            // 
            // yawSlider
            // 
            this.yawSlider.Location = new System.Drawing.Point(1066, 620);
            this.yawSlider.Maximum = 360;
            this.yawSlider.Minimum = -360;
            this.yawSlider.Name = "yawSlider";
            this.yawSlider.Size = new System.Drawing.Size(550, 21);
            this.yawSlider.SmallChange = 5;
            this.yawSlider.TabIndex = 2;
            // 
            // pitchSlider
            // 
            this.pitchSlider.Location = new System.Drawing.Point(1066, 702);
            this.pitchSlider.Maximum = 360;
            this.pitchSlider.Minimum = -360;
            this.pitchSlider.Name = "pitchSlider";
            this.pitchSlider.Size = new System.Drawing.Size(550, 21);
            this.pitchSlider.SmallChange = 5;
            this.pitchSlider.TabIndex = 3;
            // 
            // rollSlider
            // 
            this.rollSlider.Location = new System.Drawing.Point(1066, 780);
            this.rollSlider.Maximum = 360;
            this.rollSlider.Minimum = -360;
            this.rollSlider.Name = "rollSlider";
            this.rollSlider.Size = new System.Drawing.Size(550, 21);
            this.rollSlider.SmallChange = 5;
            this.rollSlider.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1319, 595);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Yaw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1319, 677);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pitch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1325, 755);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Roll";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rollSlider);
            this.Controls.Add(this.pitchSlider);
            this.Controls.Add(this.yawSlider);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.kinect_display);
            this.Name = "MainWindow";
            this.Text = "Kinect Mapping";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kinect_display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox kinect_display;
        private OpenGL.GlControl glControl1;
        private HScrollBar yawSlider;
        private HScrollBar pitchSlider;
        private HScrollBar rollSlider;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}

