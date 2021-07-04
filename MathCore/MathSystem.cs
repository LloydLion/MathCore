using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public class MathSystem
	{
		private readonly List<MathTypeRecord> types = new List<MathTypeRecord>();


		public MathSystem()
		{

		}
		

		public void AddType<T>(MathObjectType type) where T : MathObject, new()
		{
			types.Add(new MathTypeRecord() { MathTypeInfo = type, ObjectType = typeof(T) });
		}

		public T CreateInstance<T>() where T : MathObject, new() => CreateInstance<T>(false);

		public T CreateInstance<T>(bool disableAutoInit) where T : MathObject, new()
		{
			if (types.FindIndex(s => s.ObjectType == typeof(T)) == -1)
				throw new InvalidOperationException(typeof(T).FullName + " is not registed in this system. Use AddType<T>(MathObjectType) to fix it");
			else
			{
				var ret = new T();

				ret.SetMathSystem(this);

				if(!disableAutoInit)
					ret.InitValues();

				return ret;
			}
		}


		struct MathTypeRecord
		{
			public Type ObjectType { get; set; }

			public MathObjectType MathTypeInfo { get; set; }
		}
	}
}
