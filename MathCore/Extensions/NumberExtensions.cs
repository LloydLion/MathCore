using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Extensions
{
	public static class NumberExtensions
	{
		public static bool IsAround(this double num, double tnum, double range)
		{
			return Math.Abs(num - tnum) <= range;
		}
	}
}
