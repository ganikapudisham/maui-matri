using ChristianJodi.Business;
using ChristianJodi.CustomExceptions;
using ChristianJodi.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristianJodi.ViewModel
{
    public partial class RegisterViewModel :ObservableObject
    {
        IServiceManager _serviceManager;
        public RegisterViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            BDDates = new ObservableCollection<Master>();
            BDYears = new ObservableCollection<Master>();
            BDMonths = new ObservableCollection<Master>();
            Genders = new ObservableCollection<Master>();

            for (var i = 1; i < 32; i++)
            {
                BDDates.Add(new Master { Id = i.ToString(), Name = i.ToString() });
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
                BDMonths.Add(new Master { Id = id.ToString(), Name = months[i] });
            }

            Genders.Add(new Master { Id = "Male", Name = "Male" });
            Genders.Add(new Master { Id = "Female", Name = "Female" });
        }

        [ObservableProperty]
        public bool isBusy;

        [ObservableProperty]
        public string firstName;

        [ObservableProperty]
        public string lastName;

        [ObservableProperty]
        public string userName;

        [ObservableProperty]
        public string password;

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }

        [RelayCommand]
        public async Task Register()
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide FirstName", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide LastName", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Mobile Number", "Ok");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Password", "Ok");
                return;
            }


            if (string.IsNullOrWhiteSpace(selectedGender.Name))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please specify Gender", "Ok");
                return;
            }

            if (SelectedBirthDate == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Birth Date", "Ok");
                return;
            }

            if (SelectedBirthMonth == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Birth Month", "Ok");
                return;
            }

            if (SelectedBirthYear == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Birth Year", "Ok");
                return;
            }

            var year = Convert.ToInt16(selectedBirthYear.Name);
            var month = Convert.ToInt16(selectedBirthMonth.Id);
            var date = Convert.ToInt16(selectedBirthDate.Name);

            var birth = new DateTime(year, month, date);

            isBusy = true;
            try
            {
                var isSuccess = await _serviceManager.RegisterUserAsync(firstName, lastName, userName,
                    password, password, selectedGender.Name, birth, "");
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Thank You For Registering With Us. You Can Now LogIn.", "Ok");
                await Shell.Current.GoToAsync("///LoginPage");
            }
            catch (ChristianJodiInternetException exception)
            {
                isBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "Ok");
            }
            catch (Exception exception)
            {
                isBusy = false;
                await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "Ok");
            }
            isBusy = false;
        }

        private ObservableCollection<Master> bdDates;
        public ObservableCollection<Master> BDDates
        {
            get { return bdDates; }
            set
            {
                bdDates = value;
            }
        }

        private ObservableCollection<Master> bdMonths;
        public ObservableCollection<Master> BDMonths
        {
            get { return bdMonths; }
            set
            {
                bdMonths = value;
            }
        }

        private ObservableCollection<Master> bdYears;
        public ObservableCollection<Master> BDYears
        {
            get { return bdYears; }
            set
            {
                bdYears = value;
            }
        }

        private ObservableCollection<Master> Genders;
        public ObservableCollection<Master> genders
        {
            get { return genders; }
            set
            {
                genders = value;
            }
        }

        [ObservableProperty]
        public Master selectedBirthDate;

        [ObservableProperty]
        public Master selectedBirthMonth;

        [ObservableProperty]
        public Master selectedBirthYear;

        [ObservableProperty]
        public Master selectedGender;

    }
}
