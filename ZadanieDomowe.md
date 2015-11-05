# Zadanie domowe

Uwaga: Każde z zadań implementujemy zgodnie z zasadami TDD - wpierw tworzymy testy a potem stopniowo implementacje.

## 1. Zaimplementować metode IRepository<T>.Take
```IEnumerable<T> Take(int count);```
Metoda zwraca pierwsze ```count``` elementów z naszego repozytorium. Jezeli repozytorium jest puste lub zawiera mniej niż ```count``` elementów to wyrzuca wyjatek ```ArgumentException```.

## 2. Zaimplementować metode IRepository<T>.GetByIds
```IEnumerable<T> GetByIds(IEnumerable<int> ids);```
Metoda zwraca wszystkie elementy o id podanych w parametrze ```ids```. Jeżeli element o podanym id nie istnieje to go pomijamy.
