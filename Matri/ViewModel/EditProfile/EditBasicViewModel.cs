using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Abstract;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using MvvmHelpers;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Matri.ViewModel
{
    public partial class EditBasicViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private readonly IServiceManager _serviceManager;
        private readonly ISharedService _sharedService;

        [ObservableProperty]
        public Profile profile;
        [ObservableProperty]
        public string birthPlace;
        [ObservableProperty]
        public string birthTime;
        [ObservableProperty]
        public string aboutMe;
        [ObservableProperty]
        public string firstName;
        [ObservableProperty]
        public string lastName;
        [ObservableProperty]
        public bool showHinduFields;
        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public Master selectedMotherTongue;

        private ObservableRangeCollection<Master> mdLanguages;
        public ObservableRangeCollection<Master> MDLanguages
        {
            get { return mdLanguages; }
            set
            {
                mdLanguages = value;
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

        [ObservableProperty]
        public Master selectedMaritalStatus;

        public EditBasicViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            _sharedService = ServiceHelper.GetService<ISharedService>();
            MDLanguages = new ObservableRangeCollection<Master>();
            MDMaritalStatus = new ObservableRangeCollection<Master>();

            var defaultMaster = new Master();
            defaultMaster.Id = "SELECT";
            defaultMaster.Name = "SELECT";

            SelectedMotherTongue = defaultMaster;
            SelectedMaritalStatus = defaultMaster;

            BDDates = new ObservableRangeCollection<Master>();
            BDYears = new ObservableRangeCollection<Master>();
            BDMonths = new ObservableRangeCollection<Master>();

            Task.Run(async () => { await Init(); });
        }

        public async Task Init()
        {

            //var sessionToken = await SecureStorage.GetAsync("Token");
            Profile =  _sharedService.GetValue<Profile>("LoggedInUser");
            var md = _sharedService.GetValue<MDD>("MasterData");
            FirstName = Profile.FirstName;
            LastName = Profile.LastName;
            BirthTime = Profile.BirthTime;
            BirthPlace = Profile.BirthPlace;
            AboutMe = Profile.AboutMe;


            for (var i = 1; i < 32; i++)
            {
                BDDates.Add(new Master { Id = i.ToString("00"), Name = i.ToString("00") });
            }

            var currentYear = DateTime.Now.Year;
            var UpperLimit = currentYear - 19; // 18 years back
            var LowerLimit = UpperLimit - 70;

            for (var i = UpperLimit; i > LowerLimit; i--)
            {
                BDYears.Add(new Master { Id = i.ToString(), Name = i.ToString() });
            }

            var months = new List<string>();
            months.Add("Jan");
            months.Add("Feb");
            months.Add("Mar");
            months.Add("Apr");
            months.Add("May");
            months.Add("Jun");
            months.Add("Jul");
            months.Add("Aug");
            months.Add("Sep");
            months.Add("Oct");
            months.Add("Nov");
            months.Add("Dec");

            for (int i = 0; i < 12; i++)
            {
                var id = i + 1;
                BDMonths.Add(new Master { Id = id.ToString("00"), Name = months[i] });
            }

            var birthDate = Profile.BirthDate.Split('-');
            SelectedBirthDate = BDDates.Where(bd => bd.Name == birthDate[2]).FirstOrDefault();
            SelectedBirthMonth = BDMonths.Where(bm => bm.Name == birthDate[1]).FirstOrDefault();
            SelectedBirthYear = BDYears.Where(bys => bys.Name == birthDate[0]).FirstOrDefault();


            var showHinduFields = await SecureStorage.GetAsync("ShowHinduFields");

            ShowHinduFields = Convert.ToBoolean(showHinduFields);

            try
            {
                MDLanguages.AddRange(md.Languages);

                SelectedMotherTongue = md.Languages.Where(mt => mt.Id.ToLower() == Profile.MotherTongue.ToLower()).FirstOrDefault();

                MDMaritalStatus.AddRange(md.MaritalStatuses);

                SelectedMaritalStatus = md.MaritalStatuses.Where(mt => mt.Id.ToLower() == Profile.MaritalStatus.ToLower()).FirstOrDefault();
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
            }
        }

        private ObservableRangeCollection<Master> bdDates;
        public ObservableRangeCollection<Master> BDDates
        {
            get { return bdDates; }
            set
            {
                bdDates = value;
            }
        }

        private ObservableRangeCollection<Master> bdMonths;
        public ObservableRangeCollection<Master> BDMonths
        {
            get { return bdMonths; }
            set
            {
                bdMonths = value;
            }
        }

        private ObservableRangeCollection<Master> bdYears;
        public ObservableRangeCollection<Master> BDYears
        {
            get { return bdYears; }
            set
            {
                bdYears = value;
            }
        }

        [ObservableProperty]
        public Master selectedBirthDate;

        [ObservableProperty]
        public Master selectedBirthMonth;

        [ObservableProperty]
        public Master selectedBirthYear;


        [RelayCommand]
        public async Task Update()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Provide First Name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Provide Last Name", "OK");
                return;
            }

            if (SelectedMaritalStatus == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Provide Marital Status", "OK");
                return;
            }

            if (SelectedMotherTongue == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please Specify Mother Tongue", "OK");
                return;
            }

            var firstName = FirstName;
            var lastName = LastName;
            var aboutMe = AboutMe;
            var birthPlace = BirthPlace;
            var birthTime = BirthTime;

            var year = Convert.ToInt16(SelectedBirthYear.Name);
            var month = Convert.ToInt16(SelectedBirthMonth.Id);
            var date = Convert.ToInt16(SelectedBirthDate.Name);

            var BirthDateInApiFormat = $"{year}-{month:00}-{date:00}";

            var sessionToken = await SecureStorage.GetAsync("Token");

            var basicDetails = new Profile
            {
                FirstName = firstName,
                LastName = lastName,
                AboutMe = aboutMe,
                BirthTime = birthTime,
                BirthPlace = birthPlace,
                MaritalStatus = SelectedMaritalStatus.Id,
                MotherTongue = SelectedMotherTongue.Id,
                BirthDate = BirthDateInApiFormat
            };
            try
            {
                IsBusy = true;
                var status = await _serviceManager.UpdateBasicDetails(new Guid(sessionToken), basicDetails);
                IsBusy = false;
                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Basic Details Updated", "OK");
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
    }
}

