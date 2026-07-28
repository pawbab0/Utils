using UnityEngine;

namespace PawBab.DesignPatterns.Strategy.Abstracts
{
    /// <summary>
    /// Abstrakcyjna klasa bazowa dla strategii opartych o <see cref="ScriptableObject"/>.
    /// Pozwala na tworzenie strategii parametryzowalnych z poziomu Inspektora Unity (Data-Driven Strategy).
    /// </summary>
    /// <typeparam name="TContext">Typ kontekstu z danymi wejściowymi dla strategii.</typeparam>
    public abstract class ScriptableStrategy<TContext> : ScriptableObject, IStrategy<TContext>
    {
        /// <summary>
        /// Wykonuje logikę danej strategii z wykorzystaniem parametrów skonfigurowanych w Inspektorze.
        /// </summary>
        /// <param name="context">Obiekt kontekstu z danymi operacji.</param>
        public abstract void Execute(TContext context);
    }
}
