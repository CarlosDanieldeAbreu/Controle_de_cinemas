using ControleCinema.ConsoleApp.ModuloGenero;
using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    { 
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly IRepositorio<Genero> _repositorioGenero;
        private readonly TelaCadastroGenero _telaCadastroGenero;
        private readonly Notificador _notificador;

        public TelaCadastroFilme(IRepositorio<Filme> repositorioFilme, IRepositorio<Genero> repositorioGenero, TelaCadastroGenero telaCadastroGenero, Notificador notificador) : base("Cadastro de Filmes")
        {
            _repositorioFilme = repositorioFilme;
            _notificador = notificador;
            _repositorioGenero = repositorioGenero;
            _telaCadastroGenero = telaCadastroGenero;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Filme");
            bool temFilmeCadastrado = VisualizarRegistros("Pesquisando");

            if (temFilmeCadastrado == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para editar,", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();
            Genero generoSelecionado = ObtemGenero();
            Filme filmeAtualizado = ObterFilme(generoSelecionado);

            bool conseguiuEditar = _repositorioFilme.Editar(numeroFilme, filmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala editada com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFilme.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmesRegistrados = VisualizarRegistros("Pesquisando");

            if (temFilmesRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFilme.Excluir(numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala excluída com sucesso1", TipoMensagem.Sucesso);
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Filme");
            Genero generoSelecionado = ObtemGenero();
            Filme novoFilme = ObterFilme(generoSelecionado);
            _repositorioFilme.Inserir(novoFilme);
            _notificador.ApresentarMensagem("Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        private Filme ObterFilme(Genero generoSelecionado)
        {
            Console.WriteLine("Digite o título do filme: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Digite a duração do filme");
            double duracao = double.Parse(Console.ReadLine());
            return new Filme(titulo, duracao, generoSelecionado);
        }
        private Genero ObtemGenero()
        {
            bool temGeneroDisponiveis = _telaCadastroGenero.VisualizarRegistros("");

            if (!temGeneroDisponiveis)
            {
                _notificador.ApresentarMensagem("Não há nenhum gênero disponível para cadastrar em filmes", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do gênero que irá inserir: ");
            int numGeneroSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Genero generoSelecionado = _repositorioGenero.SelecionarRegistro(numGeneroSelecionado);

            return generoSelecionado;
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = _repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
