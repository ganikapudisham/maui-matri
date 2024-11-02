using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Helper;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditLifeStyleViewModel : ObservableObject
    {

        IServiceManager _serviceManager;
        //public INC<Profile> LoggedInUser = new NC<Profile>();
        public EditLifeStyleViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
            //_userDialogs = userDialog;

            //MDDrinks = new SmartObservableCollection<Master>();
            //MDSmokes = new SmartObservableCollection<Master>();
            //MDDiets = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedDiet = defaultMaster;
            //SelectedDrink = defaultMaster;
            //SelectedSmoke = defaultMaster;
        }

        //public override void Prepare(Profile profile)
        //{
        //    LoggedInUser.Value = profile;
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();
        //    var sessionToken = await SecureStorage.GetAsync("Token");
        //    try
        //    {
        //        var mdDiets = await _serviceManager.GetMasterData(sessionToken, "masterdata?type=diet");
        //        MDDiets.AddRange(mdDiets);
        //        var userDiet = mdDiets.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Diet.ToLower()).FirstOrDefault();
        //        SelectedDiet = userDiet;

        //        var mdSmokeTypes = await _serviceManager.GetMasterData(sessionToken, "masterdata?type=smoke");
        //        MDSmokes.AddRange(mdSmokeTypes);
        //        var userSmoke = mdSmokeTypes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Smoke.ToLower()).FirstOrDefault();
        //        SelectedSmoke = userSmoke;

        //        var mdDrinks = await _serviceManager.GetMasterData(sessionToken, "masterdata?type=drink");
        //        MDDrinks.AddRange(mdDrinks);
        //        var userDrink = mdDrinks.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Drink.ToLower()).FirstOrDefault();
        //        SelectedDrink = userDrink;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        public void CommandUpdate()
        {
            Task.Run(async () => { await UpdateAsync(); });
        }

        private async Task UpdateAsync()
        {
            //var lifeStyleDetails = new Profile
            //{
            //    Diet = SelectedDiet.Id,
            //    Smoke = SelectedSmoke.Id,
            //    Drink = SelectedDrink.Id,
            //};
            //try
            //{
            //    var sessionToken = await SecureStorage.GetAsync("Token");
            //    var status = await _serviceManager.UpdateLifeStyleDetails(sessionToken, lifeStyleDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Life Style Details Have Been Updated");
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

        //private SmartObservableCollection<Master> _MDDiets;
        //public SmartObservableCollection<Master> MDDiets
        //{
        //    get { return _MDDiets; }
        //    set
        //    {
        //        _MDDiets = value;
        //    }
        //}

        //private Master _selectedDiet;
        //public Master SelectedDiet
        //{
        //    get
        //    {
        //        return _selectedDiet;
        //    }
        //    set
        //    {
        //        _selectedDiet = value;
        //        RaisePropertyChanged("SelectedDiet");
        //    }
        //}

        //private SmartObservableCollection<Master> _MDSmokes;
        //public SmartObservableCollection<Master> MDSmokes
        //{
        //    get { return _MDSmokes; }
        //    set
        //    {
        //        _MDSmokes = value;
        //    }
        //}

        //private Master _selectedSmoke;
        //public Master SelectedSmoke
        //{
        //    get
        //    {
        //        return _selectedSmoke;
        //    }
        //    set
        //    {
        //        _selectedSmoke = value;
        //        RaisePropertyChanged("SelectedSmoke");
        //    }
        //}

        //private SmartObservableCollection<Master> _MDDrinks;
        //public SmartObservableCollection<Master> MDDrinks
        //{
        //    get { return _MDDrinks; }
        //    set
        //    {
        //        _MDDrinks = value;
        //    }
        //}

        //private Master _selectedDrink;
        //public Master SelectedDrink
        //{
        //    get
        //    {
        //        return _selectedDrink;
        //    }
        //    set
        //    {
        //        _selectedDrink = value;
        //        RaisePropertyChanged("SelectedDrink");
        //    }
        //}
    }
}

