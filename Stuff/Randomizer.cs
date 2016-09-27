using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
	public class Randomizer
	{
		/*private int low;
		private int medium;
		private int high;*/
		private int val;
		Random rand = new Random();
		
		
		public int low()		//[1..20]
		{
			return rand.Next(1, 21);
		}

		public int medium()		//[20..50]
		{
			return rand.Next(20, 51);
		}

		public int high()		//[50..100]
		{
			return rand.Next(50, 101);
		}

		public Randomizer(string choice)
		{
			if (choice == "low")
				val = low();
			else if (choice == "medium")
				val = medium();
			else
				val = high();
		}
	}
}
