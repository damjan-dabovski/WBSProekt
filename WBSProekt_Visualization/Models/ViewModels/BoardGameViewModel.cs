using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBSProekt_Visualization.Models.ViewModels {
    public class BoardGameViewModel {
        public string Uri { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }

        public int MinAge { get; set; }
        public int MinPlayTime { get; set; }
        public int MaxPlayTime { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public string DesignerUri { get; set; }
        public string DesignerName { get; set; }
        public string ArtistUri { get; set; }
        public string ArtistName { get; set; }
        public string PublisherUri { get; set; }
        public string PublisherName { get; set; }

        public double AverageRating { get; set; }
        public double AverageWeight { get; set; }
    }
}