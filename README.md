# XoperoCal

- [X] Wyświetlanie/dodawanie/edytowanie/usuwanie alarmów/zdarzeń do wbudowanego kalendarza
- [X] Wyszukiwanie zdarzeń po nazwie bądź okresie czasu
- [x] Eksportowanie wybranych zdarzeń
- [x] Importowanie zdarzeń z pliku
- [x] W przypadku alarmu, program powinien wyświetlić powiadomienie (o ile jest uruchomiony) z możliwości snooze/confirm
- [X] (Bonus, jeśli reszta pójdzie za szybko) Umożliwienie 2-3 w LAN
> bonus chyba zrobiony 

client Run: 
``` bash

cd xopCalClient
npm install
npm run dev

```

server Run:

>
> xopCal/appsettings.Development.json ,
> xopCal/appsettings.json
>```json
>    "ConnectionStrings" : {
>    "Npgsql" : "Host=172.17.0.2;Database=cal;Username=postgres;Password=r"
>   }
>```