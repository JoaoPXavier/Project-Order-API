🚀 Orders API - Sistema de Gestão de Pedidos e Ocorrências

https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet
https://img.shields.io/badge/PostgreSQL-16.2-336791?logo=postgresql
https://img.shields.io/badge/Entity_Framework_Core-8.0-512BD4
https://img.shields.io/badge/Swagger-6.5-85EA2D?logo=swagger

Uma API RESTful completa desenvolvida em .NET 8 para gestão de pedidos e ocorrências, implementando Domain-Driven Design (DDD) com Domínio Rico e Repository Pattern.

📋 Índice
✨ Funcionalidades

🏗️ Arquitetura

🚀 Como Executar

🔐 Autenticação

📚 Endpoints

🎯 Regras de Negócio

🧪 Testes

🛠️ Tecnologias

✨ Funcionalidades
✅ Requisitos Implementados
Autenticação JWT - Segurança com tokens

Cadastro de Pedidos - Com número único

Registro de Ocorrências - Com regras complexas

Domínio Rico - Entidades com comportamentos

Repository Pattern - Abstração de dados

PostgreSQL - Banco de dados robusto

Testes Unitários - Cobertura de regras críticas

Swagger - Documentação interativa

🎯 Diferenciais
Value Objects para conceitos de domínio

FluentValidation para validações

Tratamento global de erros

Logs estruturados com Serilog

Clean Architecture

🏗️ Arquitetura
text
OrdersApi/
├── 📂 Api/Controllers/          # Camada de Apresentação
├── 📂 Application/              # DTOs e Validators
├── 📂 Domain/                   # Domínio Rico (Core)
│   ├── Entities/                # Entidades com comportamentos
│   ├── Enums/                   # Enumerações
│   └── ValueObjects/            # Value Objects
├── 📂 Infrastructure/           # Implementações Externas
│   ├── Data/                   # DbContext e Configurações
│   └── Repositories/           # Repository Pattern
├── 📂 Shared/                  # Recursos Compartilhados
│   ├── Logging/               # Configuração Serilog
│   └── Middleware/            # Middlewares Customizados
└── 📂 Tests/                   # Testes Unitários
🚀 Como Executar
Pré-requisitos
.NET 8.0 SDK

PostgreSQL 12+

Visual Studio Code ou Visual Studio

1. Clone o repositório
bash
git clone (https://github.com/JoaoPXavier/Project-Order-AP)
cd orders-api
2. Configure o banco de dados
json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ordersdb;Username=postgres;Password=123456"
  }
}
3. Execute as migrações
bash
dotnet ef database update
4. Execute a aplicação
bash
dotnet run
5. Acesse a documentação
text
📚 Swagger UI: http://localhost:5000/swagger
📄 Documentação: http://localhost:5000/api/docs
🔐 Autenticação
A API utiliza JWT (JSON Web Tokens) para autenticação.

Obter Token:
http
POST /api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "senha"
}
Usar Token:
http
GET /api/pedidos
Authorization: Bearer seu_token_jwt_aqui
📚 Endpoints Principais
🔐 Autenticação
Método	Endpoint	Descrição
POST	/api/Auth/login	Obter token JWT
📦 Pedidos
Método	Endpoint	Descrição
GET	/api/pedidos	Listar todos os pedidos
POST	/api/pedidos	Criar novo pedido
GET	/api/pedidos/{id}	Obter pedido específico
🚚 Ocorrências
Método	Endpoint	Descrição
POST	/api/pedidos/{id}/ocorrencias	Adicionar ocorrência
DELETE	/api/pedidos/{id}/ocorrencias/{ocorrenciaId}	Remover ocorrência
🎯 Regras de Negócio Implementadas
⏱️ Regra dos 10 Minutos
csharp
// Bloqueia ocorrências do mesmo tipo em menos de 10 minutos
var delta = hora - lastSameType.HoraOcorrencia;
if (delta.TotalMinutes < 10)
    throw new InvalidOperationException("Não é permitido...");
🏁 Ocorrência Finalizadora
csharp
// Segunda ocorrência automaticamente marca como finalizadora
bool isFinalizadora = _ocorrencias.Any();
✅ Status de Entrega Automático
csharp
// Define status baseado no tipo da ocorrência final
if (isFinalizadora)
    IndEntregue = tipo == ETipoOcorrencia.EntregueComSucesso;
🚫 Bloqueio de Alterações
csharp
// Pedidos concluídos não permitem alterações
if (IsConcluded)
    throw new InvalidOperationException("Pedido já está concluído...");
🧪 Testes
Executar Testes:
bash
dotnet test
Cenários Testados:
✅ Adição de ocorrência finalizadora

✅ Regra dos 10 minutos

✅ Status de entrega automático

✅ Bloqueio em pedidos concluídos

Exemplo de Teste:
csharp
[Fact]
public void AddOcorrencia_Should_MarkSecondAsFinalizadora()
{
    var pedido = new Pedido(new NumeroPedidoVO(1));
    
    pedido.AddOcorrencia(ETipoOcorrencia.EmRotaDeEntrega, DateTime.UtcNow);
    var second = pedido.AddOcorrencia(ETipoOcorrencia.EntregueComSucesso, DateTime.UtcNow.AddMinutes(11));
    
    Assert.True(second.IndFinalizadora);
    Assert.True(pedido.IndEntregue);
}
🛠️ Tecnologias
Backend
.NET 8.0 - Framework principal

Entity Framework Core 8.0 - ORM

PostgreSQL - Banco de dados

xUnit - Testes unitários

Segurança & Documentação
JWT Bearer - Autenticação

Swagger/OpenAPI - Documentação

FluentValidation - Validações

Logs & Monitoramento
Serilog - Logs estruturados

Middleware Customizado - Tratamento de erros

Padrões & Arquitetura
Domain-Driven Design (DDD)

Repository Pattern

Clean Architecture

SOLID Principles

📊 Estrutura de Dados
Entidade Pedido
csharp
public class Pedido
{
    public int IdPedido { get; private set; }
    public NumeroPedidoVO NumeroPedido { get; private set; }
    public DateTime HoraPedido { get; private set; }
    public bool IndEntregue { get; private set; }
    public IReadOnlyCollection<Ocorrencia> Ocorrencias { get; }
    public bool IsConcluded { get; } // Propriedade calculada
}
Value Object NumeroPedido
csharp
public sealed class NumeroPedidoVO
{
    public int Numero { get; }
    // Validação: número deve ser positivo
}
Enum Tipos de Ocorrência
csharp
public enum ETipoOcorrencia
{
    EmRotaDeEntrega = 0,
    EntregueComSucesso = 1,
    ClienteAusente = 2,
    AvariaNoProduto = 3
}
🤝 Contribuição
Fork o projeto

Crie uma branch para sua feature (git checkout -b feature/AmazingFeature)

Commit suas mudanças (git commit -m 'Add some AmazingFeature')

Push para a branch (git push origin feature/AmazingFeature)

Abra um Pull Request
