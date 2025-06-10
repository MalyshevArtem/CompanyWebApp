# Company Web App: Employee Portal

The **Company Web App** is a mock, internal web application designed to provide employees with convenient access to their personal HR and work-related data.
Employees can view information such as vacation balances, sick leave, pay stubs, training courses, certifications, business trip details, and more.


## How It Works

This solution consists of two distinct projects:
1.  A **data access layer** responsible for communication with an Oracle database.
2.  A **Blazor Server application** that serves the user interface.

To access the web app, a user must be a registered employee of the company. Upon initial login, employees receive a temporary password at work, which they are then prompted to change before gaining full access.

A single, dedicated database user handles all data queries. However, each query is dynamically filtered by the authenticated user's worker ID, ensuring that employees can only view their own specific data.

## App Screenshots

Here are some screenshots showcasing the application's user interface:
 <br>
 <br>
 <br>

![1](https://github.com/user-attachments/assets/516f3489-713d-44f8-9aa9-459211b99d17)
 <br>
 <br>
 <br>
 <br>
![2](https://github.com/user-attachments/assets/dbbd258f-de71-427a-99d1-209ef32e8c45)
 <br>
 <br>
 <br>

## Technical Details & Dependencies

This is a **Blazor Server** application built on **.NET 8.0**. It uses the following key libraries:

* [MudBlazor](https://www.nuget.org/packages/MudBlazor) - a library for building web apps without having to struggle with CSS and Javascript
* [BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next) - a library for encrypting passwords
* [Oracle.ManagedDataAccess.Core](https://www.nuget.org/packages/oracle.manageddataaccess.core) - a library for getting access to Oracle databases
* [Dapper](https://www.nuget.org/packages/dapper/) - a simple micro-ORM used to simplify working with ADO.NET
