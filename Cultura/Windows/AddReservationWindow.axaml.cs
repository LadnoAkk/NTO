using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddReservationWindow : Window
    {
        private long _id;
        private long _spaceId;
        private long _eventId;

        public AddReservationWindow(long id, long spaceId, long eventId, DateTime date)
        {
            InitializeComponent();
            _id = id;
            _spaceId = spaceId;
            _eventId = eventId;
            BeginningDateCdp.SelectedDate = date;
            EndingDateCdp.SelectedDate = date;
            CreateDateCdp.SelectedDate = DateTime.Now;
            BeginningDateCdp.SelectedDateChanged += BeginningDateCdp_SelectedDateChanged;
            EndingDateCdp.SelectedDateChanged += EndingDateCdp_SelectedDateChanged;
            LoadSpaces();
            LoadData();
            OkBtn.Click += OkBtn_Click;
            DiscardBtn.Click += DiscardBtn_Click;

        }

        private void EndingDateCdp_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadSpaces();
        }

        private void BeginningDateCdp_SelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadSpaces();
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

        private void LoadSpaces()
        {            
            var spaces = context.Spaces.ToList();
            try
            {
                foreach (Reservation r in context.Reservations)
                {
                    if (Convert.ToDateTime(r.BeginningDate).Date <= BeginningDateCdp.SelectedDate.Value.Date && Convert.ToDateTime(r.EndingDate).Date >= BeginningDateCdp.SelectedDate.Value.Date ||
                        Convert.ToDateTime(r.BeginningDate).Date <= EndingDateCdp.SelectedDate.Value.Date && Convert.ToDateTime(r.EndingDate).Date >= EndingDateCdp.SelectedDate.Value.Date)
                    {
                        if (spaces.Contains(r.Space))
                        {
                            spaces.Remove(r.Space);
                        }
                    }
                }
            }
            catch(Exception ex) { }
            SpaceNameCb.ItemsSource = spaces;
        }


        private void LoadData()
        {
            context.Reservations.Load();
            context.Spaces.Load();
            context.Events.Load();
            EventDescriptionCb.ItemsSource = context.Events;
            

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
                if (_spaceId != -1)
                {
                    SpaceNameCb.SelectedItem = context.Spaces.First(el => el.Id == _spaceId);
                }
                if (_eventId != -1)
                {
                    EventDescriptionCb.SelectedItem = context.Events.First(el => el.Id == _eventId);
                }
            }

        }
    }
}
