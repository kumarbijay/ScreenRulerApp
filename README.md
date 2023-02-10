# Screen Ruler
This application is a simple yet useful tool for measuring the distance between two points on the screen. It is a Windows Forms based application that works on .NET Framework and provides measurements in pixels and centimeters. It provides a transparent overlay on the screen so that you can measure distances in any program window. The ruler form can be launched with a button click from the main form and the main form can be minimized to keep it out of the way while you measure.

## Features 
- Measure distances between two points on the screen in pixels and centimeters
- Transparent background for the ruler form for accurate measurement
- Measurement values displayed in a text label
- Minimizes the main form when the ruler form is opened
- Transparent ruler form to measure distance over any window or screen.
- User-friendly UI

## Requirements
- .NET Framework 4.5 or higher
- Visual Studio 2017 or higher (for building the project from source code)

## Getting Started
1. Clone or download the repository
2. Open the solution file in Visual Studio
3. Build and run the application
4. Click the "Start" button to open the ruler form 
5. Select the starting point and end point to measure distance
6. The measurement in pixels and centimeters will be displayed on the ruler form

## Project Structure
The solution consists of two projects:
1. ScreenRuler: A Windows Forms application to display the measurement
2. RulerForm: A form that acts as a transparent ruler to measure distance between two points

## Code Explanation
- The `Form1` class contains the UI for the main application window and implements the button click event to open the ruler form
- The `RulerForm` class implements the transparent ruler form and displays the measurement
- The measurement is calculated using the `System.Drawing` namespace's `Graphics` class and `GetPixel` method
- The measurement in centimeters is calculated based on the screen DPI

## Limitations
- The measurement is limited to the screen resolution
- The measurement accuracy may be affected by screen DPI settings

## Contributing
- Fork the repository
- Make changes to the code
- Submit a pull request

## License
This project is licensed under the MIT License. Refer to the LICENSE file for details.
