using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard
{
    /// <summary>
    /// Standardowa czysto kodowa strategia ataku przebijającego pancerz (Armor Piercing Attack).
    /// Ignotuje 50% pancerza celu.
    /// </summary>
    public class ArmorPiercingStrategy : IStrategy<DamageContext>
    {
        /// <inheritdoc />
        public void Execute(DamageContext context)
        {
            var effectiveArmor = context.TargetArmor * 0.5f;
            var damage = context.BaseDamage - effectiveArmor;
            if (damage < 0) damage = 0;
            
            var message = $"[Standard ArmorPiercing] Piercing attack dealt {damage} damage (Effective Armor: {effectiveArmor}).";
            Debug.Log(message);
        }
    }
}
