using System.Collections.ObjectModel;

namespace CosplayApp.Views;

[QueryProperty("NewCard", "NewCard")]
public partial class CosPlan : ContentPage
{
    public ObservableCollection<CosplayCard> Cosplays { get; set; } = new();
    public CosplayCard NewCard
    {
        get => BindingContext as CosplayCard;
        set => BindingContext = value;
    }

    CosPlanItemDatabase database;

    public CosPlan(CosPlanItemDatabase cosPlanItemDatabase)
    {
        InitializeComponent();
        database = cosPlanItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(NewCard.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(NewCard);
        await Shell.Current.GoToAsync("..");

    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        //if (NewCard.ID == 0)
        //    return;
        await database.DeleteItemAsync(NewCard);
        await Shell.Current.GoToAsync("..");
    }

    private async void TakePhoto(object sender, EventArgs e)
    {
        FileResult photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions());

        if (photo != null)
        {
            NewCard.MainImage = photo.FullPath;
            await database.SaveItemAsync(NewCard);
        }
    }

    private async void OnToDoClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ToDoListPage), false, new Dictionary<string, object>()
        {
            ["CosplayCardId"] = NewCard.ID
        });
    }
}

