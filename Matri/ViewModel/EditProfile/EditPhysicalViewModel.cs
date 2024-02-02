using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Helper;
using Matri.Model;
using Newtonsoft.Json;

namespace Matri.ViewModel
{
    public class EditPhysicalViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        //public INC<Profile> LoggedInUser = new NC<Profile>();
        public EditPhysicalViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();

            //MDHeights = new SmartObservableCollection<Master>();
            //MDWeights = new SmartObservableCollection<Master>();

            //MDPhysicalStatus = new SmartObservableCollection<Master>();
            //MDBodyTypes = new SmartObservableCollection<Master>();

            //MDComplexions = new SmartObservableCollection<Master>();
            //MDBloodGroups = new SmartObservableCollection<Master>();

            //MDProfileCreators = new SmartObservableCollection<Master>();

            //var defaultMaster = new Master();
            //defaultMaster.Id = "SELECT";
            //defaultMaster.Name = "SELECT";

            //SelectedHeight = defaultMaster;
            //SelectedWeight = defaultMaster;
            //SelectedComplexion = defaultMaster;
            //SelectedPhysicalStatus = defaultMaster;
            //SelectedBodyType = defaultMaster;
            //SelectedBloodGroup = defaultMaster;
            //SelectedCreatedBy = defaultMaster;
        }

        //private Master selectedHeight;
        //public Master SelectedHeight
        //{
        //    get
        //    {
        //        return selectedHeight;
        //    }
        //    set
        //    {
        //        selectedHeight = value;
        //        RaisePropertyChanged("SelectedHeight");
        //    }
        //}

        //private Master selectedWeight;
        //public Master SelectedWeight
        //{
        //    get
        //    {
        //        return selectedWeight;
        //    }
        //    set
        //    {
        //        selectedWeight = value;
        //        RaisePropertyChanged("SelectedWeight");
        //    }
        //}

        //private Master selectedPhysicalStatus;
        //public Master SelectedPhysicalStatus
        //{
        //    get
        //    {
        //        return selectedPhysicalStatus;
        //    }
        //    set
        //    {
        //        selectedPhysicalStatus = value;
        //        RaisePropertyChanged("SelectedPhysicalStatus");
        //    }
        //}

        //private Master selectedBodyType;
        //public Master SelectedBodyType
        //{
        //    get
        //    {
        //        return selectedBodyType;
        //    }
        //    set
        //    {
        //        selectedBodyType = value;
        //        RaisePropertyChanged("SelectedBodyType");
        //    }
        //}

        //private Master selectedComplexion;
        //public Master SelectedComplexion
        //{
        //    get
        //    {
        //        return selectedComplexion;
        //    }
        //    set
        //    {
        //        selectedComplexion = value;
        //        RaisePropertyChanged("SelectedComplexion");
        //    }
        //}

        //private Master selectedBloodGroup;
        //public Master SelectedBloodGroup
        //{
        //    get
        //    {
        //        return selectedBloodGroup;
        //    }
        //    set
        //    {
        //        selectedBloodGroup = value;
        //        RaisePropertyChanged("SelectedBloodGroup");
        //    }
        //}

        //private Master selectedCreatedBy;
        //public Master SelectedCreatedBy
        //{
        //    get
        //    {
        //        return selectedCreatedBy;
        //    }
        //    set
        //    {
        //        selectedCreatedBy = value;
        //        RaisePropertyChanged("SelectedCreatedBy");
        //    }
        //}

        //public override void Prepare(Profile profile)
        //{
        //    //LoggedInUser.Value = profile;

        //    //for (var i = 40; i <= 120; i++)
        //    //{
        //    //    MDWeights.Add(new Master { Id = i.ToString(), Name = i.ToString() });
        //    //}
        //    //SelectedWeight = MDWeights.Where(mdw => mdw.Name == profile.Weight.ToString()).FirstOrDefault();
        //}

        //public override async Task Initialize()
        //{
        //    await base.Initialize();
        //    var sessionToken = await SecureStorage.GetAsync("Token");
        //    try
        //    {
        //        var masterDataHeights = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=height");
        //        MDHeights.AddRange(masterDataHeights);
        //        var userHeight = masterDataHeights.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Height.ToLower()).FirstOrDefault();
        //        SelectedHeight = userHeight;

        //        var masterDataPhysical = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=physical");
        //        MDPhysicalStatus.AddRange(masterDataPhysical);
        //        var userPhysicalStatus = masterDataPhysical.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.PhysicalStatus.ToLower()).FirstOrDefault();
        //        SelectedPhysicalStatus = userPhysicalStatus;

        //        var masterDataBodyTypes = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=body");
        //        MDBodyTypes.AddRange(masterDataBodyTypes);
        //        var userBodyType = masterDataBodyTypes.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.BodyType.ToLower()).FirstOrDefault();
        //        SelectedBodyType = userBodyType;

        //        var masterDataComplexions = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=complexion");
        //        MDComplexions.AddRange(masterDataComplexions);
        //        var userComplexion = masterDataComplexions.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.Complexion.ToLower()).FirstOrDefault();
        //        SelectedComplexion = userComplexion;

        //        var masterDataBloodGroups = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=blood");
        //        MDBloodGroups.AddRange(masterDataBloodGroups);
        //        var userBloodGroup = masterDataBloodGroups.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.BloodGroup.ToLower()).FirstOrDefault();
        //        SelectedBloodGroup = userBloodGroup;

        //        var masterDataProfileCreators = await _serviceManager.GetMasterData(new Guid(sessionToken), "masterdata?type=profilecreatedby");
        //        MDProfileCreators.AddRange(masterDataProfileCreators);
        //        var userProfileCreatedBy = masterDataProfileCreators.Where(mt => mt.Id.ToLower() == LoggedInUser.Value.ProfileCreatedBy.ToLower()).FirstOrDefault();
        //        SelectedCreatedBy = userProfileCreatedBy;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        //private SmartObservableCollection<Master> mdHeights;
        //public SmartObservableCollection<Master> MDHeights
        //{
        //    get { return mdHeights; }
        //    set
        //    {
        //        mdHeights = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdWeights;
        //public SmartObservableCollection<Master> MDWeights
        //{
        //    get { return mdWeights; }
        //    set
        //    {
        //        mdWeights = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdPhysicalStatus;
        //public SmartObservableCollection<Master> MDPhysicalStatus
        //{
        //    get { return mdPhysicalStatus; }
        //    set
        //    {
        //        mdPhysicalStatus = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdBodyTypes;
        //public SmartObservableCollection<Master> MDBodyTypes
        //{
        //    get { return mdBodyTypes; }
        //    set
        //    {
        //        mdBodyTypes = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdComplexions;
        //public SmartObservableCollection<Master> MDComplexions
        //{
        //    get { return mdComplexions; }
        //    set
        //    {
        //        mdComplexions = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdBloodGroups;
        //public SmartObservableCollection<Master> MDBloodGroups
        //{
        //    get { return mdBloodGroups; }
        //    set
        //    {
        //        mdBloodGroups = value;
        //    }
        //}

        //private SmartObservableCollection<Master> mdProfileCreators;
        //public SmartObservableCollection<Master> MDProfileCreators
        //{
        //    get { return mdProfileCreators; }
        //    set
        //    {
        //        mdProfileCreators = value;
        //    }
        //}

        public void CommandUpdate()
        {
            Task.Run(async () => { await UpdateAsync(); });
        }

        private async Task UpdateAsync()
        {
            //if (SelectedHeight == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Height");
            //    return;
            //}

            //if (SelectedPhysicalStatus == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Physical Status");
            //    return;
            //}

            //if (SelectedComplexion == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Complexion");
            //    return;
            //}

            //if (SelectedBodyType == null)
            //{
            //    await _userDialogs.AlertAsync("Please Specify Body Type");
            //    return;
            //}

            //var sessionToken = await SecureStorage.GetAsync("Token");

            //var physicalDetails = new Profile
            //{
            //    Height = SelectedHeight.Id,
            //    Weight = Convert.ToInt16(SelectedWeight.Id),
            //    PhysicalStatus = SelectedPhysicalStatus.Id,
            //    BodyType = SelectedBodyType.Id,
            //    Complexion = SelectedComplexion.Id,
            //    BloodGroup = SelectedBloodGroup.Id,
            //    ProfileCreatedBy = SelectedCreatedBy.Id
            //};
            //try
            //{
            //    var status = await _serviceManager.UpdatePhysicalDetails(new Guid(sessionToken), physicalDetails);

            //    if (status)
            //    {
            //        await _userDialogs.AlertAsync("Physical Details Have Been Updated");
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

