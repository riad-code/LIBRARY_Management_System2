
# 📚 Library Management System

> 💻 **Developed as part of the RPL-ICT-WDDF-L3-001545 module**
> 👨‍💻 **Created by:** Abu Hanif Riad
> 📅 **Submission Date:** January 2025
> 🧑‍🏫 **Platform:** ASP.NET Core MVC, AdminLTE Dashboard, Entity Framework Core, SQL Server
> 🧪 **Tools:** Visual Studio 2022, jQuery, AJAX, SweetAlert2


## 📝 Project Objective

The goal of this project was to design and develop a **role-based digital Library Management System** that supports Admin, Librarian, and Member functionalities. The system handles everything from **book inventory management, borrowing/return processes, user management, fine calculation, and report generation**, offering a complete solution for real-life library operations.

This project includes a clean dashboard UI, real-time interactions using AJAX & SweetAlert2, and secure user access with ASP.NET Identity.

---

## 🧰 Tools & Technologies Used

### 🔧 Backend & Logic:

* ASP.NET Core MVC (Razor Pages)
* Entity Framework Core (Code-First)
* ASP.NET Identity for Authentication/Authorization
* MS SQL Server for database

### 🖥️ Frontend & UI:

* HTML5, CSS3
* Bootstrap (via AdminLTE Template)
* jQuery, AJAX
* SweetAlert2 (for alerts & confirmations)

### 🛠️ Development Tools:

* Visual Studio 2022
* SQL Server Management Studio (SSMS)
* NuGet Package Manager

---

## 📌 Project Structure

```
/Controllers
    - BookController.cs
    - BorrowController.cs
    - UserManagementController.cs
    ...
/Models
    - Book.cs
    - BorrowRecord.cs
    - ApplicationUser.cs
    ...
/Views
    /Book
        - Create.cshtml
        - Edit.cshtml
        - Index.cshtml
    /Borrow
        - ReturnBooks.cshtml
        - PendingRequests.cshtml
    /Shared
        - _AdminLayout.cshtml
        - _LibrarianLayout.cshtml
        - _UserLayout.cshtml
...
```

---

## 🧪 Key Features & Implementation

### ✅ Role-Based Access (Admin, Librarian, Member)

* Admin can manage all users, books, categories, requests, and reports.
* Librarian can issue/return books and approve/reject requests.
* Members can browse, request, and track borrowed books.

### ✅ Book Management (Admin & Librarian)

* Add/edit/delete books with:

  * Title, Author, ISBN, Publisher
  * Category, Published Date, Cover Image
* Auto-calculate available copies
* Filter/search by title, author, category

### ✅ Book Categories (Admin Only)

* Add/edit/delete categories
* Prevent deletion if books exist under a category

### ✅ Borrow & Return System

* Members request books
* Librarians/Admins approve and issue with due dates
* Returns tracked with auto-fine calculation (৳10/day)

### ✅ Book Request Module

* Request status: Pending, Approved, Rejected
* AJAX-powered approval and rejection with confirmation popups

### ✅ Fine Management

* Late return fine system (admin-configured)
* Fine shown during return
* Admin can waive fines

### ✅ AJAX + SweetAlert2 Integration

* All major operations like create/edit/delete/approve/return use AJAX and SweetAlert2
* Provides smooth, real-time feedback to users

### ✅ Dashboard with Stats (Admin/Librarian)

* Total Books, Members, Issued Books
* Today’s Activities
* Overdue Books
* Top 5 Most Borrowed Books

### ✅ Reports & Logs (Admin Only)

* Issued/Returned Books Report
* Fine Collection Summary
* Member Borrowing Summary


## 🖼️ UI Preview

* Responsive Admin & Librarian Dashboards (AdminLTE)
* Sidebar navigation by role
* AJAX-enhanced tables and modals
* SweetAlert2 confirmation & result alerts
* Member-friendly book search & request system


## 🔐 Authentication & Member Profile

* Secure Login, Logout, Password Reset
* Role-based navigation and page access
* Members can:

  * View/edit profile
  * Track current borrowings
  * View fine amounts
  * See full history

## ✅ Key Highlights

* 🔒 Role-Based Access Control (RBAC) using ASP.NET Identity
* 📦 Real-time availability tracking with auto fine calculation
* ⚡ AJAX + SweetAlert2 for all operations (edit, delete, return, request)
* 📊 Interactive Admin/Librarian Dashboards (AdminLTE)
* 🗃️ EF Core code-first DB + migrations
* 📱 Responsive and accessible design

