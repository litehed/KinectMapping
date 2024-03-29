﻿using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;
using OpenGL;

namespace KinectMapping
{
    public partial class MainWindow : Form
    {

        private KinectSensor sensor;
        private KinectUtil kinectUtil;

        private float prevYaw = 0, prevPitch = 0, prevRoll = 0;

        public MainWindow() {
            InitializeComponent();
            this.yawSlider.ValueChanged += YawSlider_ValueChanged;
            this.pitchSlider.ValueChanged += Pitchlider_ValueChanged;
            this.rollSlider.ValueChanged += RollSlider_ValueChanged;
        }

        private void Form_Load(object sender, EventArgs e) {
            kinectUtil = new KinectUtil(ref sensor, ref kinect_display);
            if(!kinectUtil.initKinect()) {
                Close();
                return;
            }
        }

        private void Form_Close(object sender, FormClosingEventArgs e) {
            kinectUtil.stopKinect();
        }

        private void RenderControl_ContextCreated(object sender, GlControlEventArgs e) {
            // Here you can allocate resources or initialize state
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(-1.0, 1.0f, -1.0, 1.0, -1.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
        }

        private void RenderControl_Render(object sender, GlControlEventArgs e) {
            Control senderControl = (Control)sender;

            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            //Front
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(1.0f, 0.0f, 0.0f); 
            Gl.Vertex3(-0.5f, -0.5f, 0.5f);
            Gl.Vertex3(0.5f, -0.5f, 0.5f);
            Gl.Vertex3(0.5f, 0.5f, 0.5f);
            Gl.Vertex3(-0.5f, 0.5f, 0.5f);
            Gl.End();
            //Back
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex3(-0.5f, -0.5f, -0.5f);
            Gl.Vertex3(-0.5f, 0.5f, -0.5f);
            Gl.Vertex3(0.5f, 0.5f, -0.5f);
            Gl.Vertex3(0.5f, -0.5f, -0.5f);
            Gl.End();
            //Top
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex3(-0.5f, 0.5f, 0.5f);
            Gl.Vertex3(0.5f, 0.5f, 0.5f);
            Gl.Vertex3(0.5f, 0.5f, -0.5f);
            Gl.Vertex3(-0.5f, 0.5f, -0.5f);
            Gl.End();
            //Bottom
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(1.0f, 0.0f, 1.0f);
            Gl.Vertex3(-0.5f, -0.5f, 0.5f);
            Gl.Vertex3(0.5f, -0.5f, 0.5f);
            Gl.Vertex3(0.5f, -0.5f, -0.5f);
            Gl.Vertex3(-0.5f, -0.5f, -0.5f);
            Gl.End();
            //Left
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(0.0f, 1.0f, 1.0f);
            Gl.Vertex3(-0.5f, -0.5f, 0.5f);
            Gl.Vertex3(-0.5f, 0.5f, 0.5f);
            Gl.Vertex3(-0.5f, 0.5f, -0.5f);
            Gl.Vertex3(-0.5f, -0.5f, -0.5f);
            Gl.End();
            //Right
            Gl.Begin(PrimitiveType.Quads);
            Gl.Color3(1.0f, 1.0f, 0.0f);
            Gl.Vertex3(0.5f, -0.5f, -0.5f);
            Gl.Vertex3(0.5f, 0.5f, -0.5f);
            Gl.Vertex3(0.5f, 0.5f, 0.5f);
            Gl.Vertex3(0.5f, -0.5f, 0.5f);
            Gl.End();

        }

        private void YawSlider_ValueChanged(object sender, EventArgs e)
        {
            Gl.Rotate(toRadians(this.yawSlider.Value), 0, 1, 0);
        }

        private void Pitchlider_ValueChanged(object sender, EventArgs e)
        {
            Gl.Rotate(toRadians(this.pitchSlider.Value), 1, 0, 0);
        }

        private void RollSlider_ValueChanged(object sender, EventArgs e)
        {
            Gl.Rotate(toRadians(this.rollSlider.Value), 0, 0, 1);
        }

        private float toRadians(int deg)
        {
            return (float)((Math.PI / 180) * deg);
        }

    }
}
