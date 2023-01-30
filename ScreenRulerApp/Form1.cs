using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace ScreenRulerApp
{
    public partial class Form1 : Form
    {
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing;
        private Form rulerForm;

        public Form1()
        {
            InitializeComponent();
            rulerForm = new Form();
            rulerForm.FormBorderStyle = FormBorderStyle.None;
            rulerForm.AllowTransparency = true;
            rulerForm.WindowState = FormWindowState.Maximized;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });
            rulerForm.Paint += new PaintEventHandler(RulerForm_Paint);
            rulerForm.MouseDown += new MouseEventHandler(RulerForm_MouseDown);
            rulerForm.MouseUp += new MouseEventHandler(RulerForm_MouseUp);
            rulerForm.MouseMove += new MouseEventHandler(RulerForm_MouseMove);
            Button btn1 = new Button();
            btn1.Location = new Point(10, 10);
            btn1.Text = "Start";
            btn1.Size = new Size(100, 30);
            btn1.Click += new EventHandler(button1_Click);
            this.Controls.Add(btn1);

            Label label1 = new Label();
            label1.Text = "Press Alt + F4 to close the ruler anytime.";
            label1.Location= new Point(20, 80);
            label1.Size= new Size(400,40);
            this.Controls.Add(label1);

            Label label2 = new Label();
            label2.Text = "Crafted By: The IT Guy";
            label2.Location = new Point(20, 140);
            label2.Size = new Size(220, 20);
            label2.ForeColor= Color.RosyBrown;
            this.Controls.Add(label2);

            rulerForm.BackColor = Color.AntiqueWhite;
            rulerForm.Opacity = 0.5;
            rulerForm.FormClosed += new FormClosedEventHandler(frmclosed);
        }

        
        private void RulerForm_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
            isDrawing = true;
        }

        private void RulerForm_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint = e.Location;
            isDrawing = false;
            rulerForm.Refresh();
        }

        private void RulerForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location;
                rulerForm.Refresh();
            }
        }

        private void RulerForm_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                float dpi = GetDPI();
                float cmToPixel = dpi/ 2.54f;
                e.Graphics.DrawLine(pen, startPoint, endPoint);
                double distance = Distance(startPoint, endPoint);
                double distanceCm = distance / cmToPixel;
                string measurement = string.Format("{0:0.00} pixels ({1:0.00} cm)", distance, distanceCm);
                SizeF size = e.Graphics.MeasureString(measurement, rulerForm.Font);
                PointF location = new PointF((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2 - size.Height);
                e.Graphics.DrawString(measurement, rulerForm.Font, Brushes.Black, location);
                
            }
        }

        private float GetDPI()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return graphics.DpiX;
            }
        }

        private double Distance(Point p1, Point p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            return Math.Sqrt(a * a + b * b);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            rulerForm.Show();
            rulerForm.Activate();
        }

        private void frmclosed(object sender, FormClosedEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
