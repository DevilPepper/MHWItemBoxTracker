using HunterPie.GUI;
using HunterPie.Core;
using System.Collections.Generic;
using System.Windows;
using MHWItemBoxTracker.Config;
using System.Linq;
using Debugger = HunterPie.Logger.Debugger;

namespace MHWItemBoxTracker.GUI
{
    public partial class ItemBoxTracker : Widget
    {
        public ItemBoxTracker()
        {
            InitializeComponent();
            BaseWidth = Width;
            BaseHeight = Height;
            SetWindowFlags();
            ApplySettings();
        }

        public void setItemsToDisplay(List<ItemBoxRow> itemBoxRows)
        {
            Dispatch(() =>
            {
                theList.ItemsSource = itemBoxRows;
                Debugger.Log(itemBoxRows.Count > 0 ? "Loading Box!!!" : "Hiding Box!!!");
                WidgetHasContent = (itemBoxRows.Count > 0);
                ChangeVisibility();
            }
            );

        }


        public override void EnterWidgetDesignMode()
        {
            base.EnterWidgetDesignMode();
            RemoveWindowTransparencyFlag();
        }

        public override void LeaveWidgetDesignMode()
        {
            base.LeaveWidgetDesignMode();
            ApplyWindowTransparencyFlag();
            SaveSettings();
        }

        private void SaveSettings()
        {
            var config = ConfigLoader.loadConfig();

            config.Overlay.Position[0] = (int)Left - UserSettings.PlayerConfig.Overlay.Position[0];
            config.Overlay.Position[1] = (int)Top - UserSettings.PlayerConfig.Overlay.Position[1];

            ConfigLoader.saveConfig(config);
        }

        public override void ApplySettings(bool FocusTrigger = false)
        {
            Dispatch(() =>
            {
                if (!FocusTrigger)
                {
                    var config = ConfigLoader.loadConfig();

                    Left = config.Overlay.Position[0] + UserSettings.PlayerConfig.Overlay.Position[0];
                    Top = config.Overlay.Position[1] + UserSettings.PlayerConfig.Overlay.Position[1];
                    WidgetActive = config.Overlay.Enabled;
                    Opacity = config.Overlay.Opacity;
                    // Debugger.Log($"Theme: {UserSettings.PlayerConfig.HunterPie.Theme}");
                }
                base.ApplySettings();
            });
        }
        private void Dispatch(System.Action function) => Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, function);

        private void OnMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                MoveWidget();
                //SaveSettings();
            }
        }
    }

    public class ItemBoxRow
    {
        public string name { get; set; }
        public string ratio { get; set; }
        public double progress { get; set; }
    }
}
