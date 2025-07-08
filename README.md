# Vypex Coding Challenge

## Problem statement

You are provided with a fully functional project management application that allows users to view and edit projects with their associated tasks. While the application works, there are several architectural improvements that need to be made to align with modern best practices and framework capabilities.

## What's provided

A working project management application with:
- **Backend**: ASP.NET Core 9 Web API with Entity Framework Core and SQLite database
- **Frontend**: Angular 20 application with Ng-Zorro UI components

The application currently supports:
- Viewing a list of projects with an aggregation of their total points.
- Searching projects by key
- Editing projects and their associated tasks
- Dynamic task management (add/remove tasks)

## What we're evaluating
* Deep understanding of Angular 19+ features and best practices
* Knowledge of Angular Forms API and component architecture
* Understanding of clean architecture principles in .NET
* Ability to identify and fix architectural issues
* Sound abstractions and SOLID principles

## 🛠️ Technical Stack
* Backend
  * ASP.NET Core 9 Web API
  * Entity Framework Core with SQLite
  * Clean/Onion Architecture with separate Domain, Application, Infrastructure, and API layers
  * Service available at https://localhost:7189
    * OpenAPI document: https://localhost:7189/openapi/v1.json
    * API browser: https://localhost:7189/scalar/v1
* Frontend
  * Angular 20 with zoneless change detection
  * Ng-Zorro/Ant Design UI components
  * Reactive Forms
  * Tailwind CSS for styling

## ✅ Tasks

### 1. Modernise the Projects Component
The `Projects` component currently uses traditional RxJS patterns. Update it to leverage the latest Angular v19+ features for a more modern and efficient implementation.

### 2. Refactor the Project Tasks Form Component
Currently, `ProjectTasksForm` component is implemented as a reusable form component but it doesn't utilise Angular's built-in capabilities for seamlessly integrating into Angular forms framework. Please refactor this component to follow Angular best practices for custom re-usable form components.

### 3. Fix Repository Abstraction Leak
The current implementation of `ProjectRepository` on the backend contains a leaky abstraction. It needs to know about business logic in the application layer in order to include the required data. For example, if the business logic changes to make use of `ProjectManager`, then the repository needs to be updated to include the `Manager` relationship.

Please refactor to plug this leak. Feel free to change the abstraction or the methods in the repository to create a cleaner separation of concerns.

### 4. Bonus
Feel free to improve any of the existing code to demonstrate your skills. This is completely optional.


## 🚫 Out of scope
* Adding new features
* Error handling on the backend.
* Modifying the database schema
* Custom CSS beyond existing Tailwind utilities
* Unit tests or any other form of testing

## 💡 Tips
* The SQLite database is already configured and seeded with sample data
* Focus on architectural improvements rather than adding features
* The existing functionality should continue to work after your refactoring

## Running the Application

### Backend
```bash
cd Vypex.CodingChallenge.Service
dotnet run --project .\Vypex.CodingChallenge.Service\Vypex.CodingChallenge.Service.csproj --interactive
```

### Frontend
```bash
cd Vypex.CodingChallenge.Frontend
yarn
yarn start
```

## Submission
Once you've completed the challenge, create a zip archive of your submission and send it to the recruiter.

Good luck!
