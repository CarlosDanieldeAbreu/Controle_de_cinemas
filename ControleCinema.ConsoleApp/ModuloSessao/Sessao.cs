using ControleCinema.ConsoleApp.ModuloSala;
using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        private readonly int _numeroMaximoIngressos;
        private readonly DateTime _dataHora;
        private readonly Sala _sala;
        private readonly Filme _filme;

        public int NumeroMaximoIngressos { get => _numeroMaximoIngressos; }
        public Sala Sala { get => _sala; }
        public Filme Filme { get => _filme; }
        public DateTime DataHora { get => _dataHora; }

        public Sessao(int numeroMaximoIngressos, Sala sala, Filme filme, DateTime dataHora)
        {
            _numeroMaximoIngressos = numeroMaximoIngressos;
            _sala = sala;
            _filme = filme;
            _dataHora = dataHora;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine + 
                   "Número maximo de ingressos: " + NumeroMaximoIngressos + Environment.NewLine +
                   "Sala: " + Sala.id + Environment.NewLine +
                   "Filme: " + Filme.Titulo + Environment.NewLine +
                   "Data e hora do filme: " + DataHora + Environment.NewLine;
        }
    }
}
