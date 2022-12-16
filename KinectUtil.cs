using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace KinectMapping
{
    internal class KinectUtil
    {
        private KinectSensor sensor = null;
        private ColorFrameReader colorReader = null;
        private DepthFrameReader depthReader = null;
        private Bitmap colorBitmap = null;
        private CoordinateMapper coordinateMapper = null;

        private int kinect_width = 1920;
        private int kinect_height = 1080;

        private PictureBox kinect_display;

        public KinectUtil(ref PictureBox kinect_display) {
            this.kinect_display = kinect_display;
        }

        public KinectUtil(ref KinectSensor sensor, ref PictureBox kinect_display) { 
            this.sensor = sensor;
            this.kinect_display = kinect_display;
        }

        public bool initKinect() {
            try {
                sensor = KinectSensor.GetDefault();
                coordinateMapper = sensor.CoordinateMapper;
                if (sensor == null) {
                    return false;
                }

                colorReader = sensor.ColorFrameSource.OpenReader();
                colorReader.FrameArrived += colorReader_FrameArrived;

                depthReader = sensor.DepthFrameSource.OpenReader();
                depthReader.FrameArrived += depthReader_FrameArrived;

                startKinect();

                colorBitmap = new Bitmap(kinect_width, kinect_height);
                kinect_display.Image = (Image)colorBitmap.Clone();
                kinect_display.Refresh();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public void startKinect() {
            if (sensor != null && !sensor.IsOpen) {
                sensor.Open();
            }
        }

        public void stopKinect() {
            if (sensor != null && sensor.IsOpen) {
                if (colorReader != null) {
                    colorReader.FrameArrived -= colorReader_FrameArrived;
                    colorReader = null;
                }
                
                if (depthReader != null) {
                    depthReader.FrameArrived -= depthReader_FrameArrived;
                    depthReader = null;
                }

                sensor.Close();
            }
        }

        private void colorReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e) {
            ColorFrame frame = e.FrameReference.AcquireFrame();

            if (frame != null) {
                byte[] pixelData = new byte[4 * frame.FrameDescription.LengthInPixels];
                frame.CopyConvertedFrameDataToArray(pixelData, ColorImageFormat.Bgra);
                frame.Dispose();

                try {
                    TransferPixelsToBitmapObject((Bitmap)(kinect_display.Image), pixelData);
                    kinect_display.Refresh();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }

                Application.DoEvents();
            }
        }

        private void depthReader_FrameArrived(object sender, DepthFrameArrivedEventArgs e) {
            DepthFrame frame = e.FrameReference.AcquireFrame();
            if (frame != null) {
                ushort[] depthData = new ushort[frame.FrameDescription.LengthInPixels];
                frame.CopyFrameDataToArray(depthData);

                CameraSpacePoint[] cameraSpacePoints = new CameraSpacePoint[depthData.Length];
                coordinateMapper.MapDepthFrameToCameraSpace(depthData, cameraSpacePoints);

                for(int i = 0; i < cameraSpacePoints.Length; i++) {
                    CameraSpacePoint point = cameraSpacePoints[i];
                    Console.WriteLine("Point {0}: ({1:0.00}, {2:0.00}, {3:0.00})", i, point.X, point.Y, point.Z);
                }
            }

        }

        private void TransferPixelsToBitmapObject(Bitmap bmTarget, byte[] byPixelsForBitmap) {
            Rectangle rectAreaOfInterest = new Rectangle(0, 0, bmTarget.Width, bmTarget.Height);

            BitmapData bmpData = bmTarget.LockBits(rectAreaOfInterest, ImageLockMode.WriteOnly, bmTarget.PixelFormat);
            IntPtr ptrFirstScanLineOfBitmap = bmpData.Scan0;

            int length = byPixelsForBitmap.Length;

            System.Runtime.InteropServices.Marshal.Copy(byPixelsForBitmap, 0, ptrFirstScanLineOfBitmap, length);
            bmTarget.UnlockBits(bmpData);
            bmpData = null;
        }
    }
}
