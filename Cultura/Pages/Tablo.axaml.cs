using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class Tablo : UserControl
    {
        public Tablo()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            context.Circles.Load();
            context.CircleTypes.Load();
            context.Spaces.Load();
            context.Weeks.Load();
            context.Teachers.Load();
            var tmpRow = TabloGrid.RowDefinitions[0];
            TabloGrid.RowDefinitions.Clear();
            TabloGrid.RowDefinitions.Add(tmpRow);

            var circles = context.Circles.ToList();
            foreach (var circle in circles)
            {
                TabloGrid.RowDefinitions.Add(new RowDefinition(new GridLength(70)));
                Label nameLb = new Label();
                nameLb.Content = circle.Name;
                TabloGrid.Children.Add(nameLb);
                Grid.SetRow(nameLb, TabloGrid.RowDefinitions.Count - 1);
                string text = circle.BeginningTime + " " + circle.EndingTime + "\n" + circle.Space.Name + "\n"
                    + circle.Teacher.Name;
                Label tmp = new Label();
                tmp.Content = text;
                TabloGrid.Children.Add(tmp);
                Grid.SetRow(tmp, TabloGrid.RowDefinitions.Count - 1);
                Grid.SetColumn(tmp, (int)circle.FirstVarDay);

                if (circle.SecondVarDay != null)
                {
                    Label tmp1 = new Label();
                    tmp1.Content = text;
                    TabloGrid.Children.Add(tmp1);
                    Grid.SetRow(tmp1, TabloGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(tmp1, (int)circle.SecondVarDay);
                }

                if (circle.ThirdVarDay != null)
                {
                    Label tmp2 = new Label();
                    tmp2.Content = text;
                    TabloGrid.Children.Add(tmp2);
                    Grid.SetRow(tmp2, TabloGrid.RowDefinitions.Count - 1);
                    Grid.SetColumn(tmp2, (int)circle.ThirdVarDay);
                }
            }
        }
    }
}
