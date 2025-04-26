using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KinectMapping
{
    internal class KinectUtil : IDisposable
    {
        private KinectSensor sensor = null;
        private ColorFrameReader colorReader = null;
        private DepthFrameReader depthReader = null;
        private CoordinateMapper coordinateMapper = null;

        private int kinect_width = 1920;
        private int kinect_height = 1080;

        private int depth_width = 0;
        private int depth_height = 0;

        private PictureBox kinectDisplay;
        private Bitmap colorBitmap = null;

        private byte[] colorPixelData = null;
        private ushort[] depthData = null;
        private CameraSpacePoint[] cameraSpacePoints = null;

        private bool isDisposed = false;

        public KinectUtil(PictureBox kinectDisplay)
        {
            this.kinectDisplay = kinectDisplay;
        }

        public KinectUtil(KinectSensor sensor, PictureBox kinectDisplay)
        {
            this.sensor = sensor;
            this.kinectDisplay = kinectDisplay;
        }

        public bool initKinect()
        {
            try
            {
                if (sensor == null)
                {
                    return false;
                }
                coordinateMapper = sensor.CoordinateMapper;

                colorReader = sensor.ColorFrameSource.OpenReader();
                colorReader.FrameArrived += colorReader_FrameArrived;

                depthReader = sensor.DepthFrameSource.OpenReader();
                depthReader.FrameArrived += depthReader_FrameArrived;

                startKinect();

                var colorDesc = sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
                kinect_width = colorDesc.Width;
                kinect_height = colorDesc.Height;

                colorPixelData = new byte[4 * colorDesc.LengthInPixels];

                var depthDesc = sensor.DepthFrameSource.FrameDescription;
                depthData = new ushort[depthDesc.LengthInPixels];
                cameraSpacePoints = new CameraSpacePoint[depthDesc.LengthInPixels];
                depth_width = depthDesc.Width;
                depth_height = depthDesc.Height;

                colorBitmap = new Bitmap(kinect_width, kinect_height);

                kinectDisplay.Image = colorBitmap;
                kinectDisplay.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public void startKinect()
        {
            if (sensor != null && !sensor.IsOpen)
            {
                sensor.Open();
            }
        }

        public void stopKinect()
        {
            if (sensor != null && sensor.IsOpen)
            {
                if (colorReader != null)
                {
                    colorReader.FrameArrived -= colorReader_FrameArrived;
                    colorReader.Dispose();
                    colorReader = null;
                }

                if (depthReader != null)
                {
                    depthReader.FrameArrived -= depthReader_FrameArrived;
                    depthReader.Dispose();
                    depthReader = null;
                }

                sensor.Close();
                sensor = null;
            }
        }

        private void colorReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                    try
                    {
                        frame.CopyConvertedFrameDataToArray(colorPixelData, ColorImageFormat.Bgra);
                        TransferPixelsToBitmapObject((Bitmap)kinectDisplay.Image, colorPixelData);
                        kinectDisplay.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //Application.DoEvents();
                }
            }
        }


        private void depthReader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            using (DepthFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    frame.CopyFrameDataToArray(depthData);

                    coordinateMapper.MapDepthFrameToCameraSpace(depthData, cameraSpacePoints);

                    for (int i = 0; i < cameraSpacePoints.Length; i++)
                    {
                        CameraSpacePoint point = cameraSpacePoints[i];
                        //Console.WriteLine("Point {0}: ({1:0.00}, {2:0.00}, {3:0.00})", i, point.X, point.Y, point.Z);
                    }
                }
            }
        }

        public List<CameraSpacePoint> GetPointCloud()
        {
            var points = new List<CameraSpacePoint>();

            if (cameraSpacePoints == null)
                return points;

            for (int i = 0; i < cameraSpacePoints.Length; i++)
            {
                CameraSpacePoint p = cameraSpacePoints[i];

                if (!float.IsInfinity(p.X) && !float.IsNaN(p.X) && !float.IsInfinity(p.Y) && !float.IsNaN(p.Y) && !float.IsInfinity(p.Z) && !float.IsNaN(p.Z))
                {
                    points.Add(p);
                }
            }

            return points;
        }

        public List<ColorSpacePoint> MapDepthPointsToColorSpace()
        {
            if (cameraSpacePoints == null || depthData == null)
                return new List<ColorSpacePoint>();

            var depthFrameDescription = sensor.DepthFrameSource.FrameDescription;
            ColorSpacePoint[] colorSpacePoints = new ColorSpacePoint[depthFrameDescription.Width * depthFrameDescription.Height];

            coordinateMapper.MapDepthFrameToColorSpace(depthData, colorSpacePoints);

            // Filter the color space points to match depths
            var filteredPoints = new List<ColorSpacePoint>();

            for (int i = 0; i < cameraSpacePoints.Length; i++)
            {
                CameraSpacePoint p = cameraSpacePoints[i];

                if (!float.IsInfinity(p.X) && !float.IsNaN(p.X) &&
                    !float.IsInfinity(p.Y) && !float.IsNaN(p.Y) &&
                    !float.IsInfinity(p.Z) && !float.IsNaN(p.Z))
                {
                    filteredPoints.Add(colorSpacePoints[i]);
                }
            }

            return filteredPoints;
        }

        public byte[] GetColorPixelData()
        {
            return colorPixelData;
        }

        public int GetColorWidth()
        {
            return kinect_width;
        }

        public int GetColorHeight()
        {
            return kinect_height;
        }

        public int getDepthWidth()
        {
            return depth_width;
        }

        public int getDepthHeight()
        {
            return depth_height;
        }

        private void TransferPixelsToBitmapObject(Bitmap bmTarget, byte[] byPixelsForBitmap)
        {
            Rectangle rectAreaOfInterest = new Rectangle(0, 0, bmTarget.Width, bmTarget.Height);
            BitmapData bmpData = null;

            try
            {
                bmpData = bmTarget.LockBits(rectAreaOfInterest, ImageLockMode.WriteOnly, bmTarget.PixelFormat);
                IntPtr ptrFirstScanLineOfBitmap = bmpData.Scan0;
                int length = byPixelsForBitmap.Length;

                Marshal.Copy(byPixelsForBitmap, 0, ptrFirstScanLineOfBitmap, length);

            }
            finally
            {
                if (bmpData != null)
                {
                    bmTarget.UnlockBits(bmpData);
                }
            }
        }

        ~KinectUtil()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    stopKinect();
                    if (colorBitmap != null)
                    {
                        colorBitmap.Dispose();
                        colorBitmap = null;
                    }

                    colorPixelData = null;
                    depthData = null;
                    cameraSpacePoints = null;
                }

                isDisposed = true;
            }
        }
    }
}
