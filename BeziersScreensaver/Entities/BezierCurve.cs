using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeziersScreensaver.Entities
{
	public class BezierCurve
	{
		public Pen Pen { get; set; }

		public float StartX { get; set; }
		public float StartY { get; set; }
		public float Control1X { get; set; }
		public float Control1Y { get; set; }
		public float Control2X { get; set; }
		public float Control2Y { get; set; }
		public float EndX { get; set; }
		public float EndY { get; set; }

		public BezierCurve(float startX, float startY, float control1X, float control1Y, float control2X, float control2Y, float endX, float endY)
		{
			StartX = startX;
			StartY = startY;
			Control1X = control1X;
			Control1Y = control1Y;
			Control2X = control2X;
			Control2Y = control2Y;
			EndX = endX;
			EndY = endY;
			Pen = new Pen(Color.Aqua, 1);
		}

		public BezierCurve() : this(0, 0, 200, 0, 200, 400, 400, 400)
		{

		}

		public void Draw(Graphics graphics)
		{
			graphics.DrawBezier(Pen, StartX, StartY, Control1X, Control1Y, Control2X, Control2Y, EndX, EndY);
		}
	}
}
