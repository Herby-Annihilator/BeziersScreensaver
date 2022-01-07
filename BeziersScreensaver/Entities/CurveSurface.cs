using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeziersScreensaver.Entities
{
	public class CurveSurface
	{
		public BezierCurve[] Curves { get; private set; }
		public CurveSurface(int curvesCount)
		{
			Curves = new BezierCurve[curvesCount];
			for (int i = 0; i < curvesCount; i++)
			{
				Curves[i] = new BezierCurve();
			}
		}

		public CurveSurface(int count, Func<BezierCurve> initialFunction)
		{
			Curves = new BezierCurve[count];
			for (int i = 0; i < count; i++)
			{
				Curves[i] = initialFunction();
			}
		}

		public CurveSurface(int count, Func<int, BezierCurve> initialFunction)
		{
			Curves = new BezierCurve[count];
			for (int i = 0; i < count; i++)
			{
				Curves[i] = initialFunction(i);
			}
		}

		public void Draw(Graphics graphics)
		{
			for (int i = 0; i < Curves.Length; i++)
			{
				Curves[i].Draw(graphics);
			}
		}
	}
}
