using FullTextSearch.Controller.SearchController;
using FullTextSearch.Core;

namespace FullTextSearch.Test;

public class PlusSearcherTest
{
    private readonly ISearcher _sut = new PlusSearcher();
    
    
    [Test]
    public void SearchTest()
    {
        // Arange
        Query query = new Query();
        query.Text = "cat +reza -demand!";
        
        query.WordsBySign = new Dictionary<char, IEnumerable<string>>()
        {
            {'+', new List<string>(){"reza"}},
            {'-', new List<string>(){"demand"}},
            {' ', new List<string>(){"cat"}},
        };

        Document document1 = new Document();
        document1.Name = "Doc1";
        document1.Path = "./ResourcesTest/Doc1";
        document1.Text = "reza ali mohammad hello";
        document1.Words = new List<string> {"cat", "reza", "hello"};
            
            
        Document document2 = new Document();
        document2.Name = "Doc2";
        document2.Path = "./ResourcesTest/Doc2";
        document2.Text = "reza ali mohammad hello";
        document2.Words = new List<string> {"cat", "reza", "demand"};
            
            
        Document document3 = new Document();
        document3.Name = "Doc3";
        document3.Path = "./ResourcesTest/Doc3";
        document3.Text = "reza ali mohammad hello";
        document3.Words = new List<string> {"cat", "ali", "hello"};

        var invertedIndex = new Dictionary<string, IEnumerable<Document>>
        {
            { "cat", new List<Document> { document1, document2, document3} },
            { "reza", new List<Document> {document1, document2} },
            { "demand", new List<Document> { document2 } }
        };
        var expected = new List<Document>() {document1, document2};

        // Act
        var result = _sut.Search(query, invertedIndex);
        
        // Assert
        Xunit.Assert.True(result.SequenceEqual(expected));
    }
}