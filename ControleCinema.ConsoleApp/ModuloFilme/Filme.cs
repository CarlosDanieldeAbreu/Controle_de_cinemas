using ControleCinema.ConsoleApp.ModuloGenero;
using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        private readonly string _titulo;
        private readonly double _duracao;
        private readonly Genero _genero;

        public string Titulo { get => _titulo; }
        public double Duracao { get => _duracao; }
        public Genero Genero { get => _genero; }

        public Filme(string titulo, double duracao, Genero genero)
        {
            _titulo = titulo;
            _duracao = duracao;
            _genero = genero;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Título: " + Titulo + Environment.NewLine +
                   "Duração: " + Duracao + Environment.NewLine +
                   "Gênero: " + Genero.Descricao + Environment.NewLine;
        }
    }
}
