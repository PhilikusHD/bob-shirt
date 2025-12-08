using Avalonia.Controls;
using Bob.Core.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bob.Core.Utils
{
    public class ViewManager
    {
        private static Dictionary<string, UserControl> m_AvailableViews = new();

        public static MainView Host { get; set; }

        public ViewManager()
        {
        }

        public static void AddToLookup(string name, UserControl control)
        {
            bool exists = m_AvailableViews.ContainsKey(name);
            if (exists)
                return;

            m_AvailableViews.Add(name, control);
        }

        public static UserControl GetControl(string name)
        {
            bool exists = m_AvailableViews.ContainsKey(name);
            return exists ? m_AvailableViews[name] : null;
        }

        public static void TransitionTo(string name)
        {
            var control = GetControl(name);
            if (control == null)
            {
                Logger.Error($"Type: {name} not registered.");
                Logger.Error("Available:");
                foreach (var view in m_AvailableViews)
                {
                    Logger.Error($"- {view.Key}");
                }
            }
            Host.NavigateTo(control);

            if (control is MainPage mainPage)
            {
                mainPage.UpdateLoginDisplay();
            }
        }
    }
}