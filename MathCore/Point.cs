using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Point : CordBase
	{
		public Point(IReadOnlyList<int> ints) : base(ints) { }

		public Point(IReadOnlyList<double> cords) : base(cords) { }

		public Point(params int[] cords) : base(cords) { }

		public Point(params double[] cords) : base(cords) { }

		public Point(int dimensions, bool isInt) : base(dimensions, isInt) { }

		public Point(IReadOnlyList<double> cords, bool isInt) : base(cords, isInt) { }


		public Point Translate(Vector vector)
		{
			return new Point(Sum(this, vector, out bool isInt), isInt);
		}

		public override CordBase Clone(IReadOnlyList<(int, double)> overrides = null)
		{
			overrides = overrides ?? Array.Empty<(int, double)>();
			var noverrides = overrides.Select(s => ((int, double)?)s);
			return new Point(Cords.Select((s, i) =>
			{
				double ret = s;
				var @override = noverrides.SingleOrDefault(a => a.Value.Item1 == i);
				if (@override != null)
					ret = @override.Value.Item2;
				return ret;
			}).ToArray(), IsInt);
		}
	}
}
