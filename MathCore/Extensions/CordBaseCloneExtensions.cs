using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Extensions
{
	public static class CordBaseCloneExtensions
	{
		public static Point ClonePoint(this Point point)
		{
			return (Point)point.Clone();
		}

		public static Vector CloneVector(this Vector vector)
		{
			return (Vector)vector.Clone();
		}
	}
}
