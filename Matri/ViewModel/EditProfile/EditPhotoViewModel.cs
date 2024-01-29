using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditPhotoViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        //public INC<string> ImageSource = new NC<string>();
        public EditPhotoViewModel(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //public override void Prepare(Profile parameter)
        //{
        //}

        public void BrowsePhoto()
        {
            Task.Run(async () => { await BrowsePhotoAsync(); });
        }

        private async Task BrowsePhotoAsync()
        {
            //await CrossMedia.Current.Initialize();

            //Plugin.Media.Abstractions.MediaFile file = null;

            //var requestStorageRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            //if (requestStorageRead == PermissionStatus.Granted)
            //{
            //    file = await CrossMedia.Current.PickPhotoAsync();

            //    if (file == null)
            //        return;
            //    ImageSource.Value = file.Path;
            //}
            //else if (requestStorageRead == PermissionStatus.Denied)
            //{
            //    await _userDialogs.AlertAsync("Please allow app to access Storage");
            //    AppInfo.ShowSettingsUI();
            //}
        }


        public void UploadPhoto()
        {
            Task.Run(async () => { await UploadPhotoAsync(); });
        }

        private async Task UploadPhotoAsync()
        {
            //var sessionToken = await SecureStorage.GetAsync("Token");
            //var filePath = ImageSource.Value;
            //byte[] imageBytes = File.ReadAllBytes(ImageSource.Value);

            //byte[] image = File.ReadAllBytes(filePath);
            //var fileName = filePath.Split('/')[filePath.Split('/').Length - 1];

            //var formData = new MultipartFormDataContent();

            //formData.Add(new StringContent(sessionToken), "sessiontoken");
            //formData.Add(new ByteArrayContent(image, 0, image.Length), "file", fileName);
            //try
            //{
            //    var status = await _serviceManager.UploadProfilePhoto(formData);
            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Photo Was Uploaded, Thank you");
            //    }
            //}
            //catch (MatriInternetException exception)
            //{
            //    await _userDialogs.AlertAsync(exception.Message);
            //}
            //catch (Exception exception)
            //{
            //    var jsonResponse = exception.Message;
            //    var errorMessage = JsonConvert.DeserializeObject<JioMatriException>(jsonResponse);
            //    await _userDialogs.AlertAsync(errorMessage.Message);
            //}
        }
    }
}

