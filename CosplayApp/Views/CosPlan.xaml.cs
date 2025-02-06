using System.ComponentModel;

namespace CosplayApp.Views;

public partial class CosPlan : ContentPage
{
    public CosplayCard NewCard
    {
        get => BindingContext as CosplayCard;
        set => BindingContext = value;
    }

    CosPlanItemDatabase database = App.DatabaseCos;

    public CosPlan(CosplayCard card)
    {
        InitializeComponent();
        NewCard = card;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(NewCard.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(NewCard);
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        await database.DeleteItemAsync(NewCard);
        await Navigation.PopAsync();
    }

    private async void TakePhoto(object sender, EventArgs e)
    {
        FileResult photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions());

        if (photo != null)
        {
            NewCard.MainImage = photo.FullPath;
            await database.SaveItemAsync(NewCard);
            var stream = await photo.OpenReadAsync();
            myImage.Source = ImageSource.FromStream(() => stream);
        }

    }

    private async void OnToDoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToDoListPage(NewCard.ID));
    }
}

