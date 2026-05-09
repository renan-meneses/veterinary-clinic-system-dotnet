# Veterinary Clinic Management System

Sistema completo para gerenciamento de clínica veterinária com arquitetura moderna em .NET 8, React e PostgreSQL.

## Arquitetura

```
veterinary-clinic-system/
├── backend/          # API Principal (.NET 8)
│   ├── VeterinaryClinic.Api          # Controllers, Middleware
│   ├── VeterinaryClinic.Domain       # Entities, Enums, Value Objects
│   ├── VeterinaryClinic.Application  # Commands, Queries, Handlers (CQRS)
│   ├── VeterinaryClinic.Infrastructure    # EF Core, Repositories
│   ├── VeterinaryClinic.Infrastructure.Queries  # Query repositories
│   ├── VeterinaryClinic.Infrastructure.Cache  # Redis cache
│   └── VeterinaryClinic.Tests/      # Unit tests
│
├── bff/              # Backend for Frontend (.NET 8)
│   ├── VeterinaryClinic.Bff.Api
│   ├── VeterinaryClinic.Bff.Application
│   ├── VeterinaryClinic.Bff.Domain
│   ├── VeterinaryClinic.Bff.Infrastructure  # API clients, Refit
│   └── VeterinaryClinic.Bff.Tests/
│
├── frontend/        # React + Vite + TypeScript
│   └── src/
│       ├── app/            # App routes
│       ├── shared/         # Components, hooks, services
│       ├── features/        # Feature modules
│       └── styles/          # Tailwind CSS
│
├── docker-compose.yml
└── README.md
```

## Tecnologias

### Backend API
- .NET 8
- ASP.NET Core Web API
- DDD (Domain-Driven Design)
- CQRS (Command Query Responsibility Segregation)
- MediatR
- FluentValidation
- Entity Framework Core
- PostgreSQL
- Redis (Cache)
- JWT Authentication

### BFF
- .NET 8
- Refit (HTTP Client)
- JWT forwarding
- Aggregation of API data

### Frontend
- React 18
- Vite
- TypeScript
- Tailwind CSS
- React Hook Form + Zod
- TanStack Query
- Zustand (State Management)
- Recharts (Gráficos)
- React Router

## Configuração

### Variáveis de Ambiente

**Frontend (.env):**
```env
VITE_BFF_BASE_URL=http://localhost:5001
VITE_APP_NAME=Veterinary Clinic Admin
```

**Backend API (appsettings.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=veterinary_clinic;Username=postgres;Password=postgres"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "Jwt": {
    "Issuer": "VeterinaryClinic",
    "Audience": "VeterinaryClinicUsers",
    "Secret": "CHANGE_ME_SUPER_SECRET_KEY_32CHARS_MIN"
  }
}
```

## Módulos do Sistema

1. **Autenticação** - Login, logout, refresh token, recuperação de senha
2. **Usuários** - CRUD de usuários com vinculação a perfis
3. **Perfis e Permissões** - RBAC completo com permissões granulares
4. **Tutores** - Cadastro e gerenciamento de tutores
5. **Animais** - Cadastro e gerenciamento de animais com vínculo a tutores
6. **Vacinas** - Registro de aplicações e agendamento de doses
7. **Consultas** - Agendamento e controle de consultas
8. **Internações** - Gestão de internações
9. **Financeiro** - Movimentações, dashboards, relatórios
10. **Produtos** - Cadastro e controle de estoque
11. **Serviços** - Cadastro de serviços da clínica
12. **Vendas** - Registro de vendas com itens
13. **Consultório** - Área do veterinário
14. **Fila de Atendimento** - Controle de fila
15. **Petshop** - Gestão de serviços de petshop
16. **Estrutura** - Ambientes da clínica
17. **Relatórios** - Relatórios diversos

## Perfis de Acesso

- **Administrador** - Acesso total ao sistema
- **Veterinário** - Acesso a consultas, animais, prontuário
- **Atendente** - Agenda, tutores, animais, fila, vendas
- **Financeiro** - Financeiro, vendas, relatórios
- **Petshop** - Atendimento de petshop
- **Tutor** - Portal do tutor (acesso limitado)

## Fluxo de Dados

```
React (Frontend)
    ↓ (HTTP + JWT)
.NET 8 BFF (Aggregação)
    ↓ (HTTP + JWT forwarding)
.NET 8 API (Regras de Negócio)
    ↓
PostgreSQL / Redis
```

## Executando com Docker

```bash
# Subir todos os serviços
docker-compose up -d

# Ver logs
docker-compose logs -f

# Parar serviços
docker-compose down
```

## Executando Localmente

### Backend API
```bash
cd backend/VeterinaryClinic.Api
dotnet run
```

### BFF
```bash
cd bff/VeterinaryClinic.Bff.Api
dotnet run
```

### Frontend
```bash
cd frontend
npm install
npm run dev
```

## Endpoints Principais

### API (localhost:5000)
- `POST /api/v1/auth/login` - Login
- `GET /api/v1/animals` - Listar animais
- `POST /api/v1/animals` - Criar animal
- `GET /api/v1/consultations` - Listar consultas
- `GET /api/v1/finance/dashboard` - Dashboard financeiro

### BFF (localhost:5001)
- `POST /bff/v1/auth/login` - Login
- `GET /bff/v1/dashboard` - Dashboard consolidado
- `GET /bff/v1/navigation` - Menu de navegação
- `GET /bff/v1/animals/{id}/complete-history` - Histórico completo

## Segurança

- JWT Bearer Authentication
- Refresh Token com rotação
- Permission-based Access Control
- Validação de entrada (FluentValidation)
- Auditoria de ações críticas
- Proteção contra acesso horizontal

## Próximos Passos

1. Implementar migrations do banco de dados
2. Criar seeds iniciais (perfis, permissões, usuário admin)
3. Implementar controllers restantes
4. Adicionar testes unitários
5. Implementar Real-time (SignalR) para fila
6. Adicionar paginação e filtros avançados
7. Implementar relatórios com exportação
8. Configurar CI/CD

## Licença

MIT