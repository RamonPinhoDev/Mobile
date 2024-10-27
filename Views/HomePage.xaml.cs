


using ScheduleListUI.ViewModels;

namespace ScheduleListUI.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new Monkeys1();
    }


}