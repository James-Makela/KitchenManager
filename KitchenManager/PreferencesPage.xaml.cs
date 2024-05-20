using KitchenManager.Controllers;

namespace KitchenManager;

public partial class PreferencesPage : FramePage
{
	public PreferencesPage()
	{
		InitializeComponent();
		Button button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
		Button button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");
		Image imageEdamamLogo = (Image)this.GetTemplateChild("Image_edamamLogo");
		button_LeftTab.Text = "Settings";
		button_RightTab.Text = "About";
		imageEdamamLogo.IsVisible = false;
		PopulateFields();
	}

	protected override void RightTab_Pressed()
	{
		base.RightTab_Pressed();
		Border_CollectionView.IsVisible = false;
	}

	protected override void LeftTab_Pressed()
	{
		base.LeftTab_Pressed();
		Border_CollectionView.IsVisible = true;
	}

	private void PopulateFields()
	{
		bool unit = PreferencesManager.GetUnit();
		Picker_Unit.SelectedIndex = unit ? 0 : 1;

		int people = PreferencesManager.GetPeople();
		Picker_People.SelectedIndex = people - 1;

		int theme = PreferencesManager.GetTheme();
		Picker_Theme.SelectedIndex = theme;
	}

	private void Picker_Unit_SelectedIndexChanged(object sender, EventArgs e)
	{
		string? value = Picker_Unit.SelectedItem as string;
		if (value == "Metric")
		{
			PreferencesManager.SetUnit(true);
		}
		if (value == "Imperial")
		{
			PreferencesManager.SetUnit(false);
		}

	}

	private void Picker_People_SelectedIndexChanged(object sender, EventArgs e)
	{
		PreferencesManager.SetPeople(Picker_People.SelectedIndex + 1);
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

	private void Picker_Theme_SelectedIndexChanged(object sender, EventArgs e)
	{
		PreferencesManager.SetTheme(Picker_Theme.SelectedIndex);
		PreferencesManager.ApplyTheme();
	}

	private async void Button_ReportBug_Pressed(object sender, EventArgs e)
	{
		try
		{
			string? url = "https://github.com/James-Makela/KitchenManager/issues/new";
			if (url == null) { return; }
			Uri uri = new Uri(url);
			await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
		}
		catch (Exception ex)
		{
			// TODO: handle exceptions
		}
	}
}