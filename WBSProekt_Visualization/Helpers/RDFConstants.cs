using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VDS.RDF;

namespace WBSProekt_Visualization.Helpers {
    public static class RDFConstants {
        public const string PREFIX_ONTOLOGY = "http://davidcastella.com/boardgameontology#";
        public const string PREFIX_RDF = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        public const string PREFIX_OWL = "http://www.w3.org/2002/07/owl#";
        public const string PREFIX_XSD = "http://www.w3.org/2001/XMLSchema#";
        public const string PREFIX_TERMS = "http://purl.org/dc/terms/";

        public const string BOARD_GAME = PREFIX_ONTOLOGY + "BoardGame";
        public const string TITLE = PREFIX_TERMS + "title";
        public const string TYPE = PREFIX_RDF + "type";
        public const string YEAR_PUBLISHED = PREFIX_ONTOLOGY + "yearPublished";
        public const string MIN_AGE = PREFIX_ONTOLOGY + "minimumAge";
        public const string MIN_PLAYERS = PREFIX_ONTOLOGY + "minimumPlayers";
        public const string MAX_PLAYERS = PREFIX_ONTOLOGY + "maximumPlayers";
        public const string PLAYING_TIME = PREFIX_ONTOLOGY + "playingTime";
        public const string AVG_RATING = PREFIX_ONTOLOGY + "averageRating";
        public const string AVG_WEIGHT = PREFIX_ONTOLOGY + "averageWeight";
        public const string ARTIST = PREFIX_ONTOLOGY + "artist";
        public const string DESIGNER = PREFIX_ONTOLOGY + "designer";
        public const string CREATOR = PREFIX_TERMS + "creator";
        public const string PUBLISHER = PREFIX_ONTOLOGY + "publisher";
        public const string PUBLISHES = PREFIX_ONTOLOGY + "publishes";
        public const string NAME = "http://schema.org/name";
    }
}