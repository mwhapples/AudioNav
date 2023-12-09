using AudioNav.ViewModels;
using Microsoft.Maui.Accessibility;
using Microsoft.Maui.Controls;
using ReactiveUI;
using ReactiveUI.Maui;
using System;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        ViewModel = new MainViewModel();
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, x => x.HeadingText, x => x.HeadingLabel.Text).DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.CompassFilterRate, v => v.CompassFilterRateSlider.Value).DisposeWith(disposables);
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.LargeDecrementButton.Command);
            this.LargeDecrementButton.CommandParameter = -10;
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.DecrementButton.Command);
            this.DecrementButton.CommandParameter = -1;
            this.OneWayBind(ViewModel, vm => vm.Course, v => v.CourseLabel.Text);
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.IncrementButton.Command);
            this.IncrementButton.CommandParameter = 1;
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.LargeIncrementButton.Command);
            this.LargeIncrementButton.CommandParameter = 10;
        });
    }
}
