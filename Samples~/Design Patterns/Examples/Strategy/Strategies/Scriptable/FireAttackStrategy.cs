using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Scriptable
{
    /// <summary>
    /// Przykładowa strategia oparta o ScriptableObject dla ataku ognistego (Fire Attack).
    /// Parametryzowalna w Inspektorze Unity przez Game Designerów.
    /// </summary>
    [CreateAssetMenu(fileName = "FireAttackStrategy", menuName = "PawBab/Design Patterns/Strategy/Combat/Fire Attack")]
    public class FireAttackStrategy : ScriptableStrategy<DamageContext>
    {
        [SerializeField] private float _burnDamage = 5f;

        /// <inheritdoc />
        public override void Execute(DamageContext context)
        {
            var totalDamage = context.BaseDamage + _burnDamage;
            if (context.TargetArmor > 10f) totalDamage -= 2f;
            
            var message = $"[Scriptable FireAttack] Fire attack dealt {totalDamage} damage (Burn Damage: {_burnDamage}).";
            Debug.Log(message);
        }
    }
}
