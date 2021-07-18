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
	public class FunctionTest
	{
		[TestMethod]
		public void BasicTest()
		{
			var func = new Function(new Type[] { typeof(string), typeof(object[]) }, new Type[] { typeof(string) }, TestDelegate);

			var res = (string)func.InvokeSingle("Hello {0}! Lorem {1} {2}...... Our site {3}", new object[] { "Gevery", "Impsamp", -1295, "https://NONONONO" });
			Assert.AreEqual(res, "Hello Gevery! Lorem Impsamp -1295...... Our site https://NONONONO");
		}

		[TestMethod]
		public void Exceptions()
		{
			var func1 = new Function(new Type[] { typeof(string), typeof(object[]) }, new Type[] { typeof(string) }, TestDelegate);

			var func2 = new Function(new Type[] { typeof(string), typeof(object[]) }, new Type[] { typeof(int) }, TestDelegate);
			var func3 = new Function(new Type[] { typeof(string), typeof(object[]) }, new Type[] { typeof(string), typeof(int) }, TestDelegate);

			Assert.ThrowsException<ArgumentException>(() => func1.Invoke(321, "dsadsa", new object())); // error by count
			Assert.ThrowsException<ArgumentException>(() => func1.Invoke(321, new object())); // error by types

			//Invalid delegete

			Assert.ThrowsException<Exception>(() => func2.Invoke("dsa", Array.Empty<object>())); // error by types
			Assert.ThrowsException<Exception>(() => func3.Invoke("dsa", Array.Empty<object>())); // error by count
		}

		private static IReadOnlyList<object> TestDelegate(IReadOnlyList<object> args)
		{
			var str = (string)args[0];
			var format = (object[])args[1];

			return new object[] { string.Format(str, format) };
		}
	}
}
