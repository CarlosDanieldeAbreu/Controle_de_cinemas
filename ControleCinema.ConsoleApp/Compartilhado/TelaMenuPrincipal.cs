using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSessao;
using ControleCinema.ConsoleApp.ModuloSala ;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;

namespace ControleCinema.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private IRepositorio<Funcionario> repositorioFuncionario;
        private TelaCadastroFuncionario telaCadastroFuncionario;

        private IRepositorio<Genero> repositorioGenero;
        private TelaCadastroGenero telaCadastroGenero;

        private IRepositorio<Sala> repositorioSala;
        private TelaCadastroSala telaCadastroSala;

        private IRepositorio<Filme> repositorioFilme;
        private TelaCadastroFilme telaCadastroFilme;

        private IRepositorio<Sessao> repositorioSessao;
        private TelaCadastroSessao telaCadastroSessao;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioFuncionario = new RepositorioFuncionario();
            telaCadastroFuncionario = new TelaCadastroFuncionario(repositorioFuncionario, notificador);

            repositorioGenero = new RepositorioGenero();
            telaCadastroGenero = new TelaCadastroGenero(repositorioGenero, notificador);

            repositorioSala = new RepositorioSala();
            telaCadastroSala = new TelaCadastroSala(repositorioSala, notificador);

            repositorioFilme = new RepositorioFilme();
            telaCadastroFilme = new TelaCadastroFilme(repositorioFilme, repositorioGenero, telaCadastroGenero, notificador);

            repositorioSessao = new RepositorioSessao();
            telaCadastroSessao = new TelaCadastroSessao(repositorioFilme, repositorioSessao, repositorioSala, telaCadastroFilme, telaCadastroSala, notificador);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Sessões de Cinema 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Funcionários");
            Console.WriteLine("Digite 2 para Gerenciar Gêneros de Filme");
            Console.WriteLine("Digite 3 para Gerenciar as Salas");
            Console.WriteLine("Digite 4 para Gerenciar os Filmes");
            Console.WriteLine("Digite 5 para Gerenciar os Sessão");

            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela =  telaCadastroFuncionario;

            else if (opcao == "2")
                tela =  telaCadastroGenero;

            else if (opcao == "3")
                tela =  telaCadastroSala;

            else if (opcao == "4")
                tela =  telaCadastroFilme;

            else if (opcao == "5")
                tela =  telaCadastroSessao;

            return tela;
        }
    }
}
