using System.Collections.ObjectModel;
using CosplayApp;
namespace CosplayApp.Views;

public partial class CosplaysPage : ContentPage
{
    CosPlanItemDatabase database = App.DatabaseCos;
    ToDoItemDatabase databaseToDo = App.DatabaseTD;
    public ObservableCollection<CosplayCard> Cosplays { get; set; } = new();

    public CosplaysPage(CosPlanItemDatabase cosPlanItemDatabase, ToDoItemDatabase toDoItemDatabase)
	{
		InitializeComponent();
        //database = cosPlanItemDatabase;
        //databaseToDo = toDoItemDatabase;
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var items = await database.GetItemsAsync();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Cosplays.Clear();
            foreach (var item in items)
                Cosplays.Add(item);
        });
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        CosplayCard NewCard = new CosplayCard ();
        await Navigation.PushAsync(new CosPlan(database, databaseToDo, NewCard));
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not CosplayCard NewCard)
            return;

        await Navigation.PushAsync(new CosPlan(database, databaseToDo, NewCard));
    }
}