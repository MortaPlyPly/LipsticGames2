namespace Assets._Scripts_._Character_Types_
{
    interface CharacterTypeInterface
    {
        int LeftLife { get; }
        int FullLife { get; }
        //int: lvl, exp, hp, att, heal, leftLife, fullLife, str, int, dex
        void CalculateStats(int lvl);
        int Attack(); //basically return att
        void Heal(); //heal yourself
        void Walk(); //just for triggering walking animation
        void GetHurt(int dmg);
    }
}
