using System;
using System.Windows.Forms;
using Microsoft.Kinect;
using OpenTK.Graphics.OpenGL;

namespace KinectMapping
{
    public partial class MainWindow : Form
    {

        private KinectSensor sensor;
        private KinectUtil kinectUtil;

        private float prevYaw = 0, prevPitch = 0, prevRoll = 0;

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
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            kinectUtil.stopKinect();
        }

        private void pointCloudControl_Load(object sender, EventArgs e)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Viewport(0, 0, pointCloudControl.Width, pointCloudControl.Height);
            GL.Enable(EnableCap.DepthTest);
        }

        private void pointCloudControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();

            // Apply rotation from sliders
            GL.Rotate(yawSlider.Value, 0.0f, 1.0f, 0.0f);   // Yaw around Y axis
            GL.Rotate(pitchSlider.Value, 1.0f, 0.0f, 0.0f); // Pitch around X axis
            GL.Rotate(rollSlider.Value, 0.0f, 0.0f, 1.0f);  // Roll around Z axis

            drawCube();

            pointCloudControl.SwapBuffers();
        }

        private void YawSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Yaw: " + this.yawSlider.Value);
            pointCloudControl.Invalidate();
        }

        private void PitchSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Pitch: " + this.pitchSlider.Value);
            pointCloudControl.Invalidate();
        }

        private void RollSlider_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Roll: " + this.rollSlider.Value);
            pointCloudControl.Invalidate();
        }

        private float toRadians(int deg)
        {
            return (float)((Math.PI / 180) * deg);
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
