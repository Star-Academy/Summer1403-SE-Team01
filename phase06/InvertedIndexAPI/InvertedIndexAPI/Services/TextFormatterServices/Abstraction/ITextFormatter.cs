namespace FullTextSearch.Controller.TextFormatter.Abstraction;

public interface ITextFormatter
{
    List<int> GetQuoteIndices(List<string> words);
    List<int> GetIndicesToRemove(List<int> quoteIndices);
    List<string> FilterOutIndices(List<string> words, List<int> indicesToRemove);
    List<string> ConcatenateQuotedWords(List<string> words, List<int> quoteIndices);
}