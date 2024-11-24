# FlightService - Airline Reservation System
**FlightService** manages flight schedules and availability in the Airline Reservation System. It provides CRUD operations for flight details such as adding new flights, updating schedules, retrieving flight information, and deleting flights. The service listens on port 5003 but can also be accessed through the **API Gateway** running on port 5000.

### Key Features

- **Flight Management**: Create, update, retrieve, and delete flight details.
- **Schedule Management**: Manage flight schedules and availability.
- **API Gateway Access**: The service is also accessible via the API Gateway on port 5000.

### Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/srvignesh5/FlightService.git
   cd FlightService
   ```
2. **Restore the NuGet packages**
      ```bash
    dotnet restore
   ```
4. **Run the FlightService**
   ```bash
    dotnet run
   ```
   The service will start and listen on https://localhost:5003. However, it is also accessible via the API Gateway on port 5000.

Access the API documentation (Swagger UI) for FlightService directly
```bash
https://localhost:5003/swagger
```
Access the API through the API Gateway on port 5000:
```bash
https://localhost:5000
```
