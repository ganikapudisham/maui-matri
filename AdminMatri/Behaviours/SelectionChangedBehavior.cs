using AdminMatri.ViewModel;
using CommunityToolkit.Maui.Core;
using Matri.Business;
using Matri.Model;
using Syncfusion.Maui.Inputs;

namespace AdminMatri.Behavior;

public class SelectionChangedBehavior : Behavior<SfComboBox>
{
    IServiceManager _serviceManager;
    private readonly IPopupService _popupService;

    public string Name { get; set; }

    public SelectionChangedBehavior(IServiceManager serviceManager, IPopupService popupService)
    {
        _serviceManager = serviceManager;
        _popupService = popupService;
    }

    public SelectionChangedBehavior()
    {
    }

    public Command Display { get; private set; }
    LeadsViewModel leadsViewModel;
    OCRViewModel ocrViewModel;
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
        if (Name == "Lead")
        {
            leadsViewModel = new LeadsViewModel(_serviceManager, _popupService);
            var selectedGroupName = new Master();

            if (e.CurrentSelection[0] != null)
            {
                selectedGroupName = e.CurrentSelection[0] as Master;
                leadsViewModel.SelectedGroupName = selectedGroupName;
            }
        }
        else if (Name == "OCR")
        {
            ocrViewModel = new OCRViewModel(_serviceManager);
            var selectedGroupName = new Master();

            if (e.CurrentSelection[0] != null)
            {
                selectedGroupName = e.CurrentSelection[0] as Master;
                ocrViewModel.SelectedGroupName = selectedGroupName;
            }
        }
    }
}
