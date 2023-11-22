using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddApplicationWindow : Window
    {
        private long _id;

        public AddApplicationWindow(long id)
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
                var ev = (Application)MainGrid.DataContext;
                ev.Date = DateCdp.SelectedDate.Value.ToString("d");
                ev.Timing = TimingCdp.SelectedDate.Value.ToString("d");
                if (_id == -1)
                {
                    context.Applications.Add(ev);
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
            context.Applications.Load();
            context.Events.Load();
            context.WorkTypes.Load();
            context.Spaces.Load();
            context.Statuses.Load();
            EventNameCb.ItemsSource = context.Events;
            WorkNameCb.ItemsSource = context.WorkTypes;
            SpaceNameCb.ItemsSource = context.Spaces;
            StatusNameCb.ItemsSource = context.Statuses;

            if (_id != -1)
            {
                var ev = context.Applications.First(el => el.Id == _id);
                DateCdp.SelectedDate = Convert.ToDateTime(ev.Date);
                TimingCdp.SelectedDate = Convert.ToDateTime(ev.Timing);
                MainGrid.DataContext = ev;
                EventNameCb.SelectedIndex = 0;
                WorkNameCb.SelectedIndex = 0;
                SpaceNameCb.SelectedIndex = 0;
                StatusNameCb.SelectedIndex = 0;
            }
            else
            {
                MainGrid.DataContext = new Application();
            }

        }
    }
}
