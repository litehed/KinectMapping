using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace KinectMapping
{
    internal class KinectPointCloudRenderer
    {
        private KinectUtil kinectUtil;
        private GLControl glControl;

        private int vboVertexId;
        private int vboColorId;
        private bool vboInitialized = false;

        private float cameraAngle = 0.0f;
        private float cameraDistance = 3.0f;
        private float cameraPitch = 15.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;
        private float roll = 0.0f;

        public KinectPointCloudRenderer(KinectUtil kinectUtil, GLControl glControl)
        {
            this.kinectUtil = kinectUtil;
            this.glControl = glControl;

            SetupOpenGL();

            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;
        }

        private void SetupOpenGL()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.ClearDepth(1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.PointSmooth);
            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);

            GlControl_Resize(glControl, EventArgs.Empty);
            InitializeVBOs();
        }

        private void InitializeVBOs()
        {
            if (vboInitialized)
            {
                GL.DeleteBuffer(vboVertexId);
                GL.DeleteBuffer(vboColorId);
            }

            int width = 512;
            int height = 424;

            // Generate vertex buffer
            vboVertexId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertexId);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(width * height * 3 * sizeof(float)),
                          IntPtr.Zero, BufferUsageHint.DynamicDraw);

            // Generate color buffer
            vboColorId = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboColorId);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(width * height * 3 * sizeof(float)),
                          IntPtr.Zero, BufferUsageHint.DynamicDraw);

            vboInitialized = true;
        }

        private void UpdateVBOs()
        {
            if (!vboInitialized)
                InitializeVBOs();

            var points = kinectUtil.GetPointCloud();
            if (points.Count == 0)
                return;

            float[] vertices = new float[points.Count * 3];
            float[] colors = new float[points.Count * 3];

            for (int i = 0; i < points.Count; i += 3)
            {
                var point = points[i];

                vertices[i * 3] = point.X;
                vertices[i * 3 + 1] = point.Y;
                vertices[i * 3 + 2] = -point.Z;
                float normalizedDepth = Math.Max(0.0f, Math.Min(1.0f, (point.Z - 0.5f) / 4.0f));
                colors[i * 3] = 1 - normalizedDepth;
                colors[i * 3 + 1] = 0.0f;
                colors[i * 3 + 2] = normalizedDepth;
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertexId);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero,
                            (IntPtr)(vertices.Length * sizeof(float)), vertices);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboColorId);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero,
                            (IntPtr)(colors.Length * sizeof(float)), colors);
        }

        private void RotateCamera()
        {
            GL.LoadIdentity();

            float camX = cameraDistance * (float)Math.Sin(MathHelper.DegreesToRadians(cameraAngle));
            float camZ = cameraDistance * (float)Math.Cos(MathHelper.DegreesToRadians(cameraAngle));

            Matrix4 lookAt = Matrix4.LookAt(
                new Vector3(camX, 0, camZ),    // Camera position
                new Vector3(0, 0, 0),          // Target (looking at origin)
                new Vector3(0, 1, 0)           // Up vector
            );
            GL.LoadMatrix(ref lookAt);

            GL.Rotate(yaw, 0.0f, 1.0f, 0.0f);    // Yaw around Y
            GL.Rotate(pitch, 1.0f, 0.0f, 0.0f);  // Pitch around X
            GL.Rotate(roll, 0.0f, 0.0f, 1.0f);   // Roll around Z
        }

        private void DrawPointCloud()
        {
            UpdateVBOs();

            int pointCount = kinectUtil.GetPointCloud().Count;
            if (pointCount == 0)
                return;

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertexId);
            GL.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboColorId);
            GL.ColorPointer(3, ColorPointerType.Float, 0, IntPtr.Zero);

            GL.PointSize(1.0f);
            GL.DrawArrays(PrimitiveType.Points, 0, pointCount);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (!glControl.Context.IsCurrent)
                glControl.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            RotateCamera();
            DrawPointCloud();

            glControl.SwapBuffers();
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            float aspectRatio = (float)glControl.Width / glControl.Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45),
                aspectRatio,
                0.1f,
                20.0f
            );

            Matrix4 zFlip = Matrix4.CreateScale(1, 1, -1);
            perspective *= zFlip;

            GL.LoadMatrix(ref perspective);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public void SetYawPitchRoll(float yaw, float pitch, float roll)
        {
            this.yaw = yaw;
            this.pitch = pitch;
            this.roll = roll;
            glControl.Invalidate();
        }

        public void SetCameraDistance(float distance)
        {
            this.cameraDistance = Math.Max(0.5f, Math.Min(10.0f, distance));
            glControl.Invalidate();
        }

        public void Dispose()
        {
            if (vboInitialized)
            {
                GL.DeleteBuffer(vboVertexId);
                GL.DeleteBuffer(vboColorId);
                vboInitialized = false;
            }
        }
    }
}
