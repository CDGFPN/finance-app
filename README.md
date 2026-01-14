# Finance App

Aplicativo de gerenciamento de finanças pessoais desenvolvido em C# e .NET 8 como projeto de aprendizado para praticar arquitetura limpa, princípios SOLID e boas práticas de desenvolvimento.

## Tecnologias

- **Runtime:** .NET 8
- **Linguagem:** C# 12
- **Segurança:** BCrypt para hash de senhas
- **Arquitetura:** Clean Architecture com Repository Pattern

## Funcionalidades

### Gerenciamento de Usuários
- Cadastro com validação de email
- Login seguro com hash BCrypt
- Edição de perfil (nome, email, senha)
- Gerenciamento de sessão (login/logout)

### Gerenciamento de Transações
- Adicionar receitas e despesas
- Visualizar todas as transações com detalhes
- Editar transações existentes
- Deletar transações com confirmação
- Cálculo de saldo em tempo real

### Categorias

| Receitas | Despesas |
|----------|----------|
| Salário | Alimentação |
| Investimentos | Transporte |
| Freelance | Moradia |
| | Contas |
| | Lazer |
| | Saúde |
| | Educação |

## Arquitetura

```
FinanceApp/
├── Models/              # Entidades do domínio
│   ├── User.cs          # Usuário: Id, Name, Email, Password
│   ├── Transaction.cs   # Transação: Value, Date, Description, Type
│   ├── Category.cs      # Enum com atributos customizados
│   └── TransactionType.cs
│
├── Repositories/        # Camada de acesso a dados
│   ├── ITransactionRepository.cs
│   ├── TransactionRepository.cs
│   └── UserRepository.cs
│
├── Services/            # Camada de regras de negócio
│   ├── IUserService.cs
│   ├── UserService.cs
│   ├── ITransactionService.cs
│   └── TransactionService.cs
│
├── UI/                  # Camada de apresentação
│   └── Menu.cs          # Interface console
│
├── Attributes/          # Atributos customizados
│   └── TransactionTypeAttribute.cs
│
└── Program.cs           # Ponto de entrada
```

### Padrões de Projeto Utilizados

| Padrão | Onde | Por quê |
|--------|------|---------|
| **Repository Pattern** | `Repositories/` | Abstrai acesso a dados, facilita trocar storage (JSON → SQL) |
| **Service Layer** | `Services/` | Encapsula regras de negócio, mantém controllers enxutos |
| **Dependency Injection** | `Program.cs` | Baixo acoplamento, facilita testes, segue SOLID |
| **Interface Segregation** | `IUserService`, `ITransactionService` | Clientes dependem apenas dos métodos que usam |

## Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Instalação

```bash
# Clonar o repositório
git clone https://github.com/SEU_USUARIO/finance-app.git

# Navegar até o diretório
cd finance-app

# Restaurar dependências
dotnet restore

# Executar a aplicação
dotnet run
```

## Jornada de Aprendizado

Este projeto faz parte da minha jornada para me tornar um desenvolvedor .NET.

### Concluído
- [x] Programação Orientada a Objetos (classes, enums, properties)
- [x] Interfaces e abstração
- [x] Repository Pattern
- [x] Arquitetura em camadas (Service Layer)
- [x] Hash de senhas com BCrypt
- [x] Atributos customizados com Reflection
- [x] Consultas LINQ
- [x] Git workflow com commits semânticos

### Em Progresso
- [ ] Integração com banco de dados (SQL Server + Entity Framework Core)
- [ ] Padrões async/await
- [ ] Testes unitários

### Planejado
- [ ] API REST com ASP.NET Core
- [ ] Frontend com HTML/CSS/JavaScript
- [ ] Containerização com Docker

## Conceitos de C# Demonstrados

```csharp
// Atributos Customizados com Reflection
[TransactionType(TransactionType.Despesa)]
Alimentacao,

// LINQ para filtragem e agregação
decimal totalReceitas = transacoes
    .Where(t => t.Type == TransactionType.Receita)
    .Sum(t => t.Value);

// Injeção de Dependência
public TransactionService(ITransactionRepository repository)
{
    this.transactionRepository = repository;
}

// Nullable reference types
public Transaction? GetById(Guid id)
```

## Licença

Este projeto está sob a licença [MIT](LICENSE).

---

*Desenvolvido com dedicação por um futuro Desenvolvedor .NET*
