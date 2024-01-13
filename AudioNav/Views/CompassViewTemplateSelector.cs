using AudioNav.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AudioNav.Views;

internal class CompassViewTemplateSelector : DataTemplateSelector
{
    private readonly IImmutableDictionary<Type, DataTemplate?> _templateDictionary = new Dictionary<Type, DataTemplate?>
    {
        [typeof(CourseToHeadingViewModel)] = new DataTemplate(typeof(CourseToHeadingView)),
        [typeof(FilterRateViewModel)] = new DataTemplate(typeof(FilterRateView)),
        [typeof(OutputSelectorViewModel)] = new DataTemplate(typeof(OutputSelectorView)),
        [typeof(SensorSelectorViewModel)] = new DataTemplate(typeof(SensorSelectorView)),
        [typeof(SimpleCourseViewModel)] = new DataTemplate(typeof(SimpleCourseView)),
        [typeof(SimpleHeadingViewModel)] = new DataTemplate(typeof(SimpleHeadingView))
    }.ToImmutableDictionary();
    public DataTemplate? UnknownTemplate { get; set; }
    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        return _templateDictionary.GetValueOrDefault(item.GetType(), UnknownTemplate);
    }
}
