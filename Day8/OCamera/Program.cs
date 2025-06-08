using OpenCvSharp;

using var capture = new VideoCapture(0);
using var window = new Window("Camera Feed");
using var frame = new Mat();
while (true)
{
    if (!capture.Read(frame) || frame.Empty())
    {
        break; // Exit if no frame is captured
    }

    window.ShowImage(frame);

    // Exit on 'q' key press
    if (Cv2.WaitKey(1) == 'q')
    {
        break;
    }
}