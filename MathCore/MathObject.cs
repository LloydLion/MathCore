using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathCore
{
	public class MathObject<T> : MathObjectPresentation<T> where T : MathObjectType, new()
	{
		private readonly object iface;


		public MathObject()
		{
			iface = Type.GetDefaultInterface();
		}

		public MathObject(object iface)
		{
			this.iface = iface;
		}


		public override object GetInterfaceObject() => iface;
	}
}
