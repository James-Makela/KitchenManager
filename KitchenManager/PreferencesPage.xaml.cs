using KitchenManager.Controllers;

namespace KitchenManager;

public partial class PreferencesPage : ContentPage
{
	public PreferencesPage()
	{
		InitializeComponent();
		PopulateFields();
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

	private void PopulateFields()
	{
		bool unit = PreferencesManager.GetUnit();
		Picker_Unit.SelectedIndex = unit ? 0 : 1;
	}
}