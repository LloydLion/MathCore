using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Extensions
{
	public static class CordBaseCastExtensions
	{
		public static Vector ToVector(this CordBase cords)
		{
			return new Vector(cords.Cords, cords.IsInt);
		}

		public static Point ToPoint(this CordBase cords)
		{
			return new Point(cords.Cords, cords.IsInt);
		}
	}
}
