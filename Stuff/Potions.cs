

	class Potions
	{
		private int amount = 0;
		
		public void ApplyPotion(string potion, int size, int _amount)
		{
			switch (potion)
			{
				case "hp":
					_amount = PotionSize(size);
					break;
				case "mp":
					_amount = PotionSize(size);
					break;
				default:
					break;
			}
			return;
		}
		
		public int PotionSize(int size)
		{
			switch (size)
			{
				case 0:
					amount = 5;
					break;
				case 1:
					amount = 10;
					break;
				case 2:
					amount = 20;
					break;
				case 3:
					amount = 30;
					break;
				case 4:
					amount = 50;
					break;
				case 5:		
					amount = 75;
					break;
				default:
					break;
			}
			return amount;
		}
	}