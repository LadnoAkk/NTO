using Avalonia.Controls;
using Avalonia.Media;
using Cultura.data;
using Cultura.Helper;
using Cultura.Windows;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class ReApplications : UserControl
    {
        public ReApplications()
        {
            InitializeComponent();
            LoadData();
            RefreshBtn.Click += RefreshBtn_Click;
        }

        private void RefreshBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            context.Applications.Load();
            context.Events.Load();
            context.Spaces.Load();
            context.Statuses.Load();
            context.WorkTypes.Load();
            ReApplicationsDG.ItemsSource = null;
            ReApplicationsDG.ItemsSource = context.Applications.Where(el => el.StatusId == 1);
        }
    }
}
