using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VacationPlanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlideMenuMaster : ContentPage
    {
        public ListView ListView;

        public SlideMenuMaster()
        {
            InitializeComponent();

            BindingContext = new SlideMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class SlideMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<SlideMenuMasterMenuItem> MenuItems { get; set; }

            public SlideMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<SlideMenuMasterMenuItem>(new[]
                {
                    new SlideMenuMasterMenuItem { Id = 0, Title = "User Name" },
                    new SlideMenuMasterMenuItem { Id = 1, Title = "Vacation Days Till Now" },
                    new SlideMenuMasterMenuItem { Id = 2, Title = "Explored World" },
                    new SlideMenuMasterMenuItem { Id = 3, Title = "Total Expense on Travel" },
                    new SlideMenuMasterMenuItem { Id = 4, Title = "Settings" },
                    new SlideMenuMasterMenuItem { Id = 5, Title = "Logout" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}