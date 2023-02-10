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
            rulerForm.Load += new System.EventHandler(RulerForm_Load);

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
            float screenDPI = e.Graphics.DpiX;
            float cmSizeInPixels = screenDPI * (1f / 2.54f); // 1 cm in pixels
            float mmSizeInPixels = cmSizeInPixels / 10;

            e.Graphics.DrawLine(new Pen(Brushes.Black, 2), startPoint, endPoint);

            // Draw division marks along the line
            float distance = (float)Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            int cmCount = (int)(distance / cmSizeInPixels);

            for (int i = 0; i <= cmCount; i++)
            {
                PointF cmDivisionPoint = new PointF(
                    startPoint.X + i * cmSizeInPixels * (endPoint.X - startPoint.X) / distance,
                    startPoint.Y + i * cmSizeInPixels * (endPoint.Y - startPoint.Y) / distance
                );

                e.Graphics.DrawLine(new Pen(Brushes.Black, 2), cmDivisionPoint.X, cmDivisionPoint.Y - 10, cmDivisionPoint.X, cmDivisionPoint.Y + 10);

                // Display cm label
                if (i != cmCount)
                {
                    SizeF labelSize = e.Graphics.MeasureString((i).ToString(), this.Font);
                    PointF labelLocation = new PointF(
                        cmDivisionPoint.X - labelSize.Width / 2,
                        cmDivisionPoint.Y + 15
                    );
                    e.Graphics.DrawString((i).ToString(), this.Font, Brushes.Black, labelLocation);
                }

                // Draw mm division marks
                for (int j = 1; j < 10; j++)
                {
                    PointF mmDivisionPoint = new PointF(
                        cmDivisionPoint.X + j * mmSizeInPixels * (endPoint.X - startPoint.X) / distance,
                        cmDivisionPoint.Y + j * mmSizeInPixels * (endPoint.Y - startPoint.Y) / distance
                    );
                    if (j == 5)
                    {
                        e.Graphics.DrawLine(new Pen(Brushes.Black, 1), mmDivisionPoint.X, mmDivisionPoint.Y - 15, mmDivisionPoint.X, mmDivisionPoint.Y + 15);
                    }
                    else
                    {
                        e.Graphics.DrawLine(new Pen(Brushes.Black, 1), mmDivisionPoint.X, mmDivisionPoint.Y - 10, mmDivisionPoint.X, mmDivisionPoint.Y + 10);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            rulerForm.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            rulerForm.WindowState = FormWindowState.Minimized;
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

        private void RulerForm_Load(object sender, EventArgs e)
        {
            Button btnClose = new Button();
            btnClose.Text = "X";
            btnClose.BackColor = Color.Red;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Size = new Size(30, 30);
            btnClose.Location = new Point(rulerForm.Width - 35, 5);
            btnClose.Click += new EventHandler(btnClose_Click);

            Button btnMinimize = new Button();
            btnMinimize.Text = "-";
            btnMinimize.BackColor = Color.White;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.Location = new Point(rulerForm.Width - 70, 5);
            btnMinimize.Click += new EventHandler(btnMinimize_Click);

            rulerForm.Controls.Add(btnClose);
            rulerForm.Controls.Add(btnMinimize);
        }
    }
}
