using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddReservationWindow : Window
    {
        private long _id;

        public AddReservationWindow(long id)
        {
            InitializeComponent();
            _id = id;
            LoadData();
            OkBtn.Click += OkBtn_Click;
            DiscardBtn.Click += DiscardBtn_Click;
        }

        private void DiscardBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Close();
        }

        private void OkBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                var res = (Reservation)MainGrid.DataContext;
                res.CreateDate = CreateDateCdp.SelectedDate.Value.ToString("d");
                res.BeginningDate = BeginningDateCdp.SelectedDate.Value.ToString("d");
                res.BeginningTime = BeginningTimeTp.SelectedTime.Value.ToString();
                res.EndingDate = EndingDateCdp.SelectedDate.Value.ToString("d");
                res.EndingTime = EndingTimeTp.SelectedTime.Value.ToString();
                if (_id == -1)
                {
                    context.Reservations.Add(res);
                }
                context.SaveChanges();
                Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadData()
        {
            context.Reservations.Load();
            context.Spaces.Load();
            context.Events.Load();
            EventDescriptionCb.ItemsSource = context.Events;
            SpaceNameCb.ItemsSource = context.Spaces;

            if (_id != -1)
            {
                var res = context.Reservations.First(el => el.Id == _id);
                CreateDateCdp.SelectedDate = Convert.ToDateTime(res.CreateDate);
                BeginningDateCdp.SelectedDate = Convert.ToDateTime(res.BeginningDate);
                BeginningTimeTp.SelectedTime = TimeSpan.Parse(res.BeginningTime);
                EndingDateCdp.SelectedDate = Convert.ToDateTime(res.EndingDate);
                EndingTimeTp.SelectedTime = TimeSpan.Parse(res.EndingTime);
                MainGrid.DataContext = res;
                EventDescriptionCb.SelectedIndex = 0;
                SpaceNameCb.SelectedIndex = 0;
            }
            else
            {
                MainGrid.DataContext = new Reservation();
            }

        }
    }
}
