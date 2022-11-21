=======================================================
Phone Book App - Assessment
=======================================================

1. Download the zip file from Github
2. Extract the zip file

========== Technologies and tools used ================

1. Visual Studio 2015 Professional Edition
2. .Net Framework 4.5
3. Web API 2.0 - Restfull services
4. Autofac 3.4
5. Angularjs
6. Automapper
7. Entity Framework 6
8. Linq
9. Sql Server database 2012
10. MS Tests for Unit Testing

========== Configure steps to run the application =========

1. Install above required softwares
2. Restore database file in Sql Server(PhoneBookDB backup File available in documents folder in the solution)
3. Change connectionstring datasource in the web.config and app.config files

========== Patterns and methodologies used =========

1. SOLID Principles
2. Facade Design pattern
3. Autofac for the unity container - constructor injection
4. Automapper - to map database objects to the entity models
5. Repository Pattern
6. Unit of work

========== Architecture =========

1. BusinessModels - It contains Entity Framework and repository classes
2. BusinessEntities - It contains entity models
3. BusinessServices - It contains services for the data retrieval
4. GoodExample  - It cotains Controllers and Angualrjs UI
5. UnitTesting - Test cases

========== User manual ==========

1. Download user manual from the folder Documents in the solution folder
