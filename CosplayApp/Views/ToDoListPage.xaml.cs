using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace CosplayApp.Views;

[QueryProperty(nameof(CosplayCardId), "CosplayCardId")]
public partial class ToDoListPage : ContentPage
{
    public int CosplayCardId;

    ToDoItemDatabase database;
    public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new();

    public ToDoListPage(ToDoItemDatabase toDoPlanItemDatabase, int cardId)
    {
        InitializeComponent();
        database = toDoPlanItemDatabase;
        CosplayCardId = cardId;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsByCardAsync(CosplayCardId);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ToDoItems.Clear();
            foreach (var item in items)
                ToDoItems.Add(item);
        });
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        ToDoItem item = new ToDoItem()
        {
            Name = "Костюм",
            Description = "Найти референсы",
            Done = false,
            CosplayCardId = CosplayCardId
        };
        await Navigation.PushAsync(new ToDoItemPage(database, item));
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ToDoItem item)
            return;

        await Navigation.PushAsync(new ToDoItemPage(database, item));
    }
}