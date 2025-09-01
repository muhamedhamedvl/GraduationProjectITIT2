# CourseManagementProject

---
## Mohamed Hamed  – User Management 
### Tasks:
- Setup DbContext + Migrations with SQL Server.
- Full CRUD for Users (Admin, Instructor, Trainee).
- Search + Pagination by name or role.
- Validations:
  - Name: Required, length 3–50.
  - Email: Required, valid, unique (Remote Validation).
  - Role: Required.
- Views: Create, Edit, Delete, List.
---

## Fatma Mohamed – Course Management
### Tasks:
- Full CRUD for Courses.
- Link each Course to an Instructor.
- Search + Pagination by name or category.
- Validations:
  - Name: Required, 3–50, Unique.
  - Category: Required.
  - Custom Attribute: NoNumberAttribute.
- Views: Create, Edit, Delete, List.

### Order:
- Starts after Infrastructure is ready (Repositories + DbContext).
---

## Hagar Mohamed – Session Management
### Tasks:
- CRUD for Sessions linked to a Course.
- StartDate & EndDate with validations:
  - StartDate: Required, cannot be in the past.
  - EndDate: Required, must be after StartDate.
- Search + Pagination by Course name.
- Views: Create, Edit, Delete, List.

### Order:
- Starts after Fatma finishes Course entity (since Sessions depend on Courses).

---

## Mahmoud El-Daly – Infrastructure & Architecture
### Tasks:
- Prepare the full Solution Structure (DAL, BLL, Web).
- Create Repository Interfaces & Implementations:
- ICourseRepository, ISessionRepository, IUserRepository, IGradeRepository.
- Configure Dependency Injection for all Services/Repositories.
- Shared Partial Views + Bootstrap Alerts/Notifications.

### Order:
- **must start first** 

---

## Mahmoud Hegazy – Grades Management
### Tasks:
- Record grades for each Trainee in a Session.
- Display student grades with Pagination.
- Validations:
  - Value: Required, between 0–100.
  - SessionId & TraineeId: Required.
- Views: Create, List, Filter per Trainee.

### Order:
- Can only start after Sessions and Users are ready.

---

# Workflow

1. **Mahmoud El-Daly** starts first (Infrastructure).  
2. **Mohamed Hamed** can start immediately in parallel (Users).  
3. **Fatma Mohamed** starts after Infrastructure is ready.  
4. **Hagar Mohamed** starts after Courses are done.  
5. **Mahmoud Hegazy** starts last, after Sessions + Users are finished.  
