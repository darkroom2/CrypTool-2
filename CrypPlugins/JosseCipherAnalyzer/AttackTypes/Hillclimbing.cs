﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace CrypTool.JosseCipherAnalyzer.AttackTypes
{
    public class Hillclimbing : AttackType
    {
        private int _initialRestarts = int.MaxValue;
        private int _restarts;
        private int Restarts
        {
            get => _restarts;
            set
            {
                _restarts = value;
                _initialRestarts = value;
            }
        }
        private char[] Alphabet { get; }
        private int KeyLength { get; }
        private ResultEntry _finalResultEntry;

        public Hillclimbing(string alphabet, int keyLength, int restarts, JosseCipherAnalyzerPresentation presentation)
        {
            Alphabet = alphabet.ToUpper().ToCharArray();
            KeyLength = keyLength;
            Presentation = presentation;
            Restarts = restarts;
        }

        public override ResultEntry Start(string ciphertext)
        {
            ciphertext = ciphertext.ToUpper();
            var globalHighestCost = double.MinValue;
            var bestKey = new char[KeyLength];
            var alphabetLength = Alphabet.Length;
            var random = new Random(Guid.NewGuid().GetHashCode());
            var lastTime = DateTime.Now;
            var keys = 0;
            var plaintext = string.Empty;
            var runKey = new char[KeyLength];
            var cipher = new CipherImplementation {Alphabet = new string(Alphabet)};


            while (Restarts > 0)
            {
                runKey = Alphabet.OrderBy(_ => random.Next()).Take(KeyLength).ToArray();

                bool betterKeyFound;
                var highestCost = double.MinValue;

                cipher.BuildDictionaries(runKey);

                do
                {
                    betterKeyFound = false;
                    for (var i = 0; i < KeyLength; i++)
                    {
                        for (var j = 0; j < alphabetLength; j++)
                        {
                            var oldLetter = runKey[i];
                            runKey[i] = Alphabet[j];
                            if (runKey.Distinct().Count() != runKey.Length)
                            {
                                continue;
                            }
                            cipher.BuildDictionaries(runKey);
                            plaintext = cipher.Decipher(ciphertext);

                            keys++;
                            var currentCost = Evaluator.Evaluate(plaintext);

                            if (currentCost > highestCost)
                            {
                                highestCost = currentCost;
                                bestKey = (char[]) runKey.Clone();
                                betterKeyFound = true;
                            }
                            else
                            {
                                //reset key
                                runKey[i] = oldLetter;
                                if (j == alphabetLength - 1)
                                {
                                    cipher.BuildDictionaries(runKey);
                                    plaintext = cipher.Decipher(ciphertext);
                                }
                            }

                            if (ShouldStop)
                            {
                                return null;
                            }

                            if (DateTime.Now < lastTime.AddMilliseconds(100))
                            {
                                continue;
                            }

                            var keysDispatcher = keys*10;
                            Presentation.Dispatcher.Invoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                try
                                {
                                    Presentation.Keys.Value = $"{keysDispatcher:0,0}";
                                }
                                // ReSharper disable once EmptyGeneralCatchClause
                                catch (Exception)
                                {
                                    // ignore
                                }
                            }, null);
                            keys = 0;
                            lastTime = DateTime.Now;
                        }
                    }

                    runKey = (char[]) bestKey.Clone();
                } while (betterKeyFound);

                Restarts--;


                if (highestCost > globalHighestCost)
                {
                    globalHighestCost = highestCost;
                    cipher.BuildDictionaries(bestKey);
                    var bestPlaintext = cipher.Decipher(ciphertext);
                    AddNewBestListEntry(bestKey, highestCost, bestPlaintext);
                }

                OnProcessChanged(_initialRestarts == 0 ? 0 : _restarts / _initialRestarts);
            }

            return _finalResultEntry;
        }

        /// <summary>
        /// Adds an entry to the BestList
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="plaintext"></param>
        private void AddNewBestListEntry(char[] key, double value, string plaintext)
        {
            var entry = new ResultEntry
            {
                Key = new string(key),
                Text = plaintext,
                Value = value
            };

            if (Presentation.BestList.Count == 0 || entry.Value > Presentation.BestList.First().Value)
            {
                _finalResultEntry = entry;
            }

            Presentation.Dispatcher.Invoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                try
                {
                    if (Presentation.BestList.Count > 0 && entry.Value <= Presentation.BestList.Last().Value)
                    {
                        return;
                    }
                    //Insert new entry at correct place to sustain order of list:
                    var insertIndex = Presentation.BestList.TakeWhile(e => e.Value > entry.Value).Count();
                    Presentation.BestList.Insert(insertIndex, entry);

                    if (Presentation.BestList.Count > MaxBestListEntries)
                    {
                        Presentation.BestList.RemoveAt(MaxBestListEntries);
                    }
                    var ranking = 1;
                    foreach (var e in Presentation.BestList)
                    {
                        e.Ranking = ranking;
                        ranking++;
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            }, null);
        }
    }
}