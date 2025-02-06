using CosplayApp.Views;

namespace CosplayApp
{
    public partial class App : Application
    {
        public static CosPlanItemDatabase DatabaseCos { get; set; }
        public static ToDoItemDatabase DatabaseTD { get; set; }


        public App(CosPlanItemDatabase db, ToDoItemDatabase db2)
        {
            InitializeComponent();
            DatabaseCos = db;
            DatabaseTD = db2;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new NavigationPage(new CosplaysPage(DatabaseCos, DatabaseTD)));
        }
    }
}
