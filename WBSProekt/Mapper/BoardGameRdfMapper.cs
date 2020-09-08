using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;
using VDS.RDF.Writing;
using static WBSProekt.Mapper.MapperConstants;

namespace WBSProekt.Mapper {
    public class BoardGameRdfMapper {
        private readonly IGraph graph = new Graph();
        private IUriNode rdfType;

        private IUriNode boardGameNode;
        private IUriNode titleNode;
        private IUriNode yearPublishedNode;
        private IUriNode minAgeNode;
        private IUriNode minPlayersNode;
        private IUriNode maxPlayersNode;
        private IUriNode playTimeNode;
        private IUriNode ratingNode;
        private IUriNode weightNode;

        private IUriNode artistNode;
        private IUriNode designerNode;
        private IUriNode creatorNode;
        private IUriNode publisherNode;
        private IUriNode nameNode;
        private IUriNode publishesNode;

        public BoardGameRdfMapper() {
            this.Init();
        }

        public void Init() {
            MapperConstants.SetPrefixList(graph);

            rdfType = graph.CreateUriNode(TYPE);

            boardGameNode = graph.CreateUriNode(BOARD_GAME);
            titleNode = graph.CreateUriNode(TITLE);
            yearPublishedNode = graph.CreateUriNode(YEAR_PUBLISHED);
            minAgeNode = graph.CreateUriNode(MIN_AGE);
            minPlayersNode = graph.CreateUriNode(MIN_PLAYERS);
            maxPlayersNode = graph.CreateUriNode(MAX_PLAYERS);
            playTimeNode = graph.CreateUriNode(PLAYING_TIME);
            ratingNode = graph.CreateUriNode(AVG_RATING);
            weightNode = graph.CreateUriNode(AVG_WEIGHT);

            artistNode = graph.CreateUriNode(ARTIST);
            designerNode = graph.CreateUriNode(DESIGNER);
            creatorNode = graph.CreateUriNode(CREATOR);
            publisherNode = graph.CreateUriNode(PUBLISHER);
            nameNode = graph.CreateUriNode(UriFactory.Create(NAME));
            publishesNode = graph.CreateUriNode(PUBLISHES);
        }

        public void MapBasicProperties(BoardGame boardGame) {
            IUriNode gameUri = graph.CreateUriNode(UriFactory.Create(boardGame.Uri));
            graph.Assert(new Triple(gameUri, rdfType, boardGameNode));

            ILiteralNode gameTitle = graph.CreateLiteralNode(boardGame.Title);
            graph.Assert(new Triple(gameUri, titleNode, gameTitle));

            ILiteralNode yearPublished = graph.CreateLiteralNode(boardGame.YearPublished.ToString());
            graph.Assert(new Triple(gameUri, yearPublishedNode, yearPublished));

            ILiteralNode minAge = graph.CreateLiteralNode(boardGame.MinAge.ToString());
            graph.Assert(new Triple(gameUri, minAgeNode, minAge));

            ILiteralNode minPlayers = graph.CreateLiteralNode(boardGame.MinPlayers.ToString());
            graph.Assert(new Triple(gameUri, minPlayersNode, minPlayers));

            ILiteralNode maxPlayers = graph.CreateLiteralNode(boardGame.MaxPlayers.ToString());
            graph.Assert(new Triple(gameUri, maxPlayersNode, maxPlayers));

            ILiteralNode playTime = graph.CreateLiteralNode(boardGame.MinPlayTime.ToString());
            graph.Assert(new Triple(gameUri, playTimeNode, playTime));

            ILiteralNode averageRating = graph.CreateLiteralNode(boardGame.AverageRating.ToString());
            graph.Assert(new Triple(gameUri, ratingNode, averageRating));

            ILiteralNode averageWeight = graph.CreateLiteralNode(boardGame.AverageWeight.ToString());
            graph.Assert(new Triple(gameUri, weightNode, averageWeight));

            MapPeople(boardGame, gameUri);
        }

        public void MapPeople(BoardGame boardGame, IUriNode gameUri) {
            IUriNode artist = graph.CreateUriNode(UriFactory.Create(boardGame.ArtistUri));
            graph.Assert(new Triple(artist, rdfType, artistNode));
            graph.Assert(new Triple(gameUri, artistNode, artist));

            ILiteralNode artistName = graph.CreateLiteralNode(boardGame.ArtistName);
            graph.Assert(new Triple(artist, nameNode, artistName));

            IUriNode designer = graph.CreateUriNode(UriFactory.Create(boardGame.DesignerUri));
            graph.Assert(new Triple(designer, rdfType, designerNode));
            graph.Assert(new Triple(gameUri, creatorNode, designer));

            ILiteralNode designerName = graph.CreateLiteralNode(boardGame.DesignerName);
            graph.Assert(new Triple(designer, nameNode, designerName));

            IUriNode publisher = graph.CreateUriNode(UriFactory.Create(boardGame.PublisherUri));
            graph.Assert(new Triple(publisher, rdfType, publisherNode));
            graph.Assert(new Triple(publisher, publishesNode, gameUri));

            ILiteralNode publisherName = graph.CreateLiteralNode(boardGame.PublisherName);
            graph.Assert(new Triple(publisher, nameNode, publisherName));
        }

        public void WriteGraph() {
            CompressingTurtleWriter writer = new CompressingTurtleWriter();

            writer.Save(graph, "C:\\Users\\PC\\Desktop\\boardgame.ttl");
        }

        //FOR TESTING ONLY
        public void LogGraph() {
            foreach (var triple in graph.Triples) {
                Console.WriteLine(triple.ToString());
            }
        }
    }
}
