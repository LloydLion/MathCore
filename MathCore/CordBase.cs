using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public abstract class CordBase : ICloneable
	{
		private double[] cords;


		public IReadOnlyList<double> Cords => cords;

		public int Dimensions => cords.Length;

		public bool IsInt { get; private set; }

		public double XCord { get => cords[0]; }

		public double YCord { get => cords[1]; }

		public double ZCord { get => cords[2]; }

		public int XCordInt { get => GetIntCord(0); }

		public int YCordInt { get => GetIntCord(1); }

		public int ZCordInt { get => GetIntCord(2); }


		public int GetIntCord(int dim)
		{
			ThrowIfNotInt();
			return (int)cords[dim];
		}

		protected void ThrowIfNotInt(bool inverse = false)
		{
			string add = inverse ? "n'" : "";
			if (IsInt != inverse) throw new InvalidOperationException
					($"Vector mus{add}t be Integer to do this operation. Check IsInt property");
		}

		public void DisableInt()
		{
			IsInt = false;
		}

		public void RoundCords()
		{
			cords = cords.Select(s => Math.Floor(s)).ToArray();
			IsInt = true;
		}

		protected static IReadOnlyList<double> Sum(CordBase cord1, CordBase cord2, out bool isInt)
		{
			if (cord1.Dimensions != cord2.Dimensions)
				throw new ArgumentException("Can't calculate sum of objects with difference dimensions count");

			isInt = cord1.IsInt && cord2.IsInt;
			return cord1.cords.Select((s, i) => s + cord2.cords[i]).ToArray();
		}

		public abstract CordBase Clone(IReadOnlyList<(int, double)> overrides = null);

		object ICloneable.Clone()
		{
			return Clone();
		}


		public CordBase(int dimensions, bool isInt)
		{
			cords = new double[dimensions];
			IsInt = isInt;
		}

		public CordBase(IReadOnlyList<int> ints)
		{
			cords = ints.Select(s => (double)s).ToArray();
			IsInt = true;
		}

		public CordBase(IReadOnlyList<double> cords)
		{
			this.cords = cords.ToArray();
			IsInt = false;
		}

		public CordBase(IReadOnlyList<double> cords, bool isInt)
		{
			this.cords = cords.Select(s => Math.Floor(s)).ToArray();
			IsInt = isInt;
		}

		public CordBase(params int[] cords) : this((IReadOnlyList<int>)cords) { }

		public CordBase(params double[] cords) : this((IReadOnlyList<double>)cords) { }
	}
}
