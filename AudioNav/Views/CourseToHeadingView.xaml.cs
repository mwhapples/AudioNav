using AudioNav.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;
using System.Reactive.Disposables;

namespace AudioNav.Views;

public partial class CourseToHeadingView : ReactiveContentView<CourseToHeadingViewModel>
{
    public CourseToHeadingView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, vm => vm.ChangeCourseToCurrentHeadingCommand, v => v.CourseToHeadingButton.Command).DisposeWith(disposables);
        });
    }
}