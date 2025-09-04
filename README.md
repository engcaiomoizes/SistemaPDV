# 🛒 Sistema de Ponto de Venda (PDV)

Este projeto é um sistema de Ponto de Venda (PDV) desenvolvido em **C#** com banco de dados **Firebird**, voltado para estabelecimentos que desejam gerenciar vendas, estoque e caixa de forma eficiente e segura.

## 📌 Funcionalidades

- ✅ Cadastro de produtos com controle de estoque
- ✅ Cadastro de clientes e fornecedores
- ✅ Cadastro de funcionários e usuários
- ✅ Registro de vendas
- ✅ Controle de caixa (abertura, movimentações e fechamento)
- ✅ Autenticação de usuários

## 💡 Tecnologias utilizadas

- **C# (.NET)** - Linguagem principal para desenvolvimento
- **Firebird** - Banco de dados relacional leve e robusto
- **Windows Forms** - Interface gráfica do sistema

## Próximas implementações

- Emissão de comprovante de venda
- Relatórios gerenciais (vendas, estoque, fluxo de caixa)
- Níveis de acesso
- Backup automático do banco de dados
- Opção com outro banco de dados relacional (MySQL/PostgreSQL)
- Geração de instalador com Inno Setup

## Como executar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/engcaiomoizes/SistemaPDV.git
   ```

2. Configure o banco de dados Firebird
   - Instale o Firebird (versão recomendada: 2.5.9)
   - O arquivo `PDV.FDB` encontra-se dentro da pasta `Banco/` do projeto

3. Abra o projeto no Visual Studio
   - Configure a string de conexão no arquivo `Conexao.cs`
   - Compile a solução

4. Execute o sistema
   - Faça login com o usuário padrão:
     - **Usuário:** admin
     - **Senha:** admin