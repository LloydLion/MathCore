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
		public Predicate<GraphPoint> PointSelector { get; }

		public double SampleCount { get; }


		public Graph(Predicate<GraphPoint> pointSelector, double sampleCount)
		{
			PointSelector = pointSelector;
			SampleCount = sampleCount;
		}


		public Task<GraphResolver> Build3D(GraphArea graphArea, IProgress<double> progress)
		{
			return Task.Run(() =>
			{
				var xScale = graphArea.EndPoint.X - graphArea.StartPoint.X;
				var yScale = graphArea.EndPoint.Y - graphArea.StartPoint.Y;
				var zScale = graphArea.EndPoint.Z - graphArea.StartPoint.Z;

				var list = new List<GraphPoint>();

				var totalCycles = Math.Pow(SampleCount, 3);
				var currentIterations = 0;
				progress.Report(currentIterations);

				for (double x = graphArea.StartPoint.X; x < graphArea.EndPoint.X; x += 1 / (double)SampleCount * xScale)
					for (double y = graphArea.StartPoint.Y; y < graphArea.StartPoint.Y; y += 1 / (double)SampleCount * yScale)
						for (double z = graphArea.StartPoint.Z; z < graphArea.StartPoint.Z; z += 1 / (double)SampleCount * zScale)
						{
							var point = new GraphPoint() { X = x, Y = y, Z = z };

							if (PointSelector.Invoke(point))
							{
								list.Add(point);
							}

							currentIterations++;
							progress.Report(currentIterations);
						}

				return new GraphResolver(list);
			});
		}

		public Task<GraphResolver> Build2D(GraphArea graphArea, IProgress<double> progress)
		{
			var start = graphArea.StartPoint; var end = graphArea.EndPoint;
			start.Z = end.Z = 0;
			graphArea.StartPoint = start;
			graphArea.EndPoint = end;

			return Build3D(graphArea, progress);
		}


		public struct GraphPoint
		{
			public GraphPoint(double x, double y, double z)
			{
				X = x;
				Y = y;
				Z = z;
			}


			public double X { get; set; }

			public double Y { get; set; }

			public double Z { get; set; }
		}

		public struct GraphArea
		{
			public GraphPoint StartPoint { get; set; }

			public GraphPoint EndPoint { get; set; }
		}

		public class GraphResolver : IEnumerable<GraphPoint>
		{
			private readonly IReadOnlyList<GraphPoint> graphPoints;


			public GraphResolver(IReadOnlyList<GraphPoint> graphPoints)
			{
				this.graphPoints = graphPoints;
			}


			public bool HasPointAt(double x, double y, double z = 0)
			{
				return graphPoints.Contains(new GraphPoint(x, y, z));
			}

			public GraphResolver GetPointsOnXLine(double y, double z = 0)
			{ 
				return new GraphResolver(graphPoints.Where(s => s.Y == y && s.Z == z).ToList());
			}

			public GraphResolver GetPointsOnYLine(double x, double z = 0)
			{
				return new GraphResolver(graphPoints.Where(s => s.X == x && s.Z == z).ToList());
			}

			public GraphResolver GetPointsOnZLine(double x, double y)
			{
				return new GraphResolver(graphPoints.Where(s => s.X == x && s.Y == y).ToList());
			}

			public GraphResolver GetPointsOnXYPlane(double z)
			{
				return new GraphResolver(graphPoints.Where(s => s.Z == z).ToList());
			}

			public GraphResolver GetPointsOnXZPlane(double y)
			{
				return new GraphResolver(graphPoints.Where(s => s.Y == y).ToList());
			}

			public GraphResolver GetPointsOnYZPlane(double x)
			{
				return new GraphResolver(graphPoints.Where(s => s.X == x).ToList());
			}

			public IEnumerator<GraphPoint> GetEnumerator()
			{
				return graphPoints.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
