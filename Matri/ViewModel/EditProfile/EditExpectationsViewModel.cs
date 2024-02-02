using CommunityToolkit.Mvvm.ComponentModel;
using Matri.Business;
using Matri.Helper;
using Matri.Model;

namespace Matri.ViewModel
{
    public class EditExpectationsViewModel : ObservableObject
    {
        IServiceManager _serviceManager;
        public EditExpectationsViewModel()
        {
            _serviceManager = ServiceHelper.GetService<IServiceManager>();
        }

        //public override void Prepare(Profile parameter)
        //{
        //    //throw new NotImplementedException();
        //}
    }
}

