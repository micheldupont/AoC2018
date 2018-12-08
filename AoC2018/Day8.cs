using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    public class Day8
    {
        private readonly List<int> _inputs;

        public Day8(string input)
        {
            var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            _inputs = parts.Select(int.Parse).ToList();
        }

        public int InputLength => _inputs.Count;

        public Node ParseNodes()
        {
            var root = new Node();
            ParseNodeAt(0, root);

            return root;
        }

        private int ParseNodeAt(int index, Node thisNode)
        {
            var currentIndex = index;

            thisNode.ChildCount = _inputs[currentIndex];
            currentIndex++;

            thisNode.MetadataLength = _inputs[currentIndex];
            currentIndex++;
 
            for (var i = 0; i < thisNode.ChildCount; i++)
            {            
                var child = new Node();
                thisNode.ChildNodes.Add(child);
                currentIndex = ParseNodeAt(currentIndex, child);
            }

            for (var i = 0; i < thisNode.MetadataLength; i++)
            {
                thisNode.Metadata.Add(_inputs[currentIndex]);
                currentIndex++;
            }

            return currentIndex;
        }
    }

    public class Node
    {
        public int ChildCount { get; set; }
        public int MetadataLength { get; set; }

        public List<Node> ChildNodes { get; }
        public List<int> Metadata { get;}

        public Node()
        {
            ChildCount = 0;
            ChildNodes = new List<Node>();

            MetadataLength = 0;
            Metadata = new List<int>();
        }

        public int GetChecksum()
        {
            var checksum = Metadata.Sum();

            foreach (var childNode in ChildNodes)
            {
                checksum += childNode.GetChecksum();
            }

            return checksum;
        }

        public int GetValue()
        {
            var value = 0;

            if (ChildCount == 0)
            {
                return Metadata.Sum();
            }

            foreach (var i in Metadata)
            {
                if(i == 0)
                    continue;

                var index = i - 1;
                if(index >= ChildCount)
                    continue;

                value += ChildNodes[index].GetValue();
            }

            return value;
        }
    }
}