using System.Collections.Generic;

namespace Wel.Battle.Game.Wpf.Classes.cs
{
    public class TypeOfPokemonOrAttack
    {
        public string TypeName { get; set; }
        public List<string> Advantages { get; set; }
        public List<string> Disadvantages { get; set; }

        public TypeOfPokemonOrAttack(string type, List<string> advantages, List<string> disadvantages)
        { 
            TypeName = type;
            Advantages = advantages;
            Disadvantages = disadvantages;
        }

        public override string ToString()
        {
            return $"{TypeName}";
        }

        public bool HasAdvantageAgainst(TypeOfPokemonOrAttack targetType)
        {
            return Advantages.Contains(targetType.TypeName);
        }

        public bool HasDisadvantageAgainst(TypeOfPokemonOrAttack targetType)
        {
            return Disadvantages.Contains(targetType.TypeName);
        }
    }
}
