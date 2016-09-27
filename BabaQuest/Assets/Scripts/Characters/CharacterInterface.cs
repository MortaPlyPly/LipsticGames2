using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Characters
{
    interface CharacterInterface
    {
        int Lvl { get; set; }
        int Intel { get; set; }
        int Dex { get; set; }
        int Str { get; set; }
        int Proffession { get; set; }
        int FullLife { get; set; }
        int LeftLife { get; set; }
        int Damage { get; set; }
        int Heal { get; set; }
        int ChanceAtt { get; set; }
        int ChanceDef { get; set; }
        int ChanceHeal { get; set; }
        ////// APPEARANCE FOR TYPE 1
        int Head { get; set; }
        int Hair { get; set; }
        int Skin { get; set; }
        int Eyes { get; set; }
        int Clothes { get; set; }

        //void Start(); //set all stats, count hp, att & other, set speed to walk waitforseconds
        void CountYourStats(); //this can be used in Start() o recalculate players character
        void SetAppearance(); //set sprites & if needed other things for animations
        void SetEmotion(int[] percents); //this is called in every turn
        void Attack(); //count if needed attack damage
        void HealMove(); //count if needed heal
        void EvadeBlock(); //evade, block algorythm
        //////////
        void AnimWalk(); //animations.. do i need them in this manner?
        void AnimIdle();
        void AnimAttack();
        void AnimHeal();
        void AnimDeff();
        void AnimHit();
        void AnimDeath();
    }
}
