Простое приложение для управления изображениями, состоящее из:
- **Web API** (ASP.NET Core)
- **Desktop клиента** (WPF)

## 📌 Основной функционал
- Загрузка/выгрузка изображений
- Просмотр списка изображений
- Редактирование и удаление записей

## 🛠 Стек технологий
- **Backend**: ASP.NET Core 9.0 (Web API)
- **Frontend**: WPF (.NET 9.0)
- **Database**: PostgreSQL
- **Язык**: C# 

## 🗄 Структура базы данных
Для работы приложения создайте таблицу:

```sql
CREATE TABLE IF NOT EXISTS "Images" (
    "Id" SERIAL PRIMARY KEY,
    "FileName" VARCHAR(255) NOT NULL,
    "Data" BYTEA NOT NULL,
    "ContentType" VARCHAR(100) NOT NULL,
    "UploadDate" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Description" TEXT
);
