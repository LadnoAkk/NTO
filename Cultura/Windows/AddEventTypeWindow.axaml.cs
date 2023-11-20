using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddEventTypeWindow : Window
    {
        private long _id;

        public AddEventTypeWindow(long id)
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
            var ev = (EventType)MainGrid.DataContext;
            if (_id == -1)
            {
                context.EventTypes.Add(ev);
            }
            context.SaveChanges();
            Close();
        }

        private void LoadData()
        {
            context.EventTypes.Load();

            if (_id != -1)
            {
                var ev = context.EventTypes.First(el => el.Id == _id);
                MainGrid.DataContext = ev;
            }
            else
            {
                MainGrid.DataContext = new EventType();
            }

        }
    }
}
