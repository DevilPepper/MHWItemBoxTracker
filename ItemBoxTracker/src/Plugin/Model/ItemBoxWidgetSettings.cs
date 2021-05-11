namespace MHWItemBoxTracker.Model {
  public class ItemBoxWidgetSettings : HunterPie.Core.Settings.IWidgetSettings {
    public bool Initialize { get; set; } = true;
    public bool Enabled { get; set; } = true;
    public int[] Position { get; set; } = new int[] { 20, 20 };
    public float Opacity { get; set; } = 1;
    public double Scale { get; set; } = 1;
    public bool StreamerMode { get; set; } = false;
  }
}
