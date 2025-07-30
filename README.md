
# FastTechFoods - Ambiente de Microsserviços

Este projeto utiliza Docker Compose para orquestrar os principais serviços do MVP FastTechFoods.  
A arquitetura é baseada em microsserviços: autenticação, cardápio, pedidos, gateway, mensageria (RabbitMQ) e banco de dados (SQL Server).

---

## 🏗️ Serviços Disponíveis

| Serviço               | Porta Host    | URL de Acesso              | Descrição                    |
|-----------------------|---------------|----------------------------|------------------------------|
| auth-service          | 5045          | http://localhost:5045      | Serviço de autenticação      |
| menu-service          | 5091          | http://localhost:5091      | Gestão de cardápio           |
| pedido-service        | 5140          | http://localhost:5140      | Gestão de pedidos            |
| FastTechFoods.Gateway | 5080          | http://localhost:5080      | API Gateway (YARP)           |
| rabbitmq              | 5672, 15672   | http://localhost:15672     | Mensageria                   |
| sqlserver             | 1433          | N/A                        | Banco de dados SQL Server    |

---

## 🚀 Como subir o ambiente

### 1. Clone o repositório e acesse a pasta do projeto

```bash
git clone <seu-repositorio>
cd <pasta-do-projeto>
```

### 2. Suba todos os containers com Docker Compose

```bash
docker compose up -d --build
```

> O parâmetro `--build` garante que todas as imagens serão reconstruídas caso haja alterações no código.

### 3. Aguarde alguns segundos e acesse os serviços pelos endereços acima.

---

## 🐰 RabbitMQ

- Interface de gerenciamento: [http://localhost:15672](http://localhost:15672)
- Usuário: `guest`
- Senha: `guest`

---

## 🛢️ SQL Server

- Host: `localhost,1433`
- Usuário: `sa`
- Senha: `Senha123!`
- Database: conforme especificado em cada serviço

---

## 🔗 Exemplos de requisição

- Autenticação:

  ```http
  POST http://localhost:5045/api/auth/login
  ```

- Cardápio:

  ```http
  GET http://localhost:5091/api/menu
  ```

- Pedidos:

  ```http
  POST http://localhost:5140/api/pedido
  ```

---

## 🧹 Comandos úteis

### Parar todos os containers

```bash
docker compose down
```

### Visualizar logs de um serviço

```bash
docker compose logs <serviço>
```

### Rebuild de um serviço específico

```bash
docker compose build <serviço>
```

---

## 📝 Observações

- O Gateway (porta 5080) faz o roteamento para os demais serviços via YARP.
- Os serviços podem demorar alguns instantes para iniciar na primeira vez, especialmente o SQL Server.
- Caso precise resetar o banco, exclua o volume associado ao container `sqlserver`.


