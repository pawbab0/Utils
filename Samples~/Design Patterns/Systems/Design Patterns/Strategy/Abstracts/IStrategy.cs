namespace PawBab.DesignPatterns.Strategy.Abstracts
{
    /// <summary>
    /// Bazowy interfejs dla wszystkich strategii w systemie.
    /// Definiuje spójny kontrakt egzekucji operacji w oparciu o podany kontekst danych.
    /// </summary>
    /// <typeparam name="TContext">Typ kontekstu z danymi wejściowymi dla strategii.</typeparam>
    public interface IStrategy<TContext>
    {
        /// <summary>
        /// Wykonuje logikę danej strategii na podstawie dostarczonego kontekstu.
        /// </summary>
        /// <param name="context">Obiekt kontekstu z danymi operacji.</param>
        void Execute(TContext context);
    }
}
