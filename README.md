News Aggregator

Описание
News Aggregator — это консольное приложение на C#, которое автоматически парсит последние статьи с заданного новостного сайта и сохраняет их в базу данных SQLite. Приложение использует Selenium для автоматического получения HTML-кода страницы и HtmlAgilityPack для парсинга данных.

Функционал
Парсинг заголовков статей, URL и даты публикации с заданного сайта.
Сохранение данных в базу данных SQLite.
Проверка на дублирование статей перед сохранением.
Вывод сохраненных статей из базы данных в консоль.

Требования
.NET SDK версии 5.0 или выше
Selenium WebDriver
HtmlAgilityPack
SQLite
Entity Framework Core

Установка
1. Клонируйте репозиторий:
git clone https://github.com/your-username/news-aggregator.git
cd news-aggregator
2. Установите зависимости:
dotnet add package Selenium.WebDriver
dotnet add package HtmlAgilityPack
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
3. Создайте базу данных и выполните миграции:
dotnet ef migrations add InitialCreate
dotnet ef database update

Использование
Запустите приложение:
dotnet run

Программа выполнит следующие действия:

Скачает HTML-контент с заданного сайта.
Спарсит заголовки статей, URL и дату публикации.
Сохранит новые статьи в базу данных SQLite.
Выведет сохраненные статьи из базы данных в консоль.

Структура проекта
Program.cs: Основная точка входа в приложение. Содержит логику парсинга и сохранения данных.
HtmlParser.cs: Логика парсинга HTML-контента.
NewsArticle.cs: Модель данных для хранения информации о статьях.
NewsContext.cs: Контекст базы данных для работы с Entity Framework.
NewsService.cs: Сервис для взаимодействия с базой данных (сохранение и чтение данных).

Примеры:
Fetching news...
Fetched 5 articles.
Title: Тест: узнайте героиню советского фильма по туфелькам
URL: https://www.culture.ru/materials/257989/test-uznaite-geroinyu-sovetskogo-filma-po-tufelkam
Published Date: 2024-08-12 18:34:22

Title: Как в СССР создавали мультфильмы
URL: https://www.culture.ru/materials/257988/kak-v-sssr-sozdavali-multfilmy
Published Date: 2024-08-12 18:35:22
