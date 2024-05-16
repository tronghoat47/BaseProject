# BaseProject

Welcome to BaseProject! This repository serves as the foundation for our solution, comprising four main projects: `Domain`, `Infrastructure`, `Application`, `WebAPI`, and `Test`.
It also includes Authentication and Authorization using JWT.

## Overview

BaseProject is structured to provide a modular and scalable architecture for building robust software solutions. Each project within this repository fulfills a specific role in our system:

- **Domain**: This layer does not depend on any other layer. This layer contains entities, enums, specifications etc.
Add repository and unit of work contracts in this layer.
  
- **Infrastructure**: This layer contains database related logic (Repositories and DbContext), and third party library implementation (like logger and email service).
This implementation is based on domain and application layer.
  
- **Application**: This layer contains business logic, services, service interfaces, request and response models.
Third party service interfaces are also defined in this layer.
This layer depends on domain layer.
  
- **WebAPI**: Offers a RESTful API interface to interact with our system. This project serves as the entry point for external clients and facilitates communication with our application.

- **Test**: The Test project within BaseProject is dedicated to ensuring the quality and reliability of our software solution.

## Getting Started

To start using BaseProject, follow these steps:

1. **Clone the Repository**: Clone this repository to your local machine using the following command:

   ```
   git clone https://github.com/your-username/BaseProject.git
   ```

2. **Build the Solution**: Open the solution file (`BaseProject.sln`) in your preferred IDE or text editor and build the solution to ensure all dependencies are resolved.

3. **Explore the Projects**: Familiarize yourself with the structure and content of each project within the solution. Dive into the codebase to understand how different components interact with each other.

4. **Customize and Extend**: Modify the projects according to your requirements. Add new features, refactor existing code, or extend functionality to meet the needs of your application.

5. **Run and Test**: Run the solution locally and test the functionality of each project. Ensure that everything behaves as expected and meets the desired criteria.

## Technologies Used:

- .Net 8
- Rest API
- Entity Framework
- Swagger
- Xunit
- Moq
- Generic Repository Pattern
- Specification pattern
- Authentication JwtBearer