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
            
            VariantsOfCalendarCb.SelectionChanged += VariantsOfCalendarCb_SelectionChanged;
            OkBtn.Click += OkBtn_Click;
            DiscardBtn.Click += DiscardBtn_Click;
            LoadData();
        }

        private void VariantsOfCalendarCb_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            ThirdDayCb.IsVisible = false;
            ThirdDayLb.IsVisible = false;
            SecondDayCb.IsVisible = false;
            SecondDayLb.IsVisible = false;
            var circle = (Circle)MainGrid.DataContext;
            if (VariantsOfCalendarCb.SelectedIndex == 0) 
            {
                circle.SecondVarDayNavigation = null;
                circle.ThirdVarDayNavigation = null;
            }
            if (VariantsOfCalendarCb.SelectedIndex == 1)
            {
                SecondDayCb.IsVisible = true;
                SecondDayLb.IsVisible = true;
                circle.ThirdVarDayNavigation = null;
            }
            if (VariantsOfCalendarCb.SelectedIndex == 2)
            {
                SecondDayCb.IsVisible = true;
                SecondDayLb.IsVisible = true;
                ThirdDayCb.IsVisible = true;
                ThirdDayLb.IsVisible = true;
            }
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
                if (circa.ThirdVarDayNavigation != null) 
                {
                    VariantsOfCalendarCb.SelectedIndex = 2;
                }
                else if (circa.SecondVarDayNavigation != null)
                {
                    VariantsOfCalendarCb.SelectedIndex = 1;
                }
            }
            else
            {
                MainGrid.DataContext = new Circle();
                SpaceCb.SelectedIndex = 0;
                TeacherCb.SelectedIndex = 0;
                CircleTypeCb.SelectedIndex = 0;
                WorkBeginDateCdp.SelectedDate = DateTime.Now;
            }

        }
    }
}
