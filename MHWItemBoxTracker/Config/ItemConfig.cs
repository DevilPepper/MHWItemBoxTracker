using System.ComponentModel.DataAnnotations;

namespace MHWItemBoxTracker.Config
{
    public class ItemConfig
    {
        public string Name { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
