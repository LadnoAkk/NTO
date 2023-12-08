using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Teachers : UserControl
    {
        public Teachers()
        {
            InitializeComponent();
            LoadData();
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = TeachersDG.SelectedItem as Teacher;
            if (selected != null)
            {
                AddTeacherWindow addTeacherWindow = new AddTeacherWindow(selected.Id);
                await addTeacherWindow.ShowDialog(MainMainWindow);
                LoadData();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddTeacherWindow addTeacherWindow = new AddTeacherWindow(-1);
            await addTeacherWindow.ShowDialog(MainMainWindow);
            LoadData();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
                {
                    var selected = TeachersDG.SelectedItem as Teacher;
                    Connect.context.Teachers.Remove(selected);
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
            context.Teachers.Load();
            TeachersDG.ItemsSource = null;
            TeachersDG.ItemsSource = context.Teachers;
        }
    }
}
