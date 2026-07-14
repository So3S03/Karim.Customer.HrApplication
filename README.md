# 🏢 HR + PM Application

A complete Human Resources Management System (HRMS) With PM Module built with **ASP.NET Core 9** for the backend and **Angular +20** for the frontend.  
The application is designed to streamline company operations, manage employees, departments, attendance, payroll, projects, contracts, tasks, tickets and organizational structure — all in one integrated platform.

---

## 🚀 Technologies Stack

### 🖥 Backend
- **ASP.NET Core 9 Web API**
- **Entity Framework Core**
- **SQL Server**
- **Mappster**
- **JWT Authentication with Refresh Token**
- **Unit of Work + Repository Pattern**
- **Specification Design Pattern**
- **Exception Handling Middleware**
- **Redis Caching**

### 🌐 Frontend
- **Angular +20**
- **RxJS**
- **Tailwind CSS**
- **State Management (NgRx or Signals)**
- **Dynamic Dashboard Charts (using Chart.js or ngx-charts)**

---

## 📦 Architecture

This project follows a **Onion Architecture Pattern** With some modifications on it:

- **APIs Folder (ASP.NET Core Web APIs)** – Have class library for Controllers seperated from Main Project + Web APIs Project.
- **Core Folder** – Domain + Business logic-service (abstraction + Business logic-service implimentation) 3 class libraries.
- **Infrastructure Folder** – Persistance + Infrastructure 2 class libraries.
- **Shared Folder** – Shared data between all folders 1 class libraries.
- **UI Folder (Angular +20)** – Handles UI and UX interactions.
---

## 🧩 Modules Overview

### 1️⃣ Dashboard
Displays the latest company statistics and progress:
- Total Employees, Departments, Active Projects
- Total Company Budget and Cash Flow
- Visual Reports using charts and graphs

---

### 2️⃣ Employees
Manage all employee-related data and actions:
- Add / Edit / Terminate Employees  
- Create System Users  
- Print Employee ID  
- Change Employee Position / Rank / Department / Team  
- Assign Tasks (linked to Task Module)  
- Manage Employee Contracts

---

### 3️⃣ Departments
Department-level management and structure:
- Add / Edit / Soft Delete / Hard Delete Departments  
- Display Active Teams and Their Projects  
- Replace Employees in Department  
- Assign Projects to Department  
- Multi-project Support per Department  

---

### 4️⃣ Attendance
Employee attendance tracking and management:
- Daily Check-in/Check-out Records  
- Add Fingerprints per day 
- Assign Vacations, Permissions, and Overtime via Calendar  
- Approve or Reject Employee Requests

---

### 5️⃣ Payroll
Comprehensive payroll and financial management:
- Manage Employee Salaries  
- Manage Employee Deductions - Bonuses

---

### 6️⃣ Contract
Contracts Managements For Both Employees & Projects:
- Controll Employee Salary & Allowamnces 
- Controll Project Budjets  
- Renew Contracts With Amount Of Years
- Terminate & Activate Contracts

---

### 7️⃣ Projects
Full lifecycle project management:
- Show All Projects  
- Manage Departments & Teams Assigned  
- Track Project Progress and Budget  
- Edit Project Timeline / Department / Team  
- Drop Lost Projects (Contract Termination)  

---

### 8️⃣ Tasks
Assign & Tracking Employees Tasks:
- Show All Tasks Per Employee  
- Manage Tasks Period & Worked Time On Them
- Track Task Progress For Employees
- Pull Tasks Per Day With Controll On Its Status

---

### 9️⃣ Ticket
Create Tickets On Projects That Have Proplems With Following Up On Them:
- Show All Tickets Per Project  
- Creating Tasks From Them And Assign It To Employees
- Track Ticket Progress For Clients

---

## 🔐 Security & Authorization
- Full **JWT Authentication & Refresh Token System**
- Role-based Access Control (Admin, HR, Employee)
- Secure Endpoints and Claims-based Validation

---

## ⚙️ Error Handling
- Centralized **Error Middleware** for unified exception handling  
- Custom API responses for better debugging  
- Logging (Serilog or custom logger planned)

---

## 🧱 Project Setup

### 🔧 Backend (ASP.NET Core 9)
```bash
cd HrApplication.Api
dotnet restore
dotnet build
dotnet run
