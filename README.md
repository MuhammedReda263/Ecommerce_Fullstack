# Ecommerce System – .NET 8 Web API + Angular 20

This repository contains a **full-stack Ecommerce application** built using **.NET 8 Web API** and **Angular 20**.  
The solution demonstrates modern software engineering practices such as **Clean Architecture**, **Repository & Unit of Work Patterns**, **Redis Caching**, **JWT Authentication**, **Email Services**, **Stripe Payment**, and a complete Angular client with guards, interceptors, and responsive UI.

---

## Features Implemented

### Authentication & Authorization
- Register, Login, Logout  
- JWT Authentication  
- Roles: **Admin** & **User**  
- Email verification (activate account)  
- Forgot password + reset password email  

---

## Ecommerce Functionalities

### For Users
- Add items to basket  
- Remove/update basket items  
- Search products  
- Filter products (category, price, etc.)  
- Create Orders
- View order history  
- Add shipping address  
- Choose delivery method  
- Checkout process  
- Payment using **Stripe**  
- Fully responsive UI  

### For Admin
- **CRUD Operations** for:  
  - Products  
  - Categories  
  - Delivery Methods  
- Admin-level secured endpoints  

---

## Backend Features (ASP.NET Core 8 Web API)

### Clean Architecture
- Layers: **API**, **Core**, **Infrastructure**
- Clear separation of concerns  

### Entity Framework Core
- Code-First Migrations  
- SQL Server  
- LINQ querying  

### **Repository Pattern & Unit of Work**
- Generic repository implementation  
- Unit of Work to manage transactions  
- Clean separation between data access and business logic  

### AutoMapper
Mapping Entities ↔ DTOs.

### Redis Cache
- Caching products  
- Caching user basket  
- Improved performance & scalability  

### JWT Authentication
- Access tokens  
- Role-based authorization  
- Secure endpoints  

### Email Service
- Activation emails  
- Forgot password email workflow  

### Stripe Payment Integration
- Secure client + server payment flow  
- Session creation & order confirmation  

### Pagination Support
- Server-side pagination  
- Filtering, searching & sorting support  

### Asynchronous Programming
- Fully asynchronous services and controllers  

---

## Frontend Features (Angular 17)

### UI & Styling
- HTML, CSS, Bootstrap  
- Clean and responsive user interface  

### Angular Features Used
- Components, Modules, Routing  
- **HttpClient** for API communication  
- **Interceptors** (JWT, Loading Spinner)  
- **Guards** (AuthGuard, AdminGuard)  
- **RxJS** for async streams and state handling  
- Custom Angular services  
- Reactive Forms  

### User Experience
- Login / Register  
- Product listing with search & filter  
- Basket sidebar  
- Checkout + payment  
- **Create Order workflow**  
- Order history page  

---

## Technologies Used

### Backend
- **.NET 8 Web API**  
- **Entity Framework Core**  
- **Repository Pattern**  
- **Unit of Work Pattern**  
- **AutoMapper**  
- **Redis**  
- **JWT Authentication**  
- **SQL Server**  
- **Stripe Payments**  
- **Clean Architecture**  
- **Asynchronous Programming**

### Frontend
- **Angular 20**  
- **RxJS**  
- **Bootstrap**  
- **HTML / CSS**  
- **Guards & Interceptors**  
- **HttpClient**
