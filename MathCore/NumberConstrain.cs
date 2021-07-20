using System;
using System.Collections.Generic;
using System.Text;

namespace MathCore
{
	public enum NumberConstrain
	{
		None		= 0b00000000,
		Integer		= 0b10000000,
		Even		= 0b10010000,
		Odd			= 0b10001000,
		NoNegative	= 0b01000000,
		NoZero		= 0b00100000,

		//Auto implements
		Positive = 0b01100000
	}
}
