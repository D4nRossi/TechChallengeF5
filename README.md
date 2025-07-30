# FastTechFoods - Hackaton Fase 5

Plataforma moderna para gestão de pedidos, cardápio, autenticação de funcionários/clientes e mensageria via RabbitMQ.  
Arquitetura baseada em microsserviços com observabilidade total (Grafana, Prometheus), CI/CD com GitHub Actions, orquestração via Docker Compose.

---

## :rocket: **Arquitetura**

- **auth-service**: Autenticação e controle de acesso de funcionários e clientes (Identity, JWT)
- **menu-service**: Cadastro e edição de cardápio (restrito a gerente)
- **pedido-service**: Fluxo de pedidos (criar, aprovar, rejeitar, cancelar)
- **cliente-service**: Cadastro e gestão do cliente
- **gateway**: API Gateway para roteamento entre serviços
- **rabbitmq**: Mensageria entre serviços (publicação e consumo de eventos de pedido)
- **sqlserver**: Banco de dados relacional principal
- **prometheus/grafana**: Observabilidade e métricas em tempo real

---

## :whale: **Como rodar**

Pré-requisitos:
- Docker e Docker Compose instalados

### **1. Clone o repositório**

```sh
git clone https://github.com/seuusuario/fasttechfoods.git
cd fasttechfoods
```

### **2. Suba todos os serviços**

```sh
docker compose up -d --build
```

### **3. Acesse os serviços**

- **Auth**: http://localhost:5100
- **Menu**: http://localhost:5101
- **Pedido**: http://localhost:5102
- **Cliente**: http://localhost:5103
- **Gateway**: http://localhost:5105
- **RabbitMQ UI**: http://localhost:15673 (guest/guest)
- **Prometheus**: http://localhost:9090
- **Grafana**: http://localhost:3002 (admin/admin)
- **SQL Server**: localhost, porta 11433 (usuario: sa / senha: Senha123!)

### **4. Endpoints principais**

Veja a collection Postman anexada ou acesse via Swagger dos serviços.  
Principais fluxos:
- Login/Cadastro (Cliente e Funcionário)
- Cadastro/Edição de itens do cardápio
- Fazer pedido, aprovar/rejeitar, cancelar pedido (com justificativa)
- Mensageria via RabbitMQ (pedido criado = mensagem publicada)
- Observabilidade em Grafana/Prometheus

---

## :wrench: **Observabilidade**

- **/metrics**: Todos os serviços expõem métricas para Prometheus.
- **Grafana**: Dashboard pronto para monitorar requisições, status e latência por serviço.

---

## :repeat: **Pipeline CI/CD**

- CI/CD configurado no GitHub Actions para build/test dos serviços a cada push/pull request.

---

## :scroll: **Como testar**

- Use o arquivo de collection do Postman disponível no repositório (`FastTechFoods.postman_collection.json`)
- Crie usuários, faça login, realize pedidos e veja as métricas subirem no Grafana!

---

## :exclamation: **Notas finais**

- O projeto **NÃO utiliza Kubernetes** no MVP, mas está pronto para evolução futura.
- Zabbix não foi incluído (só Grafana/Prometheus para observabilidade).
- O código está comentado, modular e separado por contexto.

---

## :handshake: **Créditos**
Projeto desenvolvido para a disciplina de Arquitetura de Sistemas .NET – Hackaton Fase 5 – Pós Teleperformance | FIAP | 2025.

