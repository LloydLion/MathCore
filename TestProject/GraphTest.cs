using MathCore;
using MathCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	[TestClass]
	public class GraphTest
	{
		[TestMethod]
		public void BasicTest()
		{
			var graph = new Graph(Selector);

			var res =
				graph.Build2D(new Graph.Area() 
				{ StartPoint = new Graph.Point(-10, -10, 0), EndPoint = 
				new Graph.Point(10, 10, 0) }, 20, new Progress<double>()).Result;

			foreach (var point in res)
			{
				Debug.WriteLine($"point [{point.X}, {point.Y}, {point.Z}]");
				Assert.IsTrue((point.X % 2).IsAround(0, 0.1) && (point.Y % 3).IsAround(0, 0.1));
			}
		}

		[TestMethod]
		public void Progress()
		{
			var graph = new Graph(Selector);

			var res =
				graph.Build2D(new Graph.Area()
				{
					StartPoint = new Graph.Point(-100, -100, 0),
					EndPoint =
				new Graph.Point(100, 100, 0)
				}, 200, new Progress<double>()).Result;


		}

		private static bool Selector(Graph.Point point)
		{
			return (point.X % 2).IsAround(0, 0.1) && (point.Y % 3).IsAround(0, 0.1);
		}
	}
}
