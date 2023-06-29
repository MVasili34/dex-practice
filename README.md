# dex-practice
Для проверки правильности выполнения практических заданий было создано 57 Unit-тестов. В пунктах 1-4, 6 - задания реализованы в виде консольных приложений.
Практика по работе с SQL-запросами находится в <a href="SqlPractice"> SQLPractice </a>

Ссылки на решения практических заданий по соответствующим темам:
1) <a href="Tools/PracticeWithTypes/Program.cs"> Типы значений и ссылочные типы (Value type and Reference type) </a>
2) <a href="Tools/PracticeWithCasting/Program.cs"> Приведение и преобразование типов </a>
3) <a href="Tools/PracticeWithINotifyPropertyChanged"> События. Events: INotifyPropertyChanged </a>
4) <a href="Tools/PracticeWithListDictionaryBogus/Program.cs"> Практика List, Dictionary, Bogus </a>
5) <a href="Application/ServiceTests/EquivalenceTests.cs"> Эквивалентность, Equals, GetHashCode </a>
6) <a href="Tools/PracticeWithIComparable/Program.cs"> IComparable: сравнение объектов </a>
7) <a href="Application/Services/ClientService.cs">ClientService</a> и <a href="Application/Services/EmployeeService.cs">EmployeeService</a>; <a href="Application/ServiceTests/ExceptionHandlingTests.cs">Обработка исключений (Exception handling) </a>
8) <a href="Application/ServiceTests/IEnumerableLinqTests.cs"> Тестирование интерфейсов для работы со списками (IEnumerable, IEnumerator) </a>
9) <a href="Application/Services/BankService.cs"> Практика по Generic Type </a> + <a href="Application/ServiceTests/BlackListTests.cs">тесты</a>
---
10) В <a href="ApplicationDb"> ApplicationDb </a> реализуется практика по Entity Framework и миграциям: <a href="ApplicationDb/Models">Контекст БД</a>.  Предществующие сервисы были пересозданы в соответствии с изменившимися условиям работы: <a href="ApplicationDb/ServicesDb/ClientService.cs">ClientService</a>, <a href="ApplicationDb/ServicesDb/EmployeeService.cs">EmployeeService</a>. Также реализуются соответствующие <a href="ApplicationDb/ServicesDbTests.Tests">тесты</a>
11) <a href="ApplicationDb/ExportTool/ExportService.cs">Практика по Stream, FileStream, CSV файлам</a> + <a href="ApplicationDb/ServicesDbTests.Tests/CsvFileTests.cs">тесты</a>
12) <a href="ApplicationDb/ServicesDbTests.Tests/ExtensionsMethodsTests.cs"> Практика по определению методов расширения, Extensions Methods </a>
13) <a href="ApplicationDb/ServicesDbTests.Tests/ReflectionTests.cs">Практика по рефлексии, метаданным классов: Reflection</a>
14) <a href="ApplicationDb/ServicesDbTests.Tests/ThreadAndTaskTests.cs"> Многопоточность, блокировки, дедлоки, задачи и класс Task </a>
15) <a href="ApplicationDb/ServicesDbTests.Tests/AsyncTaskTests.cs"> Асинхронность </a>
16) <a href="ApplicationDb/ServicesDbTests.Tests/SerializationTests.cs"> Cериализация объектов </a>
17) <a href="API/BankAPI"> Практика по созданию RESTful API сервиса </a>
18) <a href="ApplicationDb/ServicesDb/CurrencyService.cs"> Практика по использованию HttpClient </a> + <a href="ApplicationDb/ServicesDbTests.Tests/ConvertCurrencyTest.cs">тесты</a>
19) <a href="PostmanTestsResults"> Пратика по использовнию Postman </a>

В качестве дополнения к практике было построено небольшое <strong><em><a href="BlazorDemoBanking/BankingService">Blazor-приложение</em></strong></a>, реализующее часть сервисов, сделанных в процессе практики. Приложение устанавливает соединение с RESTful API сервисом, созданным ранее, при этом обновления DOM происходят в реальном времени посредством SignalR. В качестве временного представления данных используются таблицы. Для более упрощённого тестирования во всех таблицах отображаются идентификаторы. Ниже представлены скриншоты приложения.

Окно статистики банка. Реализует SQL-запросы соответствующей практики, представленные в виде диаграмм.

![adminpanel](https://github.com/MVasili34/dex-practice/assets/117523384/4c50facf-27d9-4dd9-8db5-ae6680f10a93)

Окно редактирования сведений о сотрудниках банка. Реализуется система фильтрации по дате рождения, поиск сотрудника по идентификатору, а также операции CRUD над записями.

![employeespage](https://github.com/MVasili34/dex-practice/assets/117523384/3c4c7293-a296-4f1b-99bc-1f322fcaec82)

Окно редактирования сведений о клиентах и счетах банка. Реализуется система фильтрации по количеству счетов клиента, поиск клиента по идентификатору, а также операции CRUD над записями.

![clientspage](https://github.com/MVasili34/dex-practice/assets/117523384/120a58a9-06bc-4f7f-ad8d-da9873a29506)

Пример окна редактирования. У пользователя есть возможность как удалить клиента, так и удалить его счета по отдельности.

![editexample](https://github.com/MVasili34/dex-practice/assets/117523384/37bce6ac-9252-4a82-b748-5f6f5cba9681)

Сервис конвертации валюты. Клиент может как продать валюту, так и купить её, заплатив дополнительную комиссию банка. Работа сервиса ограничена, так как доступ к информации о состоянии курса осуществляется через сторонний Amdoren API сервис.

![exchangeworking](https://github.com/MVasili34/dex-practice/assets/117523384/1192fbe2-a0a9-4c42-9907-3e8c902d4911)



