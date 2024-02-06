using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Helper;
using Matri.Model;
using Matri.Views;
using Newtonsoft.Json;
using Syncfusion.Maui.TabView;
using System.ComponentModel;
namespace Matri.ViewModel
{
    public partial class EditProfileViewModel : ObservableObject
    {
        public EditProfileViewModel()
        {
            TabItems = new TabItemCollection();

            var basicPage1 = new EditBasicPage();
            var religionPage2 = new EditReligionPage();
            var physicalPage3 = new EditPhysicalPage();

            var educationPage4 = new EditAcademicsPage();
            var contactsPage5 = new EditContactsPage();
            var familyPage6 = new EditFamilyPage();

            //var lifeStylePage7 = new EditLifeStylePage();
            var photoPage8 = new EditPhotoPage();
            //var expectationsPage9 = new EditExpectationsPage();

            var tabItemBasic1 = new SfTabItem { Content = basicPage1.Content, Header = "Basic" };
            tabItemBasic1.Content.BindingContext = basicPage1.BindingContext;

            var tabItemReligion2 = new SfTabItem { Content = religionPage2.Content, Header = "Religion" };
            tabItemReligion2.Content.BindingContext = religionPage2.BindingContext;

            var tabItemPhysical3 = new SfTabItem { Content = physicalPage3.Content, Header = "Physical" };
            tabItemPhysical3.Content.BindingContext = physicalPage3.BindingContext;

            var tabItemEducation4 = new SfTabItem { Content = educationPage4.Content, Header = "Education" };
            tabItemEducation4.Content.BindingContext = educationPage4.BindingContext;

            var tabItemContact5 = new SfTabItem { Content = contactsPage5.Content, Header = "Contact" };
            tabItemContact5.Content.BindingContext = contactsPage5.BindingContext;

            var tabItemFamily6 = new SfTabItem { Content = familyPage6.Content, Header = "Family" };
            tabItemFamily6.Content.BindingContext = familyPage6.BindingContext;

            //var tabItemLife7 = new SfTabItem { Content = lifeStylePage7.Content, Header = "Life Style" };
            //tabItemLife7.Content.BindingContext = lifeStylePage7.BindingContext;

            var tabItemPhoto8 = new SfTabItem { Content = photoPage8.Content, Header = "Photo" };
            tabItemPhoto8.Content.BindingContext = photoPage8.BindingContext;

            //var tabItemExpec9 = new SfTabItem { Content = expectationsPage9.Content, Header = "Expectations" };
            //tabItemExpec9.Content.BindingContext = expectationsPage9.BindingContext;

            TabItems.Add(tabItemBasic1);
            TabItems.Add(tabItemReligion2);
            TabItems.Add(tabItemPhysical3);
            TabItems.Add(tabItemEducation4);
            TabItems.Add(tabItemContact5);
            //TabItems.Add(tabItemFamily6);
            //TabItems.Add(tabItemLife7);
            TabItems.Add(tabItemPhoto8);
            //TabItems.Add(tabItemExpec9);
        }

        [ObservableProperty]
        public TabItemCollection tabItems;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

