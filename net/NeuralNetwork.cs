using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace net
{
    public class NeuralNetwork
    {
        public readonly int Layers;
        public readonly int MaxWidth;
        public readonly int OutputLayer;
        public readonly int InputLayer;

        private readonly Random _random;

        public Node[][] Nodes { get; set; }

        public Node[] Inputs
        {
            get => Nodes[InputLayer];
            set => Nodes[InputLayer] = value;
        }

        public Node[] Outputs
        {
            get => Nodes[OutputLayer];
            set => Nodes[OutputLayer] = value;
        }

        public NeuralNetwork(int layers, int maxWidth, int inputs, int outputs)
        {
            _random = new Random();

            Layers = layers;
            MaxWidth = maxWidth;
            InputLayer = 0;
            OutputLayer = layers - 1;
            
            Nodes = new Node[layers][];
            
            Nodes[0] = new Node[inputs];
            for (int i = 0; i < inputs; i++)
            {
                Nodes[0][i] = new Node();
            }

            for (var layer = 1; layer < layers; layer++)
            {
                Nodes[layer] = new Node[maxWidth];
            }

            Nodes[layers-1] = new Node[outputs];
            for (int i = 0; i < outputs; i++)
            {
                Nodes[layers-1][i] = new Node();
            }
        }

        public void Update()
        {
            for (var layer = Layers-1; layer >= 0; layer--)
            {
                for (var index = 0; index < Nodes[layer].Length; index++)
                {
                    if (Nodes[layer][index] == null) continue;

                    var sum = 0f;
                    foreach (var nodeConnection in Nodes[layer][index].Connections)
                    {
                        sum += Nodes[nodeConnection.FromNodeLayerIndex][nodeConnection.FromNodeIndex].Value * nodeConnection.Weight;
                    }

                    Nodes[layer][index].Value = (float) Math.Tanh(sum);
                }
            }
        }

        public void AddNode( int layer, int index)
        {
            Nodes[layer][index] = new Node();
        }

        public void AddNode(int layer, int index, Node node)
        {
            Nodes[layer][index] = node;
        }

        public void AddConnection(float weight, int fromLayer, int fromIndex, int toLayer, int toIndex)
        {
            var toNode = Nodes[toLayer][toIndex] ?? new Node();


            toNode.Connections.Add(new Connection
            {
                Weight = weight,
                FromNodeLayerIndex = fromLayer,
                FromNodeIndex = fromIndex
            });
            Nodes[toLayer][toIndex] = toNode;
            Nodes[fromLayer][fromIndex] = Nodes[fromLayer][fromIndex] ?? new Node();
        }

        public void AddRandomConnection()
        {
            var fromLayer = _random.Next(0,OutputLayer+1);
            var toLayer = _random.Next(0, OutputLayer + 1);
            int fromIndex;
            int toIndex;

            if (fromLayer == InputLayer) fromIndex = _random.Next(Inputs.Length);
            else if (fromLayer == OutputLayer) fromIndex = _random.Next(Outputs.Length);
            else fromIndex = _random.Next(0, MaxWidth);

            if (toLayer == InputLayer) toIndex = _random.Next(Inputs.Length);
            else if (toLayer == OutputLayer) toIndex = _random.Next(Outputs.Length);
            else toIndex = _random.Next(0, MaxWidth);

            AddConnection(
                weight: (float) (_random.NextDouble() * 2) - 1,
                fromLayer: fromLayer,
                fromIndex: fromIndex, 
                toLayer: toLayer, 
                toIndex: toIndex);
        }
    }
}
