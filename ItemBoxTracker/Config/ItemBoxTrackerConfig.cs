using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HunterPie.Plugins;

namespace MHWItemBoxTracker.Config
{
    public class ItemBoxTrackerConfig
    {
        [DisplayName("Track Always")]
        [Description("Items to track everywhere")]
        public TrackingTabConfig Always { get; set; } = new TrackingTabConfig();
        
        [DisplayName("Track in Village")]
        [Description("Items to track only in the village/gathering hub")]
        public TrackingTabConfig Village { get; set; } = new TrackingTabConfig();
        
        [DisplayName("Track in Quest")]
        [Description("Items to track during a quest/expedition/guiding lands")]
        public TrackingTabConfig Quest { get; set; } = new TrackingTabConfig();
    }
}
