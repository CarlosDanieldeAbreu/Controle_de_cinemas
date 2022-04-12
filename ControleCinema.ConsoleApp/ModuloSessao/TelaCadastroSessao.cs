using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly IRepositorio<Sessao> _repositorioSessao;
        private readonly IRepositorio<Sala> _repositorioSala;
        private readonly TelaCadastroFilme _telaCadastroFilme;
        private readonly TelaCadastroSala _telaCadastroSala;
        private readonly Notificador _notificador;

        public TelaCadastroSessao(IRepositorio<Filme> repositorioFilme, IRepositorio<Sessao> repositorioSessao, IRepositorio<Sala> repositorioSala, TelaCadastroFilme telaCadastroFilme, TelaCadastroSala telaCadastroSala, Notificador notificador) : base("Cadastro de sessão")
        {
            _repositorioFilme = repositorioFilme;
            _repositorioSessao = repositorioSessao;
            _repositorioSala = repositorioSala;
            _telaCadastroFilme = telaCadastroFilme;
            _telaCadastroSala = telaCadastroSala;
            _notificador = notificador;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sessão");
            bool temSessaoCadastrada = VisualizarRegistros("Pesquisando");

            if (temSessaoCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão cadastrada para editar,", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();
            Filme filmeSelecionado = ObtemFilme();
            Sala salaSelecionada = ObtemSala();
            Sessao sessaoAtualizada = ObterSessao(filmeSelecionado, salaSelecionada);

            bool conseguiuEditar = _repositorioSessao.Editar(numeroSessao, sessaoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão editada com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Sessão que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Sessão não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessão");
            bool temSessaoCadastrada = VisualizarRegistros("Pesquisando");

            if (temSessaoCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão cadastrada para excluir,", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroRegistro();
            bool conseguiuExcluir = _repositorioSessao.Excluir(numeroSessao);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sessão excluida com sucesso!", TipoMensagem.Sucesso);
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sessão");
            Filme filmeSelecionado = ObtemFilme();
            Sala salaSelecionada = ObtemSala();
            Sessao novaSessao = ObterSessao(filmeSelecionado, salaSelecionada);
            _repositorioSessao.Inserir(novaSessao);
            _notificador.ApresentarMensagem("Sessão cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        private Sessao ObterSessao(Filme filmeSelecionado, Sala salaSelecionada)
        {
            Console.WriteLine("Digite a data e a hora");
            DateTime dataEhora = DateTime.Parse(Console.ReadLine());
            int numeroMaximoIngressos = salaSelecionada.Capacidade;
            return new Sessao(numeroMaximoIngressos, salaSelecionada, filmeSelecionado, dataEhora);
        }

        private Sala ObtemSala()
        {
            bool temSalasDisponiveis = _telaCadastroSala.VisualizarRegistros("");

            if (!temSalasDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhuma Sala disponível para cadastrar em sessão", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da Sala que irá inserir: ");
            int numSalaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Sala salaSelecionada = _repositorioSala.SelecionarRegistro(numSalaSelecionada);

            return salaSelecionada;
        }

        private Filme ObtemFilme()
        {
            bool temFilmesDisponiveis = _telaCadastroFilme.VisualizarRegistros("");

            if (!temFilmesDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum Filme disponível para cadastrar em sessão", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do filme que irá inserir: ");
            int numFilmeSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Filme filmeSelecionado = _repositorioFilme.SelecionarRegistro(numFilmeSelecionada);

            return filmeSelecionado;
        }
        public bool VisualizarRegistrosAgrupadosPorFilmes(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = _repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = _repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma Sessão disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
