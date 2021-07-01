using MathCore;
using MathCore.Aliases;
using MathCore.Types;
using NUnit.Framework;

namespace TestProject
{
	public class BaseTest
	{

		[Test]
		public void Test1()
		{
			var number = new MNumberObj(4m);
			int num = (int)(decimal)number.GetOperation<MNumber, MNumber, MNumber>("+").Calculate(number, number).GetInterfaceObject();
			Assert.AreEqual(num, 8m);
		}
	}
}