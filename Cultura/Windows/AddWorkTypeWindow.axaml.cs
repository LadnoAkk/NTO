using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddWorkTypeWindow : Window
    {
        private long _id;

        public AddWorkTypeWindow(long id)
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
            var woType = (WorkType)MainGrid.DataContext;
            if (_id == -1)
            {
                context.WorkTypes.Add(woType);
            }
            context.SaveChanges();
            Close();
        }

        private void LoadData()
        {
            context.WorkTypes.Load();

            if (_id != -1)
            {
                var woType = context.WorkTypes.First(el => el.Id == _id);
                MainGrid.DataContext = woType;
            }
            else
            {
                MainGrid.DataContext = new WorkType();
            }

        }
    }
}
