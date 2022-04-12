using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class Sala : EntidadeBase
    {
        private readonly int _capacidade;
        private readonly int _numeroAssentos;

        public int Capacidade { get => _capacidade; }
        public int NumeroAssentos { get => _numeroAssentos; }

        public Sala(int capacidade, int numeroAssentos)
        {
            _capacidade = capacidade;
            _numeroAssentos = numeroAssentos;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Capacidade: " + Capacidade + Environment.NewLine +
                   "Número de assentos: " + NumeroAssentos + Environment.NewLine;
        }
    }
}
