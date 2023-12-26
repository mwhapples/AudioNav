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
            this.SensorPickerView.ViewModel = new SensorSelectorViewModel(ViewModel.AudioCompass);
            this.OneWayBind(ViewModel, vm => vm.AudioOutput, v => v.AudioView.ViewModel);
            this.HeadingView.ViewModel = new SimpleHeadingViewModel(ViewModel.AudioCompass);
            this.CoursePicker.ViewModel = new SimpleCourseViewModel(ViewModel.AudioCompass);
            this.CourseToHeadingView.ViewModel = new CourseToHeadingViewModel(ViewModel.AudioCompass);
            this.OneWayBind(ViewModel, vm => vm.FilterRate, v => v.FilterRateView.ViewModel);
        });
    }
}
