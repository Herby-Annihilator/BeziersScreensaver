using BeziersScreensaver.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeziersScreensaver.Services
{
	public class MyNoise : INoise<float>
	{
		public float GetNoize(float param)
		{
			int a = (int)param;
			float b = param - a;
			return Mix(Random(a), Random(a + 1), CubicFunction(b));
		}

		private float CubicFunction(float value) =>  value * value * (3 - 2 * value);

		private float Mix(float x, float y, float a) => x * (1 - a) + y * a;

		private float Random(int value)
		{
			float result;
			double sin = Math.Abs(Math.Sin(ToRadians(value)) * 10000);
			int a = (int)sin;
			result = (float)(sin - a);
			return result;
		}

		private double ToRadians(int degree) => degree * Math.PI / 180;
	}
}
