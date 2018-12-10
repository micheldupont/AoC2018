using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018
{
    public class Day9
    {
        private readonly int _playerCount;
        private readonly int _lastMarbleValue;
        private const int ScoreTrigger = 23;

        public LinkedList<int> Marbles { get; }
        public List<Score> Scores { get; }

        public Day9(int playerCount, int lastMarbleValue)
        {
            _playerCount = playerCount;
            _lastMarbleValue = lastMarbleValue;
 
            Marbles = new LinkedList<int>();
            Scores = new List<Score>();

            Simulate();
        }

        private void Simulate()
        {
            Marbles.AddFirst(0);
            
            if (_lastMarbleValue >= 1)
            {
                Marbles.AddLast(1);
            }

            var currentNode = Marbles.Last;

            for (var newMarbleValue = 2; newMarbleValue <= _lastMarbleValue; newMarbleValue++)
            {
                if (newMarbleValue % ScoreTrigger == 0)
                {
                    var nodeToRemove = MoveCcw(currentNode, 7);

                    currentNode = nodeToRemove.Next;
                    Marbles.Remove(nodeToRemove);

                    Scores.Add(new Score {Player = newMarbleValue % _playerCount, Value = newMarbleValue + nodeToRemove.Value});
                }
                else
                {
                    var targetNode = MoveCw(currentNode, 2);

                    if (targetNode == Marbles.First)
                    {
                        currentNode = Marbles.AddLast(newMarbleValue);
                    }
                    else
                    {
                        currentNode = Marbles.AddBefore(targetNode, newMarbleValue);
                    }
                }
            }
        }

        private LinkedListNode<int> MoveCw(LinkedListNode<int> currentNode, int steps)
        {
            var newNode = currentNode;
            for (var i = 0; i < steps; i++)
            {
                newNode = newNode.Next ?? Marbles.First;
            }

            return newNode;
        }

        private LinkedListNode<int> MoveCcw(LinkedListNode<int> currentNode, int steps)
        {
            var newNode = currentNode;
            for (var i = 0; i < steps; i++)
            {
                newNode = newNode.Previous ?? Marbles.Last;
            }

            return newNode;
        }

        public long GetMaxScore()
        {
            var maxScore = Scores.GroupBy(s => s.Player).Select(grp => new {id = grp.Key, totalValue = grp.Sum(s => s.Value)})
                .OrderByDescending(p => p.totalValue).Select(p => p.totalValue).First();

            return maxScore;
        }

        public class Score
        {
            public int Player { get; set; }
            public long Value { get; set; }
        }
    }
}