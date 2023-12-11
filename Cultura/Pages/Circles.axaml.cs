using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Circles : UserControl
    {
        public Circles()
        {
            InitializeComponent();
            fillCircleTypeCb();
            LoadData();
            CircleTypeCb.SelectionChanged += CircleTypeCb_SelectionChanged;
            SearchTb.TextChanged += SearchTb_TextChanged;
            DeleteBtn.Click += DeleteBtn_Click;
            AddBtn.Click += AddBtn_Click;
            EditBtn.Click += EditBtn_Click;
        }

        private void fillCircleTypeCb()
        {
            context.CircleTypes.Load();
            var circleTypes = context.CircleTypes.ToList();
            circleTypes.Insert(0, new CircleType() { Name = "Все"});
            CircleTypeCb.ItemsSource = circleTypes;
            CircleTypeCb.SelectedIndex = 0;
        }

        private void CircleTypeCb_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void SearchTb_TextChanged(object? sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private async void EditBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selected = CirclesDG.SelectedItem as Circle;
            if (selected != null)
            {
                AddCircleWindow addCircleWindow = new AddCircleWindow(selected.Id);
                await addCircleWindow.ShowDialog(MainMainWindow);
                LoadData();
                Tablo1.LoadData();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddCircleWindow addCircleWindow = new AddCircleWindow(-1);
            await addCircleWindow.ShowDialog(MainMainWindow);
            LoadData();
            Tablo1.LoadData();
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
                    Tablo1.LoadData();
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
            var circles = context.Circles.ToList();
            if (CircleTypeCb.SelectedIndex != 0)
            {
                circles = circles.Where(el => el.Type == CircleTypeCb.SelectedItem as CircleType).ToList();
            }
            if (!string.IsNullOrEmpty(SearchTb.Text))
            {
                circles = circles.Where(el => el.Name.Contains(SearchTb.Text)).ToList();
            }
            CirclesDG.ItemsSource = circles;
        }
    }
}
