# 💍 Wedding System API

`.NET` `C#` `ASP.NET Core` `Entity Framework Core` `SQL Server` `JWT` `REST API` `Evo Code` `Status: Em desenvolvimento`

## 📌 Sobre o Projeto

A **Wedding System API** é o Back-End de uma plataforma personalizada para gerenciamento de casamentos, desenvolvida pela **Evo Code** para um cliente.

A solução foi projetada para centralizar informações do casamento e fornecer uma estrutura segura e escalável para o gerenciamento de convidados, confirmação de presença, lista de presentes, pagamentos e demais funcionalidades da plataforma.

O projeto utiliza **ASP.NET Core** e segue uma arquitetura organizada em camadas, buscando manter o código limpo, seguro, escalável e de fácil manutenção.

---

## 🏢 Desenvolvido por

### 🚀 Evo Code

A **Evo Code** é uma empresa de desenvolvimento de software focada na criação de soluções personalizadas para empresas e clientes.

Neste projeto, a Evo Code é responsável pelo desenvolvimento da API e da estrutura Back-End do sistema.

---

## 🧠 Arquitetura do Sistema

O projeto é estruturado em camadas para manter as responsabilidades bem separadas e facilitar a manutenção e evolução da aplicação.

```text
📦 Controllers
   → Entrada e gerenciamento das requisições HTTP

⚙️ Services
   → Regras de negócio da aplicação

📑 Interfaces
   → Definição dos contratos das Services

📤 DTOs
   → Transferência segura de dados entre as camadas

🧱 Models
   → Entidades e modelos utilizados pela aplicação

🗄️ Data
   → Contexto e configuração do Entity Framework Core

📊 Migrations
   → Versionamento e evolução do banco de dados
