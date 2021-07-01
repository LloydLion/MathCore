using System; 
using System.Collections.Generic;
using System.Text;

namespace MathCore.Types
{
	public class MNumber : MathObjectType
	{
		public override object GetDefaultInterface()
		{
			return (decimal)0;
		}

		public override OperationFactory[] GetDefaultOperations()
		{
			return new OperationFactory[]
			{
				new DelegateOperationFactory<MNumber, MNumber, MNumber>(AddOperation, "add", "+"),
				new DelegateOperationFactory<MNumber, MNumber, MNumber>(SubOperation, "sub", "-"),
				new DelegateOperationFactory<MNumber, MNumber, MNumber>(MultOperation, "mult", "*"),
				new DelegateOperationFactory<MNumber, MNumber, MNumber>(DivineOperation, "divine", "/"),
			};
		}

		public static MathObjectPresentation<MNumber> AddOperation(MathObjectPresentation<MNumber> number1, MathObjectPresentation<MNumber> number2) =>
			new MathObject<MNumber>(((decimal)number1.GetInterfaceObject()) + ((decimal)number2.GetInterfaceObject()));

		public static MathObjectPresentation<MNumber> SubOperation(MathObjectPresentation<MNumber> number1, MathObjectPresentation<MNumber> number2) =>
			new MathObject<MNumber>(((decimal)number1.GetInterfaceObject()) - ((decimal)number2.GetInterfaceObject()));

		public static MathObjectPresentation<MNumber> MultOperation(MathObjectPresentation<MNumber> number1, MathObjectPresentation<MNumber> number2) =>
			new MathObject<MNumber>(((decimal)number1.GetInterfaceObject()) * ((decimal)number2.GetInterfaceObject()));

		public static MathObjectPresentation<MNumber> DivineOperation(MathObjectPresentation<MNumber> number1, MathObjectPresentation<MNumber> number2) =>
			new MathObject<MNumber>(((decimal)number1.GetInterfaceObject()) / ((decimal)number2.GetInterfaceObject()));
	}
}
