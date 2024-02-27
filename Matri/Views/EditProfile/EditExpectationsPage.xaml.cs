using Matri.Model;
using Matri.ViewModel;
using System.Collections.ObjectModel;

namespace Matri.Views;

public partial class EditExpectationsPage : ContentPage
{
	public EditExpectationsPage()
	{
		InitializeComponent();

        //EditExpectationsViewModel mdAcademicsViewModel = (this.cmbAcademics.BindingContext as EditExpectationsViewModel);

        //ObservableCollection<Master> mdAcademicsList = mdAcademicsViewModel.MDAcademics;

        //var expectations = mdAcademicsViewModel.Profile.Expectations;

        //foreach(var edus in expectations.Educations)
        //{
        //    this.cmbAcademics.SelectedItems.Add(edus.Code);
        //}

       // this.cmbAcademics.SelectedItems.Add(mdAcademicsList[0]);
        //this.cmbAcademics.SelectedItems.Add(mdAcademicsList[1]);
        //this.cmbAcademics.SelectedItems.Add(mdAcademicsList[2]);
    }
}