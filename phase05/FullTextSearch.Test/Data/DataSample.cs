﻿using FullTextSearch.Core;

namespace FullTextSearch.Test.Data
{
    public static class DataSample
    {
        
        public static List<Document> GetDocuments()
        {
            Document document1 = new Document()
            {
                Name = "DOC1",
                Path = "./ResourcesTest/Doc1",
                Text = "reza ali hello",
                Words = new List<string> { "reza", "ali", "hello" },
            };

            Document document2 = new Document()
            {
                Name = "DOC2",
                Path = "./ResourcesTest/Doc2",
                Text = "reza mohammad hello",
                Words = new List<string> { "reza", "mohammad", "hello" },
            };
            
            Document document3 = new Document()
            {
                Name = "DOC3",
                Path = "./ResourcesTest/Doc3",
                Text = "reza ali mohammad",
                Words = new List<string> { "reza", "ali", "mohammad" },
            };

            return new List<Document> { document1, document2, document3 };
        }
        public static Dictionary<string, IEnumerable<Document>> GetInvertedIndexMap(Document document1, Document document2, Document document3)
        {
            return new Dictionary<string, IEnumerable<Document>>
            {
                { "reza", new List<Document> { document1, document2, document3 } },
                { "mohammad", new List<Document> { document2, document3 } },
                { "ali", new List<Document> { document1, document3 } },
                { "hello", new List<Document> { document1, document2 } },
                { "reza ali", new List<Document> { document3, document1 } },
                { "reza ali hello", new List<Document> { document1 } },
                { "ali hello", new List<Document> { document1 } },
                { "reza mohammad", new List<Document> { document2 } },
                { "reza mohammad hello", new List<Document> { document2 } },
                { "mohammad hello", new List<Document> { document2 } },
                { "reza ali mohammad", new List<Document> { document3 } },
                { "ali mohammad", new List<Document> { document3 } },
            };
        }
        
    }
}