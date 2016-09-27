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
    }

    public void CountYourStats()
    {
        //caounting heal, damage and fullLife.
        // probably this is good place to set appearance with sprites
    }

    public void Heal()
    {
        leftLife = leftLife + heal;
    }

    public void SetEmotion (int[] percents)
    {
        chanceAtt = percents[0];
        chanceDef = percents[1];
        chanceHeal = percents[2];
    }*/
    
    void Update ()
    {
	
	}

    public void CountYourStats()
    {
        //appearance
        //head = Random(1, 5); like this...
        ///
        fullLife = str * 40;
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

    public void Attack(int dmg)
    {
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
    }

    public void EvadeBlock(int dmg)
    {
        //how th eevasion will be calculated? damage = 0? or stats?
        /*if (mydex > enemydex)
        {
            //damageforme = 0;
        }*/
    }

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

    public void HealMove(int dmg)
    {
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
    }

    public int ChooseWhatToDo()
    {
        //choose random using chanses...
        return 1; //dabar visada puls
    }
}
