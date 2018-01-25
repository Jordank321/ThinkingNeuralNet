using System;
using System.Collections.Generic;
using System.Text;

namespace net
{
    public class EvolutionManager
    {
        private readonly int _networkCount;
        private readonly float _mutationchance;

        public List<NeuralNetwork> Networks { get; private set; }

        private readonly Random _random;

        public EvolutionManager(int networks, int startconnections, float mutationchance, int layers, int width, int inputs, int outputs)
        {
            _random = new Random();

            _networkCount = networks;
            _mutationchance = mutationchance;
            Networks = new List<NeuralNetwork>();
            for (var i = 0; i < networks; i++)
            {
                var neuralNetwork = new NeuralNetwork(layers, width, inputs, outputs);
                for (var j = 0; j < startconnections; j++)
                {
                    neuralNetwork.AddRandomConnection();
                }

                Networks.Add(neuralNetwork);
            }
        }

        public void SurvivingNetworks(IEnumerable<NeuralNetwork> survivingNetworks)
        {
            var networks = new List<NeuralNetwork>();
            foreach (var survivingNetwork in survivingNetworks)
            {
                var daughterNetwork = survivingNetwork;
                if(_random.NextDouble() < _mutationchance) survivingNetwork.AddRandomConnection();
                networks.Add(survivingNetwork);
                networks.Add(daughterNetwork);
            }

            while (networks.Count > _networkCount)
            {
                networks.RemoveAt(_random.Next(networks.Count));
            }

            Networks = networks;
        }
    }
}
