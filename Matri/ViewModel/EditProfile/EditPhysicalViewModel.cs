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
using System.Collections.ObjectModel;

namespace Matri.ViewModel
{
    public partial class EditPhysicalViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;

        [ObservableProperty]
        public Profile profile;

        [ObservableProperty]
        public bool isBusy = true;

        //private Command<SfComboBox> onHeightChangedCommand;

        //public Command<SfComboBox> OnHeightChangedCommand
        //{
        //    get { return onHeightChangedCommand; }
        //    set { onHeightChangedCommand = value; OnPropertyChanged(nameof(OnHeightChanged)); }
        //}

        //private Command<SfComboBox> onWeightChangedCommand;

        //public Command<SfComboBox> OnWeightChangedCommand
        //{
        //    get { return onWeightChangedCommand; }
        //    set { onWeightChangedCommand = value; OnPropertyChanged(nameof(OnWeightChanged)); }
        //}

        //private Command<SfComboBox> onPhysicalChangedCommand;

        //public Command<SfComboBox> OnPhysicalChangedCommand
        //{
        //    get { return onPhysicalChangedCommand; }
        //    set { onPhysicalChangedCommand = value; OnPropertyChanged(nameof(OnPhysicalChanged)); }
        //}

        //private Command<SfComboBox> onComplexionChangedCommand;

        //public Command<SfComboBox> OnComplexionChangedCommand
        //{
        //    get { return onComplexionChangedCommand; }
        //    set { onComplexionChangedCommand = value; OnPropertyChanged(nameof(OnComplexionChanged)); }
        //}

        //private Command<SfComboBox> onBodyTypeChangedCommand;

        //public Command<SfComboBox> OnBodyTypeChangedCommand
        //{
        //    get { return onBodyTypeChangedCommand; }
        //    set { onBodyTypeChangedCommand = value; OnPropertyChanged(nameof(OnBodyTypeChanged)); }
        //}

        //private Command<SfComboBox> onCreatedByChangedCommand;

        //public Command<SfComboBox> OnCreatedByChangedCommand
        //{
        //    get { return onCreatedByChangedCommand; }
        //    set { onCreatedByChangedCommand = value; OnPropertyChanged(nameof(OnCreatedByChanged)); }
        //}


        public EditPhysicalViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();

            MDHeights = new ObservableRangeCollection<Master>();
            MDWeights = new ObservableRangeCollection<Master>();

            MDPhysicalStatus = new ObservableRangeCollection<Master>();
            MDBodyTypes = new ObservableRangeCollection<Master>();

            MDComplexions = new ObservableRangeCollection<Master>();
            MDProfileCreators = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedHeight = defaultMaster;
            SelectedWeight = defaultMaster;
            SelectedComplexion = defaultMaster;
            SelectedPhysicalStatus = defaultMaster;
            SelectedBodyType = defaultMaster;
            SelectedCreatedBy = defaultMaster;

            Init();
        }

        public void Init()
        {
            IsBusy = true;
            var weightList = new List<Master>();
            for (var i = 40; i <= 120; i++)
            {
                weightList.Add(new Master { Id = i.ToString(), Name = i.ToString() });
            }

            MDWeights.AddRange(weightList);

            Profile = _sharedService.GetValue<Profile>("LoggedInUser");
            var md = _sharedService.GetValue<MDD>("MasterData");

            SelectedWeight = MDWeights.Where(mdw => mdw.Name == Profile.Weight.ToString()).FirstOrDefault();
            try
            {
                MDHeights.AddRange(md.Heights);
                SelectedHeight = md.Heights.Where(mt => mt.Id.ToLower() == Profile.Height.ToLower()).FirstOrDefault();

                MDPhysicalStatus.AddRange(md.PhysicalStatuses);
                SelectedPhysicalStatus = md.PhysicalStatuses.Where(mt => mt.Id.ToLower() == Profile.PhysicalStatus.ToLower()).FirstOrDefault();

                MDBodyTypes.AddRange(md.BodyTypes);
                SelectedBodyType = md.BodyTypes.Where(mt => mt.Id.ToLower() == Profile.BodyType.ToLower()).FirstOrDefault();

                MDComplexions.AddRange(md.Complexions);
                SelectedComplexion = md.Complexions.Where(mt => mt.Id.ToLower() == Profile.Complexion.ToLower()).FirstOrDefault();

                MDProfileCreators.AddRange(md.ProfileCreatedBy);
                SelectedCreatedBy = md.ProfileCreatedBy.Where(mt => mt.Id.ToLower() == Profile.ProfileCreatedBy.ToLower()).FirstOrDefault();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        [ObservableProperty]
        public Master selectedHeight;

        [ObservableProperty]
        public Master selectedWeight;

        [ObservableProperty]
        public Master selectedPhysicalStatus;

        [ObservableProperty]
        public Master selectedBodyType;

        [ObservableProperty]
        public Master selectedComplexion;

        [ObservableProperty]
        public Master selectedCreatedBy;

        private ObservableRangeCollection<Master> mDHeights;

        public ObservableRangeCollection<Master> MDHeights
        {
            get { return mDHeights; }
            set
            {
                mDHeights = value;
            }
        }

        private ObservableRangeCollection<Master> mDWeights;

        public ObservableRangeCollection<Master> MDWeights
        {
            get { return mDWeights; }
            set
            {
                mDWeights = value;
            }
        }

        private ObservableRangeCollection<Master> mDPhysicalStatus;

        public ObservableRangeCollection<Master> MDPhysicalStatus
        {
            get { return mDPhysicalStatus; }
            set
            {
                mDPhysicalStatus = value;
            }
        }

        private ObservableRangeCollection<Master> mDBodyTypes;

        public ObservableRangeCollection<Master> MDBodyTypes
        {
            get { return mDBodyTypes; }
            set
            {
                mDBodyTypes = value;
            }
        }

        private ObservableRangeCollection<Master> mDComplexions;

        public ObservableRangeCollection<Master> MDComplexions
        {
            get { return mDComplexions; }
            set
            {
                mDComplexions = value;
            }
        }

        private ObservableRangeCollection<Master> mDProfileCreators;

        public ObservableRangeCollection<Master> MDProfileCreators
        {
            get { return mDProfileCreators; }
            set
            {
                mDProfileCreators = value;
            }
        }


        [RelayCommand]
        private async Task Update()
        {
            IsBusy = true;

            if (SelectedHeight == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Height", "OK");
                return;
            }

            if (SelectedPhysicalStatus == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Physical Status", "OK");
                return;
            }

            if (SelectedComplexion == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Complexion", "OK");
                return;
            }

            if (SelectedBodyType == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Body Type", "OK");
                return;
            }

            var sessionToken = await SecureStorage.GetAsync("Token");

            Profile.Height = SelectedHeight.Id;
            Profile.Weight = Convert.ToInt16(SelectedWeight.Id);
            Profile.PhysicalStatus = SelectedPhysicalStatus.Id;
            Profile.BodyType = SelectedBodyType.Id;
            Profile.Complexion = SelectedComplexion.Id;
            Profile.ProfileCreatedBy = SelectedCreatedBy.Id;

            try
            {
                var status = await _serviceManager.UpdatePhysicalDetails(new Guid(sessionToken), Profile);

                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Physical Details Have Been Updated", "OK");
                }
                else
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please try again", "OK");
                }
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
            IsBusy = false;
        }
    }
}

