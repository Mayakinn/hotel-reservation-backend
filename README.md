# Hotel Reservation Backend
This is the backend for the Hotel Reservation system. It uses ASP.NET Core 9, Entity Framework Core, and SQL Server.

## Prerequisites
* .NET 9 SDK
* SQL Server

## Project setup
### Clone the repository
```
git clone https://github.com/Mayakinn/hotel-reservation-backend.git
cd hotel-reservation-backend
```
### Create a .env file in the project root
```
DB_SERVER=localhost #Server
DB_NAME=HotelReservationDB # DB name
DB_USER=sa # DB username
DB_PASSWORD=YourPassword # DB password
```
### Install dependencies
```
dotnet restore
```
### Run the project
```
dotnet run
```
