using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace CosplayApp.Views;

[QueryProperty(nameof(CosplayCardId), "CosplayCardId")]
public partial class ToDoListPage : ContentPage
{
    public int CosplayCardId;

    ToDoItemDatabase database;
    public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new();

    public ToDoListPage(ToDoItemDatabase cosPlanItemDatabase)
    {
        InitializeComponent();
        database = cosPlanItemDatabase;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsByCardAsync(CosplayCardId);
        if (items != null && items.Count != 0)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ToDoItems.Clear();
                foreach (var item in items)
                    ToDoItems.Add(item);
            });
        }
        else
        {
            var item = new ToDoItem()
            {
                Name = "Костюм",
                Description = "Найти референсы",
                Done = false,
                CosplayCardId = CosplayCardId
            };
            ToDoItems.Add(item);
            await database.SaveItemAsync(item);
            items = await database.GetItemsByCardAsync(CosplayCardId);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ToDoItems.Clear();
                foreach (var item in items)
                    ToDoItems.Add(item);
            });
        }
        
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ToDoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new ToDoItem() 
            {
                Name = "Костюм",
                Description = "Найти референсы",
                Done = false,
                CosplayCardId = CosplayCardId
            }
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ToDoItem item)
            return;

        await Shell.Current.GoToAsync(nameof(ToDoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}