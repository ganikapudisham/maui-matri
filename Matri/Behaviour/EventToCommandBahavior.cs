using Matri.Business;
using Matri.Business.Impl;
using Matri.Model;
using Matri.ViewModel;
using Syncfusion.Maui.Inputs;
using System.Reflection;
using System.Windows.Input;

namespace Matri.ComboBoxCommand
{
    public class EventToCommandBehavior : Behavior<SfComboBox>
    {
        IServiceManager _serviceManager;

        public EventToCommandBehavior(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public EventToCommandBehavior()
        {
        }
        public Command Display { get; private set; }
        SearchAdvancedViewModel searchAdvancedViewModel;
        protected override void OnAttachedTo(SfComboBox bindable)
        {
            bindable.SelectionChanged += Bindable_SelectionChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(SfComboBox bindable)
        {
            bindable.SelectionChanged -= Bindable_SelectionChanged;
            base.OnDetachingFrom(bindable);
        }

        async void Bindable_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
        {
            searchAdvancedViewModel = new SearchAdvancedViewModel(_serviceManager);
            var selectedReligion = new Master();

            if (e.CurrentSelection[0] != null)
            {
                selectedReligion = e.CurrentSelection[0] as Master;

                searchAdvancedViewModel.SelectedReligion = selectedReligion;

                if (selectedReligion.Name.ToLower() == "christian")
                {
                    searchAdvancedViewModel.ShowDenominations = true;
                }
                else
                {
                    searchAdvancedViewModel.ShowDenominations = false;
                }
            }
        }
    }
}
