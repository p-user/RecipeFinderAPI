# RecipeFinder API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
<!-- Optional: Add build status badge if you have CI/CD setup -->
<!-- [![Build Status](YOUR_BUILD_STATUS_BADGE_URL)](YOUR_BUILD_PIPELINE_URL) -->

## 📌 Project Description

RecipeFinder is a simple RESTful API built using .NET 8  designed to manage cooking recipes. Its primary purpose is to serve as a practical demonstration of:

*   **Clean Architecture:** Separating concerns into distinct layers (Domain, Application, Infrastructure, Presentation(Api)).
*   **Command Query Responsibility Segregation (CQRS):** Using separate models/paths for reading (Queries) and writing (Commands) data, implemented with MediatR.
*   **Repository Pattern:** Abstracting data access logic.
*   **RESTful API Design:** Providing standard HTTP endpoints for interaction.
*   **Basic Authentication/Authorization:** Implementing JWT-based security using IdentityServer for securing certain endpoints.

This project focuses on code structure, maintainability, and demonstrating these patterns rather than being a feature-complete, production-ready application.

## 🏗️ Architecture

The project follows the principles of Clean Architecture:

1.  **Domain:** Contains core business entities (e.g., `Recipe`), value objects, domain events, and interfaces for repositories. Has no dependencies on other layers.
2.  **Application:** Contains application logic, including CQRS handlers (Commands, Queries), validation logic, DTOs, and interfaces for infrastructure services (e.g., `IApplicationDbContext`, `IIdentityService`). Depends only on the Domain layer.
3.  **Infrastructure:** Implements interfaces defined in the Application layer. Contains data access logic (EF Core DbContext, Repositories), implementations for external services (like Identity services). Depends on the Application layer.
4.  **Presentation (API):** The entry point of the application. An ASP.NET Core Web API project containing Controllers, middleware, and configuration. Depends on the Application and Infrastructure layers (for DI setup).

## 🚀 Technologies Used

*   **.NET 8**
*   **ASP.NET Core:** For building the REST API.
*   **Entity Framework Core:** ORM for data access.
*   **MediatR:** For implementing CQRS pattern.
*   **FluentValidation:** For request validation.
*   **Automapper**: For mapping between layers.
*   **IdentityServer** (Duende IdentityServer): For Authentication.
*   **SQL Server** : Database (requires local installation).
*   **Swagger:** For API documentation and testing.

## 📌 Features

*   **CRUD Operations for Recipes:** Create, Read, Update, and Delete recipes.
*   **Layered Architecture:** Clear separation between business logic, data access, and API presentation.
*   **CQRS Implementation:** Uses MediatR library to handle commands and queries distinctly.
*   **Data Persistence:** Uses Entity Framework Core with the Repository pattern.
*   **Validation:** Uses FluentValidation for validating incoming requests (Commands).
*   **Authentication:** Secures endpoints using JWT Bearer tokens issued by a configured Identity Provider (IdentityServer - running locally).

