# Aplikacja do zarządzania domowym budżetem

## Ogólny opis aplikacji
Aplikacja ma służyć do zarządzania domowymi finansami. Powinna posiadać możliwość definiowania budżetów na wybrany okres czasu (np. miesiąc) oraz sprawdzenia jego realizacji.
Aby śledzić poszczególne wpływy i wydatki, każde z nich będzie powiązane z jednym ze zdefiniowanych w systemie kont.
Konta te mogą być powiązane z kontem bankowym, ale nie muszą - mogą być wirtualnym rozliczeniem wpływów i wydatków.
Poszczególne operacje będą powiązane z kontem źródłowym i docelowym, a także budżetem w ramach którego będą rozliczone.

## Podstawowe encje w systemie
* **Konto** - grupuje transakcje danego typu. 
* **Transakcje** - definiują operację przepływu środków pomiędzy kontami. Mogą być jednorazowe lub okresowe. Mogą mieć ustalony termin wykonania.
* **Szablon budżetu** - pozwala zdefiniować powtarzalny schemat budżetu, który będzie generowany co wybrany okres czasu.
* **Budżet** - grupuje operacje w ramach danego okresu czasu. Pozwala rozliczyć wpływy i wydatki oraz pokazać bilans środków na poszczególnych kontach. 

## Podstawowe operacje
* Utworzenie listy kont
* Utworzenie szablonu budżetu
* Utworzenie budżetu na podstawie szablonu
* Dodawanie transakcji do budżetu
* Zamknięcie budżetu
