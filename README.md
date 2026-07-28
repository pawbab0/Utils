# PawBab - Utils

**PawBab - Utils** to paczka narzędziowa dla programistów Unity 6, łącząca czyste wzorce projektowe z gotowymi rozwiązaniami architektonicznymi. Pomaga w budowaniu modularnego, elastycznego i skalowalnego kodu gier w środowisku runtime oraz edytorowym.

---

## Główne Systemy i Wzorce Projektowe

Wszystkie wzorce projektowe znajdują się w katalogu sampli `Samples~/Design Patterns`.

### 1. Strategia (Strategy Pattern)
Ścieżka: `Samples~/Design Patterns/Systems/Design Patterns/Strategy`

Pozwala na elastyczną wymianę algorytmów w czasie działania gry. Oferuje dwa warianty implementacji:
- **Standard C# (`IStrategy<TContext>`)**: Czyste strategie kodowe, idealne dla wyliczeń matematycznych i logiki runtime bez edycji z Inspektora.
- **ScriptableObject (`ScriptableStrategy<TContext>`)**: Strategie zasilane danymi (Data-Driven), wyklikane bezpośrednio w Inspektorze Unity jako pliki `.asset`.

### 2. Komenda (Command Pattern)
Ścieżka: `Samples~/Design Patterns/Systems/Design Patterns/Command`

Asynchroniczny system komend z natywnym wsparciem dla Unity 6 (`Awaitable`), obsługą historii cofania działań (Undo), limitem kroków oraz komendami złożonymi (`CompositeCommand`).

### 3. Obserwator (Observer Pattern / Event Bus)
Ścieżka: `Samples~/Design Patterns/Systems/Design Patterns/Observer`

Wydajny, typowany system szyny zdarzeń (`EventBus`, `GlobalEventBus`) oparty na interfejsach `IEvent` oraz `IEventListener<TEvent>`, eliminujący silne powiązania między obiektami.

### 4. Maszyna Stanów (State Machine / FSM)
Ścieżka: `Samples~/Design Patterns/Systems/Design Patterns/State Machine`

Generyczna maszyna stanów (`StateMachine<TOwner>`) obsługująca stany podstawowe (`State<TOwner>`) oraz stany przyjmujące parametry startowe (`IPayloadState<TPayload>`), wraz z cyklem życia `OnEnter`, `Tick`, `FixedTick` oraz `OnExit`.

### 5. Architektura Inicjalizacji (Initiator Pipeline)
Ścieżka: `Samples~/Design Patterns/Systems/Architecture/Initiator`

Rurociąg inicjalizacyjny pozwalający na kontrolowane i sekwencyjne uruchamianie podsystemów gry przy starcie aplikacji.

---

## Struktura Katalogów Paczki

```text
PawBab - Utils/
├── Editor/                    # Narzędzia edytorowe i skrypty Editor Unity
├── Runtime/                   # Komponenty runtime paczki
├── Samples~/                  # Przykłady i wzorce projektowe
│   └── Design Patterns/
│       ├── Systems/           # Główne systemy wzorców (Core)
│       │   ├── Architecture/  # Initiator Pipeline
│       │   └── Design Patterns/# Command, Observer, State Machine, Strategy
│       └── Examples/          # Przykłady użycia w scenach i skryptach
├── CHANGELOG.md               # Historia zmian paczki
├── package.json               # Konfiguracja Unity Package Manager
└── README.md                  # Dokumentacja główna
```

---

## Instalacja

Możesz dodać tę paczkę do projektu Unity 6 poprzez **Unity Package Manager (UPM)**:

1. Otwórz w Unity menu `Window -> Package Manager`.
2. Kliknij ikonę `+` w lewym górnym rogu i wybierz **Add package from git URL...**
3. Wklej URL repozytorium:
   ```text
   https://github.com/pawbab0/Utils.git
   ```
4. Po zaimportowaniu paczki możesz zaimportować przykłady z zakładki `Samples -> Design Patterns`.

---

## Licencja i Autor

- **Autor**: Paweł Babiuch
- **E-mail**: pawel.babiuch0@gmail.com
- **LinkedIn**: [Paweł Babiuch](https://www.linkedin.com/in/pawe%C5%82-babiuch/)
- **Licencja**: Informacje zawarte w pliku `LICENSE`.
