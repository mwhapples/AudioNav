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
        
    }
}
