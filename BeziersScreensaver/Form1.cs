using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BeziersScreensaver.Entities;
using BeziersScreensaver.Services;
using BeziersScreensaver.Services.Interfaces;

namespace BeziersScreensaver
{
	public partial class Form1 : Form
	{
        private Timer _timer;
        private Timer _timer2;

        private int _centerX => Width / 2;
        private int _centerY => Height / 2;
        private float _degree = 0;
        private const float MAX_DEGREE = 180;
        private float _degreeInc = 0.01f;

        private float _control1Inc = 0.1f;
        private float _control2Inc = 0.35f;
        private float _startInc = 0.135f;
        private float _endInc = 0.01f;

        private CurveSurface[] _curveSurfaces;
        private INoise<float> _noise;

        private int _curvesCount = 100;
        private int _curvesSurfacesCount = 5;

        public Form1()
		{
			InitializeComponent();
            this.DoubleBuffered = true;
            UpdateStyles();
            _curveSurfaces = new CurveSurface[_curvesSurfacesCount];
			for (int i = 0; i < _curvesSurfacesCount; i++)
			{
                _curveSurfaces[i] = new CurveSurface(_curvesCount, InitialFunctionWithParam);
            }
            _noise = new MyNoise();
            Paint += Form1_Paint;
			KeyDown += Form1_KeyDown;
            
            _timer = new Timer();
            _timer.Interval = 16;
			_timer.Tick += _timer_Tick;

            _timer2 = new Timer();
            _timer2.Interval = 1;
			_timer2.Tick += _timer2_Tick;

            _timer.Start();
            _timer2.Start();
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Space)
			{
                Random ranadom = new Random();
                _startInc = (float)ranadom.NextDouble();
                _control1Inc = (float)ranadom.NextDouble();
                _control2Inc = (float)ranadom.NextDouble();
                _endInc = (float)ranadom.NextDouble();
                //_degreeInc = (float)ranadom.NextDouble();
            }         
		}

		private void _timer2_Tick(object sender, EventArgs e)
		{
            _degree += _degreeInc;
            if (_degree > MAX_DEGREE)
                _degree = 0;
			for (int j = 0; j < _curveSurfaces.Length; j++)
			{
                for (int i = 0; i < _curveSurfaces[j].Curves.Length; i++)
                {
                    _curveSurfaces[j].Curves[i].StartX = _noise.GetNoize(_degree + j) * Width;
                    _curveSurfaces[j].Curves[i].StartY =  _noise.GetNoize(_degree + j) * Height;

                    _curveSurfaces[j].Curves[i].Control1X = _noise.GetNoize(_degree + _noise.GetNoize(j + _control1Inc)) * Width;
                    _curveSurfaces[j].Curves[i].Control1Y = _noise.GetNoize(_degree + _control1Inc + j) * Height;

                    _curveSurfaces[j].Curves[i].Control2X = _noise.GetNoize(_degree + i) * Width;
                    _curveSurfaces[j].Curves[i].Control2Y = _noise.GetNoize(_degree + _control2Inc + i) * Height;

                    _curveSurfaces[j].Curves[i].EndX = _noise.GetNoize(_degree + i) * Width;
                    _curveSurfaces[j].Curves[i].EndY = _noise.GetNoize(_degree + i) * Height;

                    _curveSurfaces[j].Curves[i].Pen.Color = Color.FromArgb(i % 256, _curveSurfaces[j].Curves[i].Pen.Color);
                }
            }			
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
            Refresh();
        }

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			for (int i = 0; i < _curveSurfaces.Length; i++)
			{
                _curveSurfaces[i].Draw(e.Graphics);
            }
        }

        private BezierCurve InitialFunction()
        {
            Random random = new Random();
            var result = new BezierCurve(0, 0, _centerX, 0, _centerX, Height, Width, Height);
            result.Pen.Color = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
            return result;
        }

        private BezierCurve InitialFunctionWithParam(int param)
        {
            Random random = new Random();
            var result = new BezierCurve(0, 0, _centerX, 0, _centerX, Height, Width, Height);
            int a = param % 3;
            switch (a)
			{
                case 0:
                    result.Pen.Color = Color.Aqua;
                    break;
                case 1:
                    result.Pen.Color = Color.White;
                    break;
                case 2:
                    result.Pen.Color = Color.LightYellow;
                    break;

            }
            return result;
        }
    }
}
