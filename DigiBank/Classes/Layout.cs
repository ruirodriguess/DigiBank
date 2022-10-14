using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiBank.Classes
{
    public class Layout
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();
        private static int opcao = 0;

        public static void TelaPrincipal()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            Console.WriteLine("                        BEM VINDO AO BANCO DO RUI                   ");
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                        Selecione a opção desejada:                 ");
            Console.WriteLine("                    ===================================             ");
            Console.WriteLine("                         1- Criar conta                             ");
            Console.WriteLine("                    ===================================             ");
            Console.WriteLine("                         2- Entrar com NIF e Senha                  ");
            Console.WriteLine("                    ===================================             ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaCriarConta();
                    break;

                case 2:
                    TelaLogin();
                    break;

                default:
                    Console.WriteLine("Opção Inválida");
                    break;
            }

        }

        private static void TelaCriarConta()
        {
            Console.Clear();

            Console.WriteLine("                                                                ");
            Console.WriteLine("                    Informe o seu seu nome:                          ");
            string nome = Console.ReadLine();
            Console.WriteLine("                ===================================             ");
            Console.WriteLine("                    Informe o seu NIF:                               ");
            string nif = Console.ReadLine();
            Console.WriteLine("                ===================================             ");
            Console.WriteLine("                    Defina a sua password:                         ");
            string senha = Console.ReadLine();
            Console.WriteLine("                ===================================             ");

            // Criar conta

            ContaCorrente contaCorrente = new ContaCorrente();
            Pessoa pessoa = new Pessoa();

            pessoa.SetNome(nome);
            pessoa.SetNIF(nif);
            pessoa.SetSenha(senha);
            pessoa.Conta = contaCorrente;

            pessoas.Add(pessoa);

            Console.Clear();

            Console.WriteLine("                    Conta criada com sucesso                    ");
            Console.WriteLine("               ===================================              ");

            // Este código espera 1 segundo
            Thread.Sleep(1000);

            TelaContaLogada(pessoa);

        }

        public static void TelaLogin()
        {
            Console.Clear();

            Console.WriteLine("                                                        ");
            Console.WriteLine("               Escreva seu o NIF:                            ");
            string nif = Console.ReadLine();
            Console.WriteLine("           ===================================          ");
            Console.WriteLine("               Escreva a sua password:                    ");
            string senha = Console.ReadLine();
            Console.WriteLine("           ===================================          ");

            Pessoa pessoa = pessoas.FirstOrDefault(x => x.NIF == nif && x.Senha == senha);

            if(pessoa != null)
            {
                TelaBoasVindas(pessoa);
                TelaContaLogada(pessoa);
            }
            else
            {
                Console.Clear();

                Console.WriteLine("               ===================================              ");
                Console.WriteLine("                              Erro                              ");
                Console.WriteLine("               ===================================              ");
                Console.WriteLine("                      Registo não encontrado                    ");
                Console.WriteLine("               ===================================              ");
                Console.WriteLine("                         Tente novamente                        ");
                Console.WriteLine("               ===================================              ");

                Console.WriteLine();
                Console.WriteLine();
                Thread.Sleep(2000);
                TelaPrincipal();
            }
        }

        private static void TelaBoasVindas(Pessoa pessoa)
        {

            string msgTelaBemVindo =
                $"{pessoa.Nome} | Banco: {pessoa.Conta.GetCodigoDoBanco()} " +
                $"| Agência: {pessoa.Conta.GetNumeroAgencia()}" +
                $"| Conta: {pessoa.Conta.GetNumeroDaConta()}";

            Console.WriteLine("");
            Console.WriteLine($"  Seja bem vindo, {msgTelaBemVindo} ");
            Console.WriteLine("");
        }

        private static void TelaContaLogada(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                Informe a opção desejada:                         ");
            Console.WriteLine("                ==================================               ");
            Console.WriteLine("                1- Realizar Depósito                             ");
            Console.WriteLine("                ==================================               ");
            Console.WriteLine("                2- Realizar Levantamento                         ");
            Console.WriteLine("                ==================================               ");
            Console.WriteLine("                3- Consultar saldo                               ");
            Console.WriteLine("                ==================================               ");
            Console.WriteLine("                4- Consultar Extrato                             ");
            Console.WriteLine("                ==================================               ");
            Console.WriteLine("                5- Sair                                          ");
            Console.WriteLine("                ==================================               ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaDeposito(pessoa);
                    break;
                case 2:
                    TelaSaque(pessoa);
                    break;
                case 3:
                    TelaSaldo(pessoa);
                    break;
                case 4:
                    TelaExtrato(pessoa);
                    break;
                case 5:
                    TelaPrincipal();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("                Opção inválida                         ");
                    Console.WriteLine("                ========================               ");
                    break;
            }

        }

        private static void TelaDeposito (Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Informe o valor do depósito:                         ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("               ======================================               ");

            pessoa.Conta.Deposita(valor);

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            Console.WriteLine("                   Depósito realizado com sucesso                   ");
            Console.WriteLine("               ======================================               ");
            Console.WriteLine("                                                                    ");

            OpcaoVoltarLogado(pessoa);

        }

        private static void TelaSaldo(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine($"                   Seu saldo é: {pessoa.Conta.ConsultaSaldo()}                    ");
            Console.WriteLine("               =====================================================               ");
            Console.WriteLine("                                                                                   ");

            OpcaoVoltarLogado(pessoa);
        }

        private static void TelaExtrato(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            if (pessoa.Conta.Extrato().Any())
            {
                // Mostrar Extrato
                double total = pessoa.Conta.Extrato().Sum(x => x.Valor);

                foreach (Extrato extrato in pessoa.Conta.Extrato())
                {
                    Console.WriteLine("                                                                          ");
                    Console.WriteLine($"             Data: {extrato.Data.ToString("dd/MM/yyyy HH:mm:ss")}        ");
                    Console.WriteLine($"             Tipo de movimentação: {extrato.Descricao}                   ");
                    Console.WriteLine($"             Valor: {extrato.Valor}                                      ");
                    Console.WriteLine("          =============================================                   ");
                }

                Console.WriteLine("                                                        ");
                Console.WriteLine("                                                        ");
                Console.WriteLine($"               Sub-total: {total}                      ");
                Console.WriteLine("          ====================================          ");
            }
            else
            {
                Console.WriteLine("                                      ");
                Console.WriteLine("    Não há extrato a ser exibido!!    ");
                Console.WriteLine("                                      ");
                Console.WriteLine("                                      ");
            }

            OpcaoVoltarLogado(pessoa);
        }

        private static void OpcaoVoltarLogado(Pessoa pessoa)
        {

            Console.WriteLine("                   Entre com uma opção em baixo:                    ");
            Console.WriteLine("               ======================================               ");
            Console.WriteLine("                   1 - Voltar para minha conta                      ");
            Console.WriteLine("               ======================================               ");
            Console.WriteLine("                   2- Sair                                          ");
            Console.WriteLine("               ======================================               ");


            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaContaLogada(pessoa);
                    break;
                case 2:
                    TelaPrincipal();
                    break;
            }

        }

        private static void OpcaoVoltarDeslogado(Pessoa pessoa)
        {
 
            Console.WriteLine("                   Escolha umadas opções:                           ");
            Console.WriteLine("               ======================================               ");
            Console.WriteLine("                   1 - Voltar para o menu principal                 ");
            Console.WriteLine("               ======================================               ");
            Console.WriteLine("                   2- Sair                                          ");
            Console.WriteLine("               ======================================               ");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    TelaPrincipal();
                    break;
                case 2:
                    Console.WriteLine("                    Opção Inválida                     ");
                    Console.WriteLine("             ===============================           ");
                    break;
            }
                            
        }
        private static void TelaSaque(Pessoa pessoa)
        {
            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("               Informe o valor do levantamento:                     ");
            double valor = double.Parse(Console.ReadLine());
            Console.WriteLine("               ======================================               ");

            bool okSaque = pessoa.Conta.Saca(valor);

            

            Console.Clear();

            TelaBoasVindas(pessoa);

            Console.WriteLine("                                                                    ");
            Console.WriteLine("                                                                    ");
            if (okSaque)
            {
                Console.WriteLine("                 Levantamento realizado com sucesso                 ");
                Console.WriteLine("               ======================================               ");
            }
            else
            {
                Console.WriteLine("                       Saldo insuficiente                           ");
                Console.WriteLine("               ======================================               ");
            }
            Console.WriteLine("                                                                    ");

            OpcaoVoltarLogado(pessoa);

        }
    }

    
}
