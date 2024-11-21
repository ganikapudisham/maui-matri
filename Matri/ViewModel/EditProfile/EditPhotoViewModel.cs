using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Business;
using Matri.CustomExceptions;
using Matri.Helper;
using Matri.Model;
using System.Collections.ObjectModel;

namespace Matri.ViewModel
{
    public partial class EditPhotoViewModel : ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public List<string> imageSources = new List<string>();

        [ObservableProperty]
        public bool isBusy;

        [ObservableProperty]
        public bool showUpload = false;

        private ObservableCollection<CarouselModel> imageCollection = new();
        public ObservableCollection<CarouselModel> ImageCollection
        {
            get => imageCollection;
            set => SetProperty(ref imageCollection, value);
        }

        public EditPhotoViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
        }

        [RelayCommand]
        public async Task BrowsePhoto()
        {
            ImageCollection.Clear();
            var requestStorageRead = await Permissions.CheckStatusAsync<Permissions.Media>();

            if (requestStorageRead == PermissionStatus.Granted)
            {
                var files = await FilePicker.PickMultipleAsync(new PickOptions { FileTypes = FilePickerFileType.Images });

                if (files == null)
                    return;

                if (files.Count() > 5)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", "Only 5 photos can be uploaded, " +
                        "please remove additional photos and try again", "OK");
                    return;
                }

                foreach (var file in files)
                {
                    ImageSources.Add(file.FullPath);
                    ImageCollection.Add(new CarouselModel(file.FullPath));
                }

                if (ImageSources.Count > 0)
                {
                    ShowUpload = true;
                }
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

            var successCount = 0;
            var failureCount = 0;

            foreach (var path in ImageSources)
            {
                var filePath = path;
                byte[] imageBytes = File.ReadAllBytes(path);

                byte[] image = File.ReadAllBytes(filePath);
                var fileName = path.Split('/')[filePath.Split('/').Length - 1];

                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(sessionToken), "sessiontoken");
                formData.Add(new ByteArrayContent(image, 0, image.Length), "file", fileName);
                try
                {
                    IsBusy = true;
                    var status = await _serviceManager.UploadProfilePhoto(formData);
                    if (status)
                    {
                        successCount = successCount + 1;

                    }
                    else
                    {
                        failureCount = failureCount + 1;
                    }
                    IsBusy = false;
                }
                catch (MatriInternetException exception)
                {
                    IsBusy = false;
                }
                catch (Exception exception)
                {
                    IsBusy = false;
                }
            }

            await Shell.Current.CurrentPage.DisplayAlert("Alert", $"{successCount} Uploaded {failureCount} Failed, Thank you", "OK");
            ImageCollection.Clear();
        }
    }
}

