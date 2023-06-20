# dex-practice
Для проверки выполнения практических заданий проводились как Unit-тесты, так и тесты в консольных приложениях.
Практика по работе с базами данных и скриншоты SQL-запросов находятся в папке SQLPractice

Список папок и файлов по соответствующим темам:
1) Tools/PraticeWithTypes - Типы значений и ссылочные типы (Value type and Reference type)
2) Tools/PraticeWithCasting - Приведение и преобразование типов
3) Tools/PraticeWithINotifyPropertyChanged - События. Events: INotifyPropertyChanged
4) Tools/PracticeWithListDictionaryBogus - Практика List, Dictionary, Bogus
5) Application/ServiceTests/EquivalenceTests - Эквивалентность, Equals, GetHashCode
6) Tools/PracticeWithIComparable - IComparable: сравнение объектов
7) Application/ServiceTests/ExceptionHandlingTests - Тестирование ClientService и EmployeeService, обработка исключений (Exception handling)
8) Application/ServiceTests/IEnumerableLinqTests - Тестирование интерфейсов для работы со списками (IEnumerable, IEnumerator)
9) Application/ServiceTests/BlackListTests - Тестирование работы методов с применением Generic Type  
---
10) ApplicationDb - в данной папке реализуется практика по EntityFramework и миграциям. Предществующие сервисы были пересозданы в соответствии с изменившимися условиям работы. В ServicesDbTests.Tests реализуются соответствующие тесты
11) ApplicationDb/ServicesDbTests/CsvFileTests - практика по Stream, FileStream, CSV файлам
12) ApplicationDb/ServicesDbTests/ExtensionsMethodsTests - практика по определению методов расширения, Extensions Methods
13) ApplicationDb/ServicesDbTests/ReflectionTests - практика по рефлексии, метаданным классов: Reflection
14) ApplicationDb/ServicesDbTests/ThreadAndTaskTests - практика по многопоточности, блокировкам, дедлокам, задачи и класс Task
15) ApplicationDb/ServicesDbTests/AsyncTaskTests - практика по асинхронности. Также переписаны сервисы и их тесты по работе с клиентами и сотрудниками
16) ApplicationDb/ServicesDbTests/SerializationTests - практика по сериализации объектов класса
17) API/BankAPI - практика по созданию RESTful API сервиса (контекст базы данных добавляется с помощью Extension-метода в ApplicationDb/EntityModels)
18) ApplicationDb/ServicesDbTests/ConvertCurrencyTests - практика по использованию HttpClient для создания запросов удалённому API-Сервису для конвертации валют
