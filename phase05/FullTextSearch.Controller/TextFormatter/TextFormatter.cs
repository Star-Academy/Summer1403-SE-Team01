using FullTextSearch.Controller.TextFormatter.Abstraction;

namespace FullTextSearch.Controller.TextFormatter;

public class TextFormatter : ITextFormatter
{
    public List<int> GetQuoteIndices(List<string> words)
    {
        var quoteIndices = new List<int>();
        for (int i = 0; i < words.Count; i++)
            if (words[i].StartsWith("\"") || words[i].EndsWith("\"") || (words[i].Length > 1 && words[i][1] == '\"'))
                quoteIndices.Add(i);
        return quoteIndices;
    }
    public List<int> GetIndicesToRemove(List<int> quoteIndices)
    {
        var indicesToRemove = new List<int>();
        for (int i = 0; i < quoteIndices.Count; i += 2)
        {
            if (i + 1 < quoteIndices.Count)
            {
                int start = quoteIndices[i];
                int end = quoteIndices[i + 1];
                for (int j = start; j <= end; j++)
                {
                    indicesToRemove.Add(j);
                }
            }
        }
        return indicesToRemove;
    }
    public List<string> FilterOutIndices(List<string> words, List<int> indicesToRemove)
    {
        return words.Where((word, index) => !indicesToRemove.Contains(index)).ToList();
    }
    public List<string> ConcatenateQuotedWords(List<string> words, List<int> quoteIndices)
    {
        var result = new List<string>();
        for (int i = 0; i < quoteIndices.Count - 1; i += 2)
        {
            int start = quoteIndices[i];
            int end = quoteIndices[i + 1];
            
            var sublist = words.Skip(start).Take(end - start + 1).ToList();
            var concatenated = string.Join(" ", sublist);
            result.Add(concatenated);
        }
        return result;
    }
}