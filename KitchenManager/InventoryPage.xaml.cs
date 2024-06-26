using CommunityToolkit.Maui.Views;
using KitchenManager.Controllers;
using KitchenManager.Models;

namespace KitchenManager;

public partial class InventoryPage : FramePage
{
    private readonly LocalDBService service = new();

    public InventoryPage()
    {
        InitializeComponent();

        // TODO: Tidy this up
        Button button_LeftTab = (Button)this.GetTemplateChild("Button_LeftTab");
        Button button_RightTab = (Button)this.GetTemplateChild("Button_RightTab");
        Image imageEdamamLogo = (Image)this.GetTemplateChild("Image_edamamLogo");
        button_LeftTab.Text = "Stock";
        button_RightTab.Text = "Costs";
        imageEdamamLogo.IsVisible = false;
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

    private async void CollectionView_Stock_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionView_Stock.SelectedItem == null) return;

        InventoryItem itemToEdit = (InventoryItem)CollectionView_Stock.SelectedItem;
        await EditItemPopUp(itemToEdit);
        CollectionView_Stock.SelectedItem = null;
    }

    public async Task AddItemPopUp()
    {
        bool success = false;
        var addPopup = new AddInventoryItem();
        InventoryItem? newItem = await this.ShowPopupAsync(addPopup) as InventoryItem;

        if (newItem != null)
        {
            success = await service.AddInventoryItem(newItem);
        }
        else return;
        if (!success)
        {
            await DisplayAlert("Error", "Unable to add item", "Ok");
        }
        RefreshList();
    }

    public async Task EditItemPopUp(InventoryItem item)
    {
        bool success = false;
        var editPopup = new AddInventoryItem(item);
        InventoryItem? newItem = await this.ShowPopupAsync(editPopup) as InventoryItem;

        if (newItem != null)
        {
            success = await service.EditInventoryItem(newItem);
        }
        else return;
        if (!success)
        {
            await DisplayAlert("Error", "Unable to edit item", "Ok");
        }
        RefreshList();
    }

    private async void RefreshList()
    {
        List<InventoryItem> items = await service.GetInventory();
        CollectionView_Stock.ItemsSource = items;
        CollectionView_Costs.ItemsSource = items;
    }

    private async void CollectionView_Costs_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CollectionView_Costs.SelectedItem == null) return;

        InventoryItem itemToEdit = (InventoryItem)CollectionView_Costs.SelectedItem;
        await EditItemPopUp(itemToEdit);
        CollectionView_Costs.SelectedItem = null;
    }

    private async void Button_Add_Pressed(object sender, EventArgs e)
    {
        await AddItemPopUp();
    }

    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
        InventoryItem item = (InventoryItem)CollectionView_Stock.SelectedItem;
        await service.RemoveInventoryItem(item);
        RefreshList();
    }
}