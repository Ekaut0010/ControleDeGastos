# 🏦 Controle de Gastos Residenciais

Sistema web para gerenciamento e controle de finanças residenciais. A aplicação permite o cadastramento de pessoas, lançamento de movimentações financeiras (receitas e despesas) e visualização de saldos consolidados por indivíduo e no total geral.

---

## 🛠️ Tecnologias Utilizadas

### **Front-end**
* **React** + **TypeScript**
* **Vite** (Build tool & Dev Server)
* **CSS3** (Design System com temática corporativa e tabelas densas)

### **Back-end**
* **.NET / C#** (API RESTful)

---

## ✨ Funcionalidades Principais

* **Gestão de Pessoas:**
  * Cadastro de moradores (Nome e Idade).
  * Exclusão de cadastros com remoção em cascata de suas movimentações.
* **Controle de Transações:**
  * Registro de receitas e despesas vinculadas a uma pessoa.
  * Validação de regras de negócio (menores de 18 anos só podem registrar despesas).
* **Painel de Totais:**
  * Exibição tabular com total de receitas, despesas e saldo individual.
  * Linha de balanço geral com os totais acumulados de toda a residência.

---

## 🚀 Como Executar o Projeto

### **Pré-requisitos**
* [Node.js](https://nodejs.org/) (v18 ou superior)
* [.NET SDK](https://dotnet.microsoft.com/) (6.0 ou superior)

---

### **1. Executando a API (.NET)**

```bash
# Entre na pasta da API
cd ControleGastos.API

# Restaure as dependências e rode o projeto
dotnet run
