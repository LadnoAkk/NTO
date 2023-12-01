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
        var sp1 = new Spaces();
        var sp2 = new Spaces();
        SpaceCCP.Content = sp;
        SpaceCCR.Content = sp1;
        SpaceCCO.Content = sp2;
        Sp1 = sp;
        Sp2 = sp1;
        Sp3 = sp2;

        var app = new Applications();
        var app1 = new Applications();
        ApplicationCCP.Content = app;
        ApplicationCCR.Content = app1;
        App1 = app;
        App2 = app1;

        var reApp = new ReApplications();
        var reApp1 = new ReApplications();
        WorkspaceCCP.Content = reApp;
        WorkspaceCCR.Content = reApp1;
        ReApp2 = reApp1;
        ReApp1 = reApp;

        var work = new WorkTypes();
        var work1 = new WorkTypes();
        WorkCCP.Content = work;
        WorkCCR.Content = work1;
        Work1 = work;
        Work2 = work1;

        var res = new Reservations();
        var res1 = new Reservations();
        ReservationCCP.Content = res;
        ReservationCCR.Content = res1;
        Res1 = res;
        Res2 = res1;
    }
}
