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

    protected override void LeftTab_Pressed()
    {
        base.LeftTab_Pressed();
        CollectionView_Costs.IsVisible = false;
        CollectionView_Stock.IsVisible = true;
    }

    protected override void RightTab_Pressed()
    {
        base.RightTab_Pressed();
        CollectionView_Stock.IsVisible = false;
        CollectionView_Costs.IsVisible = true;
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
        CollectionView_Costs.ItemsSource = items;
    }

    private void CollectionView_Costs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}