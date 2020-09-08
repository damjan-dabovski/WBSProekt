using System;
using System.Collections.Generic;
using System.Text;
using VDS.RDF;

namespace WBSProekt.Mapper {
    public static class MapperConstants {
        public const string BOARD_GAME = "bgg:BoardGame";
        public const string TITLE = "terms:title";
        public const string TYPE = "rdf:type";
        public const string YEAR_PUBLISHED = "bgg:yearPublished";
        public const string MIN_AGE = "bgg:minimumAge";
        public const string MIN_PLAYERS = "bgg:minimumPlayers";
        public const string MAX_PLAYERS = "bgg:maximumPlayers";
        public const string PLAYING_TIME = "bgg:playingTime";
        public const string AVG_RATING = "bgg:averageRating";
        public const string AVG_WEIGHT = "bgg:averageWeight";
        public const string ARTIST = "bgg:artist";
        public const string DESIGNER = "bgg:designer";
        public const string CREATOR = "terms:creator";
        public const string PUBLISHER = "bgg:publisher";
        public const string PUBLISHES = "bgg:publishes";
        public const string NAME = "http://schema.org/name";

        public const string PREFIX_ONTOLOGY = "http://davidcastella.com/boardgameontology#";
        public const string PREFIX_RDF = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        public const string PREFIX_OWL = "http://www.w3.org/2002/07/owl#";
        public const string PREFIX_XSD = "http://www.w3.org/2001/XMLSchema#";
        public const string PREFIX_TERMS = "http://purl.org/dc/terms/";

        public static void SetPrefixList(IGraph graph) {
            graph.NamespaceMap.AddNamespace("bgg", new Uri(PREFIX_ONTOLOGY));
            graph.NamespaceMap.AddNamespace("rdf", new Uri(PREFIX_RDF));
            graph.NamespaceMap.AddNamespace("owl", new Uri(PREFIX_OWL));
            graph.NamespaceMap.AddNamespace("xsd", new Uri(PREFIX_XSD));
            graph.NamespaceMap.AddNamespace("terms", new Uri(PREFIX_TERMS));
        }
    }
}
