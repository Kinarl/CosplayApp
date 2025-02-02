using System.Collections.ObjectModel;

namespace CosplayApp.Views;

public partial class CosplaysPage : ContentPage
{
    CosPlanItemDatabase database;
    public ObservableCollection<CosplayCard> Cosplays { get; set; } = new();

    public CosplaysPage(CosPlanItemDatabase cosPlanItemDatabase)
	{
		InitializeComponent();
        database = cosPlanItemDatabase;
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
        await Shell.Current.GoToAsync(nameof(CosPlan), true, new Dictionary<string, object>
        {
            ["NewCard"] = new CosplayCard()
        });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not CosplayCard NewCard)
            return;

        await Shell.Current.GoToAsync(nameof(CosPlan), true, new Dictionary<string, object>
        {
            ["NewCard"] = NewCard
        });
    }
}