using CommunityToolkit.Maui.Views;
using KitchenManager.Models;
using KitchenManager.Controllers;

namespace KitchenManager;

public partial class AddInventoryItem : Popup
{
    public AddInventoryItem()
    {
        InitializeComponent();
        PopulatePicker();
    }

    private async void Button_Save_Pressed(object sender, EventArgs e)
    {
        decimal.TryParse(Entry_StockAmount.Text, out decimal amount);
        decimal.TryParse(Entry_StockCost.Text, out decimal cost);

        string? costUnit = Picker_CostUnit.SelectedItem.ToString();
        string? stockUnit = Picker_StockUnit.SelectedItem.ToString();

        if (costUnit == null || stockUnit == null)
        {
            return;
        }

        InventoryItem item = new(Entry_StockName.Text, stockUnit, costUnit, amount, cost);

        await CloseAsync(item);
    }

    private async void Button_Cancel_Pressed(object sender, EventArgs e)
    {
        await CloseAsync(null);
    }

    private void PopulatePicker()
    {
        foreach (var item in UnitHandler.GetCostUnits().Keys)
        {
            Picker_CostUnit.Items.Add(item);
        }

        foreach (var item in UnitHandler.GetStockUnits().Keys)
        {
            Picker_StockUnit.Items.Add(item);
        }
    }
}