# XoperoCal

-[x] Wyświetlanie/dodawanie/edytowanie/usuwanie alarmów/zdarzeń do wbudowanego kalendarza
-[X] Wyszukiwanie zdarzeń po nazwie bądź okresie czasu
-[x] Eksportowanie wybranych zdarzeń
-[ ] Importowanie zdarzeń z pliku
-[ ] W przypadku alarmu, program powinien wyświetlić powiadomienie (o ile jest uruchomiony) z możliwości snooze/confirm
-[X] (Bonus, jeśli reszta pójdzie za szybko) Umożliwienie 2-3 w LAN
> bonus chyba zrobiony 

client Run: 
``` bash

cd xopCalClient
npm install
npm run dev

```

server Run:

>xopCal/Entity/EventDbContext.cs line-7
>```csharp
>    private string _connectionString = "Host=172.17.0.2;Database=cal;Username=postgres;Password=r";
>```