using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Elasmobranch.Encryption
{
    public class RivestShamirAdleman
    {
        public readonly BigInteger CommonKey;
        public readonly BigInteger PrivateKey;
        public readonly BigInteger PublicKey;

        /// <summary>
        ///     Generate an RSA Key Pair
        /// </summary>
        /// <see href="https://en.wikipedia.org/wiki/RSA_(cryptosystem)#Example" />
        public RivestShamirAdleman()
        {
            // Choose two distinct prime numbers
            List<BigInteger> primes;
            do
            {
                primes = GenerateRandomPrimes(2);
            } while (primes[0] == primes[1]);

            // find their product
            var product = primes[0] * primes[1];

            // compute the Carmichael's totient function of the product as λ(n) = lcm(p − 1, q − 1) 
            var carmichaelLambda = LeastCommonMultiple(primes[0] - 1, primes[1] - 1);

            // public key — choose any number 1 < e < λ(n) that is coprime to λ(n).
            var publicKey = new BigInteger(3);
            while (GreatestCommonDivisor(publicKey, carmichaelLambda) != 1) publicKey += 2;

            // compute the private key, the modular multiplicative inverse of the public key (mod λ(n))
            var privateKey = ModularMultiplicativeInverse(publicKey, carmichaelLambda);

            // save the generated keys
            PrivateKey = privateKey;
            PublicKey = publicKey;
            CommonKey = product;
        }

        /// <summary>
        ///     Initialise an RSA Key Pair object with existing key pair values
        /// </summary>
        /// <param name="privateKey">int the private key</param>
        /// <param name="publicKey">int the public key</param>
        /// <param name="commonKey">the the shared mod value</param>
        public RivestShamirAdleman(BigInteger privateKey, BigInteger publicKey, BigInteger commonKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
            CommonKey = commonKey;
        }

        /// <summary>
        ///     Encrypt, using the public key, a plaintext message
        /// </summary>
        /// <param name="message">The plaintext message</param>
        /// <returns>The encrypted ciphertext</returns>
        /// <remarks>
        ///     We had to resort to BigInteger here otherwise even very small values easily overflowed,
        ///     making my life very difficult. BigInteger kindly provides a wonderful method for finding the
        ///     result of modular exponentiation efficiently. I was overjoyed.
        /// </remarks>
        /// <see href="https://en.wikipedia.org/wiki/Modular_exponentiation" />
        public BigInteger Encrypt(BigInteger message)
        {
            return BigInteger.ModPow(message, PublicKey, CommonKey);
        }

        public string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(StringToBigInt(input)).ToByteArray(isBigEndian: true));
        }

        /// <summary>
        ///     Decrypt, using the private key, an encrypted message
        /// </summary>
        /// <param name="encrypted">The encrypted ciphertext</param>
        /// <returns>The plaintext message</returns>
        public BigInteger Decrypt(BigInteger encrypted)
        {
            return BigInteger.ModPow(encrypted, PrivateKey, CommonKey);
        }

        public string Decrypt(string input)
        {
            return BigIntToString(Decrypt(new BigInteger(Convert.FromBase64String(input), isBigEndian: true)));
        }

        private static string BigIntToString(BigInteger input)
        {
            return Encoding.UTF8.GetString(input.ToByteArray(isBigEndian: true));
        }

        private static BigInteger StringToBigInt(string input)
        {
            var bigint = new BigInteger(Encoding.UTF8.GetBytes(input), isBigEndian: true);
            Console.WriteLine(bigint);
            return bigint;
        }

        /// <summary>
        ///     Generate an array of boolean values, where each value represents whether the index is prime
        /// </summary>
        /// <param name="limit">The highest number to check for prime divisibility</param>
        /// <returns>A boolean array where each value represents whether the index is prime</returns>
        /// <see href="https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes#Pseudocode" />
        private static BitArray SieveOfEratosthenes(int limit)
        {
            var bitArray = new BitArray(limit + 1, true) {[0] = false, [1] = false, [2] = true};
            for (var i = 0; i * i <= limit; i++)
            {
                if (!bitArray[i]) continue;
                for (var j = i * i; j <= limit; j += i) bitArray[j] = false;
            }

            return bitArray;
        }

        /// <summary>
        ///     Use the Sieve of Eratosthenes to generate a list of primes
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static List<BigInteger> GenerateListOfPrimes(int limit)
        {
            if (limit > int.MaxValue - 1) throw new OverflowException();

            var primes = new List<BigInteger>();

            var bits = SieveOfEratosthenes(limit);
            for (var i = 0; i < bits.Length; i++)
                if (bits[i])
                    primes.Add(new BigInteger(i));

            return primes;
        }

        /// <summary>
        ///     Generate a list of "random" prime numbers
        /// </summary>
        /// <param name="numberOfPrimes">Number of random primes to generate</param>
        /// <param name="rangeStart">[optional] range lower bound within which to search for primes</param>
        /// <param name="rangeEnd">[optional] range upper bound within which to search for primes</param>
        /// <returns>A list of randomly selected prime numbers</returns>
        private static List<BigInteger> GenerateRandomPrimes(int numberOfPrimes, int rangeStart = 1000,
            int rangeEnd = 10000)
        {
            // list of primes within the specified range
            var primes = GenerateListOfPrimes(rangeEnd).Where(number => number >= rangeStart).ToList();

            // list to store the randomly selected primes from the range
            var randomPrimes = new List<BigInteger>();

            // random number generation and thus element selection
            var random = new Random();
            for (var i = 0; i < numberOfPrimes; i++) randomPrimes.Add(primes[random.Next(primes.Count)]);

            return randomPrimes;
        }

        /// <summary>
        ///     Find the greatest common divisor of two integers
        /// </summary>
        /// <param name="a">one integer</param>
        /// <param name="b">another integer</param>
        /// <returns>their greatest common divisor</returns>
        /// <see href="https://en.wikipedia.org/wiki/Euclidean_algorithm#Implementations" />
        public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            // use a while loop instead of recursion for efficiency
            while (true)
            {
                // break once gcd found
                if (b == 0) return a;

                // if we could do parallel assignment easily this would be equivalent to a, b = b, a % b
                var a1 = a;
                a = b;
                b = a1 % b;
            }
        }

        /// <summary>
        ///     Find the modular multiplicative inverse, given two integers
        /// </summary>
        /// <param name="a">integer a</param>
        /// <param name="m">modulus m</param>
        /// <returns>modular multiplicative inverse x</returns>
        /// <see href="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse#Computation" />
        public static BigInteger ModularMultiplicativeInverse(BigInteger a, BigInteger m)
        {
            // use the Extended Euclidean Algorithm to find x in the Bézout identity 
            // the y coefficient does not bother us since my % m = 0, it can be discarded
            // https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm#Pseudocode
            var r = new[] {a, m};
            var s = new[] {new BigInteger(1), new BigInteger(0)};

            // use a while loop instead of recursion for mAxImUm eFFiCiEnCY
            while (r[1] != 0)
            {
                var quotient = r[0] / r[1];
                r = new[] {r[1], r[0] - quotient * r[1]};
                s = new[] {s[1], s[0] - quotient * s[1]};
            }

            // s[0], or old_s as wiki calls it, is x in the bézout identity
            // add m in order to handle a negative x result
            return (s[0] % m + m) % m;
        }

        /// <summary>
        ///     Find the least common multiple of two integers
        /// </summary>
        /// <param name="a">one integer</param>
        /// <param name="b">another integer</param>
        /// <returns>their least common multiple</returns>
        /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple#Calculation" />
        public static BigInteger LeastCommonMultiple(BigInteger a, BigInteger b)
        {
            return BigInteger.Abs(a * b) / GreatestCommonDivisor(a, b);
        }
    }
}