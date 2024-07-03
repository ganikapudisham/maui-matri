using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;

namespace Matri.ViewModel
{
    public partial class EditPhotoViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        [ObservableProperty]
        public string imageSource;

        [ObservableProperty]
        public bool isBusy;
        public EditPhotoViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
        }

        [RelayCommand]
        public async Task BrowsePhoto()
        {
            var requestStorageRead = await Permissions.CheckStatusAsync<Permissions.Media>();

            if (requestStorageRead == PermissionStatus.Granted)
            {
                var file = await FilePicker.PickAsync(new PickOptions { FileTypes = FilePickerFileType.Images });

                if (file == null)
                    return;
                ImageSource = file.FullPath;
            }
            else if (requestStorageRead == PermissionStatus.Denied)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please allow app to access Storage", "OK");
                AppInfo.ShowSettingsUI();
            }
        }


        [RelayCommand]
        public async Task UploadPhoto()
        {
            var sessionToken = await SecureStorage.GetAsync("Token");
            var filePath = ImageSource;
            byte[] imageBytes = File.ReadAllBytes(ImageSource);

            byte[] image = File.ReadAllBytes(filePath);
            var fileName = filePath.Split('/')[filePath.Split('/').Length - 1];

            var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(sessionToken), "sessiontoken");
            formData.Add(new ByteArrayContent(image, 0, image.Length), "file", fileName);
            try
            {
                IsBusy = true;
                var status = await _serviceManager.UploadProfilePhoto(formData);
                if (status)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Photo Was Uploaded, Thank you", "OK");
                }
                else
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please try again", "OK");
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
    }
}

