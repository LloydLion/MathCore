using MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	[TestClass]
	public class VectorTest
	{
		[TestMethod]
		public void Creation()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.Next();
				var y = random.Next();

				var vector = new Vector(x, y);

				Assert.IsTrue(vector.IsInt);
				Assert.AreEqual(vector.Dimensions, 2);

				Assert.AreEqual(vector.XCordInt, x);
				Assert.AreEqual(vector.YCordInt, y);

				//------------------------------

				vector = new Vector((double)x, y);

				Assert.IsFalse(vector.IsInt);
				Assert.AreEqual(vector.Dimensions, 2);

				Assert.AreEqual(vector.XCord, x);
				Assert.AreEqual(vector.YCord, y);

				//------------------------------

				vector = new Vector(34, true);

				Assert.AreEqual(vector.Dimensions, 34);
				Assert.IsTrue(vector.IsInt);
			}
		}

		[TestMethod]
		public void VectorSum()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x1 = random.Next(int.MaxValue / 3);
				var y1 = random.Next(int.MaxValue / 3);
				var x2 = random.Next(int.MaxValue / 3);
				var y2 = random.Next(int.MaxValue / 3);

				var v1 = new Vector(x1, y1);
				var v2 = new Vector(x2, y2);
				var vs = v1.Add(v2);

				Assert.IsTrue(vs.IsInt);
				Assert.AreEqual(vs.XCordInt, x1 + x2);
				Assert.AreEqual(vs.YCordInt, y1 + y2);

				//------------------------------

				v1 = new Vector((double)x1, y1);
				v2 = new Vector(x2, y2);
				vs = v1.Add(v2);

				Assert.IsFalse(vs.IsInt);
				Assert.AreEqual(vs.XCord, (double)x1 + x2);
				Assert.AreEqual(vs.YCord, (double)y1 + y2);
			}
		}

		[TestMethod]
		public void Scaling()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.Next(10000);
				var y = random.Next(10000);
				var s = random.Next(10000);

				var vector = new Vector(x, y);

				vector = vector.Scale(s);

				Assert.IsTrue(vector.IsInt);
				Assert.AreEqual(vector.XCordInt, s * x);
				Assert.AreEqual(vector.YCordInt, s * y);

				//-------------------------

				var d = random.NextDouble() * random.Next(10000);

				vector = new Vector(x, y).Scale(d);

				Assert.IsFalse(vector.IsInt);
				Assert.AreEqual(vector.XCord, d * x);
				Assert.AreEqual(vector.YCord, d * y);
			}
		}

		[TestMethod]
		public void LengthProperty()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.Next(10000);
				var y = random.Next(10000);

				var vector = new Vector(x, y);

				Assert.AreEqual(Math.Sqrt(x * x + y * y), vector.Length);
			}
		}

		[TestMethod]
		public void DotProduct()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x1 = random.Next(10000);
				var y1 = random.Next(10000);
				var x2 = random.Next(10000);
				var y2 = random.Next(10000);

				var v1 = new Vector(x1, y1);
				var v2 = new Vector(x2, y2);

				Assert.AreEqual(v1.DotProduct(v2), x1*x2+y1*y2);
			}
		}

		[TestMethod]
		public void Clone()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.Next(10000);
				var y = random.Next(10000);
				var z = random.Next(10000);
				var w = random.Next(10000);

				var y1 = random.Next(10000);
				var z1 = random.Next(10000);

				var vector = new Vector(x, y, z, w);

				vector = (Vector)vector.Clone(new (int, double)[] { (1, y1), (2, z1) });

				Assert.AreEqual(vector.XCordInt, x);
				Assert.AreEqual(vector.YCordInt, y1);
				Assert.AreEqual(vector.ZCordInt, z1);
				Assert.AreEqual(vector.GetIntCord(3), w);
			}
		}

		[TestMethod]
		public void Extend()
		{
			var random = new Random(3000);
			for (int i = 0; i < 1000; i++)
			{
				var x = random.Next(10000);
				var y = random.Next(10000);
				var z = random.Next(10000);
				var w = random.Next(10000);
				var u = random.Next(10000);

				var vector = new Vector(x, y, z, w, u);

				var v1 = vector.Extend(2, 0);

				Assert.AreEqual(v1.Dimensions, 7);
				Assert.AreEqual(v1.GetIntCord(0), 0);
				Assert.AreEqual(v1.GetIntCord(1), 0);
				Assert.AreEqual(v1.GetIntCord(2), x);
				Assert.AreEqual(v1.GetIntCord(3), y);
				Assert.AreEqual(v1.GetIntCord(4), z);
				Assert.AreEqual(v1.GetIntCord(5), w);
				Assert.AreEqual(v1.GetIntCord(6), u);

				var v2 = vector.Extend(-2, 0);

				Assert.AreEqual(v2.Dimensions, 3);
				Assert.AreEqual(v2.GetIntCord(0), z);
				Assert.AreEqual(v2.GetIntCord(1), w);
				Assert.AreEqual(v2.GetIntCord(2), u);

				var v3 = vector.Extend(0, 2);

				Assert.AreEqual(v3.Dimensions, 7);
				Assert.AreEqual(v3.GetIntCord(0), x);
				Assert.AreEqual(v3.GetIntCord(1), y);
				Assert.AreEqual(v3.GetIntCord(2), z);
				Assert.AreEqual(v3.GetIntCord(3), w);
				Assert.AreEqual(v3.GetIntCord(4), u);
				Assert.AreEqual(v3.GetIntCord(5), 0);
				Assert.AreEqual(v3.GetIntCord(6), 0);

				var v4 = vector.Extend(0, -2);

				Assert.AreEqual(v4.Dimensions, 3);
				Assert.AreEqual(v4.GetIntCord(0), x);
				Assert.AreEqual(v4.GetIntCord(1), y);
				Assert.AreEqual(v4.GetIntCord(2), z);

				var v5 = vector.Extend(-2, 2);

				Assert.AreEqual(v5.Dimensions, 5);
				Assert.AreEqual(v5.GetIntCord(0), z);
				Assert.AreEqual(v5.GetIntCord(1), w);
				Assert.AreEqual(v5.GetIntCord(2), u);
				Assert.AreEqual(v5.GetIntCord(3), 0);
				Assert.AreEqual(v5.GetIntCord(4), 0);
			}
		}
	}
}
