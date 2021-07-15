using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Vector : CordBase
	{
		public double Length
		{ get => Math.Sqrt(Cords.Select(s => s * s).Sum()); }


		public Vector(IReadOnlyList<int> ints) : base(ints) { }

		public Vector(IReadOnlyList<double> cords) : base(cords) { }

		public Vector(params int[] cords) : base(cords) { }

		public Vector(params double[] cords) : base(cords) { }

		public Vector(int dimensions, bool isInt) : base(dimensions, isInt) { }

		public Vector(IReadOnlyList<double> cords, bool isInt) : base(cords, isInt) { }


		public Vector Add(Vector vector)
		{
			var cords = Sum(this, vector, out bool isInt);
			return new Vector(cords, isInt);
		}

		public Vector Scale(double scalar) => Scale(scalar, false);

		public Vector Scale(int scalar) => Scale(scalar, true);

		private Vector Scale(double scalar, bool isInt)
		{
			return new Vector(Cords.Select(s => s * scalar).ToArray(), isInt);
		}

		public double DotProduct(Vector vector)
		{
			if (Dimensions != vector.Dimensions)
				throw new ArgumentException("Can't calculate dot product " +
					"of vectors with difference dimensions count");

			double sum = 0;
			for (int i = 0; i < vector.Dimensions; i++)
				sum += vector.Cords[i] * Cords[i];

			return sum;
		}

		public Vector Normalize()
		{
			return Scale(1 / Length);
		}

		public override CordBase Clone(IReadOnlyList<(int, double)> overrides = null)
		{
			overrides = overrides ?? Array.Empty<(int, double)>();
			var noverrides = overrides.Select(s => ((int, double)?)s);
			return new Vector(Cords.Select((s, i) => 
			{
				double ret = s;
				var @override = noverrides.SingleOrDefault(a => a.Value.Item1 == i);
				if (@override != null)
					ret = @override.Value.Item2;
				return ret;
			}).ToArray(), IsInt);
		}

		public Vector ApplyToAllCords(Func<int, double, double> func)
		{
			double[] newCords = Cords.Select((s, i) => func(i, s)).ToArray();
			return new Vector(newCords, IsInt);
		}

		public double ColliniarDegree(Vector vector)
		{
			return Normalize().DotProduct(vector.Normalize());
		}
	}
}
