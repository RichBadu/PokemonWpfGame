namespace Wel.Battle.Game.Wpf.Classes.cs
{
    public class Attack
    {
        #region properties
        public string AttackName { get; set; }
        public TypeOfPokemonOrAttack AttackType { get; set;}
        public double Damage { get; set; }
        public int Defense { get; set; }
        #endregion

        #region constructs

        public Attack (string attackName, TypeOfPokemonOrAttack type, double damage, int defense)

        {
            AttackName = attackName;
            AttackType = type;
            Damage = damage; 
            Defense = defense;
        }

        #endregion

        #region public methodes

        public override string ToString()
        {
            return $"Damage dealt: {Damage}\nHealth gained {Defense}\n ";
        }

        public double CalculateDamage(Pokemon pokemon)
        {
            double effectiveness = 1.0;
            
            if(AttackType.HasAdvantageAgainst(pokemon.PokemonType))
            {
                effectiveness = 2.0;
            }
            else if (AttackType.HasDisadvantageAgainst(pokemon.PokemonType))
            {
                effectiveness = 0.5;
            }

            double damage = Damage * effectiveness;

            if (damage < 0) { damage = 0; }

            return damage;

        }
        #endregion

        
    }
}
