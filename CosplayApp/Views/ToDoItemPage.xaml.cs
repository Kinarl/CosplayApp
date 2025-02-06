namespace CosplayApp.Views;

public partial class ToDoItemPage : ContentPage
{
    public ToDoItem Item
    {
        get => BindingContext as ToDoItem;
        set => BindingContext = value;
    }

    ToDoItemDatabase database = App.DatabaseTD;

    public ToDoItemPage(ToDoItem item)  
    {
        InitializeComponent();
        Item = item;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Navigation.PopAsync();
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.Id == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Navigation.PopAsync();
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}