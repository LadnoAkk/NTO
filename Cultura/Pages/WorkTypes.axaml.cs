using Avalonia.Controls;
using Avalonia.Styling;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class WorkTypes : UserControl
    {
        public WorkTypes()
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
            var selected = WorkTypesDG.SelectedItem as WorkType;
            if (selected != null)
            {
                AddWorkTypeWindow addWorkTypeWindow = new AddWorkTypeWindow(selected.Id);
                await addWorkTypeWindow.ShowDialog(MainMainWindow);
                Work1.Reload();
                Work2.Reload();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddWorkTypeWindow addWorkTypeWindow = new AddWorkTypeWindow(-1);
            await addWorkTypeWindow.ShowDialog(MainMainWindow);
            Work1.Reload();
            Work2.Reload();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
                {
                    var selected = WorkTypesDG.SelectedItem as WorkType;
                    Connect.context.WorkTypes.Remove(selected);
                    Connect.context.SaveChanges();
                    Work1.Reload();
                    Work2.Reload();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadData()
        {
            context.WorkTypes.Load();
            WorkTypesDG.ItemsSource = null;
            WorkTypesDG.ItemsSource = context.WorkTypes;
        }
    }
}
