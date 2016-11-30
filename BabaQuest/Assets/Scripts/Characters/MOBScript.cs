using UnityEngine;
using System.Collections;
using Assets.Scripts.Characters;
using System;

public class MOBScript : MonoBehaviour, CharacterInterface
{
	public int lvlForMOB;
	int lvl;
	int intel;
	int dex;
	int str;
	int proffession;
	int fullLife;
	int leftLife;
	int damage;
	int heal;
	int chanceAtt;
	int chanceDef;
	int chanceHeal;
	////// APPEARANCE FOR TYPE 1
	int head;
	int hair;
	int skin;
	int eyes;
	int clothes;

	#region get & set
	public int ChanceAtt
	{
		get { return chanceAtt; }
		set { this.chanceAtt = value; }
	}

	public int ChanceDef
	{
		get
		{
			return chanceDef;
		}

		set
		{
			this.chanceDef = value;
		}
	}

	public int ChanceHeal
	{
		get
		{
			return chanceHeal;
		}

		set
		{
			this.chanceHeal = value;
		}
	}

	public int Clothes
	{
		get
		{
			return clothes;
		}

		set
		{
			this.clothes = value;
		}
	}

	public int Damage
	{
		get
		{
			return damage;
		}

		set
		{
			this.damage = value;
		}
	}

	public int Dex
	{
		get
		{
			return dex;
		}

		set
		{
			this.dex = value;
		}
	}

	public int Eyes
	{
		get
		{
			return eyes;
		}

		set
		{
			this.eyes = value;
		}
	}

	public int FullLife
	{
		get
		{
			return fullLife;
		}

		set
		{
			this.fullLife = value;
		}
	}

	public int Hair
	{
		get
		{
			return hair;
		}

		set
		{
			this.hair = value;
		}
	}

	public int Head
	{
		get
		{
			return head;
		}

		set
		{
			this.head = value;
		}
	}

	public int Heal
	{
		get
		{
			return heal;
		}

		set
		{
			this.heal = value;
		}
	}

	public int Intel
	{
		get
		{
			return intel;
		}

		set
		{
			this.intel = value;
		}
	}

	public int LeftLife
	{
		get
		{
			return leftLife;
		}

		set
		{
			this.leftLife = value;
		}
	}

	public int Lvl
	{
		get
		{
			return lvl;
		}

		set
		{
			this.lvl = value;
		}
	}

	public int Proffession
	{
		get
		{
			return proffession;
		}

		set
		{
			this.proffession = value;
		}
	}

	public int Skin
	{
		get
		{
			return skin;
		}

		set
		{
			this.skin = value;
		}
	}

	public int Str
	{
		get
		{
			return str;
		}

		set
		{
			this.str = value;
		}
	}
	#endregion

	void Start ()
	{
		//StartCoroutine(MoveABit());
	}

	/*IEnumerator MoveABit()
	{
		float speed = 100f;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * speed * Time.deltaTime;
		yield return new WaitForSeconds(1);
		speed = 0f;
	}*/

			/*spawn mob on right screen side
			 * move from screen side more to the center in 2s, then stop n stay till mob turn
			 */
	/*public void MoveABit()
	{
		float speed = 100f;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * speed * 2000;	//2s
	}*/
	
	void Update ()
	{ }

	public void CountYourStats()
	{
		switch (proffession) //ROUND UP CALCULATIONS!!!
		{
			case 1:
				str = lvlForMOB + 6;//warrior
				intel = lvlForMOB / 2 + 4;
				dex = lvlForMOB / 2 + 4;
				break;
			case 2:
				str = lvlForMOB / 2 + 4;//wizzard
				intel = lvlForMOB + 6;
				dex = lvlForMOB / 2 + 4;
				break;
			case 3:
				str = lvlForMOB / 2 + 4;//rogue
				intel = lvlForMOB / 2 + 4;
				dex = lvlForMOB + 6;
				break;
		}
		fullLife = str * 40;
		//Debug.Log("MOBs full life: " + fullLife);
		leftLife = fullLife;
	}

	public void SetAppearance()
	{
		System.Random rnd = new System.Random();
		hair = rnd.Next(1, 10); //colour
		skin = rnd.Next(1, 10); //colour
		eyes = rnd.Next(1, 10); //colour
		clothes = rnd.Next(1, 7);
		head = rnd.Next(1, 6); //hair style basicaly
		//set it somehow...
	}

	public void SetEmotion(int[] percents)
	{
		chanceAtt = percents[0];
		ChanceDef = percents[1];
		chanceHeal = percents[2];
		//Debug.Log("MOBs chance to attack " + chanceAtt + " to defend " + chanceDef + " to heal " + chanceHeal);
	}

	public void Attack(int dmg)
	{
			//show attack anim -> idel
		leftLife = leftLife - dmg;
		switch (proffession)
		{
			case 1:
				damage = str * 3 + lvl * 5;
				break;
			case 2:
				damage = intel * 2 + str + lvl * 5;
				break;
			case 3:
				damage = str + dex * 2 + lvl * 5;
				break;
		}
		//Debug.Log("MOB attacks by " + damage + "Mob leftlife:" + leftLife);
	}

	/*public void EvadeBlock(int dmg)
	{
		//Debug.Log("MOB blocks.");
		damage = 0;
		//now it just dont let the damage through
		//how th eevasion will be calculated? damage = 0? or stats?
		//if (mydex > enemydex)
		//{
		//	damageforme = 0;
		//}
	}*/

	#region Animations
	public void AnimWalk()
	{
		throw new NotImplementedException();
	}

	public void AnimIdle()
	{
		throw new NotImplementedException();
	}

	public void AnimAttack()
	{
		throw new NotImplementedException();
	}

	public void AnimHeal()
	{
		throw new NotImplementedException();
	}

	public void AnimDeff()
	{
		throw new NotImplementedException();
	}

	public void AnimHit()
	{
		throw new NotImplementedException();
	}

	public void AnimDeath()
	{
		throw new NotImplementedException();
	}
	#endregion

	public void HealMove(int dmg)
	{
			//show heal anim -> idel
		damage = 0;
		switch (proffession) //ar reik skirstyti? //ROUND UP CALCULATIONS!!!
		{
			case 1:
					leftLife = leftLife + (fullLife * (dex + intel)) / (lvl * 20);//warrior
						break;
			case 2:
					leftLife = leftLife + (fullLife * (dex + intel)) / (lvl * 30);//wizzard
					break;
			case 3:
					leftLife = leftLife + (fullLife * (dex + intel)) / (lvl * 30);//rogue
					break;
		}
		
		if (leftLife > fullLife)
		{
			leftLife = fullLife;
		}

		leftLife = leftLife - dmg;
		//Debug.Log("MOB heals. MOBs left life: " + leftLife+ "==" + dmg);
	}

	public int ChooseWhatToDo()
	{
		int choiseForNextMove;
		System.Random rnd = new System.Random();
		int choise = rnd.Next(1, 101);
		if (choise < chanceAtt)
		{
			choiseForNextMove = 1;
		}
		else if (choise < chanceAtt + chanceDef)
		{
			choiseForNextMove = 2;
		}
		else
		{
			choiseForNextMove = 3;
		}
		return choiseForNextMove;
	}
}
