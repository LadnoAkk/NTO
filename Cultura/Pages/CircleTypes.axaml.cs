using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class CircleTypes : UserControl
    {
        public CircleTypes()
        {
            InitializeComponent();
            LoadData();
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = CircleTypesDG.SelectedItem as CircleType;
            if (selected != null)
            {
                AddCircleTypeWindow addCircleTypeWindow = new AddCircleTypeWindow(selected.Id);
                await addCircleTypeWindow.ShowDialog(MainMainWindow);
                LoadData();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddCircleTypeWindow addCircleTypeWindow = new AddCircleTypeWindow(-1);
            await addCircleTypeWindow.ShowDialog(MainMainWindow);
            LoadData();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
                {
                    var selected = CircleTypesDG.SelectedItem as CircleType;
                    Connect.context.CircleTypes.Remove(selected);
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
            context.CircleTypes.Load();
            CircleTypesDG.ItemsSource = null;
            CircleTypesDG.ItemsSource = context.CircleTypes;
        }
    }
}
