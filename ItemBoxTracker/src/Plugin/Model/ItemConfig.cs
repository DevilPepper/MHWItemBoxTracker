using System.ComponentModel.DataAnnotations;

namespace MHWItemBoxTracker.Model {
  public class ItemConfig {
    public string Name { get; set; }
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int Amount { get; set; }
  }
}
