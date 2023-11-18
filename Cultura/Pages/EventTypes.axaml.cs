using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Cultura.Helper.Connect;

namespace Cultura.Pages
{
    public partial class EventTypes : UserControl
    {
        public EventTypes()
        {
            InitializeComponent();
            LoadData();         
        }

        private void LoadData()
        {
            context.Events.Load();
            context.EventTypes.Load();
            EventTypesDG.ItemsSource = context.EventTypes;
        }
    }
}
