using Matri.Business;
using Matri.CustomExceptions;
using Matri.Model;
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
using Matri.FontsAwesome;
using Syncfusion.Maui.Picker;
using Matri.Abstract;
using Matri.Helper;

namespace Matri.ViewModel;

public partial class RegisterViewModel : ObservableObject
{
    IServiceManager _serviceManager;
    private const int NotificationIdBirthday = 307;
    //private readonly Abstract.IDateNotificationScheduler _birthdayService;
    public RegisterViewModel(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        //_birthdayService = ServiceHelper.GetService<Abstract.IDateNotificationScheduler>();
        Genders = new ObservableCollection<Master>();

        BirthDate = DateTime.Now.AddYears(-18).AddDays(1);
        MaxDateString = DateTime.Now.AddYears(-18).AddDays(1).ToString("dd MMM yyyy");
        MinDateString = DateTime.Now.AddYears(-70).ToString("dd MMM yyyy");

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

    [ObservableProperty]
    public string minDateString;

    [ObservableProperty]
    public string maxDateString;

    [ObservableProperty]
    public DateTime birthDate;

    [RelayCommand]
    public async Task Cancel()
    {
        await Shell.Current.GoToAsync("///LoginPage");
    }

    [RelayCommand]
    public async Task Register()
    {
        if (string.IsNullOrWhiteSpace(FirstName))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide FirstName", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide LastName", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(UserName))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Mobile Number", "Ok");
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please provide Password", "Ok");
            return;
        }

        if (SelectedGender == null)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please specify Gender", "Ok");
            return;
        }

        if (BirthDate.ToString("dd MMM yyyy") == MaxDateString)
        {
            await Shell.Current.CurrentPage.DisplayAlert("Alert", "You must be atleast 18 years old to use ChristianJodi app", "Ok");
            return;
        }

        IsBusy = true;
        try
        {
            var isSuccess = await _serviceManager.RegisterUserAsync(FirstName, LastName, UserName,
                Password, Password, SelectedGender.Name, BirthDate, "");

            //_birthdayService.ScheduleNotification(birthDate, "Happy Birthday", "Happy Birthday", "tagBirthday");

            await Shell.Current.CurrentPage.DisplayAlert("Alert", "Thank You For Registering With Us. You Can Now LogIn.", "Ok");
            await Shell.Current.GoToAsync("///LoginPage");
        }
        catch (MatriInternetException exception)
        {
            IsBusy = false;
            await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "Ok");
        }
        catch (Exception exception)
        {
            IsBusy = false;
            await Shell.Current.CurrentPage.DisplayAlert("Alert", exception?.Message, "Ok");
        }
        IsBusy = false;
    }

    private ObservableCollection<Master> genders;
    public ObservableCollection<Master> Genders
    {
        get { return genders; }
        set
        {
            genders = value;
        }
    }

    [ObservableProperty]
    public Master selectedGender;

    [ObservableProperty]
    public bool isHiddenPassword = true;

    [ObservableProperty]
    public string passwordVisibilityIcon = FontAwesomeIcons.EyeSlash;

    [RelayCommand]
    public void TogglePassword()
    {
        IsHiddenPassword = false;
        PasswordVisibilityIcon = FontAwesomeIcons.Eye;

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        Task.Delay(500).ContinueWith(async (t) =>
        {
            IsHiddenPassword = true;
            PasswordVisibilityIcon = FontAwesomeIcons.EyeSlash;
        }, cancellationToken);
    }

    [RelayCommand]
    public void BirthDateChanged(DatePickerSelectionChangedEventArgs datePickerSelectionChangedEventArgs)
    {
        var newSelectedDate = datePickerSelectionChangedEventArgs.NewValue;

        BirthDate = new DateTime(newSelectedDate.Value.Year, newSelectedDate.Value.Month, newSelectedDate.Value.Day);
    }
}
