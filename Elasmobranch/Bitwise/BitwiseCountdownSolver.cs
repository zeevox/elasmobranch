using System;
using System.Collections.Generic;

namespace Elasmobranch.Bitwise
{
    public static class BitwiseCountdownSolver
    {
        public static string Solve(int[] cards, int target)
        {
            var queue = new Queue<Card>();
            foreach (var card in cards) queue.Enqueue(new Card(card));

            var cardSequence = queue.Peek();
            while (cardSequence.Total != target && queue.Count != 0)
            {
                cardSequence = queue.Dequeue();

                var otherCards = new List<int>(cards);
                var previous = cardSequence;
                do
                {
                    otherCards.Remove(previous.Value);
                    previous = previous.Previous;
                } while (previous != null);

                foreach (var otherCard in otherCards)
                foreach (var op in new[] {"+", "-", ">>", "<<", "&", "|", "^"})
                    queue.Enqueue(new Card(otherCard, op, cardSequence));
                
                if (cardSequence.Total - target == 0)
                    return cardSequence.ToString();
            }

            return "No solution found";
        }

        private class Card
        {
            public readonly int Value;
            public readonly string Operator;
            public readonly Card Previous;
            public readonly int Total;
            
            public Card(int value, string op = "+", Card previous = null)
            {
                Value = value;
                Operator = op;
                Previous = previous;
                Total = Evaluate(previous?.Total ?? 0, value, op);
            }

            public override string ToString() => Previous == null ? Total.ToString() : Previous + $" {Operator} {Value} = {Total}";
        }
        
        private static int Evaluate(int value1, int value2, string op)
        {
            return op switch
            {
                "+" => value1 + value2,
                "-" => value1 - value2,
                ">>" => value1 >> value2,
                "<<" => value1 << value2,
                "&" => value1 & value2,
                "|" => value1 | value2,
                "^" => value1 ^ value2,
                _ => throw new ArgumentException("Invalid operator")
            };
        }
    }
}