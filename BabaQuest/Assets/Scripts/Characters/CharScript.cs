﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Characters;
using System;

public class CharScript : MonoBehaviour, CharacterInterface
{
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

    public void Attack()
    {
        //do counting
        throw new NotImplementedException();
    }

    public void CountYourStats()
    {
        throw new NotImplementedException();
    }

    public void EvadeBlock()
    {
        throw new NotImplementedException();
    }

    public void SetAppearance()
    {
        throw new NotImplementedException();
    }

    public void SetEmotion(int[] percents)
    {
        throw new NotImplementedException();
    }

    void Start ()
    {
	
	}

	void Update ()
    {
	
	}

    public void HealMove()
    {
        throw new NotImplementedException();
    }
}
