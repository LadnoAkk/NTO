using Avalonia.Controls;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Reservations : UserControl
    {
        public Reservations()
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
            var selected = ReservationsDG.SelectedItem as Reservation;
            if (selected != null)
            {
                AddReservationWindow addReservationWindow = new AddReservationWindow(selected.Id);
                await addReservationWindow.ShowDialog(MainMainWindow);
                Res1.Reload();
                Res2.Reload();
            }
        }

        private async void AddBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddReservationWindow addReservationWindow = new AddReservationWindow(-1);
            await addReservationWindow.ShowDialog(MainMainWindow);
            Res1.Reload();
            Res2.Reload();
        }

        private async void DeleteBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (await ShowQuestion("Вы уверены?") == MsBox.Avalonia.Enums.ButtonResult.Ok)
            {
                var selected = ReservationsDG.SelectedItem as Reservation;
                Connect.context.Reservations.Remove(selected);
                Connect.context.SaveChanges();
                Res1.Reload();
                Res2.Reload();
            }
        }

        private void LoadData()
        {
            context.Reservations.Load();
            context.Spaces.Load();
            context.Events.Load();
            ReservationsDG.ItemsSource = null;
            ReservationsDG.ItemsSource = context.Reservations;
        }
    }
}
