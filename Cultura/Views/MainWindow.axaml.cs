using Avalonia.Controls;
using Cultura.Pages;
using static Cultura.Helper.Connect;

namespace Cultura.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainMainWindow = this;

        var ev = new Events();
        var ev1 = new Events();
        EventCCR.Content = ev;
        EventCCP.Content = ev1;
        Ev1 = ev;
        Ev2 = ev1;

        var evType = new EventTypes();
        var evType1 = new EventTypes();
        TypeCCP.Content = evType;
        TypeCCR.Content = evType1;
        EvType1 = evType;
        EvType2 = evType1;

        var sp = new Spaces();
        SpaceCCP.Content = sp;

        var app = new Applications();
        ApplicationCCP.Content = app;

        var reApp = new ReApplications();
        WorkspaceCCP.Content = reApp;

        var work = new WorkTypes();
        WorkCCP.Content = work;
    }
}
