# 🤖 VK LeakProtect Bot

[![C#](https://img.shields.io/badge/C%23-10.0-purple)](https://dotnet.microsoft.com/)
[![.NET](https://img.shields.io/badge/.NET-10.0-blue)](https://dotnet.microsoft.com/)
[![VK API](https://img.shields.io/badge/VK%20API-5.199-green)](https://dev.vk.com/)
[![Render](https://img.shields.io/badge/Render-Deployed-success)](https://render.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)

<p align="center">
  <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/VK.com-logo.svg/1200px-VK.com-logo.svg.png" width="100" alt="VK Logo">
  <img src="https://cdn-icons-png.flaticon.com/512/188/188987.png" width="100" alt="Water Drop">
  <img src="https://cdn-icons-png.flaticon.com/512/10336/10336345.png" width="100" alt="Arduino">
</p>

## 📋 О проекте

**VK LeakProtect Bot** — это умный бот для ВКонтакте, который помогает защитить квартиру или дом от протечек воды. Бот взаимодействует с пользователями через удобный интерфейс с кнопками и готов к интеграции с реальным оборудованием (Arduino + датчики протечки).

### 🎯 Возможности

- ✅ **Интерактивные кнопки** — удобное управление одной рукой
- ✅ **Мгновенные ответы** — бот реагирует на каждое действие
- ✅ **Привязка устройств** — каждый пользователь может подключить свой датчик
- ✅ **Симуляция протечек** — тестовый режим для демонстрации
- ✅ **Готовность к IoT** — архитектура позволяет легко подключить реальное железо

---

## 🚀 Демо

Как только бот запущен, напиши ему **"Привет"** или **"Старт"** — и он сразу ответит:
👋 Привет! Я бот защиты от протечек.
Выбери действие ниже:

А дальше — кнопки:
- 🚰 **Перекрыть воду** — симуляция закрытия клапана
- 📡 **Статус** — проверка состояния системы
- 🔧 **Привязать датчик** — регистрация нового устройства

---

## 🛠 Технологии

| Компонент | Технология |
|-----------|------------|
| **Язык** | C# 10.0 |
| **Фреймворк** | .NET 10.0 (ASP.NET Core) |
| **API ВКонтакте** | Callback API, Messages API (v5.199) |
| **Хостинг** | Render.com (Docker-контейнер) |
| **Контейнеризация** | Docker |
| **CI/CD** | GitHub + Render Auto Deploy |
| **IDE** | JetBrains Rider |

---

## 📁 Структура проекта
VkLeakBot/
├── 📄 Program.cs # Точка входа + Callback API обработчик
├── 📄 VkService.cs # Сервис для работы с VK API
├── 📄 Dockerfile # Конфигурация Docker-образа
├── 📄 VkLeakBot.csproj # Файл проекта .NET
├── 📄 .dockerignore # Исключения для Docker
├── 📄 appsettings.json # Настройки приложения
├── 📄 README.md # Этот файл
└── 📁 Properties/ # Дополнительные настройки

---

## 🔧 Установка и запуск

### Локальный запуск

```bash
# Клонировать репозиторий
git clone https://github.com/visCERBERUS/VkLeakBot.git
cd VkLeakBot

# Запустить проект
dotnet run
Запуск через Docker
bash
# Собрать образ
docker build -t vk-leak-bot .

# Запустить контейнер
docker run -p 8080:80 vk-leak-bot

🌐 Деплой на Render.com
Проект задеплоен на Render.com с использованием Docker:

Репозиторий подключён к GitHub

При каждом пуше в master автоматически запускается сборка

Готовый образ разворачивается в контейнере

Публичный URL:
👉 https://vkbot-9h46.onrender.com

🔌 Настройка ВКонтакте
1. Создать сообщество
Если ещё нет — создай сообщество ВК, к которому будет привязан бот.

2. Включить сообщения
Управление → Сообщения → Включить сообщения сообщества

3. Настроить Callback API
Управление → Работа с API → Callback API

Параметр	Значение
URL сервера	https://vkbot-9h46.onrender.com/callback
Секретный ключ	24649f82
Версия API	5.199
4. Подписаться на события
В разделе "Типы событий" включить:

✅ Входящее сообщение (message_new)

5. Создать ключ доступа
Ключи доступа → Создать ключ → Права: Сообщения

📱 Команды бота
Команда	Описание
Привет / Старт	Приветствие и показ кнопок
🚰 Перекрыть воду	Симуляция закрытия клапана
📡 Статус	Проверка состояния системы
🔧 Привязать датчик	Регистрация нового устройства
🧪 Тестирование
После деплоя можно проверить работу бота:

bash
# Проверка, что сервер отвечает
curl https://vkbot-9h46.onrender.com/callback
# Ожидаемый ответ: 405 Method Not Allowed (это нормально!)
👨‍💻 Автор
Ilya Valentinov

GitHub: @visCERBERUS
