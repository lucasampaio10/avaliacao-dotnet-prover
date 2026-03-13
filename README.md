# Avaliação .NET - Sistema de Contatos

API REST desenvolvida em .NET 8 com arquitetura DDD para gerenciamento de contatos.

## Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- SQL Server
- xUnit + Moq + FluentAssertions
- Swagger

## Arquitetura
- **Domain** — entidades, regras de negócio, interfaces
- **Application** — services, DTOs
- **Infrastructure** — EF Core, repositórios
- **API** — controllers, middleware, configuração

## Como executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server

### Passos
1. Clone o repositório
2. Atualize a connection string em `Contatos.API/appsettings.json`
3. Execute as migrations:

dotnet ef database update --project Contatos.Infrastructure --startup-project Contatos.API


4. Execute a API:
dotnet run --project Contatos.API


5. Acesse o Swagger: `http://localhost:{porta}/swagger`

## Testes
dotnet test



## Endpoints
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/contatos | Listar contatos ativos |
| GET | /api/contatos/{id} | Buscar contato por ID |
| POST | /api/contatos | Criar contato |
| PUT | /api/contatos/{id} | Atualizar contato |
| PATCH | /api/contatos/{id}/desativar | Desativar contato |
| DELETE | /api/contatos/{id} | Excluir contato |