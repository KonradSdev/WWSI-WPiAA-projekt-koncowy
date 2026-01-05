# WWSI-WPiAA-projekt-koncowy
Repozytorium zawierające rozwiązanie zadania z przedmiotu WPiAA

## 📝 Cel Projektu
Projekt realizuje system obsługi zamówień w kawiarni, kładąc nacisk na elastyczność w dodawaniu produktów, dynamiczne modyfikowanie składników oraz automatyczne powiadamianie podsystemów o zmianach statusu. Architektura została oparta na 5 wzorcach projektowych, co eliminuje sztywne zależności (tight coupling) i ułatwia rozbudowę systemu.

---
## 🏗️ Wykorzystane Wzorce Projektowe

### 1. Factory Method (Wzorzec Kreacyjny)
* **Lokalizacja:** `DesignPatterns/Creational` (`ProductFactory`, `CoffeeFactory`, `CakeFactory`, `TeaFactory`).
* **Problem:** System musi tworzyć różne obiekty produktów (Kawa, Ciasto, Herbata) bez znajomości ich konkretnych klas implementacyjnych.
* **Uzasadnienie:** Dzięki zastosowaniu fabryk, interfejs użytkownika (UI) nie musi wiedzieć, jak zainicjalizować konkretny produkt. Dodanie nowej pozycji do menu (np. Matcha) wymaga jedynie stworzenia nowej klasy fabryki, bez modyfikacji istniejącego kodu UI.

### 2. Singleton (Wzorzec Kreacyjny)
* **Lokalizacja:** `DesignPatterns/Creational/OrderManager.cs`.
* **Problem:** Konieczność istnienia jednego, centralnego punktu dostępu do listy wszystkich zamówień, aby uniknąć niespójności danych.
* **Uzasadnienie:** W systemie kawiarni musi istnieć tylko jeden "rejestr zamówień". Singleton gwarantuje, że każda część aplikacji (UI, Fasada, Logika) korzysta z tej samej instancji menedżera, co zapewnia spójną numerację i statusy zamówień.

### 3. Decorator (Wzorzec Strukturalny)
* **Lokalizacja:** `DesignPatterns/Structural` (`ProductDecorator`, `MilkDecorator`, `WhipCreamDecorator`).
* **Problem:** Problem "eksplozji klas" przy próbie stworzenia wszystkich kombinacji dodatków do produktów (np. Kawa z mlekiem, Kawa z bitą śmietaną i mlekiem itd.).
* **Uzasadnienie:** Pozwala dynamicznie nakładać dodatkowe właściwości (koszt i opis) na bazowy produkt w czasie działania programu. Możemy "opakować" kawę w dowolną liczbę dodatków bez tworzenia dziesiątek podklas.

### 4. Facade (Wzorzec Strukturalny)
* **Lokalizacja:** `DesignPatterns/Structural/OrderFulfillmentFacade.cs`.
* **Problem:** Złożoność procesu realizacji zamówienia, obejmująca wycenę, zmianę statusów oraz komunikację między różnymi systemami.
* **Uzasadnienie:** Fasada udostępnia prosty interfejs `ProcessAndCompleteOrder(id)`. Ukrywa ona przed UI skomplikowane kroki (obliczanie ceny końcowej, wielokrotne zmiany statusu), co upraszcza kod formularza i zmniejsza ryzyko błędów.

### 5. Observer (Wzorzec Zachowań)
* **Lokalizacja:** `DesignPatterns/Behavioral` (`IOrderObserver`, `BaristaDisplay`) oraz `Core/Order.cs`.
* **Problem:** Potrzeba natychmiastowej aktualizacji wielu elementów UI i logiki w momencie zmiany stanu zamówienia.
* **Uzasadnienie:** Klasa `Order` działa jako *Subject*. Gdy status zamówienia ulega zmianie, automatycznie powiadamiani są wszyscy subskrybenci (np. ekran baristy, ekran klienta czy logi systemowe). Klasa zamówienia nie musi wiedzieć, kto ją obserwuje.

---

## 📁 Struktura Projektu
```text
📦 CafeOrderManagerApp
 ┣ 📂 Models                     # Kluczowe interfejsy (IProduct) i modele (Order)
 ┣ 📂 DesignPatterns           # Podział na wzorce Kreacyjne, Strukturalne i Zachowań
 ┃ ┣ 📂 Creational             # Singleton, Factory Method
 ┃ ┣ 📂 Structural             # Decorator, Facade
 ┃ ┗ 📂 Behavioral             # Observer
 ┣ 📂 UI                       # Interfejs Windows Forms (MainForm)
