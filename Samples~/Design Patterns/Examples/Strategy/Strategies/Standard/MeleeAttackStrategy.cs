using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard
{
    /// <summary>
    /// Standardowa czysto kodowa strategia ataku bezpośredniego (Melee Attack).
    /// </summary>
    public class MeleeAttackStrategy : IStrategy<DamageContext>
    {
        /// <inheritdoc />
        public void Execute(DamageContext context)
        {
            var damage = context.BaseDamage - context.TargetArmor;
            if (damage < 0) damage = 0;
            
            var message = $"[Standard Melee] Attack dealt {damage} damage.";
            Debug.Log(message);
        }
    }
}
