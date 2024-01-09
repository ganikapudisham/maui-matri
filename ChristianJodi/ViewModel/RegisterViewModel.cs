using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristianJodi.ViewModel
{
    public partial class RegisterViewModel :ObservableObject
    {
        public RegisterViewModel()
        {

        }

        [RelayCommand]
        public async Task Cancel()
        {
            await Shell.Current.GoToAsync("///LandingPage");
        }

        //[RelayCommand]
        //public async Task Register()
        //{

        //}
    }
}
