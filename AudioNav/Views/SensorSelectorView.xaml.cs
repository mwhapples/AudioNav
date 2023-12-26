using AudioNav.Models;
using AudioNav.ViewModels;
using Microsoft.Maui.Controls;
using ReactiveUI;
using ReactiveUI.Maui;

namespace AudioNav.Views;

public partial class SensorSelectorView : ReactiveContentView<SensorSelectorViewModel>
{
    public SensorSelectorView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.CompassProviders, v => v.SensorPicker.ItemsSource);
            SensorPicker.ItemDisplayBinding = new Binding(nameof(ICompassProvider.Name));
            this.Bind(ViewModel, vm => vm.CompassProvider, v => v.SensorPicker.SelectedItem);
        });
    }
}