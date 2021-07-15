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

		public Transformation AsTransformation()
		{
			return new Transformation(s => Apply(s.ToVector()).ToPoint());
		}
	}
}
