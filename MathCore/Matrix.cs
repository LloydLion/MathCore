using MathCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Matrix
	{
		public IReadOnlyList<Vector> Basises { get; }

		public int OutputVectorDimensions { get; }


		public Matrix(IReadOnlyList<Vector> basises)
		{
			var tdim = basises[0].Dimensions;
			if(basises.Any(s => s.Dimensions != tdim))
			{
				throw new ArgumentException
					("one or more vector(s) has difference count of dimensions", nameof(basises));
			}

			OutputVectorDimensions = tdim;
			Basises = basises;
		}

		
		public Vector Apply(Vector vector)
		{
			if (vector.Dimensions != Basises.Count)
				throw new ArgumentException(
					"Can't apply matrix to vector with unexcepted dimensions count", nameof(vector));

			Vector ret = new Vector(OutputVectorDimensions, false);

			for (int i = 0; i < Basises.Count; i++)
				ret = ret.Add(Basises[i].Scale(vector.Cords[i]));

			return ret;
		}

		public Matrix Scale(double scalar)
		{
			return new Matrix(Basises.Select(s => s.Scale(scalar)).ToArray());
		}

		public Matrix MultiplyLeft(Matrix matrix)
		{
			return new Matrix(Basises.Select(s => matrix.Apply(s)).ToArray());
		}

		public Matrix MultiplyRight(Matrix matrix) => matrix.MultiplyLeft(this);

		public Matrix Extend(int down, int up, int right, int left, bool useZero = false)
		{
			List<Vector> basises = new List<Vector>();

			var outDims = OutputVectorDimensions + down + up;
			basises.AddRange(Basises.Select(s => s.Extend(up, down)));

			if (left >= 0)
				for (int i = left - 1; i >= 0; i--)
					basises.Insert(0, useZero ? Vector.Zero(outDims) : Vector.Basis(outDims, i));
			else
				basises.RemoveRange(0, -left);

			if (right >= 0)
				for (int i = 0; i < right; i++)
					basises.Add(useZero ? Vector.Zero(outDims) : Vector.Basis(outDims, i));
			else
				basises.RemoveRange(basises.Count - 1 + right, right);

			return new Matrix(basises);
		}

		public Transformation AsTransformation()
		{
			return new Transformation(s => Apply(s.ToVector()).ToPoint());
		}

		public override string ToString()
		{
			return string.Join("\n", Basises.Select((s, i) => $"Basis {i + 1}: {s}"));
		}


		public static Matrix Identity(int dims) => Scale(new byte[dims].Select(s => 1.0).ToArray());

		public static Matrix Scale(params double[] scales)
		{
			var dims = scales.Length;
			Vector[] basises = new Vector[dims];
			var zero = new Vector(dims, true);
			for (int i = 0; i < dims; i++)
				basises[i] = (Vector)zero.Clone(new (int, double)[] { (i, scales[i]) });
			return new Matrix(basises);
		}

		public static Matrix Rotation2D(double angle)
		{
			return new Matrix(new Vector[]
			{
				new Vector(Math.Cos(angle), -Math.Sin(angle)),
				new Vector(Math.Sin(angle), Math.Cos(angle))
			});
		}
	}
}

