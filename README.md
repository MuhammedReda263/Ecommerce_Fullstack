# Ecommerce System – .NET 8 Web API + Angular 17

This repository contains a **full-stack Ecommerce application** built using **.NET 8 Web API** and **Angular 20**.  
The solution demonstrates modern software engineering practices such as **Clean Architecture**, **Redis Caching**, **JWT Authentication**, **Email Services**, **Stripe Payment**, and a complete Angular client with guards, interceptors, and responsive UI.

---

## Features Implemented

### Authentication & Authorization
- Register, Login, Logout  
- JWT Authentication  
- Roles: **Admin** & **User**  
- Email verification (activate account)  
- Forget password + reset password email  

---

## Ecommerce Functionalities

### For Users
- Add items to basket  
- Remove/update basket items  
- Search products  
- Filter products (category, price, etc.)  
- **Create Orders**  
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
- Repository-like abstraction via specifications  

### AutoMapper
Mapping Entities ↔ DTOs.

### Redis Cache
- Caching products  
- Caching user basket  
- Improved app performance  

### JWT Authentication
- Access tokens  
- Secure endpoints  
- Role-based authorization  

### Email Service
- Activation emails  
- Forgot password workflow  

### Stripe Payment Integration
- Client + Server payment workflow  

### Pagination Support
- Server-side pagination  
- Filtering + sorting  

### Asynchronous Programming
- Fully async APIs for better scalability  

---

## Frontend Features (Angular 17)

### UI & Styling
- HTML, CSS, Bootstrap  
- Clean, responsive layout  

### Angular Features Used
- Components, Modules, Routing  
- **HttpClient** for communicating with API  
- **Interceptors** (JWT, Loading Spinner)  
- **Guards** (AuthGuard, AdminGuard)  
- **RxJS** for state & async streams  
- Custom services  
- Reactive forms  

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
- **AutoMapper**  
- **Redis**  
- **JWT Authentication**  
- **SQL Server**  
- **Stripe Payments**  
- **Clean Architecture**  
- **Asynchronous Programming**

### Frontend
- **Angular 17**  
- **RxJS**  
- **Bootstrap**  
- **HTML / CSS**  
- **Guards & Interceptors**  
- **HttpClient**


