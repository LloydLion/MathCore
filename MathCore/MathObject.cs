using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace MathCore
{
	public abstract class MathObject
	{
		private readonly MathPropertiesStore store;
		private readonly MathProperty[] properties;
		private bool autoInit = true;


		protected MathSystem System { get; private set; }

		protected Dictionary<MathProperty, MathObject> DefaultValues { get; } = new Dictionary<MathProperty, MathObject>();


		public MathObject(params MathProperty[] properties)
		{
			this.properties = properties;
			store = new MathPropertiesStore(properties);
		}


		public void InitValues()
		{
			if (!autoInit) return;
			foreach (var prop in properties)
			{
				if (DefaultValues.ContainsKey(prop))
				{
					if (DefaultValues[prop].GetType() == prop.StoreType)
						store.OverrideGetMethod(prop, () => DefaultValues[prop]);
					else throw new InvalidCastException($"Can't apply object of type {DefaultValues[prop].GetType().FullName} to {prop.StoreType.FullName} property");
				}
				else
				{
					store.OverrideGetMethod(prop, () => null);
				}
			}
		}

		internal void SetMathSystem(MathSystem system)
		{
			System = system;
		}

		public T GetPropertyValue<T>(MathProperty<T> property) where T : MathObject
		{
			return store.GetValue(property);
		}

		public IReadOnlyCollection<MathProperty> GetProperties() => properties;

		public void SetGetMethod<T>(MathProperty<T> property, Func<T> method) where T : MathObject
		{
			if (store.GetGetMethod(property) == null)
				store.OverrideGetMethod(property, method);
			else
				throw new InvalidOperationException("Get method has already setted for given property");
		}
	}
}
