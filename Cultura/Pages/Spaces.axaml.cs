using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Spaces : UserControl
    {
        public Spaces()
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
            var selected = SpacesDG.SelectedItem as Space;
            if (selected != null)
            {
                AddSpaceWindow AddSpaceWindow = new AddSpaceWindow(selected.Id);
                await AddSpaceWindow.ShowDialog(MainMainWindow);
                Sp1.Reload();
                Sp2.Reload();
                Sp3.Reload();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddSpaceWindow AddSpaceWindow = new AddSpaceWindow(-1);
            await AddSpaceWindow.ShowDialog(MainMainWindow);
            Sp1.Reload();
            Sp2.Reload();
            Sp3.Reload();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
            {
                var selected = SpacesDG.SelectedItem as Space;
                Connect.context.Spaces.Remove(selected);
                Connect.context.SaveChanges();
                Sp1.Reload();
                Sp2.Reload();
                Sp3.Reload();
            }
        }

        private void LoadData()
        {
            context.Spaces.Load();
            SpacesDG.ItemsSource = null;
            SpacesDG.ItemsSource = context.Spaces;
        }
    }
}
