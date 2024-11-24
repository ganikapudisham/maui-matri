using Matri.Business;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Matri.Model;
using System.Collections.ObjectModel;
using MvvmHelpers;
using System;

namespace AdminMatri.ViewModel
{
    public partial class OCRViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IServiceManager _serviceManager;

        [ObservableProperty]
        public bool flyoutIsPresented;

        [ObservableProperty]
        public List<string> imageSources = new List<string>();

        [ObservableProperty]
        public bool isBusy;

        [ObservableProperty]
        public bool showUpload = false;

        private ObservableRangeCollection<CarouselModel> imageCollection = new();

        public ObservableRangeCollection<CarouselModel> ImageCollection
        {
            get => imageCollection;
            set => SetProperty(ref imageCollection, value);
        }

        public ObservableRangeCollection<Master> groupNames = new();

        public ObservableRangeCollection<Master> GroupNames
        {
            get { return groupNames; }
            set
            {
                groupNames = value;
            }
        }

        [ObservableProperty]
        public Master selectedGroupName;

        public OCRViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            Task.Run(async () => {
                var dbGroupNames = await _serviceManager.GetWhatsappGroups("");
                GroupNames.AddRange(dbGroupNames);
            });
        }

        public async Task LoadWhatsappGroupNames()
        {
            var dbGroupNames = await _serviceManager.GetWhatsappGroups("");
            GroupNames.AddRange(dbGroupNames);
        }

        [RelayCommand]
        public async Task BrowsePhoto()
        {
            ImageCollection.Clear();
            ImageSources.Clear();

            var requestStorageRead = await Permissions.CheckStatusAsync<Permissions.Media>();

            if (requestStorageRead == PermissionStatus.Granted)
            {
                var files = await FilePicker.PickMultipleAsync(new PickOptions { FileTypes = FilePickerFileType.Images });

                if (files == null)
                    return;
                //ImageSource = file.FullPath;

                foreach (var file in files)
                {
                    imageSources.Add(file.FullPath);
                    ImageCollection.Add(new CarouselModel(file.FullPath));
                }

                if(imageSources.Count > 0)
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

            if (selectedGroupName == null || string.IsNullOrEmpty(selectedGroupName.Name))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Alert", "Please select group name", "Ok");
                return;
            }

            foreach (var path in imageSources)
            {
                var filePath = path;
                byte[] imageBytes = File.ReadAllBytes(path);

                byte[] image = File.ReadAllBytes(filePath);
                var fileName = filePath.Split('/')[filePath.Split('/').Length - 1];

                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(sessionToken), "sessiontoken");
                formData.Add(new StringContent(SelectedGroupName.Name), "whatsAppGroupName");
                formData.Add(new ByteArrayContent(image, 0, image.Length), "file", fileName);
                try
                {
                    IsBusy = true;
                    var status = await _serviceManager.RetrieveNumbers4mImage(formData);
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
                //catch (MatriInternetException exception)
                //{
                //    IsBusy = false;
                //    await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
                //}
                catch (Exception exception)
                {
                    IsBusy = false;
                    await Shell.Current.CurrentPage.DisplayAlert("Alert", exception.Message, "OK");
                }
            }

            await Shell.Current.CurrentPage.DisplayAlert("Alert", $"{successCount} Uploaded {failureCount} failed , Thank you", "Ok");
            ImageCollection.Clear();
            ImageSources.Clear();
        }
    }
}
