using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace CosplayApp.Views;

[QueryProperty(nameof(CosplayCard), "CosplayCard")]
public partial class ToDoListPage : ContentPage
{
    public CosplayCard CosplayCard
    {
        get => BindingContext as CosplayCard;
        set => BindingContext = value;
    }

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
        if (CosplayCard.ToDoItems != null)
        {
            var items = await database.GetItemsByCardAsync(CosplayCard);
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
                CosplayCard = CosplayCard
            };
            ToDoItems.Add(item);
            await database.SaveItemAsync(item);
            CosplayCard.ToDoItems = ToDoItems;
        }
        
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ToDoItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = new ToDoItem()
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