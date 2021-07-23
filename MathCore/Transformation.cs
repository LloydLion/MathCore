using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class Transformation : Function
	{
		private readonly Func<Point, Point> @delegate;


		public Transformation(Func<Point, Point> @delegate)
			: base(new Type[] { typeof(Point) }, new Type[] { typeof(Point) },
				  (a) => new object[] { @delegate((Point)a.Single()) })
		{
			this.@delegate = @delegate;
		}

		public Point Calculate(Point point)
		{
			return @delegate(point);
		}
	}
}
