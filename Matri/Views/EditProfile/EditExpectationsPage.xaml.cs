using Matri.Model;
using Matri.ViewModel;
using System.Collections.ObjectModel;

namespace Matri.Views;

public partial class EditExpectationsPage : ContentPage
{
    public EditExpectationsPage()
    {
        InitializeComponent();

        var mdAcademicsViewModel = (this.cmbAcademics.BindingContext as EditExpectationsViewModel);

        var mdAcademics = mdAcademicsViewModel.MDAcademics;
        var mdComplexions = mdAcademicsViewModel.MDComplexions;
        var mdLanguages = mdAcademicsViewModel.MDLanguages;
        var mdMaritalStatuses = mdAcademicsViewModel.MDMaritalStatus;
        var mdPhysicalStatuses = mdAcademicsViewModel.MDPhysicalStatus;
        var mdSectors = mdAcademicsViewModel.MDSectors;

        var expectations = mdAcademicsViewModel.Profile.Expectations;

        foreach (var e in expectations.Educations)
        {
            var index = mdAcademics.ToList().FindIndex(i => i.Id == e.Code);
            var val = mdAcademics[index];
            this.cmbAcademics.SelectedItems.Add(mdAcademics[index]);
        }

        foreach (var j in expectations.JobTypes)
        {
            var index = mdSectors.ToList().FindIndex(i => i.Id == j.Code);
            this.cmbJobTypes.SelectedItems.Add(mdSectors[index]);
        }

        foreach (var m in expectations.MaritalStatus)
        {
            var index = mdMaritalStatuses.ToList().FindIndex(i => i.Id == m.Code);
            this.cmbMaritalStatuses.SelectedItems.Add(mdMaritalStatuses[index]);
        }

        foreach (var p in expectations.PhysicalStatuses)
        {
            var index = mdPhysicalStatuses.ToList().FindIndex(i => i.Id == p.Code);
            this.cmbPhysicalStatuses.SelectedItems.Add(mdPhysicalStatuses[index]);
        }

        foreach (var c in expectations.Complexions)
        {
            var index = mdComplexions.ToList().FindIndex(i => i.Id == c.Code);
            this.cmbComplexions.SelectedItems.Add(mdComplexions[index]);
        }

        foreach (var l in expectations.Languages)
        {
            var index = mdLanguages.ToList().FindIndex(i => i.Id == l.Code);
            this.cmbLanguages.SelectedItems.Add(mdLanguages[index]);
        }
    }
}