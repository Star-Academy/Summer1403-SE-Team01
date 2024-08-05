using System.Diagnostics;
using FullTextSearch.Core;

namespace FullTextSearch.Controller.InvertedIndexController;

public class InvertedIndexMapper : IInvertedIndexMapper
{
    /*public Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents)
    {
        var invertedIndex = documents
            .SelectMany(document => document.Words.Select(word => new { Word = word, Document = document }))
            .GroupBy(x => x.Word)
            .ToDictionary(entry => entry.Key, entry => entry.Select(x => x.Document));

        return invertedIndex;
    }*/
    
    public Dictionary<string, IEnumerable<Document>> Map(IEnumerable<Document> documents)
    {
        Dictionary<string, List<Document>> invertedIndex = new Dictionary<string, List<Document>>();
    
        foreach (var d in documents)
        {
            var s = d.Words.ToList();
        
            for (int i = 0; i < s.Count(); i++)
            {
                for (int j = i + 1; j <= Math.Min(s.Count(), i  + 5); j++)
                {
                    var c = "";
                
                    for (int k = i; k < j - 1; k++)
                    {
                        c += s[k] + " ";
                    }
                
                    c += s[j - 1];

                    if (invertedIndex.ContainsKey(c))
                    {
                        invertedIndex[c].Add(d);
                    }
                    else
                    {
                        invertedIndex[c] = new List<Document> { d };
                    }
                }
            }
        }
        return invertedIndex.ToDictionary(pair => pair.Key, pair => (IEnumerable<Document>)pair.Value);
    }
}