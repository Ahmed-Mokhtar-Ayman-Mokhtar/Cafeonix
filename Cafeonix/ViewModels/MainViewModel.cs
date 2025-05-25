namespace Cafeonix.ViewModels
{
    public class MainViewModel
    {
        public UserViewModel CurrentUser { get; set; }

        public MainViewModel(UserViewModel user)
        {
            CurrentUser = user;
        }

    }
}
