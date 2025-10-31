# 🏢 HR Application

A complete Human Resources Management System (HRMS) built with **ASP.NET Core 9** for the backend and **Angular 20** for the frontend.  
The application is designed to streamline company operations, manage employees, departments, attendance, payroll, projects, and organizational structure — all in one integrated platform.

---

## 🚀 Technologies Stack

### 🖥 Backend
- **ASP.NET Core 9 Web API**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **JWT Authentication with Refresh Token**
- **Unit of Work + Repository Pattern**
- **Specification Design Pattern**
- **Exception Handling Middleware**
- **Redis Caching**

### 🌐 Frontend
- **Angular 20**
- **RxJS**
- **Tailwind CSS**
- **State Management (NgRx or Signals)**
- **Dynamic Dashboard Charts (using Chart.js or ngx-charts)**

---

## 📦 Architecture

This project follows a **Onion Architecture Pattern**:

- **APIs Folder (ASP.NET Core Web APIs)** – Have class library for Controllers, Web APIs Project.
- **Core Folder** – Domain  , Business logic-service abstraction and Business logic-service implimentation 3 class libraries.
- **Infrastructure Folder** – Persistance and Infrastructure 2 class libraries.
- **Shared Folder** – Shared data between all libraries 1 class libraries.
- **UI Folder (Angular 20)** – Handles UI and UX interactions.
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
- Add / Edit / Terminate / Pre-Terminate Employees  
- Create System Users  
- Print Employee ID  
- Change Employee Position / Rank / Department / Team  
- Assign Tasks (linked to Project ID)  
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
- Add Single or Multiple Fingerprints  
- Assign Tasks, Vacations, Permissions, and Overtime via Calendar  
- Approve or Reject Employee Requests  
- Cancel Delay Records  

---

### 5️⃣ Payroll
Comprehensive payroll and financial management:
- Manage Employee Salaries  
- Manage Project Budgets  
- Manage Overall Company Budget  
- (Future) Integration with Finance APIs and Stripe  

---

### 6️⃣ Organization Chart
Hierarchical visualization of the company:
- Display Company Structure (CEO → Employees)  
- Display All Job Titles and Relations  
- Show Employee Placement in the Hierarchy  
- (Future) Editable Chart and Analytics  

---

### 7️⃣ Projects
Full lifecycle project management:
- Show All Projects  
- Manage Departments & Teams Assigned  
- Track Project Progress and Budget  
- Edit Project Timeline / Department / Team  
- Drop Lost Projects (Contract Termination)  
- Multi-department Collaboration Support  

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
