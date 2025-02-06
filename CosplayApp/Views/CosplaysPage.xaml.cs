using System.Collections.ObjectModel;
namespace CosplayApp.Views;

public partial class CosplaysPage : ContentPage
{
    CosPlanItemDatabase database = App.DatabaseCos;
    public ObservableCollection<CosplayCard> Cosplays { get; set; } = new();

    public CosplaysPage()
	{
		InitializeComponent();
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
        await Navigation.PushAsync(new CosPlan(NewCard));
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not CosplayCard NewCard)
            return;

        await Navigation.PushAsync(new CosPlan(NewCard));
    }
}