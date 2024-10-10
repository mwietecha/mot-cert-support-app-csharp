# TimeSheet Manager
This project is a simple time sheet manager application. It allows users to log in to the application and add time sheet entries. Users can log in as either standard or admin users with admin users being able to create new timesheets, users and view reports.

## Getting Started

### Prerequisites
To build and run this project you will require the following:
- DotNet Core 8

### Running project
Assuming DotNet Core is installed correctly on your machine. The following is required to start the application up:

```Bash
cd Timesheet
dotnet run
```

Once complete, the application will be available via http://localhost:8080

Have fun 😬

### Running tests locally
You should be able to run tests from the IDE, but if you're working in a terminal you can always `dotnet test` from inside the Timesheet.Test folder.

Note that for the API and E2E tests the application should be already running when you start them.