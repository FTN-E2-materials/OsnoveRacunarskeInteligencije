using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaiveBayes
{
    public class NaiveBayes
    {
        Dictionary<int, double> documents_sentiment_count = new Dictionary<int, double>();
        public static Dictionary<string, int> vocabulary = new Dictionary<string, int>();
        public static Dictionary<int, Dictionary<string, int>> word_counts = new Dictionary<int, Dictionary<string, int>>();

        public NaiveBayes()
        {
            documents_sentiment_count[0] = 0.0;
            documents_sentiment_count[1] = 0.0;
            word_counts[0] = new Dictionary<string, int>();
            word_counts[1] = new Dictionary<string, int>();
        }
        /// <summary>
        /// Formiranje globalnog recnika, recnika iz pozitivnih i negativnih recenzija 
        /// </summary>
        /// <param name="model">train model ucitan iz tsv datoteke</param>
        public void fit(DataModel model)
        {
            for (int i = 0; i < model.Text.Count; i++)
            {
                string text = model.Text[i];
                int sentiment = model.Sentiment[i];

                documents_sentiment_count[sentiment] += 1;
                string[] words = TextUtil.Tokenize(text);
                Dictionary<string, int> counts = TextUtil.CountWords(words);

                foreach (KeyValuePair<string, int> item in counts)
                {
                    string word = item.Key;
                    int count = item.Value;

                    //TODO 5 - Popuniti globalni recnik svih reci, kao i recnike za odredjene sentimente
                    if (vocabulary.ContainsKey(word))
                    {
                        vocabulary[word] += count;
                    } else
                    {
                        vocabulary[word] = count;
                    }

                    Dictionary<string, int> sentiment_count = word_counts[sentiment];
                    if (sentiment_count.ContainsKey(word))
                    {
                        sentiment_count[word] += count;
                    } else
                    {
                        sentiment_count[word] = count;
                    }
                }
            }
        }
        /// <summary>
        /// Racunanje verovatnoca za prosledjeni tekst
        /// </summary>
        /// <param name="text">Tekst koji se klasifikuje</param>
        public void predict(string text)
        {
            string[] words = TextUtil.Tokenize(text);
            var counts = TextUtil.CountWords(words);

            double documentCount = documents_sentiment_count.Values.Sum();
            //TODO 6 - Izracunati verovatnoce da je dokument za predikciju bas pozitivnog ili negativnog sentimenta - P(cj)
            double Pcj_neg = documents_sentiment_count[0] / documentCount;
            double Pcj_pos = documents_sentiment_count[1] / documentCount;


            double log_prob_neg = 0.0;
            double log_prob_pos = 0.0;

            double sum_count_neg = word_counts[0].Values.Sum();
            double sum_count_pos = word_counts[1].Values.Sum();
            double V = vocabulary.Count;

            foreach (KeyValuePair<string, int> item in counts)
            {
                string w = item.Key;
                int cnt = item.Value;
                //TODO 7.1 - Iterativno racunati logaritamski zbir verovatnoca sentimenta svake reci
                double count_neg = word_counts[0].ContainsKey(w) ? word_counts[0][w] : 0;
                double count_pos = word_counts[1].ContainsKey(w) ? word_counts[1][w] : 0;

                log_prob_neg += Math.Log(count_neg + 1) - Math.Log(sum_count_neg + V);
                log_prob_pos += Math.Log(count_pos + 1) - Math.Log(sum_count_pos + V);
            }

            //TODO 7.2 Izracunati konacnu vrednost verovatnoce sentimenta prosledjenog teksta
            log_prob_neg += Math.Log(Pcj_neg);
            log_prob_pos += Math.Log(Pcj_pos);

            //TODO 8 - Ispisati vrednosti predikcije za pozitivan i negativan sentiment teksta
            Console.WriteLine("positive: {0}\nnegative: {1}\nsolution: {2}", log_prob_pos, log_prob_neg, log_prob_pos > log_prob_neg ? "POSITIVE" : "NEGATIVE");
        }
    }
}
