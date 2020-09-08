using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VDS.RDF;
using VDS.RDF.Parsing;
using WBSProekt_Visualization.Models.ViewModels;

namespace WBSProekt_Visualization.Helpers {
    public static class RDFHelper {
        public static IGraph graph;

        public static IGraph LoadGraph() {
            if (RDFHelper.graph == null) {
                graph = new Graph();
                UriLoader.Load(graph, new Uri("https://www.dropbox.com/s/pkhn61383jqni8v/boardgame.ttl?dl=1"), new TurtleParser());
            }
            return graph;
        }

        public static List<string> GetGameNames() {
            IGraph g = RDFHelper.LoadGraph();

            List<string> result = new List<string>();

            foreach (var triple in g.Triples) {
                result.Add(triple.Subject.ToString());
            }

            return result;
        }

        public static List<BoardGameViewModel> GetAverageRatings() {
            IGraph g = RDFHelper.LoadGraph();

            List<BoardGameViewModel> ratings= new List<BoardGameViewModel>();

            IUriNode ratingNode = graph.CreateUriNode(UriFactory.Create(RDFConstants.AVG_RATING));

            var triples = graph.GetTriplesWithPredicate(ratingNode);

            ratings.AddRange(triples.Select(triple => new BoardGameViewModel {
                Uri = triple.Subject.ToString(),
                AverageRating = Convert.ToDouble(triple.Object.ToString())
            }));

            return ratings;
        }

        public static List<RatingWeightViewModel> GetRatingOverWeight() {
            IGraph g = RDFHelper.LoadGraph();

            List<RatingWeightViewModel> scores = new List<RatingWeightViewModel>();

            IUriNode ratingNode = graph.CreateUriNode(UriFactory.Create(RDFConstants.AVG_RATING));
            IUriNode weightNode = graph.CreateUriNode(UriFactory.Create(RDFConstants.AVG_WEIGHT));
            IUriNode titleNode = graph.CreateUriNode(UriFactory.Create(RDFConstants.TITLE));

            var ratingTriples = graph.GetTriplesWithPredicate(ratingNode);
            var weightTriples = graph.GetTriplesWithPredicate(weightNode);
            var titleTriples = graph.GetTriplesWithPredicate(titleNode);

            foreach (var ratingTriple in ratingTriples) {
                var weightTriple = weightTriples.First(t => t.Subject.ToString().Equals(ratingTriple.Subject.ToString()));
                var titleTriple = titleTriples.First(t => t.Subject.ToString().Equals(ratingTriple.Subject.ToString()));
                scores.Add(new RatingWeightViewModel {
                    Uri = ratingTriple.Subject.ToString(),
                    Title = titleTriple.Object.ToString(),
                    RatingWeightRatio = Convert.ToDouble(ratingTriple.Object.ToString()) / Convert.ToDouble(weightTriple.Object.ToString())
                });
            }

            return scores;
        }

        public static GroupedWeightViewModel GetGroupedWeights() {
            IGraph g = RDFHelper.LoadGraph();

            GroupedWeightViewModel groups = new GroupedWeightViewModel();

            IUriNode weightNode = graph.CreateUriNode(UriFactory.Create(RDFConstants.AVG_WEIGHT));

            var weightTriples = graph.GetTriplesWithPredicate(weightNode);

            foreach (var triple in weightTriples) {
                groups.Insert(Convert.ToDouble(triple.Object.ToString()));
            }

            return groups;            
        }

    }
}