using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddCircleTypeWindow : Window
    {
        private long _id;

        public AddCircleTypeWindow(long id)
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
                var ci = (CircleType)MainGrid.DataContext;
                if (_id == -1)
                {
                    context.CircleTypes.Add(ci);
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
            context.CircleTypes.Load();

            if (_id != -1)
            {
                var ci = context.CircleTypes.First(el => el.Id == _id);
                MainGrid.DataContext = ci;
            }
            else
            {
                MainGrid.DataContext = new CircleType();
            }

        }
    }
}
