using AudioNav.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class SimpleCourseView : ReactiveContentView<SimpleCourseViewModel>
{
    public SimpleCourseView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.LargeDecrementButton.Command).DisposeWith(disposables);
            this.LargeDecrementButton.CommandParameter = -10;
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.DecrementButton.Command).DisposeWith(disposables);
            this.DecrementButton.CommandParameter = -1;
            this.OneWayBind(ViewModel, vm => vm.Course, v => v.CourseLabel.Text).DisposeWith(disposables);
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.IncrementButton.Command).DisposeWith(disposables);
            this.IncrementButton.CommandParameter = 1;
            this.OneWayBind(ViewModel, vm => vm.IncrementCourseCommand, v => v.LargeIncrementButton.Command).DisposeWith(disposables);
            this.LargeIncrementButton.CommandParameter = 10;
        });
    }
}