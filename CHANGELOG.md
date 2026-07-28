# Changelog
Wszystkie zmiany w tej paczce będą dokumentowane w tym pliku.


## [0.1.1] - 2026-07-28

### Added
- Dodano system **Strategia (Strategy Pattern)** w przestrzeni nazw `PawBab.DesignPatterns.Strategy.Abstracts`.
- Wdrożono podwójny wariant wzorca Strategia:
  - **Bez ScriptableObject (Standard C#)** (`IStrategy<TContext>`) — dla obliczeń logicznych i wyliczeń kodowych.
  - **Ze ScriptableObject (Data-Driven)** (`ScriptableStrategy<TContext>`) — dla konfiguracji strategii przez Game Designerów w Inspektorze Unity.
- Dodano wyczerpującą dokumentację `README.md` dla systemu Strategii.
- Dodano przykłady użycia w katalogu `Samples~/Design Patterns/Examples/Strategy` (`DamageContext`, `MeleeAttackStrategy`, `ArmorPiercingStrategy`, `FireAttackStrategy`, `CombatController`).


## [0.1.0] - 2026-02-20

### This is the first release of *PawBab - Utils*.
