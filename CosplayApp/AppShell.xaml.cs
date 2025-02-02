using CosplayApp.Views;

namespace CosplayApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CosplaysPage), typeof(CosplaysPage));
            Routing.RegisterRoute(nameof(CosPlan), typeof(CosPlan));
            Routing.RegisterRoute(nameof(ToDoListPage), typeof(ToDoListPage));
            Routing.RegisterRoute(nameof(ToDoItemPage), typeof(ToDoItemPage));
        }
    }
}
