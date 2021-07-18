using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	[TestClass]
	public class MatrixTest
	{
		[TestMethod]
		public void Creation()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x1 = random.Next();
				var y1 = random.Next();

				var x2 = random.Next();
				var y2 = random.Next();

				var matrix = new Matrix(new Vector[] { new Vector(x1, y1), new Vector(x2, y2) });

				Assert.AreEqual(matrix.OutputVectorDimensions, 2);
				Assert.AreEqual(matrix.Basises[0].XCordInt, x1);
				Assert.AreEqual(matrix.Basises[0].YCordInt, y1);
				Assert.AreEqual(matrix.Basises[1].XCordInt, x2);
				Assert.AreEqual(matrix.Basises[1].YCordInt, y2);

				//-----------------------------

				Assert.ThrowsException<ArgumentException>(() => new Matrix(new Vector[] { new Vector(x1, y1), new Vector(x2, y2, 4) }));
			}
		}

		[TestMethod]
		public void Apply()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x1 = random.Next(10000);
				var y1 = random.Next(10000);
				var z1 = random.Next(10000);

				var x2 = random.Next(10000);
				var y2 = random.Next(10000);
				var z2 = random.Next(10000);

				var x3 = random.Next(10000);
				var y3 = random.Next(10000);

				var matrix = new Matrix(new Vector[] { new Vector(x1, y1, z1), new Vector(x2, y2, z2) });
				var vector = new Vector(x3, y3);

				var apply = matrix.Apply(vector);

				Assert.AreEqual(apply.XCord, x3*x1+y3*x2);
				Assert.AreEqual(apply.YCord, x3*y1+y3*y2);
				Assert.AreEqual(apply.ZCord, x3*z1+y3*z2);

				//----------------------

				Assert.ThrowsException<ArgumentException>(() => matrix.Apply(new Vector(x3, y3, 1)));
			}
		}

		[TestMethod]
		public void Scale()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x1 = random.Next(1000);
				var y1 = random.Next(1000);
				var z1 = random.Next(1000);

				var x2 = random.Next(1000);
				var y2 = random.Next(1000);
				var z2 = random.Next(1000);

				var x3 = random.Next(1000);
				var y3 = random.Next(1000);

				var q = random.Next(1, 1000);

				var matrix = new Matrix(new Vector[] { new Vector(x1, y1, z1), new Vector(x2, y2, z2) });
				var vector = new Vector(x3, y3);

				var apply = matrix.Apply(vector);
				var scale = matrix.Scale(q).Apply(vector);

				Assert.AreEqual(apply.XCord, scale.XCord / q);
				Assert.AreEqual(apply.YCord, scale.YCord / q);
				Assert.AreEqual(apply.ZCord, scale.ZCord / q);
			}
		}

		[TestMethod]
		public void Multiply()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x11 = random.Next(1000);
				var y11 = random.Next(1000);

				var x12 = random.Next(1000);
				var y12 = random.Next(1000);
				
				var x21 = random.Next(1000);
				var y21 = random.Next(1000);

				var x22 = random.Next(1000);
				var y22 = random.Next(1000);

				var x3 = random.Next(1000);
				var y3 = random.Next(1000);

				var matrix1 = new Matrix(new Vector[] { new Vector(x11, y11), new Vector(x12, y12) });
				var matrix2 = new Matrix(new Vector[] { new Vector(x21, y21), new Vector(x22, y22) });
				var vector = new Vector(x3, y3);

				var apply1 = matrix2.Apply(matrix1.Apply(vector));
				var apply2 = matrix2.MultiplyRight(matrix1).Apply(vector);
				var apply3 = matrix1.MultiplyLeft(matrix2).Apply(vector);

				Assert.AreEqual(apply1.XCord, apply2.XCord);
				Assert.AreEqual(apply1.YCord, apply2.YCord);

				Assert.AreEqual(apply1.XCord, apply3.XCord);
				Assert.AreEqual(apply1.YCord, apply3.YCord);
			}
		}
	}
}
