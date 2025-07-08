📚 Project Highlight: Library Management System 
🧑‍💻 Real-World Academic Project at FiroTech Limited | 💡 ASP.NET Core MVC, Entity Framework, MS SQL Server
 🛠️ Tools & Tech Stack: ASP.NET Core MVC, Razor Pages, Entity Framework Core, jQuery, AJAX, SweetAlert2
 🗃️ Database: MS SQL Server | 🔐 Authentication: ASP.NET Identity
 🖥️ IDE: Visual Studio 2022
💡 Project Overview
This project is a complete, role-based Library Management System built using ASP.NET Core MVC , focused on real-life use cases for Admins, Librarians, and Members. It covers book inventory management, borrowing/return operations, fines, and user authentication with a clean and interactive UI powered by jQuery AJAX + SweetAlert2.
It showcases my ability to build full-stack web applications with layered architecture and modern UI interaction.
🎯 Project Objectives
✅ Build a full-featured, secure Library Management System
 ✅ Implement user roles: Admin, Librarian, Member
 ✅ Create clean dashboard UIs using AdminLTE
 ✅ Use AJAX + SweetAlert2 for smooth UX in CRUD operations
 ✅ Auto-calculate fines and availability
 ✅ Enable members to borrow, return, and view books with login
👥 System Users & Their Permissions
🔒 Admin
Manage users, roles, books, categories
Approve/reject book requests
View reports, reset passwords, dashboard stats
👨🏫 Librarian
Manage books, issue/return, track borrowing
Handle member requests
Generate daily activity reports
📗 Member
Register/login
Search/view books and request borrow
Track borrowing history and fines
Update own profile
🧩 Module-Wise Features
📚 Book Management
Add/edit/delete books & cover images
Auto-calculate available copies
Search/filter by title, author, ISBN, category
🏷️ Book Categories
CRUD on categories with safe-delete logic
🔍 Book Search (Member & Guest)
Filter by category, availability, author
View book details & request to borrow
📦 Borrow & Return System
Members request borrow
Librarians approve/return with due dates
Auto fine calculation (৳10/day late)
💳 Fine Management
Admin-defined fine rate
Fines shown during return
Admin can waive fines
👤 User Management (Admin only)
Add/edit users, assign roles
Reset passwords, deactivate users
View individual borrowing histories
📊 Dashboard
Total books, users, issued books, overdue stats
Top 5 most borrowed books
Today’s activity & pending requests
📄 Reports & Logs
Issued/returned books (weekly/monthly)
Overdue report, fine summary, member logs
Export to PDF/Excel (planned)
📝 Book Requests
Member initiates request
Admin/Librarian approves/rejects with status update
🔐 Authentication & Authorization
ASP.NET Identity-based
Role-based UI access
Secure login/logout, password management
👤 Member Profile Page
Edit profile
See current borrowings, fines, and history
