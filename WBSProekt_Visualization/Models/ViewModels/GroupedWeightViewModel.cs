using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBSProekt_Visualization.Models.ViewModels {
    public class GroupedWeightViewModel {
        public Dictionary<string, int> Groupings { get; set; }

        public GroupedWeightViewModel() {
            Groupings = new Dictionary<string, int>();
            Groupings.Add("0-1", 0);
            Groupings.Add("1-2", 0);
            Groupings.Add("2-3", 0);
            Groupings.Add("3-4", 0);
            Groupings.Add("4-5", 0);
        }

        public void Insert(double number) {
            if (number >= 0 && number < 1) {
                Groupings["0-1"]++;
            } else if (number >= 1 && number < 2) {
                Groupings["1-2"]++;
            } else if (number >= 2 && number < 3) {
                Groupings["2-3"]++;
            } else if (number >= 3 && number < 4) {
                Groupings["3-4"]++;
            } else {
                Groupings["4-5"]++;
            }
        }
    }
}