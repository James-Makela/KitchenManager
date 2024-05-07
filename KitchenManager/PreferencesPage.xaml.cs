using KitchenManager.Controllers;

namespace KitchenManager;

public partial class PreferencesPage : ContentPage
{
	public PreferencesPage()
	{
		InitializeComponent();
		PopulateFields();
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
}