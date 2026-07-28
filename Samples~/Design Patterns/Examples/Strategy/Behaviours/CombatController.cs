using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;
using PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard;

namespace PawBab.DesignPatterns.Examples.Strategy.Behaviours
{
    /// <summary>
    /// Komponent demonstracyjny łączący oraz prezentujący użycie strategii standardowych (C#)
    /// oraz strategii opartych o ScriptableObject.
    /// </summary>
    public class CombatController : MonoBehaviour
    {
        [Header("Scriptable Object Strategy (Designer driven)")]
        [SerializeField] private ScriptableStrategy<DamageContext> _scriptableStrategy;
        
        // Strategia przypisywana z poziomu kodu
        private IStrategy<DamageContext> _standardStrategy;

        private void Start()
        {
            // Domyślna inicjalizacja strategii standardowej
            _standardStrategy = new MeleeAttackStrategy();
        }

        /// <summary>
        /// Zmienia aktualną strategię kodową w locie.
        /// </summary>
        /// <param name="newStrategy">Nowa strategia.</param>
        public void ChangeStandardStrategy(IStrategy<DamageContext> newStrategy)
        {
            if (newStrategy == null) return;
            _standardStrategy = newStrategy;
        }

        /// <summary>
        /// Wykonuje ataki przy użyciu obu zarejestrowanych strategii.
        /// </summary>
        public void PerformAttacks()
        {
            var context = new DamageContext(20f, 5f);
            
            // Wykonanie strategii z kodu
            _standardStrategy?.Execute(context);
            
            // Wykonanie strategii z Inspektora Unity
            _scriptableStrategy?.Execute(context);
        }
    }
}
