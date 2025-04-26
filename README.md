
# KinectMapping
## Overview
KinectMapping is a Windows application that utilizes the Kinect v2 sensor to visualize 3D point clouds in real-time. The project integrates Kinect's color and depth data to generate 3D point clouds of the environment. It uses OpenTK for 3D rendering and provides an interactive interface for adjusting camera angles.

This project was created to learn and understand Kinect sensors, 3D graphics programming, and interactive visualization techniques.

## Features
Kinect Integration: The application uses the Kinect v2 sensor to capture depth and color data in real-time, displaying a 3D point cloud.

Interactive Controls: Yaw, Pitch, and Roll Sliders adjust camera angles to view the point cloud from different perspectives.

Real-Time Visualization: Continuous updates of the Kinect’s data stream, with an easy-to-use interface to interact with the point cloud and camera.

## Learning Outcomes
During the development of this project, several key concepts and techniques were learned:

Kinect API Usage: Gained practical experience working with the Kinect v2 SDK, including managing depth and color frames, mapping depth data to camera space, and using coordinate mappers.

Point Cloud Generation and Rendering: Developed a deeper understanding of 3D rendering, using OpenGL, by visualizing depth data as a point cloud.

OpenTK for OpenGL Rendering: Learned how to set up and use OpenTK to create 3D graphics, particularly in managing buffers.

Interactive GUI: Built a user interface using Windows Forms, connecting it with Kinect data to allow real-time adjustments of camera angles and zoom levels.

Efficient Data Handling: Implemented optimizations for handling and displaying Kinect data, such as transferring pixel data to Bitmaps and optimizing the rendering loop for performance.