using Avalonia.Controls;
using Avalonia.Styling;
using Cultura.data;
using Cultura.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class ReservationService : UserControl
    {
        public ReservationService()
        {
            InitializeComponent();
            BigReservBtn.Click += BigReservBtn_Click;
            DateCdp.SelectedDateChanged += DateCdp_SelectedDateChanged;
            DateCdp.SelectedDate = DateTime.Now;
        }

        private void DateCdp_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadSpaces();
        }

        private async void BigReservBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                AddReservationWindow addReservationWindow = new AddReservationWindow(-1, ((Space)SpaceNameCb.SelectedItem).Id, -1, (DateTime)DateCdp.SelectedDate);
                await addReservationWindow.ShowDialog(MainMainWindow);
            }
            catch (Exception ex) { }
            Res1.Reload();
            Res2.Reload();
        }

        private void LoadSpaces()
        {
            context.Spaces.Load();
            var spaces = context.Spaces.ToList();
            try
            {
                foreach (Reservation r in context.Reservations)
                {
                    if (Convert.ToDateTime(r.BeginningDate).Date <= DateCdp.SelectedDate.Value.Date && Convert.ToDateTime(r.EndingDate).Date >= DateCdp.SelectedDate.Value.Date)
                    {
                        if (spaces.Contains(r.Space))
                        {
                            spaces.Remove(r.Space);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            SpaceNameCb.ItemsSource = spaces;
        }
    }
}
