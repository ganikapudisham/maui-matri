using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;
using Newtonsoft.Json;
using Syncfusion.Maui.Inputs;

namespace Matri.ViewModel
{
    public partial class EditReligionViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        readonly IServiceManager _serviceManager;
        private readonly ISharedService _sharedService;

        [ObservableProperty]
        public bool showDenominations = false;
        [ObservableProperty]
        public Profile profile;
        [ObservableProperty]
        public string star;
        [ObservableProperty]
        public string raasi;
        [ObservableProperty]
        public string gothram;
        [ObservableProperty]
        public string chevvaiDosham;
        [ObservableProperty]
        public string casteDetails;
        [ObservableProperty]
        public bool casteIsImportant;
        [ObservableProperty]
        public bool denominationIsImportant;
        [ObservableProperty]
        public bool showHinduFields = false;
        [ObservableProperty]
        public bool isBusy = true;

        private Command<SfComboBox> onReligionChangedCommand;

        public Command<SfComboBox> OnReligionChangedCommand
        {
            get { return onReligionChangedCommand; }
            set { onReligionChangedCommand = value; OnPropertyChanged(nameof(OnReligionChanged)); }
        }

        public EditReligionViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();

            MDReligions = new ObservableRangeCollection<Master>();
            MDCastes = new ObservableRangeCollection<Master>();
            MDDenominations = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedReligion = defaultMaster;
            SelectedCaste = defaultMaster;
            SelectedDenomination = defaultMaster;

            SelectedCommunity = defaultMaster;
            SelectedSubCaste = defaultMaster;

             Init();
        }

        private void Init()
        {
            IsBusy = true;
            try
            {
                Profile = _sharedService.GetValue<Profile>("LoggedInUser");
                var md = _sharedService.GetValue<MDD>("MasterData");

                Star = Profile.Star;
                Raasi = Profile.Raasi;
                Gothram = Profile.Gothram;
                ChevvaiDosham = Profile.ChevvaiDosham;
                CasteDetails = Profile.CasteInformation;
                CasteIsImportant = Profile.CasteCriteria;
                DenominationIsImportant = Profile.DenominationCriteria;

                var showHinduFields = _sharedService.GetBool("ShowHinduFields");

                ShowHinduFields = Convert.ToBoolean(showHinduFields);

                //var md = await _serviceManager.GetMasterData(new Guid(token));
                MDReligions.AddRange(md.Religions);

                SelectedReligion = md.Religions.Where(mt => mt.Id.ToLower() == Profile.Religion.ToLower()).FirstOrDefault();

                MDCastes.AddRange(md.Castes);
                SelectedCaste = md.Castes.Where(mt => mt.Id.ToLower() == Profile.Caste.ToLower()).FirstOrDefault();

                if (SelectedReligion.Name.ToLower() == "hindu")
                {
                    ShowDenominations = false;
                }
                else if (SelectedReligion.Name.ToLower() == "christian")
                {
                    ShowDenominations = true;
                    MDDenominations.Clear();
                    MDDenominations.AddRange(md.Denominations);

                    SelectedDenomination = md.Denominations.Where(mt => mt.Id.ToLower() == Profile.Denomination.ToLower()).FirstOrDefault();
                }
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
            }
        }

        public ObservableRangeCollection<Master> mDReligions;
        public ObservableRangeCollection<Master> MDReligions
        {
            get { return mDReligions; }
            set
            {
                mDReligions = value;
            }
        }
        private ObservableRangeCollection<Master> mDCastes;
        public ObservableRangeCollection<Master> MDCastes
        {
            get { return mDCastes; }
            set
            {
                mDCastes = value;
            }
        }
        private ObservableRangeCollection<Master> mDDenominations;
        public ObservableRangeCollection<Master> MDDenominations
        {
            get { return mDDenominations; }
            set
            {
                mDDenominations = value;
            }
        }

        [ObservableProperty]
        public Master selectedDenomination;
        [ObservableProperty]
        public Master selectedCaste;
        [ObservableProperty]
        public Master selectedReligion;
        [ObservableProperty]
        public Master selectedSubCaste;
        [ObservableProperty]
        public Master selectedCommunity;

        [RelayCommand]
        public async Task Update()
        {
            if (selectedReligion == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Religion", "OK");
                return;
            }

            if (selectedCaste == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Caste", "OK");
                return;
            }

            var sessionToken = await SecureStorage.GetAsync("Token");

            //var regligionDetails = new Profile
            //{
            Profile.Religion = SelectedReligion.Id;
            Profile.Denomination = SelectedDenomination.Id;
            Profile.Caste = SelectedCaste.Id;
            Profile.Star = Star;
            Profile.Raasi = Raasi;
            Profile.Gothram = Gothram;
            Profile.ChevvaiDosham = ChevvaiDosham;
            Profile.CasteInformation = CasteDetails;
            Profile.CasteCriteria = CasteIsImportant;
            Profile.DenominationCriteria = DenominationIsImportant;
            //};



            try
            {
                IsBusy = true;
                var status = await _serviceManager.UpdateReligionDetails(new Guid(sessionToken), Profile);

                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Religious Details Updated", "OK");
                }
                IsBusy = false;
            }
            catch (MatriInternetException exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
            catch (Exception exception)
            {
                IsBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
            }
        }

        private async void OnReligionChanged(SfComboBox cmbReligion)
        {
            var selectedItem = cmbReligion.SelectedItem;
            var religion = (selectedItem as Master).Id;

            var token = await SecureStorage.GetAsync("Token");
            var md = await _serviceManager.GetMasterData(new Guid(token));

            if (religion.ToLower() == "hindu")
            {
                
                Device.BeginInvokeOnMainThread(() =>
                {
                    MDCastes.Clear();
                    MDCastes.AddRange(md.Castes);
                    var userCaste = md.Castes.Where(mt => mt.Id.ToLower() == Profile.Caste.ToLower()).FirstOrDefault();
                    SelectedCaste = userCaste;
                    ShowDenominations = false;
                });
            }
            if (religion.ToLower() == "christian")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MDDenominations.Clear();
                    MDDenominations.AddRange(md.Denominations);
                    SelectedDenomination = md.Denominations.Where(mt => mt.Id.ToLower() == Profile.Denomination.ToLower()).FirstOrDefault();
                    ShowDenominations = true;
                });
            }
        }
    }
}

