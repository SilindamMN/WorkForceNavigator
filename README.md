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
