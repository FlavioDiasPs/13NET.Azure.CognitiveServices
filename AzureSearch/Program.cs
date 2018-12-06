using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureSearch
{
    class IndexLetras
    {
        [Key]
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Id { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string NomeBanda { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        public string Album { get; set; }
        [IsRetrievable(true)]
        [IsSortable]
        [IsFilterable]
        [IsSearchable]
        public string Letra { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "4A91F9970AB73ADB4106D6BC56FD0EE2";
            var client = new SearchServiceClient("teste-azuresearch-dois", new SearchCredentials(apiKey));

            var index = client.Indexes.GetClient("index-bandas");
            var index2 = client.Indexes.Get("index-bandas");

            index2.Analyzers.Add(new PatternAnalyzer { Name = "custom", Pattern = @"||,||" });

            index2.Analyzers.Add(new CustomAnalyzer
            {
                Name = "custom",
                Tokenizer = TokenizerName.Standard,
                TokenFilters = new []
                {
                    TokenFilterName.Phonetic,
                    TokenFilterName.Lowercase,
                    TokenFilterName.AsciiFolding
                }
            });

            index2.Fields[3].Analyzer = "custom";

            client.Indexes.CreateOrUpdate(index2, true);

            var batch = IndexBatch.Upload<IndexLetras>(new List<IndexLetras>()
            {
                new IndexLetras()
                {
                    Id = "a330286",
                    Album = "Quer aprender a mascar chiclete?",
                    Letra = "Quer quer quer,  quer aprender a mascar chiclete??? vem vem com o gordinho sexy, vemvem com o gordinho sexy !! pararam paaaaaaaaam pam panam panam panam agora palavras só pra tentar aparecer na busca da galera, fé, rappa, alelui, glória a deus, busca mutantes thiago, haaaaaa buscou o próprio nooooome, mas então justin bieber é o que há ta ligado na quebrada que o rolê do esquema brinca de pé com muamba. eu hora desligado",
                    NomeBanda = "MC_Chiclete"
                }
            });

            index.Documents.Index(batch);

            var term = Console.ReadLine();
            var result = index.Documents.Search<IndexLetras>(term,
                new SearchParameters { IncludeTotalResultCount = true });

            Console.WriteLine($"{result.Count} resultados encontrados");
            foreach (var item in result.Results)
                Console.WriteLine($"{item.Document.Id} - {item.Document.NomeBanda}");

            Console.Read();
        }
    }
}
