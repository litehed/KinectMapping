using System;
using System.Windows.Forms;
using Microsoft.Kinect;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace KinectMapping
{
    public partial class MainWindow : Form
    {

        private KinectSensor sensor;
        private KinectUtil kinectUtil;
        private KinectPointCloudRenderer pointCloudRenderer;

        private float prevYaw = 0, prevPitch = 0, prevRoll = 0;
        private float zoomLevel = 1.0F;

        public MainWindow()
        {
            InitializeComponent();
            this.yawSlider.ValueChanged += YawSlider_ValueChanged;
            this.pitchSlider.ValueChanged += PitchSlider_ValueChanged;
            this.rollSlider.ValueChanged += RollSlider_ValueChanged;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            sensor = KinectSensor.GetDefault();
            kinectUtil = new KinectUtil(sensor, kinect_display);
            if (!kinectUtil.initKinect())
            {
                Close();
                return;
            }

            pointCloudRenderer = new KinectPointCloudRenderer(kinectUtil, pointCloudControl);

            Timer renderTimer = new Timer();
            renderTimer.Interval = 34; // ~30 frames per second
            renderTimer.Tick += (s, args) => pointCloudControl.Invalidate();
            renderTimer.Start();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (kinectUtil != null)
            {
                kinectUtil.stopKinect();
                sensor = null;
            }

            if (pointCloudRenderer != null)
            {
                pointCloudRenderer.Dispose();
                pointCloudRenderer = null;
            }
        }

        private void YawSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Yaw: " + this.yawSlider.Value);
            pointCloudRenderer.SetYawPitchRoll(this.yawSlider.Value, this.pitchSlider.Value, this.rollSlider.Value);
            pointCloudControl.Invalidate();
        }

        private void PitchSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Pitch: " + this.pitchSlider.Value);
            pointCloudRenderer.SetYawPitchRoll(this.yawSlider.Value, this.pitchSlider.Value, this.rollSlider.Value);

            pointCloudControl.Invalidate();
        }

        private void pointCloudControl_Load(object sender, EventArgs e)
        {

        }

        private void RollSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Roll: " + this.rollSlider.Value);
            pointCloudRenderer.SetYawPitchRoll(this.yawSlider.Value, this.pitchSlider.Value, this.rollSlider.Value);

            pointCloudControl.Invalidate();
        }


        private void drawCube()
        {
            // Cube
            // Front
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);

            // Back
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);

            // Top
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);

            // Bottom
            GL.Color3(1.0f, 0.0f, 1.0f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);

            // Left
            GL.Color3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(-0.5f, -0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, 0.5f);
            GL.Vertex3(-0.5f, 0.5f, -0.5f);
            GL.Vertex3(-0.5f, -0.5f, -0.5f);

            // Right
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(0.5f, -0.5f, -0.5f);
            GL.Vertex3(0.5f, 0.5f, -0.5f);
            GL.Vertex3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(0.5f, -0.5f, 0.5f);
            GL.End();
        }

    }
}
