# Contacts Web Application

A full-stack web application built with **ASP.NET Core** and **React** for managing a list of contacts. This app supports creating, viewing, editing, and deleting contacts stored in a local database.

## 📌 Homework Assignment

> **Goal:** Develop a web application with a single page on which to display a list of contacts, with the ability to add, edit, and delete them. Data is persisted in a local database.

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core Web API
- **Frontend:** React (JavaScript)
- **Database:** SQL Server
- **API Communication:** REST
- **Styling:** Bootstrap
- **Validation:** Client-side JavaScript (custom logic) && Server-side FluentValidation logic

---

## 📂 Features

### ✅ Server-Side Logic (ASP.NET Core)
- Full CRUD API for managing contacts:
  - Create a new contact
  - Read contacts
  - Update contact details
  - Delete a contact
- Entity: `Contact` with the following properties:
  - `Id`
  - `Name`
  - `MobilePhone`
  - `JobTitle`
  - `BirthDate`

### ✅ Frontend Functionality (React)
- Displays a paginated list of contacts
- Add new contact via a popup form
- Edit contact details in a popup
- Delete contact via confirmation
- Validates input fields using custom JavaScript validation:
  - Example validation rules:
    - Name must be at least 1 character
    - Mobile phone must be at least 10 characters and no more than 20
    - Job title cannot be empty
    - Birth date must be a valid date and not in the future

---

## 📦 Getting Started

1. Clone the repository
```console
git clone https://github.com/XmyriyCat/beesender-contacts.git
```
2. Next, build the compose.yaml file in the terminal
```console
docker compose build
```
3. Run the compose.yaml file in the terminal
```console
docker compose up -d
```
4. Next, we will view the running docker containers
```console
docker ps
```
after that you should see this output:
```console
CONTAINER ID   IMAGE                                   COMMAND                  CREATED          STATUS          PORTS                                                   NAMES
34e4130c3d8d   beesender-contact.client                "docker-entrypoint.s…"   23 seconds ago   Up 22 seconds   0.0.0.0:3000->3000/tcp, [::]:3000->3000/tcp             contact.client
674554737ea4   contact.api                             "dotnet Contact.Api.…"   23 seconds ago   Up 22 seconds   0.0.0.0:8080->8080/tcp, [::]:8080->8080/tcp, 8081/tcp   contact-api
1f58cef46a61   mcr.microsoft.com/mssql/server:latest   "/bin/bash /entrypoi…"   23 seconds ago   Up 22 seconds   0.0.0.0:1433->1433/tcp, [::]:1433->1433/tcp             mssql-db
```
5. Next, open the initial project page
```
http://localhost:3000/
```
