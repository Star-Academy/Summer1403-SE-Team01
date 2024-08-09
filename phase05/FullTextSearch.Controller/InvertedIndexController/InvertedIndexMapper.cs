using System;
using System.Collections.Generic;
using System.Linq;
using FullTextSearch.Controller.InvertedIndexController.Abstraction;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController;

public class InvertedIndexMapper : IInvertedIndexMapper
{
    public Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents)
    {
        var invertedIndex = new Dictionary<string, List<Document>>();

        foreach (var document in documents)
        {
            var words = document.Words.ToList();

            for (int startIndex = 0; startIndex < words.Count; startIndex++)
            {
                for (int endIndex = startIndex + 1; endIndex <= Math.Min(words.Count, startIndex + 5); endIndex++)
                {
                    var phrase = string.Join(" ", words.Skip(startIndex).Take(endIndex - startIndex));
                    
                    if (!invertedIndex.ContainsKey(phrase))
                        invertedIndex.Add(phrase, new List<Document>(){document});
                    
                    else if(!invertedIndex[phrase].Contains(document))
                        invertedIndex[phrase].Add(document);
                }
            }
        }
        return invertedIndex.ToDictionary(pair => pair.Key, pair => (IEnumerable<Document>)pair.Value);
    }
}