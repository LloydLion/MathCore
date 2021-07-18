using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MathCore
{
	public class InfinitySequence<T> : IEnumerable<T>
	{
		public Func<T, BigInteger, T> TransitRule { get; }

		public T StartElement { get; }


		public InfinitySequence(Func<T, BigInteger, T> transitRule, T start)
		{
			TransitRule = transitRule;
			StartElement = start;
		}

		public InfinitySequence(Func<T, T> transitRule, T start) : this((t, i) => transitRule(t), start) { }

		public InfinitySequence(Func<BigInteger, T> transitRule, T start) : this((t, i) => transitRule(i), start) { }


		public IEnumerator<T> GetEnumerator() => new InfinitySequenceEnumerator(TransitRule, StartElement);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


		private class InfinitySequenceEnumerator : IEnumerator<T>
		{
			private BigInteger index;
			private readonly Func<T, BigInteger, T> transitRule;
			private readonly T start;


			public InfinitySequenceEnumerator(Func<T, BigInteger, T> transitRule, T start)
			{
				this.transitRule = transitRule;
				this.start = start;
				Reset();
			}


			public T Current { get; private set; }

			object IEnumerator.Current => Current;

			public void Dispose() { }

			public bool MoveNext()
			{
				index++;
				if(index == 0) Current = start;
				else Current = transitRule(Current, index);
				return true;
			}

			public void Reset()
			{
				index = -1;
				Current = default;
			}
		}
	}
}
