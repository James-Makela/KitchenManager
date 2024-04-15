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

        

        header.Text = labelStrings[0];
        button_LeftTab.Text = labelStrings[1];
        button_RightTab.Text = labelStrings[2];
    }
}