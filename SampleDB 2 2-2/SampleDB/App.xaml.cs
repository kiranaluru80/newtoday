using Xamarin.Forms;

namespace SampleDB
{
    public partial class App : Application
    {
      static EmpDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CommunicationsPage(12));
        }


        public static EmpDatabase Database
        {
            get

            {
                if(database==null)
                {
                    database = new EmpDatabase(DependencyService.Get<ISQLite>().GetFilePath("JSSE.db3"));
                    
                }
                return database;
            }
        } 

        //public static void newtworkCheking(responceStr){
            
        //}

        protected override void OnStart()
        {
		
			// Handle when your app starts
		}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
