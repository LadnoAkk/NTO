using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddSpaceWindow : Window
    {
        private long _id;

        public AddSpaceWindow(long id)
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
            var sp = (Space)MainGrid.DataContext;
            if (_id == -1)
            {
                context.Spaces.Add(sp);
            }
            context.SaveChanges();
            Close();
        }

        private void LoadData()
        {
            context.Spaces.Load();

            if (_id != -1)
            {
                var sp = context.Spaces.First(el => el.Id == _id);
                MainGrid.DataContext = sp;
            }
            else
            {
                MainGrid.DataContext = new Space();
            }

        }
    }
}
