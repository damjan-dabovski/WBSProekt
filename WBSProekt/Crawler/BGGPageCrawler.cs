using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WBSProekt.Crawler {
    public class BggPageCrawler {
        private const string BASE_URL = "https://boardgamegeek.com";
        private const string regexForJson = "(?<=GEEK.geekitemPreload = ).*(?=;)";

        private HttpClient httpClient { get; set; }
        public string BaseUrl { get; set; }

        public BggPageCrawler() {
            httpClient = new HttpClient();
        }

        public async Task<byte[]> FetchPage(string url) {
            //we get the page as a byte[] because of UTF8 encoding not cooperating well with strings
            var rawPageData = await httpClient.GetByteArrayAsync(url);
            return rawPageData;
        }

        public BoardGame ParseBoardGamePage(byte[] rawPageData, string url) {
            string pageHtml = Encoding.UTF8.GetString(rawPageData);

            var document = new HtmlDocument();
            document.LoadHtml(pageHtml);

            var scriptNode = document.DocumentNode.ChildNodes.FindFirst("script");

            var scriptJson = Regex.Match(scriptNode.InnerHtml, regexForJson);

            dynamic parsedJson = JsonConvert.DeserializeObject(scriptJson.Value);


            BoardGame parsedBoardGame = new BoardGame() {
                Uri = url,
                Title = parsedJson.item.name,
                YearPublished = Convert.ToInt32(parsedJson.item.yearpublished),
                MinAge = Convert.ToInt32(parsedJson.item.minage),
                MinPlayTime = Convert.ToInt32(parsedJson.item.minplaytime),
                MaxPlayTime = Convert.ToInt32(parsedJson.item.maxplaytime),
                MinPlayers = Convert.ToInt32(parsedJson.item.minplayers),
                MaxPlayers = Convert.ToInt32(parsedJson.item.maxplayers),
                DesignerUri = BASE_URL + parsedJson.item.links.boardgamedesigner[0].href,
                DesignerName = parsedJson.item.links.boardgamedesigner[0].name,
                ArtistUri = BASE_URL + parsedJson.item.links.boardgameartist[0].href,
                ArtistName = parsedJson.item.links.boardgameartist[0].name,
                PublisherUri = BASE_URL + parsedJson.item.links.boardgamepublisher[0].href,
                PublisherName = parsedJson.item.links.boardgamepublisher[0].name,
                AverageRating = Convert.ToDouble(parsedJson.item.stats.average),
                AverageWeight = Convert.ToDouble(parsedJson.item.stats.avgweight)
            };

            return parsedBoardGame;
        }

        public List<string> GetGameUrlsFromList(string gameListBaseUrl, int page) {
            var parsedUrls = new List<string>();
            string url = $"{gameListBaseUrl}/page/{page.ToString()}";

            var rawPageData = this.FetchPage(url);
            string pageHtml = Encoding.UTF8.GetString(rawPageData.Result);

            var document = new HtmlDocument();
            document.LoadHtml(pageHtml);

            var tableRootElement = document.GetElementbyId("collectionitems");
            //FOR TESTING ONLY - proof-of-concept with only 5 games - real implementation is commented out
            //var tableRows = tableRootElement.ChildAttributes("tr").Skip(1).ToList();
            var tableRows = tableRootElement.ChildNodes.Where(node => node.Name == "tr").Skip(1).Take(10).ToList();

            foreach (var row in tableRows) {
                var targetTableCell = row.ChildNodes.Where(node => node.HasClass("collection_objectname")).First();
                var targetCellDiv = targetTableCell.ChildNodes.Where(node => node.Name == "div").Skip(1).First();
                var targetAnchor = targetCellDiv.ChildNodes.Where(node => node.Name == "a").First();
                parsedUrls.Add(targetAnchor.Attributes.Where(attr => attr.Name == "href").First().Value);
            }

            return parsedUrls;
        }

        public List<BoardGame> CrawlGamesFromBggList(string gameListBaseUrl, int startPage, int endPage) {
            var parsedGames = new List<BoardGame>();

            var urlsToCrawl = new List<string>();

            for (int i = startPage; i <= endPage; i++) {
                urlsToCrawl.AddRange(this.GetGameUrlsFromList(gameListBaseUrl, i));
            }

            foreach (var url in urlsToCrawl) {
                string fullGameUrl = BASE_URL + url;
                var rawPageData = this.FetchPage($"{fullGameUrl}/credits");
                BoardGame parsedBoardGame = this.ParseBoardGamePage(rawPageData.Result, fullGameUrl);
                parsedGames.Add(parsedBoardGame);
            }

            return parsedGames;
        }
    }
}
