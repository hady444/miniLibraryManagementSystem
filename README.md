# 📚 mini Library Management System — Implementation Summary

> 🏗️ Built using **ASP.NET MVC**, **Entity Framework Core (Code-First)**, and **In-Memory Database**.  
> Follows an **N-Tier Architecture** separating presentation, business logic, and data layers.

---

## 🚀 What We’ve Done

### 🧩 Project Structure
✅ Implemented a **three-layer architecture**:
- **Presentation Layer:** ASP.NET MVC (Controllers + Views + viewModels)
- **Business Layer:** Services & Interfaces (contains all logic)
- **Data Layer:** EF Core (Code-First + In-Memory DB)

✅ Added **Dependency Injection** to connect layers cleanly and decouple dependencies.

---

## 👨‍🏫 Author Management

✅ Created full CRUD operations:
- Add, Edit, Delete, List Authors

✅ Implemented strict **validation rules**:
- Full name must contain 4 parts (each ≥ 2 characters)
- Unique name and email
- Valid email format check
- Optional fields: Website, Bio (max 300 characters)

✅ Added **service layer (`IAuthorService`)** to handle all author-related logic.  
✅ Integrated **server-side validation** with clear UI feedback.  
✅ Displayed each author’s books in a **read-only list** view.

---

## 📘 Book Management

✅ Implemented CRUD operations:
- Add, Edit, Delete, and List Books

✅ Each Book includes:
- Title, Genre (Enum), Description, and Author association  
- Author selection via dropdown (populated dynamically)

✅ Added **Genre Enum** covering all 13 categories.  
✅ Added service layer (`IBookService`) for all book logic.

✅ Connected Books with Authors via navigation property and proper relationships in EF Core.

---

## 🏛️ Book Library (Borrowing System)

✅ Built **Book Library View** listing all books with their current **status** (Available / Borrowed).  
✅ Added dynamic **filtering** based on:
- Book Status  
- Borrow Date  
- Return Date

✅ Created Borrow and Return functionality:
- Borrow Book → sets `BorrowedDate`
- Return Book → sets `ReturnedDate` & updates availability
- Prevents borrowing if already checked out

✅ Added **Borrowing Service (`IBorrowingService`)** that encapsulates all borrowing rules and operations.

---

## 💡 JavaScript & jQuery Features

✅ Implemented **dynamic dropdown** in Borrowing page:
- Shows live status: "Available" or "Checked Out"
- Updates availability immediately when selecting a book

✅ Used **jQuery** for:
- Dynamic DOM updates  
- Smooth user interactions without page reloads  

---

## 🗃️ Database & Data Seeding

✅ Configured **Entity Framework Core In-Memory Database**.  
---

## 🧠 Business Logic Separation

✅ No logic written in controllers — all handled by Services.  
✅ Controllers only:
- Receive requests  
- Call appropriate service methods  
- Return data to Views  

✅ Added clear **interfaces** for each service (e.g., `IAuthorService`, `IBookService`, `IBorrowingService`)  
to maintain testability and flexibility.

---

## 🎨 User Interface

✅ Designed clean, responsive **Bootstrap-based UI**.  
✅ Used **Partial Views** to display:
- Author details
- Book details
- Borrowing status  
✅ Enhanced UX with **table styling**, **pagination**, and consistent layout.


---

## 🧩 Tools & Technologies Used

| Category | Technology |
|-----------|-------------|
| Framework | ASP.NET MVC 9 |
| ORM | Entity Framework Core (In-Memory) |
| Language | C# |
| UI | Bootstrap + jQuery |
| Architecture | N-Tier |
| IDE | Visual Studio 2022 |
| Version Control | Git + GitHub |
