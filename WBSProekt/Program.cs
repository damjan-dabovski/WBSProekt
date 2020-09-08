using System;
using System.Text;
using WBSProekt.Crawler;
using WBSProekt.Mapper;

namespace WBSProekt {
    class Program {
        private const string GAME_LIST_BASE_URL = "https://boardgamegeek.com/browse/boardgame";

        static void Main(string[] args) {
            var crawler = new BggPageCrawler();
            var mapper = new BoardGameRdfMapper();

            var crawledGames = crawler.CrawlGamesFromBggList(GAME_LIST_BASE_URL, 1, 1);
            foreach (var game in crawledGames) {
                mapper.MapBasicProperties(game);
            }

            mapper.WriteGraph();
        }
    }
}
