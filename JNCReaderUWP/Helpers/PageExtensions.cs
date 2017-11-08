using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

public static class PageExtensions
{
    public static void SetUpTransitions(this Page page)
    {
        TransitionCollection collection = new TransitionCollection();
        NavigationThemeTransition theme = new NavigationThemeTransition();

        var info = new ContinuumNavigationTransitionInfo();

        theme.DefaultNavigationTransitionInfo = info;
        collection.Add(theme);
        page.Transitions = collection;
    }
}