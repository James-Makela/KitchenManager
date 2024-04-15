using CommunityToolkit.Maui.Views;
using KitchenManager.Models;

namespace KitchenManager;

public partial class AddInventoryItem : Popup
{
	public AddInventoryItem()
	{
		InitializeComponent();
	}

    private async void Button_Save_Pressed(object sender, EventArgs e)
    {
        decimal.TryParse(Entry_StockAmount.Text, out decimal amount);
        decimal.TryParse(Entry_StockCost.Text, out decimal cost);
        InventoryItem item = new(Entry_StockName.Text, Entry_StockUnit.Text, amount, cost);
        
        await CloseAsync(item);
    }

    private async void Button_Cancel_Pressed(object sender, EventArgs e)
    {
        await CloseAsync(null);
    }
}