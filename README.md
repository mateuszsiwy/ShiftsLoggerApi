# ShiftsLoggerApi

## Project Description
**ShiftsLoggerApi** is a console-based application for recording and managing worker shifts, with a Web API as the back-end and a console UI using the **Spectre.Console** library as the front-end. The application allows users to input, validate, and manage shift data, with all business logic handled through the API and data stored in a SQL Server database.

### Architecture Overview:
1. **Web API**: A lean API that handles CRUD operations for worker shifts, with business logic handled in separate services.
2. **Console UI**: A console application built using the **Spectre.Console** library for interaction and validation.

## Features
- Record, update, and delete worker shifts.
- Input validation handled in the console UI.
- API error handling with try-catch blocks around API calls to handle scenarios like the API being offline or returning errors.
- SQL Server as the database, using the Entity Framework Core "code-first" approach for database creation.
- Clean separation of concerns between the API and the console UI.

## Technologies
- **Back-End**: ASP.NET Core Web API, Entity Framework Core (code-first approach)
- **Front-End**: Console UI using **Spectre.Console** library
- **Database**: SQL Server

## Installation

### 1. Clone the repository:
```bash
git clone https://github.com/mateuszsiwy/ShiftsLoggerApi
cd ShiftsLoggerApi
