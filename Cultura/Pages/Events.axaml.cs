using Avalonia.Controls;
using Cultura.data;
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
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = EventsDG.SelectedItem as Event;
            if ( selected != null)
            {
                AddEventWindow addEventWindow = new AddEventWindow(selected.Id);
                addEventWindow.ShowDialog(MainMainWindow);
            }
            
        }

        private void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddEventWindow addEventWindow = new AddEventWindow(-1);
            addEventWindow.ShowDialog(MainMainWindow);
        }

        private void LoadData()
        {
            context.Events.Load();
            context.EventTypes.Load();
            EventsDG.ItemsSource = context.Events;
        }
    }
}
