using MathCore.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Objects
{
	public class Multitude : MathObject, IEnumerable<MathObject>
	{
		private IEnumerable<MathObject> objects;


		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<MathObject> GetEnumerator()
		{
			return objects.GetEnumerator();
		}

		public Multitude InitObject(IEnumerable<MathObject> objects)
		{
			this.objects = objects;
			return this;
		}

		public Multitude InitObject(MathObject startObj, Func<MathObject, int, MathObject> transitRule)
		{
			objects = new InfinitySequence(startObj, transitRule);
			return this;
		}
	}
}
