using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CumejaRing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterPageMenuItem> MenuItems { get; set; }

            public MasterPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterPageMenuItem>(new[]
                {
                    new MasterPageMenuItem { Id = 0, Title = "Servizi", TargetType=typeof(ServicesPage) },
                    new MasterPageMenuItem { Id = 1, Title = "Impostazioni" },
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
