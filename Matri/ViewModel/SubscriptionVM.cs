using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri.ViewModel;

public partial class SubscriptionVM : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    public SubscriptionVM()
    {
    }

    [RelayCommand]
    public async Task Tap()
    {
    }
}
