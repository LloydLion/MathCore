using MathCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
	[TestClass]
	public class InfinitySequenceTest
	{
		[TestMethod]
		public void BasicTest()
		{
			var rand1 = new Random(3000);
			var rand2 = new Random(3000);


			var seq = new InfinitySequence<int>((prev, index) => rand1.Next(), rand1.Next());

			int i = 0;
			foreach (var element in seq)
			{
				if (i >= 1000) break;
				
				Assert.AreEqual(rand2.Next(), element);

				i++;
			}
		}

		[TestMethod]
		public void PrevElementArgument()
		{
			var rand = new Random(3000);

			var realPrev = rand.Next();
			var seq = new InfinitySequence<int>((prev, index) =>
			{
				Assert.AreEqual(realPrev, prev);
				realPrev = rand.Next();
				return realPrev;
			}, realPrev);

			int i = 0;
			foreach (var element in seq)
			{
				if (i >= 1000) break;
				i++;
			}
		}

		[TestMethod]
		public void IndexArgument()
		{
			var rand = new Random(3000);

			var realIndex = 1;
			var seq = new InfinitySequence<int>((prev, index) =>
			{
				Assert.AreEqual(realIndex, (int)index);
				realIndex++;
				return rand.Next();
			}, rand.Next());

			int i = 0;
			foreach (var element in seq)
			{
				if (i >= 1000) break;
				i++;
			}
		}
	}
}
