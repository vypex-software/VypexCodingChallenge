# Vypex Coding Challenge

## Problem Statement

HR require a portal to manage employees and their leave days. The portal should be able to display a list of employees, edit an employee including their leave days, and show the total leave days taken by an employee on the employee list.

## What's provided

You're inheriting a partially completed project. The authors may or may not have been a bit sleepy after Thursday's steak special team lunch at the Carlton Brewhouse.

## What we're evaluating
* Depth of knowledge of .NET and Angular frameworks.
* Solution architecture and project layout.
* Ability to write the simplest solution that meets all the requirements.
* Sound abstractions.
* Good practices, SOLID and clean code.

## 🛠️ Provided
* Backend
  * Scaffolded ASP.NET Core 9 Web API with an endpoint that returns a mock list of employees.
  * Pre populated SQLite database that is currently NOT connected to the endpoint.
  * Basic Employee model class with Id and Name properties registered in EF Core.
  * By default the service is available at https://localhost:7189
    * OpenAPI document https://localhost:7189/openapi/v1.json
    * API browser https://localhost:7189/scalar/v1
  * `dotnet ef` tool has been installed as part of the project which can be run from the `Vypex.CodingChallenge.Service` folder.
* Frontend
  * Scaffolded Zoneless Angular 19 frontend.
  * A skeleton API service that communicates with the backend.
  * A skeleton employees dashboard.
  * A skeleton modal for editing an employee and their leave days.
  * NgZorro/Ant Design for UI components.
    * Feel free to add styles for any components you may use into */src/styles/antd.less* if they're not already included.

## ✅ Tasks

#### Use provided SQLite database.

#### Make required API changes to support the frontend.

#### Employees list component
* Use new Angular 19 resources.
* Add a refresh/reload button to refetch the list of employees.
* Handle potential API errors.
* Implement search by employee name functionality

#### Create a re-usable Angular Form Control component
* Create an employee leave form control as a separate and re-usable component.
* The user can dynamically add/modify multiple leave entries for an employee.
* Leave days can overlap (for simplicity)
* Start date is required.
* End date must be greater than start date.

#### Edit employee component
* Use the above re-usable leave form control component in `EditEmployeeComponent`
* Update `EditEmployeeComponent` to communicate with the API.
* Use Angular forms and Ant Design components for user input.

## 🚫 Out of scope
* Editing employee properties other than leave.
* Business days or holiday considerations.
* State management.
* Custom CSS (use existing Tailwind utilities).
* Additional database table columns beyond basic requirements.
* Validation or any other error handling on the backend.
* Unit tests or any other form of testing.

## 💡Tips
If you want to regenerate the SQLite db use the following command. The `--startup-project` and `--project` properties are important because of the project structure.
```bash
dotnet ef database update --startup-project .\Vypex.CodingChallenge.Service\Vypex.CodingChallenge.Service.csproj --project .\Vypex.CodingChallenge.Infrastructure\Vypex.CodingChallenge.Infrastructure.csproj
```

## Submission
Once you've completed the test, create a zip archive of your submission and send it to the recruiter.

Thank you and good luck!
