using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using Microsoft.VisualBasic;
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

            SelectedHeightFrom = new Master { Id = "SELECT", Name = "SELECT" };
            SelectedHeightTo = new Master { Id = "SELECT", Name = "SELECT" };
            SelectedAgeFrom = new Master { Id = "SELECT", Name = "SELECT" };
            SelectedAgeTo = new Master { Id = "SELECT", Name = "SELECT" };

            SelectedMaritalStatuses = new List<Master>();
            SelectedPhysicalStatuses = new List<Master>();
            SelectedComplexions = new List<Master>();
            SelectedLanguages = new List<Master>();
            SelectedAcademics = new List<Master>();
            SelectedSectors = new List<Master>();

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
                MDHeights.AddRange(md.Heights);

                if (Profile.Expectations.HeightFrom != null)
                {
                    SelectedHeightFrom = md.Heights.Where(mt => mt.Id.ToLower() == Profile.Expectations.HeightFrom.ToLower()).FirstOrDefault();
                }

                if (Profile.Expectations.HeightTo != null)
                {
                    SelectedHeightTo = md.Heights.Where(mt => mt.Id.ToLower() == Profile.Expectations.HeightTo.ToLower()).FirstOrDefault();
                }

                if (Profile.Expectations.AgeFrom != null)
                {
                    SelectedAgeFrom = MDAges.Where(mt => mt.Id.ToLower() == Profile.Expectations.AgeFrom.ToLower()).FirstOrDefault();
                }

                if (Profile.Expectations.AgeTo != null)
                {
                    SelectedAgeTo = MDAges.Where(mt => mt.Id.ToLower() == Profile.Expectations.AgeTo.ToLower()).FirstOrDefault();
                }

                MDAcademics.AddRange(md.Academics);

                var expectations = Profile.Expectations;

                foreach (var e in expectations.Educations)
                {
                    var index = MDAcademics.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedEducation = MDAcademics[index];
                    SelectedAcademics.Add(selectedEducation);
                }

                MDMaritalStatus.AddRange(md.MaritalStatuses);

                foreach (var e in expectations.MaritalStatus)
                {
                    var index = MDMaritalStatus.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedMaritalStatus = MDMaritalStatus[index];
                    SelectedMaritalStatuses.Add(selectedMaritalStatus);
                }

                MDSectors.AddRange(md.JobTypes);

                foreach (var e in expectations.JobTypes)
                {
                    var index = MDSectors.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedSector = MDSectors[index];
                    SelectedSectors.Add(selectedSector);
                }

                MDPhysicalStatus.AddRange(md.PhysicalStatuses);

                foreach (var e in expectations.PhysicalStatuses)
                {
                    var index = MDPhysicalStatus.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedPhysicalStatus = MDPhysicalStatus[index];
                    SelectedPhysicalStatuses.Add(selectedPhysicalStatus);
                }

                MDLanguages.AddRange(md.Languages);

                foreach (var e in expectations.Languages)
                {
                    var index = MDLanguages.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedEducation = MDLanguages[index];
                    SelectedLanguages.Add(selectedEducation);
                }

                MDComplexions.AddRange(md.Complexions);

                foreach (var e in expectations.Complexions)
                {
                    var index = MDComplexions.ToList().FindIndex(i => i.Id == e.Code);
                    var selectedComplexion = MDComplexions[index];
                    SelectedComplexions.Add(selectedComplexion);
                }

                ExpectationDetails = Profile.Expectations.Expectations;

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
        public List<Master> selectedMaritalStatuses;

        [ObservableProperty]
        public List<Master> selectedLanguages;

        [ObservableProperty]
        public List<Master> selectedPhysicalStatuses;

        [ObservableProperty]
        public List<Master> selectedComplexions;

        [ObservableProperty]
        public List<Master> selectedAcademics;

        [ObservableProperty]
        public List<Master> selectedSectors;


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

            foreach (var selected in SelectedAcademics)
            {
                partner.Educations.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            foreach (var selected in SelectedSectors)
            {
                partner.JobTypes.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            foreach (var selected in SelectedMaritalStatuses)
            {
                partner.MaritalStatus.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            foreach (var selected in SelectedPhysicalStatuses)
            {
                partner.PhysicalStatuses.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            foreach (var selected in SelectedComplexions)
            {
                partner.Complexions.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            foreach (var selected in SelectedLanguages)
            {
                partner.Languages.Add(new Interest { Code = selected.Id, Name = selected.Name });
            }

            partner.HeightFrom = SelectedHeightFrom.Id;
            partner.HeightTo = SelectedHeightTo.Id;
            partner.AgeFrom = SelectedAgeFrom.Id;
            partner.AgeTo = SelectedAgeTo.Id;
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

