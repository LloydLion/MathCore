using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class MathPropertiesStore
	{
		private readonly Dictionary<MathProperty, Func<MathObject>> getFunctions;


		public MathPropertiesStore(params MathProperty[] properties)
		{
			getFunctions = properties.ToDictionary(s => s, s => (Func<MathObject>)null);
		}


		public void OverrideGetMethod<T>(MathProperty<T> property, Func<T> method) where T : MathObject => OverrideGetMethod(property, () => method());

		public void OverrideGetMethod(MathProperty property, Func<MathObject> method)
		{
			getFunctions[property] = method;
		}


		public T GetValue<T>(MathProperty<T> property) where T : MathObject
		{
			if(getFunctions[property] == null)
				throw new NotImplementedException("Get method for this property is not implemented. Use OverrideGetMethod<T>(MathProperty<T>, Func<T>) for fix it");
			return (T)getFunctions[property]();
		}

		public Func<T> GetGetMethod<T>(MathProperty<T> property) where T : MathObject
		{
			return () => (T)getFunctions[property]();
		}
}
