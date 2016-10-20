using UnityEngine;
using System.Collections;
using Assets.Scripts.Characters;
using System;
using UnityEngine.SceneManagement;

public class CharScript : MonoBehaviour, CharacterInterface
{
    public Canvas controlls;
    public int exp;
    public int distributionPoints;
    //public int lastMoveDMG;
    public bool turn = true;
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

    public int ChanceAtt
    {
        get
        {
            return chanceAtt;
        }

        set
        {
            this.chanceAtt = value;
        }
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

    public void AnimAttack()
    {
        throw new NotImplementedException();
    }

    public void AnimDeath()
    {
        throw new NotImplementedException();
    }

    public void AnimDeff()
    {
        throw new NotImplementedException();
    }

    public void AnimHeal()
    {
        throw new NotImplementedException();
    }

    public void AnimHit()
    {
        throw new NotImplementedException();
    }

    public void AnimIdle()
    {
        throw new NotImplementedException();
    }

    public void AnimWalk()
    {
        throw new NotImplementedException();
    }

    public void Attack(int dmg)
    {
        Debug.Log("Player attacks: " + damage);
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
        Debug.Log("Player attacks: " + damage);
    }

    public void LevelUp() //do I need this shieettt?
    {
        lvl = exp / 100; //ROUND UP CALCULATIONS!!!
        distributionPoints++;
    }

    public void CountYourStats() //this shuuld be called at Start()...
    {
        //lvl = exp / 100; //auto lvlup... //ROUND UP CALCULATIONS!!!
        lvl = 5;
        if (lvl == 1) //give starting points
        {
            distributionPoints = 6;
            switch (proffession)
            {
                case 1:
                    str = 10;//warrior
                    intel = 6;
                    dex = 6;
                    break;
                case 2:
                    str = 6;//wizzard
                    intel = 10;
                    dex = 6;
                    break;
                case 3:
                    str = 6;//rogue
                    intel = 6;
                    dex = 10;
                    break;
            }
            fullLife = lvl * 20 + str * 10;
        }
        proffession = 1;
        if (lvl == 5) //give starting points
        {
            distributionPoints = 6;
            switch (proffession)
            {
                case 1:
                    str = 10;//warrior
                    intel = 6;
                    dex = 6;
                    break;
                case 2:
                    str = 6;//wizzard
                    intel = 10;
                    dex = 6;
                    break;
                case 3:
                    str = 6;//rogue
                    intel = 6;
                    dex = 10;
                    break;
            }
            fullLife = lvl * 20 + str * 10;
        }
        //points to distribute = lvl... 1lvl = 1point
    }

    public void EvadeBlock(int dmg)
    {
        damage = 0;
        Debug.Log("Player evades.");
        //how th eevasion will be calculated? damage = 0? or stats?
        /*if (mydex > enemydex)
        {
            //damageforme = 0;
        }*/
    }

    public void SetAppearance()
    {
        //set it somehow...
    }

    public void SetEmotion(int[] percents)
    {
        chanceAtt = percents[0];
        ChanceDef = percents[1];
        chanceAtt = percents[2];
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public void HealMove(int dmg)
    {
        damage = 0;
        Debug.Log("Players left life: " + leftLife);
        Debug.Log("Player heals.");
        switch (proffession) //ar reik skirstyti? //ROUND UP CALCULATIONS!!!
        {
            case 1:
                leftLife = (fullLife * (dex + intel)) / (lvl * 5);//warrior
                break;
            case 2:
                leftLife = (fullLife * (dex + intel)) / (lvl * 10);//wizzard
                break;
            case 3:
                leftLife = (fullLife * (dex + intel)) / (lvl * 10);//rogue
                break;
        }
        
        if (leftLife > fullLife)
        {
            leftLife = fullLife;
        }

        leftLife = leftLife - dmg;
        Debug.Log("Players left life: " + leftLife);
    }

    public int ChooseWhatToDo()
    {
        //zaidejas!!!!!!!!!!!!!!!!!!!!! choosinasiiii!
        return 1; //dabar visada puls
    }

    public void SpawnControlls()
    {
        Instantiate(controlls);
        //change coord?
    }

    public void CloseControlls()
    {
        Destroy(controlls);
    }

    public void GetDMG (int dmg)
    {
        Debug.Log("Players left life: " + leftLife);
        Debug.Log("Players gains dmg: " + dmg);
        leftLife = leftLife - dmg;
        if (leftLife < 1)
        {
            SceneManager.LoadScene(0);
        }
        Debug.Log("Players left life: " + leftLife);
    }
}
