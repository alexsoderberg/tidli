# Tidli

Full-stack application with Angular frontend and .NET backend.

## Project Structure

```
/
├── apps/
│   ├── web-ui/          # Angular application
│   └── project-service/ # .NET Web API
├── docker-compose.yaml  # Development environment
├── package.json         # Workspace configuration
└── .gitignore          # Unified ignore rules
```

## Development

### Prerequisites
- Docker and Docker Compose
- Node.js 20+
- .NET 10.0 SDK

### Getting Started

1. **Start all services:**
   ```bash
   npm run dev
   ```

2. **Start in detached mode:**
   ```bash
   npm run dev:detached
   ```

3. **Stop services:**
   ```bash
   npm run down
   ```

### Individual Services

- **Frontend (Angular):** http://localhost:3000
- **Backend (.NET API):** http://localhost:8080

### Development Scripts

- `npm run build` - Build both projects
- `npm run test` - Run all tests
- `npm run lint` - Lint frontend code
- `npm run clean` - Clean build artifacts

### Project-specific Commands

```bash
# Frontend
npm run start --workspace=web-ui
npm run build --workspace=web-ui
npm run test --workspace=web-ui

# Backend
cd apps/project-service
dotnet run
dotnet build
dotnet test
```

## Architecture

- **Frontend:** Angular 21 with SSR support
- **Backend:** .NET 10.0 Web API
- **Containerization:** Multi-stage Docker builds
- **Networking:** Docker bridge network for service communication