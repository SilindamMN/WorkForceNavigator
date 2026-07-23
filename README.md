# 🚀 Company Management Platform

**A full-stack enterprise platform for streamlining company operations** — built with **ASP.NET Core (.NET 9)** and **React TypeScript**, following **Domain-Driven Design (DDD)** and clean **OOP** architecture.

![.NET](https://img.shields.io/badge/-.NET%209-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/-React-61DAFB?style=for-the-badge&logo=react&logoColor=black)
![TypeScript](https://img.shields.io/badge/-TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)
![MySQL](https://img.shields.io/badge/-MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)
![Vite](https://img.shields.io/badge/-Vite-646CFF?style=for-the-badge&logo=vite&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)

---

### 🎯 What This Project Solves

This system enables organizations to manage their internal structure and employee lifecycle end-to-end:

- 🏢 Department and team management
- 🕒 Timesheet tracking
- 📅 Leave applications and approvals
- 🔐 Role-based system access and user experiences

---

### 💡 Highlights & Key Features

<details>
<summary><b>🔐 Role-Based Access Control (RBAC)</b></summary>
<br>

Tailored user navigation and permissions for **Admin**, **Manager**, and **Employee** roles.

</details>

<details>
<summary><b>📅 Leave Management</b></summary>
<br>

Apply for leave, view leave history, and track approval status.

</details>

<details>
<summary><b>📊 Timesheets</b></summary>
<br>

Submit, track, and review working hours.

</details>

<details>
<summary><b>🏢 Company & Department Setup</b></summary>
<br>

Create companies, departments, and assign teams.

</details>

<details>
<summary><b>🧠 Intelligent Architecture</b></summary>
<br>

Structured using DDD, enabling scalability, testability, and clean separation of concerns.

</details>

---

### 🧩 What Sets This Project Apart

- **Domain-Driven Design (DDD)** ensures the project aligns closely with how real businesses operate.
- **Bounded contexts** represent different areas like user registration, leave management, and role assignment.
- **Aggregates, Entities, and Services** model real-world behavior in a maintainable way.

<details>
<summary><b>🏗 Exploring the Design</b></summary>
<br>

- **Bounded Contexts** — Each feature (login, teams, timesheets) exists in its own logical domain.
- **Aggregates & Entities** — Represent real-world concepts (Users, Admins, Teams) with enforced consistency.
- **Services** — Handle key operations, such as assigning roles or managing teams.
- **Role-Based Access in Context**
  - Aggregate Roots (Admins): can manage users and assign roles
  - Entities (Regular Users): access scoped to their role and domain

</details>

---

### ⚡ Tech Stack

![C#](https://img.shields.io/badge/-C%23-239120?style=flat-square&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/-.NET%209-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/-EF%20Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/-React-61DAFB?style=flat-square&logo=react&logoColor=black)
![TypeScript](https://img.shields.io/badge/-TypeScript-3178C6?style=flat-square&logo=typescript&logoColor=white)
![Vite](https://img.shields.io/badge/-Vite-646CFF?style=flat-square&logo=vite&logoColor=white)
![MySQL](https://img.shields.io/badge/-MySQL-4479A1?style=flat-square&logo=mysql&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?style=flat-square&logo=swagger&logoColor=black)
![Git](https://img.shields.io/badge/-Git-F05032?style=flat-square&logo=git&logoColor=white)

---

### ⚙️ How to Run the Project

<details>
<summary><b>🔧 Backend Setup (/Backend)</b></summary>
<br>

1. Clone the repository.
2. Ensure the **.NET 9 SDK** is installed.
3. Make sure all `.csproj` files target `net9.0`.
4. Open `migration.txt` in the **Persistence** project for migration setup instructions.
5. Check `appsettings.Development.json` for the database name — you may need to create it manually in SQL Server/MySQL.
6. Run the project.
7. In Swagger, in order:
   - Run the **SeedRole** endpoint to seed default roles.
   - Use the **Register** endpoint to create a user.
   - Use the **Login** endpoint to authenticate.

</details>

<details>
<summary><b>💻 Frontend Setup (/Frontend)</b></summary>
<br>

1. Open the project in Visual Studio Code.
2. Follow the steps in the frontend `README.md` to get started.

</details>

---

### 📘 Architecture Overview

This project doubles as a template for building **DDD-based .NET APIs** with registration, login, and role-based access built in from the start.

**Getting Started** — Set up the solution like any modern .NET app; this becomes the foundation of your "user domain."

**Customizing Domains** — Easily tailor registration, leave, or timesheet logic to your organization's rules.

**Domain Services** — Handle logic like role assignment and workflow transitions within clear domain boundaries.


🚀 Company Management Platform – Built with ASP.NET Core & React TypeScript
A robust full-stack enterprise platform designed to streamline company operations, built using ASP.NET Core, React TypeScript, and modern software architecture practices such as Domain-Driven Design (DDD) and Object-Oriented Programming (OOP).

🎯 What This Project Solves
This system enables organizations to manage their internal structure and employee lifecycle, offering a comprehensive solution for:

Department and team management

Timesheet tracking

Leave applications and approvals

Role-based system access and user experiences

💡 Highlights & Key Features
🔐 Role-Based Access Control (RBAC)
Tailored user navigation and permissions (Admin, Manager, Employee)

📅 Leave Management
Apply for leave, view leave history and approval status

📊 Timesheets
Submit, track, and review working hours

🏢 Company & Department Setup
Create companies, departments, and assign teams

🧠 Intelligent Architecture
Structured using DDD, enabling scalability, testability, and clean separation of concerns

🛠 Tech Stack
Backend: ASP.NET Core (.NET 9), C#, Entity Framework Core, MySQL

Frontend: React, TypeScript, Vite

Tools & Practices: LINQ, REST APIs, OOP, DDD, Swagger, Git

⚙️ How to Run the Project
🔧 Backend Setup (/Backend)
Clone the repository.

Ensure .NET 9 SDK is installed.

Make sure all .csproj files are targeting net9.0.

Open the **migration.txt** file in the Persistence project for migration setup instructions.

Check the appsettings.Development.json for the database name — you may need to create it manually in SQL Server.

Run the project.

In Swagger:

First, run the **SeedRole** endpoint to seed default roles.

Then use the **Register** endpoint to create a user.

Finally, use the **Login** endpoint to authenticate.

💻 Frontend Setup (/Frontend)
Open the project in Visual Studio Code.

Follow the steps in the README.md file to get started.

📘 Welcome to the .NET API Registration and Login Template Project!
Hello! 🌟 This guide will help you understand the architecture and purpose of the platform — a powerful tool built using Domain-Driven Design (DDD) principles. Beyond registration and login, this system features sophisticated role-based access and cleanly modeled business logic.

🧩 What Sets This Project Apart?
Domain-Driven Design (DDD) ensures that the project aligns closely with how real businesses operate.

Bounded contexts represent different areas like user registration, leave management, and role assignment.

Aggregates, Entities, and Services model real-world behavior in a maintainable way.

🏗 Exploring the Design
Bounded Contexts:
Each feature (e.g., login, teams, timesheets) exists in its own logical domain.

Aggregates & Entities:
Represent real-world concepts (Users, Admins, Teams) with enforced consistency.

Services:
Handle key operations, such as assigning roles or managing teams.

Role-Based Access in Context:

Aggregate Roots (Admins): Can manage users and assign roles.

Entities (Regular Users): Access is scoped to their role and domain.

🎯 Leveraging DDD in Your Project
Getting Started:
Set up the solution just like any modern .NET app — this becomes the foundation of your “user domain.”

Customizing Domains:
Easily tailor registration, leave, or timesheet logic to your organization’s rules.

Domain Services:
Handle logic like role assignment and workflow transitions within clear domain boundaries.
