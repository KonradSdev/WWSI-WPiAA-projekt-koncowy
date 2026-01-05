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
* **Lokalizacja:** `DesignPatterns/Structural/OrderFulfillmentFacade.cs`, `DesignPatterns/Structural/OrderPlacementFacade.cs`.
* **Problem:** Złożoność procesu realizacji zamówienia, obejmująca wycenę, zmianę statusów oraz komunikację między różnymi systemami.
* **Uzasadnienie:** Fasada udostępnia prosty interfejs `ProcessAndCompleteOrder(id)`. Ukrywa ona przed UI skomplikowane kroki (obliczanie ceny końcowej, wielokrotne zmiany statusu), co upraszcza kod formularza i zmniejsza ryzyko błędów.

### 5. Observer (Wzorzec Zachowań)
* **Lokalizacja:** `DesignPatterns/Behavioral` (`IOrderObserver`, `BaristaDisplay`) oraz `Core/Order.cs`.
* **Problem:** Potrzeba natychmiastowej aktualizacji wielu elementów UI i logiki w momencie zmiany stanu zamówienia.
* **Uzasadnienie:** Klasa `Order` działa jako *Subject*. Gdy status zamówienia ulega zmianie, automatycznie powiadamiani są wszyscy subskrybenci (np. ekran baristy, ekran klienta czy logi systemowe). Klasa zamówienia nie musi wiedzieć, kto ją obserwuje.

---
## Obsługa GUI
### 1. Widok domyślny
<img width="635" height="710" alt="image" src="https://github.com/user-attachments/assets/e4f730f4-6e17-4f51-be7c-e8ddd700f7f2" />

### 2. Wybór produktów z listy
Użytkownik może wybrać 3 produkty z listy rozwijalnej (1) oraz wybrać 2 dodatki (dekoratory), które wpływają zarówno na opis pozycji zamówienia jak i na cenę. Po wybraniu pozycji zamówienia należy kliknąć przycisk `Dodaj do zamówienia` (3).
Tak stworzona pozycja wyświetli się następnie na liście (4). Na jedno zamówienie może skłądać się wiele pozycji, nie ma tutaj ustawionego górnego limitu.
<img width="641" height="707" alt="image" src="https://github.com/user-attachments/assets/e3fa66cd-46b2-4f6e-9131-e8c87204c883" />
<img width="636" height="712" alt="image" src="https://github.com/user-attachments/assets/610fda76-a893-43d7-9e08-4b735fd55b20" />

### 3. Usuwanie pozycji zamówienia
W przypadku potrzeby usunięcia poszczególnych pozycji zamówienia, użytkownik może zrobić to poprzez kliknięcie prawym przyciskiem myszy na wybraną pozycję. Następnie należy wybrać `Usuń pozycję` i zatwierdzić wybór w pojawiającym się oknie.
W przypadku chęci usunięcia wszystkich pozycji z listy, można skorzystać z guzika `Wyczyść listę`. Usunięcie danej pozycji zamówienia zostanie odnotowane w logach systemowych. Usunięcie całej listy jest uważane za sytuację, w której nie
ma potrzeby logowania akcji (klient całkiem zrezygnował z zakupu). Z tego powodu logi nie są odkładane.

<img width="637" height="707" alt="image" src="https://github.com/user-attachments/assets/bed95d36-a989-4d8c-a5bd-d7e9aab4697b" />
<img width="640" height="707" alt="image" src="https://github.com/user-attachments/assets/059bf26f-ef6c-4a86-bc7c-e63f1eb31586" />
<img width="640" height="705" alt="image" src="https://github.com/user-attachments/assets/e12327f2-f888-4d19-ad27-83c3d8febc15" />
<img width="639" height="714" alt="image" src="https://github.com/user-attachments/assets/5e85a3ba-6396-410a-93c7-0689c9cbcbd3" />
<img width="649" height="715" alt="image" src="https://github.com/user-attachments/assets/a0f6124e-87c3-493c-b000-5a8d81a80d31" />

### 3. Zatwierdzanie zamówienia
Gdy wszystkie pozycje zamówienia są skompletowane, należy je zatwierdzić przyciskiem `Zatwierdź zamówienie`. Po wykonaniu tej czynności zostanie ono zarejestrowane na liście zamówień do realizacji przez bariste (1 - widok ekranu baristy), oczekujących (2 - widok ekranu klienta) oraz w logach (3 - widoczny dla wszystkich pracowników) . W logach wyświetlona zostanie również całkowita kwota do zapłaty.

<img width="644" height="708" alt="image" src="https://github.com/user-attachments/assets/c9291ee8-68ac-456c-9995-a409e326c375" />
<img width="664" height="714" alt="image" src="https://github.com/user-attachments/assets/294f8c8c-9839-40f8-a807-88d4b6952784" />

### 4. Realizacja zamówienia
Barista może zmienić status zamówienia poprzez zaznaczenie zamówienia na liście (1), kliknięcia przycisku `Rozpocznij realizację` (2) lub jeśli zamówienie może być wydane od razu `Zamówienie gotowe` (3). Zmiana statusu zostanie odwzierciedlona również
na ekranie klienta (4). W przypadku zamówienia gotowego do odbioru, zostanie ono zaznaczone na ekranie klienta na zielono, oraz przez 10 sekund zostanie wyświetlone na żółtym tle zamiast etykiety w celu zwrócenia uwagi klienta.
Barista ma możliwość zaznaczenia kilku zamówień na raz używając CTRL lub SHIFT na klawiaturze.

<img width="647" height="730" alt="image" src="https://github.com/user-attachments/assets/c544cc34-f61f-450c-be8e-137c3f1862b2" />
<img width="637" height="713" alt="image" src="https://github.com/user-attachments/assets/f021b8e9-2a26-44c9-8a4b-d8f516f6e3ec" />
<img width="643" height="709" alt="image" src="https://github.com/user-attachments/assets/44d1d7b4-4387-4c72-9ccd-cae4108d2fc4" />

### 5. Odbiór zamówienia
Gdy zamówienie klienta ma status `Gotowe do odbioru` zostaje ono usunięte z listy zamówień do realizacji przez baristę i oczekuje aż klient je odbierze. W tym celu barista musi zaznaczyć konkretne zamówienia i kliknąć dodatkowy przycisk `Zamówienie odebrane`, który potwierdza odbiór zamówienia. Ta akcja podobnie jak poprzednie, również jest rejestrowana w logach.

<img width="645" height="710" alt="image" src="https://github.com/user-attachments/assets/d4e9c51a-2ab2-4ab0-8bbb-e5c6f63e51ae" />
<img width="634" height="724" alt="image" src="https://github.com/user-attachments/assets/43288f2a-235c-4823-8b6b-a5c4137bac16" />

### 6. Anulowanie zamówienia
Barista ma również możliwość skasowania konkretnych zamówień poprzez użycie przycisku `Usuń zamówienie`. Przed jego kliknięciem musi wybrać zamówienie z którejś listy. Przycisk zadziała niezależnie czy zamówienie zostało wybrane na ekranie klienta czy baristy.

<img width="642" height="709" alt="image" src="https://github.com/user-attachments/assets/8138ad63-f45b-4b8a-8f97-fed2533902e9" />
<img width="651" height="710" alt="image" src="https://github.com/user-attachments/assets/e0505b37-b320-44d7-ba7f-94ef8f8cd33c" />
<img width="643" height="713" alt="image" src="https://github.com/user-attachments/assets/050e335d-a96a-490d-bab8-1aa2fccbfe49" />

## Uruchomienie aplikacji
Aby uruchomić aplikację należy zbudować rozwiązanie dołączone do repozytorium `.sln`.

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

