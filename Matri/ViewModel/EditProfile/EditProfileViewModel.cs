using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Views;
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

            var photoPage7 = new EditPhotoPage();
            var expectationsPage8 = new EditExpectationsPage();

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

            var tabItemPhoto7 = new SfTabItem { Content = photoPage7.Content, Header = "Photo" };
            tabItemPhoto7.Content.BindingContext = photoPage7.BindingContext;

            var tabItemExpec8 = new SfTabItem { Content = expectationsPage8.Content, Header = "Expectations" };
            tabItemExpec8.Content.BindingContext = expectationsPage8.BindingContext;

            TabItems.Add(tabItemBasic1);
            TabItems.Add(tabItemReligion2);
            TabItems.Add(tabItemPhysical3);
            TabItems.Add(tabItemEducation4);
            TabItems.Add(tabItemContact5);
            TabItems.Add(tabItemFamily6);
            TabItems.Add(tabItemPhoto7);
            TabItems.Add(tabItemExpec8);
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

