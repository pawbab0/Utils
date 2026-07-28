# Strategia (Strategy Pattern)

**Wzorzec Strategia (Strategy)** to behawioralny wzorzec projektowy, który definiuje rodzinę wymiennych algorytmów, pakuje każdy z nich w osobną klasę i sprawia, że są one ze sobą w pełni wymienne. W kontekście Unity wzorzec ten pozwala elastycznie zmieniać zachowanie obiektów w czasie rzeczywistym (runtime) lub podmieniać parametryzowalne algorytmy bez konieczności modyfikowania kaskady instrukcji warunkowych (`if` / `switch`).

W tym systemie wzorzec Strategia można zaimplementować na **dwa sposoby**, z których każdy rozwiązuje inne problemy architektoniczne. Obie wersje współdzielą ten sam bazowy interfejs oraz model danych (kontekst):

1. **Bez ScriptableObject (Standardowe klasy C#)** — podejście idealne dla czystej logiki kodowej i wyliczeń matematycznych, gdy nie zachodzi potrzeba edycji wartości w Inspektorze Unity.
2. **Ze ScriptableObject (Podejście Data-Driven)** — dodaje warstwę modyfikowalną przez projektantów gry (Game Designerów), pozwalając na tworzenie wariantów strategii jako assetów `.asset` i konfigurację parametrów w Inspektorze Unity.

---

## Architektura i Warstwy Abstrakcji

### 1. Interfejs Bazowy (`IStrategy<TContext>`)

Katalog: `Abstracts/IStrategy.cs`

Wszystkie strategie (zarówno kodowe, jak i oparte o `ScriptableObject`) implementują generyczny interfejs `IStrategy<TContext>`:

```csharp
namespace PawBab.DesignPatterns.Strategy.Abstracts
{
    public interface IStrategy<TContext>
    {
        void Execute(TContext context);
    }
}
```

### 2. Baza dla ScriptableObject (`ScriptableStrategy<TContext>`)

Katalog: `Abstracts/ScriptableStrategy.cs`

Ścieżka pośrednia pozwalająca klasom dziedziczącym działać jako pliki assetów w Unity, jednocześnie zachowując kontrakt `IStrategy<TContext>`:

```csharp
using UnityEngine;

namespace PawBab.DesignPatterns.Strategy.Abstracts
{
    public abstract class ScriptableStrategy<TContext> : ScriptableObject, IStrategy<TContext>
    {
        public abstract void Execute(TContext context);
    }
}
```

---

## Przykłady Użycia

Poniższe przykłady demonstracyjne znajdują się w folderze `Samples~/Design Patterns/Examples/Strategy`.

### 1. Kontekst Danych (`DamageContext`)

Wzorzec strategii przekazuje dane operacyjne za pośrednictwem kontenera kontekstu:

```csharp
namespace PawBab.DesignPatterns.Examples.Strategy.Contexts
{
    public class DamageContext
    {
        public float BaseDamage { get; }
        public float TargetArmor { get; }

        public DamageContext(float baseDamage, float targetArmor)
        {
            BaseDamage = baseDamage;
            TargetArmor = targetArmor;
        }
    }
}
```

---

### Wersja 1: Strategia Czysto Kodowa (Standard C#)

Wariant ten tworzony jest dynamicznie w kodzie (np. poprzez fabrykę lub wprost słowem kluczowym `new`).

#### Atak Zwykły (`MeleeAttackStrategy`)

```csharp
using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard
{
    public class MeleeAttackStrategy : IStrategy<DamageContext>
    {
        public void Execute(DamageContext context)
        {
            var damage = context.BaseDamage - context.TargetArmor;
            if (damage < 0) damage = 0;
            
            Debug.Log($"Standard attack dealt {damage} damage.");
        }
    }
}
```

#### Atak Przebijający Pancerz (`ArmorPiercingStrategy`)

```csharp
using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard
{
    public class ArmorPiercingStrategy : IStrategy<DamageContext>
    {
        public void Execute(DamageContext context)
        {
            var effectiveArmor = context.TargetArmor * 0.5f;
            var damage = context.BaseDamage - effectiveArmor;
            if (damage < 0) damage = 0;
            
            Debug.Log($"Piercing attack dealt {damage} damage.");
        }
    }
}
```

---

### Wersja 2: Strategia Data-Driven (ScriptableObject)

Pozwala Game Designerom tworzyć nowe warianty ataku w oknie `Project` (Menu: `Create -> Design Patterns -> Strategy -> Combat -> Fire Attack`) oraz regulować ich pola w Inspektorze Unity.

```csharp
using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;

namespace PawBab.DesignPatterns.Examples.Strategy.Strategies.Scriptable
{
    [CreateAssetMenu(fileName = "FireAttackStrategy", menuName = "PawBab/Design Patterns/Strategy/Combat/Fire Attack")]
    public class FireAttackStrategy : ScriptableStrategy<DamageContext>
    {
        [SerializeField] private float _burnDamage = 5f;

        public override void Execute(DamageContext context)
        {
            var totalDamage = context.BaseDamage + _burnDamage;
            if (context.TargetArmor > 10f) totalDamage -= 2f;
            
            Debug.Log($"Fire attack dealt {totalDamage} damage (including {_burnDamage} burn damage).");
        }
    }
}
```

---

### Integracja w Komponencie (`CombatController`)

Komponent prezentuje swobodne stosowanie obu wariantów w jednej klasie:

```csharp
using UnityEngine;
using PawBab.DesignPatterns.Strategy.Abstracts;
using PawBab.DesignPatterns.Examples.Strategy.Contexts;
using PawBab.DesignPatterns.Examples.Strategy.Strategies.Standard;

namespace PawBab.DesignPatterns.Examples.Strategy.Behaviours
{
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

        public void ChangeStandardStrategy(IStrategy<DamageContext> newStrategy)
        {
            if (newStrategy == null) return;
            _standardStrategy = newStrategy;
        }

        public void PerformAttacks()
        {
            var context = new DamageContext(20f, 5f);
            
            // Wykonanie strategii kodowej
            _standardStrategy?.Execute(context);
            
            // Wykonanie strategii z Inspektora Unity
            _scriptableStrategy?.Execute(context);
        }
    }
}
```

---

## Kiedy stosować które rozwiązanie?

| Cecha / Kryterium | Bez ScriptableObject (Standard C#) | Ze ScriptableObject (Data-Driven) |
| :--- | :--- | :--- |
| **Główne przeznaczenie** | Algorytmy czysto kodowe, wyliczenia matematyczne, sortowanie danych. | Algorytmy potrzebujące zmiennych konfigurowanych przez Game Designerów. |
| **Modyfikacja parametrów** | Wyłącznie w kodzie C# lub przez konstruktor. | Bezkodowa w Inspektorze Unity (tworzenie plików `.asset`). |
| **Dynamiczne instancjonowanie** | Bardzo łatwe (`new Strategy()`, DI, Fabryka). | Wymaga referencji do assetu lub `ScriptableObject.Instantiate`. |
| **Narzut pamięciowy** | Znikomy (brak assetów Unity). | Mały obiekt assetu w projekcie. |
| **Referencje do prefabów/efektów** | Wymaga przekazania przez kontekst. | Można przypisać bezpośrednio w Inspektorze (np. `ParticleSystem`). |
