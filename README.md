# 🏥 Hospital Management System

## 📘 Overview
**Hospital Management System (HMS)** is a complete backend and multi-panel web application built using **.NET 8**.  
It manages hospital operations such as patients, doctors, departments, appointments, medical records, and medications efficiently.

The system follows **Clean Architecture** and applies **SOLID Principles**, ensuring scalability, maintainability, and reusability across all layers.

---

## 🚀 Tech Stack
| Layer | Technology |
|--------|-------------|
| **Framework** | ASP.NET Core 8 Web API + ASP.NET MVC |
| **Database** | SQL Server (via Entity Framework Core) |
| **Authentication** | JWT (JSON Web Tokens) |
| **Architecture** | Clean Architecture / Onion Pattern |
| **Design Principles** | SOLID, Repository Pattern, Service Layer, DTOs |
| **Advanced Features** | Pagination, Filtering, Caching, Dependency Injection |

---

## 🧩 System Architecture Overview

The system is composed of **one main API** and **three MVC panels**, all sharing the same database.

| Project | Type | Description |
|----------|------|-------------|
| **HospitalManagementSystem** | ASP.NET Core Web API | Main backend layer handling business logic and database communication |
| **AdminPanel** | ASP.NET MVC | Web interface for administrators to manage doctors, departments, and system data |
| **DoctorPanel** | ASP.NET MVC | Interface for doctors to view appointments, write medical records, and manage patients |
| **ReceptionistPanel** | ASP.NET MVC | Interface for receptionists to schedule appointments and manage patient flow |

### 🔗 How They Work Together
- All panels send **HTTP requests** to the **HospitalManagementSystem API**.  
- The **API** processes requests, applies validation and business rules, and interacts with the **SQL Server Database** using EF Core.  
- Any change made from any panel is reflected instantly system-wide because all share the same central API and database.

### 🖇 Example Flow
1. **Receptionist** adds a new patient from the *Receptionist Panel* → request sent to the **API** → data saved to the database.  
2. **Doctor** opens the *Doctor Panel* → sees the same patient record via the **API**.  
3. **Admin** from the *Admin Panel* can view and manage doctors, departments, and monitor appointments — all from the same shared data.
4. 
---

## 🧠 Core Features

### 🧍 Patient Management
- Add, update, delete, and view patients.  
- View complete medical history.

### 👨‍⚕️ Doctor Management
- CRUD operations for doctors.  
- Assign doctors to departments.  
- View appointments and medical records.

### 🏢 Department Management
- Manage hospital departments.  
- View all doctors under each department.

### 📅 Appointment Management
- Schedule and update appointments.  
- Prevent overlapping schedules for same doctor/patient.  
- Manage status: `Scheduled`, `Completed`, `Cancelled`.

### 🩺 Medical Records Management
- Doctors can write and update medical reports.  
- Link prescriptions to medical records.  
- View patient’s full history.

### 💊 Medication Management
- Manage hospital medications.  
- Track prescriptions per patient via PrescriptionDetail.

---

## 🧩 Database Mapping

| Entity | Attributes |
|--------|-------------|
| **Patient** | Id, Name, Email, Phone, DateOfBirth, Address |
| **Doctor** | Id, Name, Email, Phone, Specialty, DepartmentId |
| **Department** | Id, Name, Location |
| **Appointment** | Id, PatientId, DoctorId, AppointmentDate, Status |
| **MedicalRecord** | Id, PatientId, DoctorId, Diagnosis, Prescription, RecordDate |
| **Medication** | Id, Name, Description, Quantity |
| **PrescriptionDetail** | Id, MedicalRecordId, MedicationId, Dosage, Frequency |

### 🔗 Relationships
- Patient ↔ Appointment → 1-to-Many  
- Doctor ↔ Appointment → 1-to-Many  
- Department ↔ Doctor → 1-to-Many  
- Patient ↔ MedicalRecord → 1-to-Many  
- Doctor ↔ MedicalRecord → 1-to-Many  
- MedicalRecord ↔ Medication → Many-to-Many (via PrescriptionDetail)

---

## 🗂️ ER Diagram (Text Version)
Patient
└─< Appointment >─┐
Doctor
Department ─< Doctor

Patient ─< MedicalRecord >─ Doctor
MedicalRecord ─< PrescriptionDetail >─ Medication


---

## 🔐 Authentication & Roles
The system uses **JWT Authentication** with role-based access.

| Role | Description |
|------|--------------|
| **Admin** | Full access to manage all entities and users. |
| **Doctor** | Can view patients, create and update medical records. |
| **Receptionist** | Can schedule and manage appointments. |

---

## 🌐 Example API Endpoints

| Resource | Method | Endpoint | Description |
|-----------|---------|-----------|-------------|
| Patients | GET | `/patients` | Get all patients |
| Patients | POST | `/patients` | Add a new patient |
| Patients | PUT | `/patients/{id}` | Update patient info |
| Patients | DELETE | `/patients/{id}` | Delete patient |
| Doctors | GET | `/doctors` | Get all doctors |
| Departments | GET | `/departments` | Get all departments |
| Appointments | POST | `/appointments` | Schedule appointment |
| Medical Records | GET | `/medicalrecords` | Get all medical records |
| Medications | GET | `/medications` | List all medications |

---

## ⚙️ Business Rules
- Prevent double-booking of appointments for same doctor/patient.  
- Only doctors can create or modify medical records.  
- Appointment status cannot be changed after completion.  
- Prescriptions must be linked to valid medical records.  

---

## 🧱 Applied Principles & Patterns
- **SOLID Principles**
- **Clean Architecture**
- **Repository Pattern**
- **Service Layer Pattern**
- **Specification Pattern**
- **DTOs and AutoMapper**
- **Dependency Injection**
- **Pagination & Caching**

---

🧑‍💻 Author

Mohamed Sallam
💼 Backend Developer | ASP.NET Core | Clean Architecture Enthusiast
📧 [Your Email Here]
🔗 [GitHub Profile Link]

🖼 Future Enhancements

Add frontend dashboard (Angular/React) for unified access.

Implement caching via Redis for performance.

Add notification system for upcoming appointments.

Integrate third-party APIs (e.g., SMS or email alerts).
