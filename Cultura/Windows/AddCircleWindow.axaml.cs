using Avalonia.Controls;
using Cultura.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Windows
{
    public partial class AddCircleWindow : Window
    {
        private long _id;

        public AddCircleWindow(long id)
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
                var circa = (Circle)MainGrid.DataContext;
                circa.WorkBeginDate = WorkBeginDateCdp.SelectedDate.Value.ToString("d");
                circa.BeginningTime = BeginningTimeTp.SelectedTime.Value.ToString();
                circa.EndingTime = EndingTimeTp.SelectedTime.Value.ToString();
                if (_id == -1)
                {
                    context.Circles.Add(circa);
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
            context.Spaces.Load();
            context.Teachers.Load();
            context.Circles.Load();
            context.CircleTypes.Load();
            context.Weeks.Load();
            CircleTypeCb.ItemsSource = context.CircleTypes;
            SpaceCb.ItemsSource = context.Spaces;
            TeacherCb.ItemsSource = context.Teachers;
            FirstDayCb.ItemsSource = context.Weeks;
            SecondDayCb.ItemsSource = context.Weeks;
            ThirdDayCb.ItemsSource = context.Weeks;

            if (_id != -1)
            {
                var circa = context.Circles.First(el => el.Id == _id);
                WorkBeginDateCdp.SelectedDate = Convert.ToDateTime(circa.WorkBeginDate);
                BeginningTimeTp.SelectedTime = TimeSpan.Parse(circa.BeginningTime);
                EndingTimeTp.SelectedTime = TimeSpan.Parse(circa.EndingTime);
                MainGrid.DataContext = circa;
            }
            else
            {
                MainGrid.DataContext = new Circle();
            }

        }
    }
}
