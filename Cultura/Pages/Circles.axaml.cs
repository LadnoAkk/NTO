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
    public partial class Circles : UserControl
    {
        public Circles()
        {
            InitializeComponent();
            LoadData();
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = CirclesDG.SelectedItem as Circle;
            if (selected != null)
            {
                AddCircleWindow addCircleWindow = new AddCircleWindow(selected.Id);
                await addCircleWindow.ShowDialog(MainMainWindow);
                LoadData();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddCircleWindow addCircleWindow = new AddCircleWindow(-1);
            await addCircleWindow.ShowDialog(MainMainWindow);
            LoadData();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
                {
                    var selected = CirclesDG.SelectedItem as Circle;
                    Connect.context.Circles.Remove(selected);
                    Connect.context.SaveChanges();
                    LoadData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadData()
        {
            context.Circles.Load();
            context.CircleTypes.Load();
            context.Spaces.Load();
            context.Weeks.Load();
            context.Teachers.Load();
            CirclesDG.ItemsSource = null;
            CirclesDG.ItemsSource = context.Circles;
        }
    }
}
