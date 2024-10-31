# SSU Entrance Exam Application and Assessment Management

## Overview
A fully integrated online application system developed to simplify the entrance examination process at Samar State University. This system replaces traditional methods such as Google Forms and manual paper-based processes, offering a more organized and efficient solution for applicants and administrators to handle exam scheduling, score tracking, and admissions data.

## Features

### Admin Side
- **Schedule Management**: Create and manage examination schedules, including assigning venues, times, and specific campuses. This feature supports multiple campus configurations.
- **Score Recording and Evaluation**: Input scores for each applicant, encompassing both examination and interview scores. The system automatically calculates pass/fail ratings based on predefined criteria and combined results, reducing the likelihood of errors.
- **Applicant Information Printing**: Generate and print applicant information using templates that adhere to university forms, ensuring compliance and consistency.

### Applicant Side
- **Application Submission**: Applicants can submit their online applications for the entrance examination, capturing all necessary information in an efficient manner.
- **Schedule Selection**: Choose preferred examination schedules based on availability and convenience.
- **Campus Selection**: Specify which campus to take the examination, providing flexibility and personalization.
- **Program/Course Selection**: Select programs or courses during the application process with predefined options and descriptions.

## Technologies Used

- **Framework**: Blazor Server (.NET 7)
- **UI Component Library**: [MudBlazor](https://mudblazor.com/)
- **Database**: Microsoft SQL Server (MSSQL)
- **Testing Database**: SQLite In-Memory

## Architecture

### Core Design Patterns
This application is built on a modular, maintainable architecture that uses several design patterns to ensure a clean separation of concerns and best practices:

- **CQRS (Command Query Responsibility Segregation)**: Separates the read and write operations, allowing each to be optimized independently. Command handlers process data-changing operations, while query handlers handle data retrieval.
- **Repository Pattern and Unit of Work**: Encapsulates data access logic and transactions, ensuring consistency and an abstraction layer over database operations. This pattern simplifies testing by providing an easy way to mock data access layers.
- **Dependency Injection**: All services and components are injected into the application, making the system more modular, testable, and adhering to SOLID principles.

### Application Structure
- **Domain Layer**: Contains core business logic, including domain entities and value objects.
- **Application Layer**: Hosts application-specific services, including command and query handlers. MediatR is used to dispatch these handlers.
- **Infrastructure Layer**: Contains data access logic, configurations, and repository implementations.
- **Presentation Layer**: Built using Blazor Server, it serves as the frontend, providing interactive UIs for both administrators and applicants.

## Testing

The project includes a comprehensive suite of unit and integration tests to ensure that all components behave as expected under various scenarios.

### Test Frameworks and Tools Used
- **xUnit**: The main testing framework used to organize and run test cases. It provides support for both unit and integration tests with minimal setup.

### Test Coverage
- **Unit Tests**: Focus on verifying the correctness of individual methods and components, such as:
  - Validation logic for domain entities.
  - Command and query handler execution with predefined inputs.
  - Business logic for score calculation and applicant eligibility.
  
- **Integration Tests**: Validate the interaction between multiple components and ensure that the application behaves as expected in an integrated environment.
  - Utilizes SQLite in-memory database to simulate real-world scenarios without the overhead of interacting with a physical database.
  - Covers end-to-end testing of CRUD operations, command processing, and service interactions.

### Key Test Scenarios
- **Examination Command and Query Tests**: Verifies that commands related to examination scheduling, score recording, and applicant management execute as intended.
- **Interview Scoring Tests**: Ensures that interview scores are calculated correctly based on input criteria and that the results are stored and retrieved accurately.
- **Applicant Registration and Validation Tests**: Tests the registration process, ensuring that all applicant details are captured correctly and validated against business rules.
- **Schedule and Program Selection Tests**: Evaluates the user flow for selecting examination schedules and choosing desired programs, including validation and error handling.
