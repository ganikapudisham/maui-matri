using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;

namespace Matri.ViewModel
{
    public partial class EditExpectationsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;
        ISharedService _sharedService;

        public EditExpectationsViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();

            MDHeights = new ObservableRangeCollection<Master>();
            MDAges = new ObservableRangeCollection<Master>();

            MDMaritalStatus = new ObservableRangeCollection<Master>();

            MDPhysicalStatus = new ObservableRangeCollection<Master>();
            MDComplexions = new ObservableRangeCollection<Master>();

            MDLanguages = new ObservableRangeCollection<Master>();

            MDSectors = new ObservableRangeCollection<Master>();
            MDAcademics = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedHeightFrom = defaultMaster;
            SelectedHeightTo = defaultMaster;
            SelectedAgeFrom = defaultMaster;
            SelectedAgeTo = defaultMaster;

            var defaultMaster1 = new List<Master>();
            defaultMaster1.Add(new Master { Id = "SELECT", Name = "SELECT" });

            SelectedMaritalStatus = defaultMaster1;
            SelectedPhysicalStatus = defaultMaster1;
            SelectedComplexion = defaultMaster1;
            SelectedLanguage = defaultMaster1;
            SelectedAcademic = defaultMaster1;
            SelectedSector = defaultMaster1;

            Init();
        }

        public void Init()
        {
            IsBusy = true;

            var agesList = new List<Master>();
            for (var i = 18; i <= 70; i++)
            {
                agesList.Add(new Master { Id = i.ToString(), Name = i.ToString() });
            }

            MDAges.AddRange(agesList);


            Profile = _sharedService.GetValue<Profile>("LoggedInUser");
            var md = _sharedService.GetValue<MDD>("MasterData");

            try
            {
                //MDHeights.AddRange(md.Heights);

                //if (Profile.Expectations.HeightFrom != null)
                //{
                //    SelectedHeightFrom = md.Heights.Where(mt => mt.Id.ToLower() == Profile.Expectations.HeightFrom.ToLower()).FirstOrDefault();
                //}

                //if (Profile.Expectations.HeightTo != null)
                //{
                //    SelectedHeightTo = md.Heights.Where(mt => mt.Id.ToLower() == Profile.Expectations.HeightTo.ToLower()).FirstOrDefault();
                //}

                //if (Profile.Expectations.AgeFrom != null)
                //{
                //    SelectedAgeFrom = MDAges.Where(mt => mt.Id.ToLower() == Profile.Expectations.AgeFrom.ToLower()).FirstOrDefault();
                //}

                //if (Profile.Expectations.AgeTo != null)
                //{
                //    SelectedAgeTo = MDAges.Where(mt => mt.Id.ToLower() == Profile.Expectations.AgeTo.ToLower()).FirstOrDefault();
                //}

                MDAcademics.AddRange(md.Academics);
                foreach (var i in Profile.Expectations.Educations)
                {
                    SelectedAcademic.Add(new Master { Id = i.Code, Name = i.Name });
                }

                //MDMaritalStatus.AddRange(md.MaritalStatuses);
                //foreach (var i in Profile.Expectations.MaritalStatus)
                //{
                //    SelectedMaritalStatus.Add(new Master { Id = i.Code, Name = i.Name });
                //}

                //MDSectors.AddRange(md.JobTypes);
                //foreach (var i in Profile.Expectations.JobTypes)
                //{
                //    SelectedSector.Add(new Master { Id = i.Code, Name = i.Name });
                //}

                //MDPhysicalStatus.AddRange(md.PhysicalStatuses);
                //foreach (var i in Profile.Expectations.PhysicalStatuses)
                //{
                //    SelectedPhysicalStatus.Add(new Master { Id = i.Code, Name = i.Name });
                //}

                //MDLanguages.AddRange(md.Languages);
                //foreach (var i in Profile.Expectations.Languages)
                //{
                //    SelectedLanguage.Add(new Master { Id = i.Code, Name = i.Name });
                //}

                //MDComplexions.AddRange(md.Complexions);
                //foreach (var i in Profile.Expectations.Complexions)
                //{
                //    SelectedComplexion.Add(new Master { Id = i.Code, Name = i.Name });
                //}

                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }

        [ObservableProperty]
        public Master selectedHeightFrom;

        [ObservableProperty]
        public Master selectedHeightTo;

        [ObservableProperty]
        public Master selectedAgeFrom;

        [ObservableProperty]
        public Master selectedAgeTo;

        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public Profile profile;

        [ObservableProperty]
        public bool sameReligion;

        [ObservableProperty]
        public bool sameCaste;

        [ObservableProperty]
        public bool sameDenomination;

        [ObservableProperty]
        public string expectationDetails;

        [ObservableProperty]
        public List<Master> selectedMaritalStatus;

        [ObservableProperty]
        public List<Master> selectedLanguage;

        [ObservableProperty]
        public List<Master> selectedPhysicalStatus;

        [ObservableProperty]
        public List<Master> selectedComplexion;

        [ObservableProperty]
        public List<Master> selectedAcademic;

        [ObservableProperty]
        public List<Master> selectedSector;


        private ObservableRangeCollection<Master> mDHeights;

        public ObservableRangeCollection<Master> MDHeights
        {
            get { return mDHeights; }
            set
            {
                mDHeights = value;
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

        private ObservableRangeCollection<Master> mDComplexions;

        public ObservableRangeCollection<Master> MDComplexions
        {
            get { return mDComplexions; }
            set
            {
                mDComplexions = value;
            }
        }

        private ObservableRangeCollection<Master> mDAges;

        public ObservableRangeCollection<Master> MDAges
        {
            get { return mDAges; }
            set
            {
                mDAges = value;
            }
        }

        private ObservableRangeCollection<Master> mdMaritalStatus;
        public ObservableRangeCollection<Master> MDMaritalStatus
        {
            get { return mdMaritalStatus; }
            set
            {
                mdMaritalStatus = value;
            }
        }

        private ObservableRangeCollection<Master> mdLanguages;
        public ObservableRangeCollection<Master> MDLanguages
        {
            get { return mdLanguages; }
            set
            {
                mdLanguages = value;
            }
        }

        private ObservableRangeCollection<Master> mDSectors;
        public ObservableRangeCollection<Master> MDSectors
        {
            get { return mDSectors; }
            set
            {
                mDSectors = value;
            }
        }

        private ObservableRangeCollection<Master> mDAcademics;
        public ObservableRangeCollection<Master> MDAcademics
        {
            get { return mDAcademics; }
            set
            {
                mDAcademics = value;
            }
        }

        [RelayCommand]
        private async Task Update()
        {
            IsBusy = true;

            var sessionToken = await SecureStorage.GetAsync("Token");
            var partner = new Partner();
            //partner.Educations = SelectedAcademic.Id;
            //partner.JobTypes = SelectedSector.Id;
            partner.HeightFrom = SelectedHeightFrom.Id;
            partner.HeightTo = SelectedHeightTo.Id;
            partner.AgeFrom = SelectedAgeFrom.Id;
            partner.AgeTo = SelectedAgeTo.Id;

            //partner.MaritalStatus = SelectedMaritalStatus.Id;
            //partner.PhysicalStatuses = SelectedPhysicalStatus.Id;
            //partner.Complexions = SelectedComplexion.Id;
            //partner.Languages = SelectedLanguage.Id;
            partner.SameReligion = SameReligion;
            partner.SameCaste = SameCaste;
            partner.SameDenomination = SameDenomination;
            partner.Expectations = ExpectationDetails;


            try
            {
                var status = await _serviceManager.UpdateExpectations(new Guid(sessionToken), partner);

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

