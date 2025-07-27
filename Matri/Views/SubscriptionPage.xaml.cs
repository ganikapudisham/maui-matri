//using Microsoft.Maui.Controls.Shapes;
using Matri.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls; // for Rectangle
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;

namespace Matri.Views;

public partial class SubscriptionPage : ContentPage
{
    public SubscriptionPage(SubscriptionVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}