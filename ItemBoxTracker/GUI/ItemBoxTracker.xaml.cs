﻿using HunterPie.GUI;
using HunterPie.Core;
using HunterPie.Core.Settings;
using System.Collections.Generic;
using System.Windows;
using MHWItemBoxTracker.Config;
using MHWItemBoxTracker.Utils;
using System.Linq;
using HunterPie.Plugins;
using static MHWItemBoxTracker.Main;
using static MHWItemBoxTracker.Utils.Dispatcher;

using Newtonsoft.Json;

namespace MHWItemBoxTracker.GUI
{
    public partial class ItemBoxTracker : Widget
    {
        private static string settingsJson = "widget.settings.json";
        private ItemBoxWidgetSettings widgetSettings {get; set;}
        public override IWidgetSettings Settings => widgetSettings;
        public ItemBoxTracker() : base()
        {
            InitializeComponent();
            Dispatch(async () => {
                widgetSettings = await Plugin.LoadJson<ItemBoxWidgetSettings>(settingsJson);
                Plugin.Log($"Loaded widget settings...{JsonConvert.SerializeObject(widgetSettings)}");
                Plugin.Log($"Settings: {JsonConvert.SerializeObject(Settings)}");

                ApplySettings();
            });
        }

        public void setItemsToDisplay(List<ItemBoxRow> itemBoxRows)
        {
            // Debugger.Log($"Theme: {UserSettings.PlayerConfig.HunterPie.Theme}");
            theList.ItemsSource = itemBoxRows;
            WidgetHasContent = (itemBoxRows.Count > 0);
            ChangeVisibility();
        }

        public override void EnterWidgetDesignMode() {
            base.EnterWidgetDesignMode();
            RemoveWindowTransparencyFlag();
        }

        public override void LeaveWidgetDesignMode() {
            base.LeaveWidgetDesignMode();
            ApplyWindowTransparencyFlag();
            Plugin.Log($"Saving widget settings...{JsonConvert.SerializeObject(widgetSettings)}");
            Dispatch(async () => { await Plugin.SaveJson(settingsJson, widgetSettings); });
        }
    }

    public class ItemBoxRow
    {
        public string name { get; set; }
        public string ratio { get; set; }
        public double progress { get; set; }
    }
}
