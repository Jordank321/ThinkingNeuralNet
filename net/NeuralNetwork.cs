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
    }
}
