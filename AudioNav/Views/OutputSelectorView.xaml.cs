using AudioNav.Models;
using AudioNav.ViewModels;
using Microsoft.Maui.Controls;
using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class OutputSelectorView : ReactiveContentView<OutputSelectorViewModel>
{
    public OutputSelectorView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.AvailableOutputs, v => v.OutputPickerView.ItemsSource).DisposeWith(disposables);
            OutputPickerView.ItemDisplayBinding = new Binding(nameof(IAudioCompassOutput.Name));
            this.Bind(ViewModel, vm => vm.Output, v => v.OutputPickerView.SelectedItem).DisposeWith(disposables);
        });
    }
}