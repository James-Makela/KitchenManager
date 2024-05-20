namespace KitchenManager;

public partial class FramePage : ContentPage
{
    public FramePage()
    {
        InitializeComponent();
    }

    public void Button_LeftTab_Pressed(object sender, EventArgs e)
    {
        LeftTab_Pressed();
    }

    private void Button_RightTab_Pressed(object sender, EventArgs e)
    {
        RightTab_Pressed();
    }

    protected override bool OnBackButtonPressed()
    {
        //Shell.Current.GoToAsync("home");
        Dispatcher.Dispatch(async () =>
        {
            await Shell.Current.GoToAsync("////home");
        });

        return true;
    }

    protected virtual void LeftTab_Pressed()
    {
        Border Border_LeftTab = (Border)this.GetTemplateChild("Border_LeftTab");
        Border Border_RightTab = (Border)this.GetTemplateChild("Border_RightTab");
        Button Button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button Button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");

        Border_LeftTab.Style = (Style)Resources["SelectedTabBorderLocal"];
        Border_RightTab.Style = (Style)Resources["UnselectedTabBorderLocal"];

        // Button_LeftTab.TextColor = Color.Parse("#d9d9d9");
        // Button_RightTab.TextColor = Color.Parse("#415a77");

        Button_LeftTab.Style = (Style)Resources["SelectedTabButtonLocal"];
        Button_RightTab.Style = (Style)Resources["UnselectedTabButtonLocal"];
    }

    protected virtual void RightTab_Pressed()
    {
        Border Border_LeftTab = (Border)this.GetTemplateChild("Border_LeftTab");
        Border Border_RightTab = (Border)this.GetTemplateChild("Border_RightTab");
        Button Button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button Button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");

        Border_LeftTab.Style = (Style)Resources["UnselectedTabBorderLocal"];
        Border_RightTab.Style = (Style)Resources["SelectedTabBorderLocal"];

        // Button_LeftTab.TextColor = Color.Parse("#415a77");
        // Button_RightTab.TextColor = Color.Parse("#d9d9d9");

        Button_LeftTab.Style = (Style)Resources["UnselectedTabButtonLocal"];
        Button_RightTab.Style = (Style)Resources["SelectedTabButtonLocal"];
    }
}