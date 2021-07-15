using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Extensions
{
	public static class CordBaseCastExtensions
	{
		public static Vector ToVector(this Point point)
		{
			return new Vector(point.Cords, point.IsInt);
		}

		public static Point ToPoint(this Vector vector)
		{
			return new Point(vector.Cords, vector.IsInt);
		}
	}
}
