using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class EventTypes : UserControl
    {
        public EventTypes()
        {
            InitializeComponent();
            LoadData();
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        public void Reload()
        {
            LoadData();
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = EventTypesDG.SelectedItem as EventType;
            if (selected != null)
            {
                AddEventTypeWindow addEventTypeWindow = new AddEventTypeWindow(selected.Id);
                await addEventTypeWindow.ShowDialog(MainMainWindow);
                EvType1.Reload();
                EvType2.Reload();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddEventTypeWindow addEventTypeWindow = new AddEventTypeWindow(-1);
            await addEventTypeWindow.ShowDialog(MainMainWindow);
            EvType1.Reload();
            EvType2.Reload();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
                {
                    var selected = EventTypesDG.SelectedItem as EventType;
                    Connect.context.EventTypes.Remove(selected);
                    Connect.context.SaveChanges();
                    EvType1.Reload();
                    EvType2.Reload();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadData()
        {
            context.EventTypes.Load();
            EventTypesDG.ItemsSource = null;
            EventTypesDG.ItemsSource = context.EventTypes;
        }
    }
}
