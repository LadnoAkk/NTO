using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddEventWindow : Window
    {
        private long _id;

        public AddEventWindow(long id)
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
            var ev = (Event)MainGrid.DataContext;
            ev.Data = DateCdp.SelectedDate.Value.ToString("d");
            if (_id == -1)
            {              
                context.Events.Add(ev);
            }
            context.SaveChanges();
            Close();

        }

        private void LoadData()
        {
            context.Events.Load();
            context.EventTypes.Load();
            TypeNameCb.ItemsSource = context.EventTypes;

            if (_id != -1)
            {
                var ev = context.Events.First(el => el.Id == _id);
                DateCdp.SelectedDate = Convert.ToDateTime(ev.Data);
                MainGrid.DataContext = ev;
                TypeNameCb.SelectedIndex = 0;
            }
            else
            {
                MainGrid.DataContext = new Event();
            }

        }
    }
}
