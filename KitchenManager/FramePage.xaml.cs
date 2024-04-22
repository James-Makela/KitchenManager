namespace KitchenManager;

public partial class FramePage : ContentPage
{
    public FramePage(string[] labelStrings)
    {
        InitializeComponent();
        this.Title = labelStrings[0];

        Label header = (Label)this.GetTemplateChild("Label_PageHeader");
        Button button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");
        Image imageEdamamLogo = (Image)this.GetTemplateChild("Image_edamamLogo");

        header.Text = labelStrings[0];
        button_LeftTab.Text = labelStrings[1];
        button_RightTab.Text = labelStrings[2];

        if (labelStrings[0] == "Inventory")
        {
            imageEdamamLogo.IsVisible = false;
        }
    }

    private async void Button_Back_Pressed(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    public void Button_LeftTab_Pressed(object sender, EventArgs e)
    {
        LeftTab_Pressed();
    }

    private void Button_RightTab_Pressed(object sender, EventArgs e)
    {
        RightTab_Pressed();
    }

    protected virtual void LeftTab_Pressed()
    {
        Border Border_LeftTab = (Border)this.GetTemplateChild("Border_LeftTab");
        Border Border_RightTab = (Border)this.GetTemplateChild("Border_RightTab");
        Button Button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button Button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");

        Border_LeftTab.Stroke = Color.Parse("#415a77");
        Border_RightTab.Stroke = Color.Parse("#d9d9d9");
        Border_LeftTab.Background = Color.Parse("#415a77");
        Border_RightTab.Background = Color.Parse("#d9d9d9");

        Button_LeftTab.TextColor = Color.Parse("#d9d9d9");
        Button_RightTab.TextColor = Color.Parse("#415a77");
    }

    protected virtual void RightTab_Pressed()
    {
        Border Border_LeftTab = (Border)this.GetTemplateChild("Border_LeftTab");
        Border Border_RightTab = (Border)this.GetTemplateChild("Border_RightTab");
        Button Button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button Button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");

        Border_LeftTab.Stroke = Color.Parse("#d9d9d9");
        Border_RightTab.Stroke = Color.Parse("#415a77");
        Border_LeftTab.Background = Color.Parse("#d9d9d9");
        Border_RightTab.Background = Color.Parse("#415a77");

        Button_LeftTab.TextColor = Color.Parse("#415a77");
        Button_RightTab.TextColor = Color.Parse("#d9d9d9");
    }
}