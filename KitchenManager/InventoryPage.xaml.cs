using CommunityToolkit.Maui.Views;
using KitchenManager.Controllers;
using KitchenManager.Models;

namespace KitchenManager;

public partial class InventoryPage : FramePage
{
    private readonly LocalDBService service = new();

	public InventoryPage(string[] labelStrings) : base(labelStrings)
	{
		InitializeComponent();
        RefreshList();
	}

    private void CollectionView_Stock_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private async void Button_Add_Stock_Pressed(object sender, EventArgs e)
    {
        await AddItemPopUp();
    }

    public async Task AddItemPopUp()
    {
        var addPopup = new AddInventoryItem();
        InventoryItem? newItem = await this.ShowPopupAsync(addPopup) as InventoryItem;

        if (newItem != null)
        {
            await service.AddInventoryItem(newItem);
        }
        RefreshList();
    }

    private async void RefreshList()
    {
        List<InventoryItem> items = await service.GetInventory();
        CollectionView_Stock.ItemsSource = items;
    }
}