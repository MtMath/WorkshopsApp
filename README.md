[![Semantic Versioning](https://github.com/MtMath/WorkshopsApp/actions/workflows/semantic-versioning.yml/badge.svg)](https://github.com/MtMath/WorkshopsApp/actions/workflows/semantic-versioning.yml)

## ER Diagram
![drawSQL-database-2025-03-16.png](.assets/drawSQL-database-2025-03-16.png)

## Esquema do Banco de Dados

A aplicação utiliza um banco de dados relacional com o seguinte esquema:

### Tabela `Workshops`
- **Descrição**: Armazena informações sobre cada workshop.
- **Colunas**:
    - `Id` (int): Chave primária.
    - `Name` (varchar): Nome do workshop.
    - `Description` (text): Descrição detalhada.
    - `Capacity` (int?, nullable): Capacidade máxima de participantes.
    - `Slug` (varchar): Identificador amigável para URLs.
    - `RealizationDate` (date): Data de realização.
- **Índices**:
    - `RealizationDate`: Para consultas por data.
    - `Slug`: Para acesso rápido via URLs.

### Tabela `AttendeesRecords`
- **Descrição**: Registra a presença dos colaboradores nos workshops.
- **Colunas**:
    - `Id` (int): Chave primária.
    - `CollaboratorId` (int): Chave estrangeira para `Collaborator`.
    - `WorkshopId` (int): Chave estrangeira para `Workshops`.

### Tabela `Collaborator`
- **Descrição**: Armazena informações dos colaboradores.
- **Colunas**:
    - `Id` (int): Chave primária.
    - `Name` (varchar): Nome do colaborador.

### Relacionamentos
- Um-para-muitos entre `Workshops` e `AttendeesRecords`: Um workshop pode ter vários registros de presença.
- Um-para-muitos entre `Collaborator` e `AttendeesRecords`: Um colaborador pode participar de vários workshops.

## Estrutura do Projeto

A solução está organizada em quatro projetos principais, seguindo o padrão de *clean architecture*:

- **Workshops.Domain**: Contém a lógica de negócios principal, entidades e serviços de domínio. É o núcleo da aplicação, focado nas regras de negócio para gerenciamento de workshops e rastreamento de participação.

- **Workshops.Application**: Inclui a lógica da aplicação, casos de uso e serviços que orquestram as interações entre as camadas de domínio e infraestrutura.

- **Workshops.Infrastructure**: Gerencia aspectos externos, como acesso ao banco de dados. Inclui configurações do Entity Framework Core para o esquema do banco de dados.

- **Workshops.Web**: A camada web, construída com ASP.NET Core, que serve como ponto de entrada da aplicação. Lida com requisições e respostas HTTP.

## Configuração do Ambiente de Desenvolvimento

### Passos para Executar a Aplicação

1. **Clonar o repositório**:
   ```bash
   git clone https://github.com/MtMath/WorkshopsApp.git
   cd WorkshopsApp
   ```

2. **Configurar variáveis de ambiente**:
    - Copie os envs para `.env` e preencha os valores necessários (ex.: string de conexão com o banco de dados).

3. **Build e execução com Docker Compose**:
   ```bash
   docker-compose up --build
   ```
   Isso iniciará a aplicação e quaisquer serviços dependentes (ex.: banco de dados).

4. **Alternativa: Executar diretamente com o .NET CLI**:
    - Certifique-se de que o banco de dados está configurado e as strings de conexão estão definidas.
    - Navegue até o projeto `Workshops.Web`:
      ```bash
      cd Workshops.Web
      dotnet run
      ```

### Migrações do Banco de Dados

Para aplicar as migrações do Entity Framework Core, execute os seguintes comandos:

```bash
make migrate
```


### Payloads de Exemplo

GET Workshops

```json
{
  "data": [
    {
      "id": 0,
      "createdAt": "2025-03-17T12:16:06.420Z",
      "updatedAt": "2025-03-17T12:16:06.420Z",
      "name": "string",
      "description": "string",
      "realizationDate": "2025-03-17T12:16:06.420Z",
      "slug": "string",
      "capacity": 0,
      "attendees": [
        {
          "id": 0,
          "createdAt": "2025-03-17T12:16:06.420Z",
          "updatedAt": "2025-03-17T12:16:06.420Z",
          "workshopId": 0,
          "collaboratorId": 0,
          "attended": true
        }
      ]
    }
  ],
  "message": "string",
  "timestamp": "2025-03-17T12:16:06.420Z"
}
```

GET Collaborators

```json
{
  "data": [
    {
      "id": 0,
      "createdAt": "2025-03-17T12:16:06.409Z",
      "updatedAt": "2025-03-17T12:16:06.409Z",
      "name": "string",
      "attendances": [
        {
          "id": 0,
          "createdAt": "2025-03-17T12:16:06.409Z",
          "updatedAt": "2025-03-17T12:16:06.409Z",
          "workshopId": 0,
          "collaboratorId": 0,
          "attended": true
        }
      ]
    }
  ],
  "message": "string",
  "timestamp": "2025-03-17T12:16:06.409Z"
}
```