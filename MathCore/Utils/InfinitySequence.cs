using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathCore.Utils
{
	class InfinitySequence : IEnumerable<MathObject>
	{
		private readonly MathObject startObj;
		private readonly Func<MathObject, int, MathObject> transitRule;


		public InfinitySequence(MathObject startObj, Func<MathObject, int, MathObject> transitRule)
		{
			this.startObj = startObj;
			this.transitRule = transitRule;
		}


		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<MathObject> GetEnumerator()
		{
			return new InfinitySequenceEnumerator(this);
		}


		class InfinitySequenceEnumerator : IEnumerator<MathObject>
		{
			private readonly MathObject startObj;
			private readonly Func<MathObject, int, MathObject> transitRule;
			private int iteration = -1;


			public InfinitySequenceEnumerator(InfinitySequence seq)
			{
				this.startObj = seq.startObj;
				this.transitRule = seq.transitRule;
			}

			public MathObject Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose()
			{

			}

			public bool MoveNext()
			{
				Current = ++iteration == -1 ? startObj : transitRule(Current, iteration);
				return true;
			}

			public void Reset()
			{
				Current = startObj;
			}
		}
	}
}
