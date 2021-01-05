using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasmobranch
{
    public static class BitwiseCountdownSolver
    {
        public static string Solve(int[] cards, int target)
        {
            var queue = new Queue<CardSequence>();
            foreach (var card in cards) queue.Enqueue(new CardSequence(card));

            var cardSequence = queue.Peek();
            while (cardSequence.Total != target && queue.Count != 0)
            {
                cardSequence = queue.Dequeue();

                foreach (var otherCard in cards)
                {
                    if (cards.Count(f => f == otherCard) - cardSequence.Cards.Count(f => f == otherCard) <= 0) continue;

                    foreach (var op in new[] {"+", "-", ">>", "<<", "&", "|", "^"})
                        queue.Enqueue(cardSequence.Copy().AddCard(otherCard, op));
                }

                if (Math.Abs(cardSequence.Total - target) < 5)
                    Console.WriteLine(cardSequence.ToString());
            }

            return cardSequence.ToString();
        }

        private class CardSequence
        {
            public readonly List<int> Cards = new List<int>();
            public readonly List<string> Operators = new List<string>();
            public int Total;

            public CardSequence(int card) => AddCard(card, "+");

            private CardSequence(List<int> cards, List<string> operators, int total)
            {
                Cards = cards;
                Operators = operators;
                Total = total;
            }

            public CardSequence AddCard(int value, string op)
            {
                Cards.Add(value);
                Operators.Add(op);
                Total = Evaluate(Total, value, op);
                return this;
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

            public override string ToString()
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.Append(Cards[0]);
                var total = Cards[0];

                for (var i = 1; i < Cards.Count; i++)
                {
                    total = Evaluate(total, Cards[i], Operators[i]);
                    stringBuilder.Append($" {Operators[i]} {Cards[i]} = {total}");
                }

                return stringBuilder.ToString();
            }

            public CardSequence Copy() => 
                new CardSequence(new List<int>(Cards), new List<string>(Operators), Total);
        }
    }
}