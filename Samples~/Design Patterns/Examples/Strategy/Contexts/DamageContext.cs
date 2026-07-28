namespace PawBab.DesignPatterns.Examples.Strategy.Contexts
{
    /// <summary>
    /// Obiekt DTO przekazujący kontekst zadawanych obrażeń do strategii walki.
    /// </summary>
    public class DamageContext
    {
        /// <summary>
        /// Bazowa wartość obrażeń przed przeliczeniem przez strategię.
        /// </summary>
        public float BaseDamage { get; }

        /// <summary>
        /// Pancerz celu redukujący obrażenia.
        /// </summary>
        public float TargetArmor { get; }

        /// <summary>
        /// Tworzy instancję kontekstu zadawanych obrażeń.
        /// </summary>
        /// <param name="baseDamage">Bazowe obrażenia.</param>
        /// <param name="targetArmor">Pancerz celu.</param>
        public DamageContext(float baseDamage, float targetArmor)
        {
            BaseDamage = baseDamage;
            TargetArmor = targetArmor;
        }
    }
}
