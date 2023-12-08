using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddTeacherWindow : Window
    {
        private long _id;

        public AddTeacherWindow(long id)
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
                var tch = (Teacher)MainGrid.DataContext;
                if (_id == -1)
                {
                    context.Teachers.Add(tch);
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
            context.Teachers.Load();

            if (_id != -1)
            {
                var tch = context.Teachers.First(el => el.Id == _id);
                MainGrid.DataContext = tch;
            }
            else
            {
                MainGrid.DataContext = new Teacher();
            }

        }
    }
}
