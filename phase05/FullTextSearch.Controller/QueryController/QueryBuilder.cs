using FullTextSearch.Controller.QueryController.Abstraction;
using FullTextSearch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using FullTextSearch.Controller.TextFormatter.Abstraction;
using InvertedIndex.Controller.Document;

namespace FullTextSearch.Controller.QueryController;

public class QueryBuilder : IQueryBuilder
{
    private readonly Query _query;
    private readonly IQueryFormatter _queryFormatter;
    private readonly ITextFormatter _textFormatter;
    public QueryBuilder(IQueryFormatter queryFormatter, ITextFormatter textFormatter)
    {
        _query = new Query();
        _queryFormatter = queryFormatter ?? throw new ArgumentNullException(nameof(queryFormatter));
        _textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
    }

    public void BuildText(string text)
    {
        _query.Text = text;
    }

    public void BuildWordsBySign(IEnumerable<char> signs)
    {
        _query.WordsBySign.Clear();
        
        var splittedText = _queryFormatter.Split(_queryFormatter.ToUpper(_query.Text), " ").ToList();
        var quoteIndices = _textFormatter.GetQuoteIndices(splittedText);
        var indicesToRemove = _textFormatter.GetIndicesToRemove(quoteIndices);
        var filteredWords = _textFormatter.FilterOutIndices(splittedText, indicesToRemove);
        var concatenatedQuotes = _textFormatter.ConcatenateQuotedWords(splittedText, quoteIndices);
        
        ProcessWordsBySign(filteredWords, signs);
        ProcessWordsBySign(concatenatedQuotes, signs);
    }
    private void ProcessWordsBySign(IEnumerable<string> words, IEnumerable<char> signs)
    {
        
        foreach (var sign in signs)
        {
            if (!_query.WordsBySign.ContainsKey(sign))
            {
                _query.WordsBySign[sign] = new List<string>();
            }
            var queryWords = words.ToList();
            var texts = _queryFormatter.CollectBySign(queryWords, sign);
            words = queryWords.Except(texts).ToList();
            _query.WordsBySign[sign] = _query.WordsBySign[sign].Concat(_queryFormatter.RemovePrefix(texts)).ToList();
        }
        if (!_query.WordsBySign.ContainsKey(' '))
        {
            _query.WordsBySign[' '] = new List<string>();
        }
        _query.WordsBySign[' '] = _query.WordsBySign[' '].Concat(_queryFormatter.RemovePrefix(words)).ToList();
    }
    
    public Query GetQuery()
    {
        return _query;
    }
}
