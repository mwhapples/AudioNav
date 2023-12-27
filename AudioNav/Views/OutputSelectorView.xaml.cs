using AudioNav.Models;
using AudioNav.ViewModels;
using Microsoft.Maui.Controls;
using ReactiveUI;
using ReactiveUI.Maui;

namespace AudioNav.Views;

public partial class OutputSelectorView : ReactiveContentView<OutputSelectorViewModel>
{
	public OutputSelectorView()
	{
		InitializeComponent();
		this.WhenActivated(disposables =>
		{
			this.OneWayBind(ViewModel, vm => vm.AvailableOutputs, v => v.OutputPickerView.ItemsSource);
			OutputPickerView.ItemDisplayBinding = new Binding(nameof(IAudioCompassOutput.Name));
			this.Bind(ViewModel, vm => vm.Output, v => v.OutputPickerView.SelectedItem);
		});
	}
}