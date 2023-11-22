using Avalonia.Controls;
using Avalonia.Media;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Applications : UserControl
    {
        public Applications()
        {
            InitializeComponent();
            ApplicationsDG.LoadingRow +=ApplicationsDG_LoadingRow;
            LoadData();
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = ApplicationsDG.SelectedItem as Application;
            if (selected != null)
            {
                AddApplicationWindow addApplicationWindow = new AddApplicationWindow(selected.Id);
                await addApplicationWindow.ShowDialog(MainMainWindow);
                LoadData();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddApplicationWindow addApplicationWindow = new AddApplicationWindow(-1);
            await addApplicationWindow.ShowDialog(MainMainWindow);
            LoadData();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
            {
                var selected = ApplicationsDG.SelectedItem as Application;
                Connect.context.Applications.Remove(selected);
                Connect.context.SaveChanges();
                LoadData();
                OrigReApp.LoadData();
            }
        }

        private void ApplicationsDG_LoadingRow(object? sender, DataGridRowEventArgs e)
        {
            var data = e.Row.DataContext as Application;
            if (data.Status.Id == 3)
            {
                e.Row.Background = new SolidColorBrush(Colors.Gray);
            }
            if (data.Status.Id == 1)
            {
                e.Row.Background = new SolidColorBrush(Colors.Pink);
            }
            if (data.Status.Id == 2)
            {
                e.Row.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        public void LoadData()
        {
            context.Applications.Load();
            context.Events.Load();
            context.Spaces.Load();
            context.Statuses.Load();
            context.WorkTypes.Load();
            ApplicationsDG.ItemsSource = null;
            ApplicationsDG.ItemsSource = context.Applications;
        }
    }
}
