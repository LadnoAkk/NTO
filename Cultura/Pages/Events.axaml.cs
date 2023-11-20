using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Events : UserControl
    {
        public Events()
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
            var selected = EventsDG.SelectedItem as Event;
            if ( selected != null)
            {
                AddEventWindow addEventWindow = new AddEventWindow(selected.Id);
                await addEventWindow.ShowDialog(MainMainWindow);
                Ev1.Reload();
                Ev2.Reload();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow(-1);
            await addEventWindow.ShowDialog(MainMainWindow);
            Ev1.Reload();
            Ev2.Reload();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
            {
                var selected = EventsDG.SelectedItem as Event;
                Connect.context.Events.Remove(selected);
                Connect.context.SaveChanges();
                Ev1.Reload();
                Ev2.Reload();
            }
        }

        private void LoadData()
        {
            context.Events.Load();
            context.EventTypes.Load();
            EventsDG.ItemsSource = null;
            EventsDG.ItemsSource = context.Events;
        }
    }
}
