# 🏢 Sistema de Contratos de Venda

API RESTful desenvolvida em .NET para gerenciamento de contratos de venda, fornecedores e ativos.

## 📋 Índice

- [Visão Geral do Projeto](#-visão-geral-do-projeto)
- [Tecnologias e Decisões Técnicas](#-tecnologias-e-decisões-técnicas)
- [Arquitetura Implementada](#%EF%B8%8F-arquitetura-implementada)
- [Funcionalidades Desenvolvidas](#-funcionalidades-desenvolvidas)
- [Como Executar](#-como-executar)
- [Endpoints da API](#-endpoints-da-api)
- [Tratamento de Erros](#-tratamento-de-erros)
- [Validações e Regras de Negócio](#-validações-e-regras-de-negócio)
- [Padrões de Design Aplicados](#-os-padrões-que-adotei)

## 🎯 Visão Geral do Projeto

Desenvolvi este sistema como parte de um desafio técnico, focando em criar uma API RESTful para o gerenciamento de contratos de venda. Meu objetivo foi demonstrar boas práticas de desenvolvimento e arquitetura limpa, sem deixar faltar nenhuma das funcionalidades solicitadas.

O sistema permite:
- Cadastro e gestão de **Fornecedores**
- Categorização de ativos através de **Tipos de Ativo**
- Gestão de completa **Ativos**
- Criação de **Contratos de Venda** com múltiplos itens e cálculos automáticos

### Requisitos Atendidos

Implementei todos os requisitos especificados:
- ✅ API RESTful com endpoints para todas as entidades
- ✅ JSON como formato de comunicação
- ✅ CRUD completo para cada entidade
- ✅ Versionamento via `/api/v1/`
- ✅ Entity Framework Core para persistência
- ✅ Documentação com Swagger
- ✅ PostgreSQL como banco de dados

## 🚀 Tecnologias e Decisões Técnicas

Escolhi as seguintes tecnologias para o desenvolvimento:

- **.NET 9**: Optei pela versão mais recente para aprender sobre as melhorias de performance e novos recursos.
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL**
- **Mapster**: Utilizei essa biblioteca para aprender mais sobre (E ela foi muito útil! 🎉).
- **Swagger/OpenAPI**

## 🏗️ Arquitetura Implementada

Estruturei o projeto seguindo princípios de **Clean Architecture**:

```
API Layer → Controllers e Middleware
Domain Layer → DTOs, Entidades, Interfaces e Validações
Infra Layer → DbContext, Repositories e Services
```

Esta separação me permitiu:
- Separar da forma clara a responsabilidade de cada camada
- Manter o código organizado e de fácil manutenção
- Implementar injeção de dependências de forma limpa

## ⚡ Funcionalidades Desenvolvidas

### 🏪 Gestão de Fornecedores

Implementei um CRUD completo para fornecedores com:
- Validação de CNPJ (formato e dígitos verificadores)
- Relacionamento com contratos de venda

### 📦 Gestão de Ativos

O CRUD de categorização de ativos:
- Relaciona cada ativo a um tipo específico
- Valida o preço de venda (sempre maior que zero)

### 📋 Gestão de Contratos

Para os contratos, implementei:
- Criação de contratos com múltiplos itens
- Cálculo automático do valor total baseado nos itens e desconto
- Controle de datas de criação e alteração
- Validação para evitar itens duplicados no mesmo contrato

## 🔧 Como Executar

Para executar o projeto em ambiente local:

### Pré-requisitos
- .NET 9 SDK
- PostgreSQL
- Git

### Passos

1. **Clone o repositório:**
```bash
git clone https://github.com/ronaldothame/meucliente.git
cd meucliente
```

2. **Configure a string de conexão** no `appsettings.json` do projeto `Solution1.Api`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=sua_database;Username=seu_username;Password=sua_senha"
  }
}

Importante: O projeto foi configurado para utilizar o schema "meuclientedb". Certifique-se de que este schema existe no seu banco PostgreSQL antes de rodar o projeto.
```

3. **Execute a aplicação:**
```bash
dotnet run --project Solution1.Api
```

4. **Acesse a documentação Swagger:**
```
https://localhost:{porta}/swagger
```

## 🌐 Endpoints da API

Implementei endpoints RESTful para todas as entidades:

### Fornecedores
- `GET /api/v1/fornecedor` - Lista todos os fornecedores
- `GET /api/v1/fornecedor/{id}` - Busca fornecedor por ID
- `POST /api/v1/fornecedor` - Cria novo fornecedor
- `PUT /api/v1/fornecedor/{id}` - Atualiza fornecedor
- `DELETE /api/v1/fornecedor/{id}` - Exclui fornecedor

### Tipos de Ativo
- `GET /api/v1/tipoativo` - Lista todos os tipos
- `GET /api/v1/tipoativo/{id}` - Busca tipo por ID
- `POST /api/v1/tipoativo` - Cria novo tipo
- `PUT /api/v1/tipoativo/{id}` - Atualiza tipo
- `DELETE /api/v1/tipoativo/{id}` - Exclui tipo

### Ativos
- `GET /api/v1/ativo` - Lista todos os ativos
- `GET /api/v1/ativo/{id}` - Busca ativo por ID
- `POST /api/v1/ativo` - Cria novo ativo
- `PUT /api/v1/ativo/{id}` - Atualiza ativo
- `DELETE /api/v1/ativo/{id}` - Exclui ativo

### Contratos de Venda
- `GET /api/v1/contratovenda` - Lista todos os contratos
- `GET /api/v1/contratovenda/{id}` - Busca contrato por ID
- `POST /api/v1/contratovenda` - Cria novo contrato
- `PUT /api/v1/contratovenda/{id}` - Atualiza contrato
- `DELETE /api/v1/contratovenda/{id}` - Exclui contrato

## 🚨 Tratamento de Erros

Implementei um middleware centralizado para tratamento de exceções com respostas padronizadas:

```json
{
  "status": 400,
  "message": "Já existe um fornecedor com este CNPJ.",
  "detail": "Detalhes adicionais quando necessário"
}
```

Mapeei alguns tipos de erro:
- `400 Bad Request` - Dados inválidos
- `404 Not Found` - Recurso não encontrado
- `422 Unprocessable Entity` - Regras de negócio violadas
- `500 Internal Server Error`

Este tipo de padrão facilita o tratamento de erros pelo frontend.

## ✅ Validações e Regras de Negócio

### Validações Implementadas

#### Fornecedor
- Unicidade de código e CNPJ

#### Ativo
- Tipo de ativo deve existir
- Código único

#### Contrato de Venda
- Número de contrato único
- Fornecedor deve existir
- Pelo menos um item obrigatório
- Todos os ativos devem existir

### Integridade Referencial

Adicionei verificações para garantir a integridade dos dados:
- Fornecedores não podem ser excluídos se possuem contratos
- Tipos de ativo não podem ser excluídos se possuem ativos associados
- Ativos não podem ser excluídos se estão em uso em contratos

## 🎨 Os padrões que adotei

### Repository Pattern

Implementei repositórios genéricos para abstrair o acesso a dados:

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
```

### BaseController Genérico

Implementei controllers genéricos para evitar duplicar código:

```csharp
[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController<TEntity, TDto, TCreateDto, TUpdateDto> : ControllerBase
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
}
```

### DTO Pattern

DTOs específicos para criação, atualização e retorno.


---

