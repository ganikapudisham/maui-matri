using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public partial class EditAcademicsViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {

        IServiceManager _serviceManager;
        ISharedService _sharedService;

        [ObservableProperty]
        public Profile profile;

        [ObservableProperty]
        public string academicDetails;

        [ObservableProperty]
        public string employementDetails;

        [ObservableProperty]
        public string incomeDetails;

        [ObservableProperty]
        public string propertyDetails;

        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public Master selectedAcademic;

        [ObservableProperty]
        public Master selectedIncome;

        [ObservableProperty]
        public Master selectedSector;

        [ObservableProperty]
        public Master selectedIndustry;
        public EditAcademicsViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();

            MDAcademics = new ObservableRangeCollection<Master>();
            MDIncomes = new ObservableRangeCollection<Master>();

            MDSectors = new ObservableRangeCollection<Master>();
            MDIndustries = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedAcademic = defaultMaster;
            SelectedIncome = defaultMaster;
            SelectedSector = defaultMaster;
            SelectedIndustry = defaultMaster;

            Init();
        }

        public void Init()
        {
            IsBusy = true;
            Profile = _sharedService.GetValue<Profile>("LoggedInUser");
            var md = _sharedService.GetValue<MDD>("MasterData");

            EmployementDetails = Profile.EmploymentDetails;
            IncomeDetails = Profile.IncomeDetails;
            PropertyDetails = Profile.PropertyDetails;
            AcademicDetails = Profile.EducationDetails;

            try
            {
                MDAcademics.AddRange(md.Academics);
                SelectedAcademic = md.Academics.Where(mt => mt.Id.ToLower() == Profile.Education.ToLower()).FirstOrDefault();

                MDIncomes.AddRange(md.Incomes);
                SelectedIncome = md.Incomes.Where(mt => mt.Id.ToLower() == Profile.AnnualIncome.ToLower()).FirstOrDefault();

                MDSectors.AddRange(md.JobTypes);
                SelectedSector = md.JobTypes.Where(mt => mt.Id.ToLower() == Profile.JobType.ToLower()).FirstOrDefault();

                MDIndustries.AddRange(md.Jobs);
                SelectedIndustry = md.Jobs.Where(mt => mt.Id.ToLower() == Profile.Job.ToLower()).FirstOrDefault();
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Update()
        {
            if (SelectedAcademic == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Select Highest Academic Qualification", "OK");
                return;
            }

            if (SelectedIncome == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Income", "OK");
                return;
            }

            var sessionToken = await SecureStorage.GetAsync("Token");

            Profile.EmploymentDetails = EmployementDetails;
            Profile.IncomeDetails = IncomeDetails;
            Profile.PropertyDetails = PropertyDetails;
            Profile.EducationDetails = AcademicDetails;
            Profile.Education = SelectedAcademic.Id;
            Profile.AnnualIncome = SelectedIncome.Id;
            Profile.Job = SelectedIndustry.Id;
            Profile.JobType = SelectedSector.Id;

            try
            {
                IsBusy = true;
                var status = await _serviceManager.UpdateEducationDetails(sessionToken, Profile);
                IsBusy = false;
                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Physical Details Updated", "OK");
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

        private ObservableRangeCollection<Master> mDIncomes;
        public ObservableRangeCollection<Master> MDIncomes
        {
            get { return mDIncomes; }
            set
            {
                mDIncomes = value;
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

        private ObservableRangeCollection<Master> mDIndustries;
        public ObservableRangeCollection<Master> MDIndustries
        {
            get { return mDIndustries; }
            set
            {
                mDIndustries = value;
            }
        }
    }
}

