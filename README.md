# MoneyBee

**.NET 8 • Onion Architecture • CQRS (MediatR) • EF Core • JWT • Ocelot**

This repository consists of 4 services:

| Module | Purpose | Default Port | Swagger |
|---|---|---:|---|
| **AuthModule** | Employee login, JWT issuance & validation | **5203** | `http://localhost:5203/swagger` |
| **CustomerModule** | Customer create/update/delete | **5202** | `http://localhost:5202/swagger` |
| **TransferModule** | Money transfer, cancel, history/limits | **5201** | `http://localhost:5201/swagger` |
| **GatewayModule** | Single entry (Ocelot), routing & token validation | **5209** | Health: `/api/health` |

> If you use different ports, update `ocelot.json` under **Gateway** accordingly.

---

## Table of Contents
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Endpoints & Examples](#endpoints--examples)
  - [AuthModule](#authmodule)
  - [CustomerModule](#customermodule)
  - [TransferModule](#transfermodule)
  - [GatewayModule](#gatewaymodule)
- [Tests](#tests)

---

## Prerequisites
- **.NET 8 SDK**
- **SQL Server** (local or Docker)
- (for CLI migrations) **dotnet-ef**:
  ```bash
  dotnet tool install --global dotnet-ef  || dotnet tool update --global dotnet-ef
  # macOS/Linux PATH:
  echo 'export PATH="$HOME/.dotnet/tools:$PATH"' >> ~/.zshrc && source ~/.zshrc
  ```

---

## Quick Start
```bash
# 1) Restore + Build
dotnet restore
dotnet build

# 2) Create databases

# 3) Run services
dotnet run --project src/MoneyBee.AuthModule.API         # :5203
dotnet run --project src/MoneyBee.CustomerModule.API     # :5202
dotnet run --project src/MoneyBee.TransferModule.API     # :5201
dotnet run --project src/MoneyBee.GatewayModule.API      # :5209

# 4) Health
curl http://localhost:5209/api/health
```

---

## Endpoints & Examples

> The following examples go through the **Gateway**. You can also call the services directly via their ports.

### AuthModule

**POST** `/auth/login` → issues JWT  
_Request_
```http
POST http://localhost:5209/auth/login
Content-Type: application/json

{ "username": "alice", "password": "P@ssw0rd" }
```
_Response (200)_
```json
{
  "accessToken": "<JWT>",
  "expiresAtUtc": "2025-08-25T12:34:56Z",
  "tokenType": "Bearer"
}
```

**POST** `/auth/validate` → validates token  
_Request_
```http
POST http://localhost:5209/auth/validate
Content-Type: application/json

{ "token": "<JWT>" }
```
_Response (200)_
```json
{
  "isValid": true,
  "subject": "1c2d3e4f-...",
  "username": "alice",
  "expiresAtUtc": "2025-08-25T12:34:56Z",
  "error": null
}
```

**Employee Management**
- **GET** `/auth/employees/{id}`
- **GET** `/auth/employees?page=1&pageSize=50`
- **POST** `/auth/employees`
  ```json
  { "username": "bob", "password": "Secret123!", "isActive": true }
  ```
- **PUT** `/auth/employees/{id}`
  ```json
  { "id": "GUID", "username": "bob2", "newPassword": "NewSecret!" }
  ```
- **DELETE** `/auth/employees/{id}`

---

### CustomerModule

> Authorization: Bearer `<JWT>`

Fields: `firstName`, `lastName`, `phoneNumber`, `address`, `dateOfBirth`, `idNumber`

- **POST** `/customers`
  ```http
  POST http://localhost:5209/customers
  Content-Type: application/json
  Authorization: Bearer <JWT>

  {
    "firstName": "Ahmet",
    "lastName": "Yilmaz",
    "phoneNumber": "+90 555 000 0000",
    "address": "Ataturk St. No:1 Istanbul",
    "dateOfBirth": "1990-01-01",
    "idNumber": "12345678901"
  }
  ```
  _Response (201)_
  ```json
  {
    "id": "GUID",
    "firstName": "Ahmet",
    "lastName": "Yilmaz",
    "phoneNumber": "+90 555 000 0000",
    "address": "Ataturk St. No:1 Istanbul",
    "dateOfBirth": "1990-01-01",
    "idNumber": "12345678901",
    "createdAtUtc": "2025-08-25T09:15:30Z"
  }
  ```

- **GET** `/customers/{id}`
- **GET** `/customers?page=1&pageSize=50`
- **PUT** `/customers/{id}`
- **DELETE** `/customers/{id}`

---

### TransferModule

> Authorization: Bearer `<JWT>`

Fields: `senderCustomerId`, `receiverCustomerId`, `amount`, `transactionFee`, `transactionCode`, `status`

- **POST** `/transfers`
  ```http
  POST http://localhost:5209/transfers
  Content-Type: application/json
  Authorization: Bearer <JWT>

  {
    "senderCustomerId": "GUID-1",
    "receiverCustomerId": "GUID-2",
    "amount": 1500.00
  }
  ```
  _Response (201)_
  ```json
  {
    "id": "GUID",
    "senderCustomerId": "GUID-1",
    "receiverCustomerId": "GUID-2",
    "amount": 1500.00,
    "transactionFee": 15.00,
    "transactionCode": "MB-839201",
    "status": "Created",
    "createdAtUtc": "2025-08-25T09:30:00Z"
  }
  ```

- **GET** `/transfers/{id}` — details
- **POST** `/transfers/{id}/cancel` — cancel transfer
  ```http
  POST http://localhost:5209/transfers/SOME-GUID/cancel
  Authorization: Bearer <JWT>
  ```
  _Response (200)_
  ```json
  { "id": "GUID", "status": "Cancelled" }
  ```

- **GET** `/transfers?customerId=<GUID>&from=2025-08-01&to=2025-08-31` — customer history
  ```json
  [
    { "id":"GUID-A","amount":750.00,"status":"Completed","createdAtUtc":"2025-08-10T08:00:00Z" },
    { "id":"GUID-B","amount":1500.00,"status":"Cancelled","createdAtUtc":"2025-08-12T10:00:00Z" }
  ]
  ```

---

### GatewayModule

- Health: **GET** `/api/health` → `{ "status": "ok", "service": "gateway" }`
- Ocelot (default) mappings:
  - `POST /auth/login`  → `http://localhost:5203/api/auth/login`
  - `POST /auth/validate` → `http://localhost:5203/api/auth/validate`
  - `GET/POST/PUT/DELETE /customers{*}` → `http://localhost:5202/api/customers{*}`
  - `GET/POST/PUT/DELETE /transfers{*}` → `http://localhost:5201/api/transfers{*}`
  - `GET/POST /auth/employees{*}` → `http://localhost:5203/api/employees{*}`

Edit `src/MoneyBee.GatewayModule.API/ocelot.json` as needed.

---

## Tests
- **NUnit** tests
- Run:
  ```bash
  dotnet test
  ```
