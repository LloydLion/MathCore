using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
	public class Graph
	{
		public Predicate<Point> PointSelector { get; }


		public Graph(Predicate<Point> pointSelector)
		{
			PointSelector = pointSelector;
		}


		public Task<Resolver> Build3D(Area graphArea, int sampleCount, IProgress<double> progress)
		{
			return Task.Run(() =>
			{
				var xScale = graphArea.EndPoint.X - graphArea.StartPoint.X; xScale = xScale == 0 ? 1 : xScale;
				var yScale = graphArea.EndPoint.Y - graphArea.StartPoint.Y; yScale = yScale == 0 ? 1 : yScale;
				var zScale = graphArea.EndPoint.Z - graphArea.StartPoint.Z; zScale = zScale == 0 ? 1 : zScale;

				var list = new List<Point>();

				var totalCycles = xScale * yScale * zScale * Math.Pow(sampleCount, 3);
				var currentIterations = 0;
				progress.Report(currentIterations);

				for (double x = graphArea.StartPoint.X; x <= graphArea.EndPoint.X; x += 1 / (double)sampleCount)
				{
					for (double y = graphArea.StartPoint.Y; y <= graphArea.EndPoint.Y; y += 1 / (double)sampleCount)
					{
						for (double z = graphArea.StartPoint.Z; z <= graphArea.StartPoint.Z; z += 1 / (double)sampleCount)
						{
							var point = new Point() { X = x, Y = y, Z = z };

							if (PointSelector.Invoke(point))
							{
								list.Add(point);
							}

							currentIterations++;
							progress.Report(currentIterations / totalCycles * 100);
						}
					}
				}

				progress.Report(100);

				return new Resolver(list);
			});
		}

		public Task<Resolver> Build2D(Area graphArea, int sampleCount, IProgress<double> progress)
		{
			var start = graphArea.StartPoint;
			var end = graphArea.EndPoint;

			start.Z = end.Z = 0;

			graphArea.StartPoint = start;
			graphArea.EndPoint = end;

			return Build3D(graphArea, sampleCount, progress);
		}


		public struct Point
		{
			public Point(double x, double y, double z)
			{
				X = x;
				Y = y;
				Z = z;
			}


			public double X { get; set; }

			public double Y { get; set; }

			public double Z { get; set; }
		}

		public struct Area
		{
			public Point StartPoint { get; set; }

			public Point EndPoint { get; set; }
		}

		public class Resolver : IEnumerable<Point>
		{
			private readonly IReadOnlyList<Point> graphPoints;


			public Resolver(IReadOnlyList<Point> graphPoints)
			{
				this.graphPoints = graphPoints;
			}


			public bool HasPointAt(double x, double y, double z = 0)
			{
				return graphPoints.Contains(new Point(x, y, z));
			}

			public Resolver GetPointsOnXLine(double y, double z = 0)
			{
				return new Resolver(graphPoints.Where(s => s.Y == y && s.Z == z).ToList());
			}

			public Resolver GetPointsOnYLine(double x, double z = 0)
			{
				return new Resolver(graphPoints.Where(s => s.X == x && s.Z == z).ToList());
			}

			public Resolver GetPointsOnZLine(double x, double y)
			{
				return new Resolver(graphPoints.Where(s => s.X == x && s.Y == y).ToList());
			}

			public Resolver GetPointsOnXYPlane(double z)
			{
				return new Resolver(graphPoints.Where(s => s.Z == z).ToList());
			}

			public Resolver GetPointsOnXZPlane(double y)
			{
				return new Resolver(graphPoints.Where(s => s.Y == y).ToList());
			}

			public Resolver GetPointsOnYZPlane(double x)
			{
				return new Resolver(graphPoints.Where(s => s.X == x).ToList());
			}

			public IEnumerator<Point> GetEnumerator()
			{
				return graphPoints.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
