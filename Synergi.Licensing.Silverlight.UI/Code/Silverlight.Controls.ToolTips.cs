//// These three tooltip classes were originally taken from http://tooltipservice.codeplex.com
//// The 2.1.1 version (and previous versions) has a memory leak when the ToolTipService 
//// stores references to the ToolTip and Container object in a dictionary called elementsAndToolTips.
//// We now register the unloaded event and clear out the container when appropriate.

///// EJM: Xavier states that he has incorporated the fix into the source.
///// Now we are just waiting for a release beyond changeset 57652. 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Controls;

namespace Silverlight.Controls.ToolTips
{
    /// <summary>
    /// Represents a control that creates a pop-up window that displays information for an element in the UI.
    /// </summary>
    public class ToolTip : System.Windows.Controls.ToolTip
    {
        ///<summary>
        /// This event is raised when the OpenAnimation storyboard has been started by the ToolTipService.
        ///</summary>
        public event EventHandler OpenAnimationStarted;

        ///<summary>
        /// This event is raised when the CloseAnimation storyboard has been started by the ToolTipService.
        ///</summary>
        public event EventHandler CloseAnimationStarted;

        private const string errorMessageNotAToolTipObject = "You can only set {0} on a ToolTip object.";

        /// <summary>
        /// Identifies the ToolTip.DisplayTime dependency property.
        /// </summary>
        /// <remarks>Default value is 5 seconds.</remarks>
        public static readonly DependencyProperty DisplayTimeProperty
            = DependencyProperty.RegisterAttached("DisplayTime", typeof(Duration), typeof(ToolTip),
                                                  new PropertyMetadata(new Duration(TimeSpan.FromSeconds(5)), OnDisplayTimePropertyChanged));

        /// <summary>
        /// Identifies the ToolTip.InitialDelay dependency property.
        /// </summary>
        /// <remarks>Default value is 1 second.</remarks>
        public static readonly DependencyProperty InitialDelayProperty
            = DependencyProperty.RegisterAttached("InitialDelay", typeof(Duration), typeof(ToolTip),
                                                  new PropertyMetadata(new Duration(TimeSpan.FromSeconds(1)), OnInitialDelayPropertyChanged));
        /// <summary>
        /// Identifies the ToolTip.CloseAnimation dependency property.
        /// </summary>
        /// <remarks>Default value is null.</remarks>
        public static readonly DependencyProperty CloseAnimationProperty
            = DependencyProperty.RegisterAttached("CloseAnimation", typeof(Storyboard), typeof(ToolTip),
                                                  new PropertyMetadata(null, OnCloseAnimationPropertyChanged));

        /// <summary>
        /// Identifies the ToolTip.OpenAnimation dependency property.
        /// </summary>
        /// <remarks>Default value is null.</remarks>
        public static readonly DependencyProperty OpenAnimationProperty
            = DependencyProperty.RegisterAttached("OpenAnimation", typeof(Storyboard), typeof(ToolTip),
                                                  new PropertyMetadata(null, OnOpenAnimationPropertyChanged));

        internal ToolTipTimer Timer { get; private set; }

        private static void OnCloseAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ToolTip))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, errorMessageNotAToolTipObject, "ToolTip.CloseAnimationProperty"));
            }
        }
        private static void OnOpenAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ToolTip))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, errorMessageNotAToolTipObject, "ToolTip.OpenAnimationProperty"));
            }
        }
        private static void OnInitialDelayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolTip = (ToolTip)d;

            if (toolTip == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, errorMessageNotAToolTipObject, "ToolTip.DisplayTimeProperty"));
            }

            UpdateToolTipTimer(toolTip);
        }
        private static void OnDisplayTimePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var toolTip = (ToolTip)d;

            if (toolTip == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, errorMessageNotAToolTipObject, "ToolTip.DisplayTimeProperty"));
            }

            UpdateToolTipTimer(toolTip);
        }

        private static void UpdateToolTipTimer(ToolTip toolTip)
        {
            toolTip.SetToolTipTimer();
        }

        internal void SetToolTipTimer()
        {
            // substract the duration of the close animation from the display time,
            // to match with the value set by the user in the ToolTip.DisplayTime property
            // var tooltipDisplayDuration = DisplayTime;
            // if (CloseAnimation != null)
            //     tooltipDisplayDuration = tooltipDisplayDuration.Subtract(CloseAnimation.Duration);

            var maximumTime = !DisplayTime.HasTimeSpan ? TimeSpan.MaxValue : DisplayTime.TimeSpan;
            var timer = new ToolTipTimer(maximumTime, InitialDelay.TimeSpan);
            if (Timer != null)
            {
                // clean up old instance
                Timer.StopAndReset();
                timer.Tick -= ToolTipService.OnTimerTick;
                timer.Stopped -= ToolTipService.OnTimerStopped;
            }
            timer.Stopped += ToolTipService.OnTimerStopped;
            timer.Tick += ToolTipService.OnTimerTick;
            Timer = timer;
        }

        internal void InvokeOpenAnimationStarted(EventArgs e)
        {
            if (OpenAnimationStarted != null)
                OpenAnimationStarted(this, e);
        }

        internal void InvokeCloseAnimationStarted(EventArgs e)
        {
            if (CloseAnimationStarted != null)
                CloseAnimationStarted(this, e);
        }

        /// <summary>
        /// Gets or sets the display time of this ToolTip instance in seconds.
        /// </summary>
        /// <remarks>
        /// The default value is 5 seconds.
        /// </remarks>
        public Duration DisplayTime
        {
            get { return (Duration)GetValue(DisplayTimeProperty); }
            set { SetValue(DisplayTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Storyboard to execute when closing the ToolTip.
        /// </summary>
        public Storyboard CloseAnimation
        {
            get { return (Storyboard)GetValue(CloseAnimationProperty); }
            set { SetValue(CloseAnimationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Storyboard to execute when opening the ToolTip.
        /// </summary>
        public Storyboard OpenAnimation
        {
            get { return (Storyboard)GetValue(OpenAnimationProperty); }
            set { SetValue(OpenAnimationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the initial delay for the tooltip to show in seconds.
        /// </summary>
        /// <remarks>
        /// The default value is 1 second.
        /// </remarks>
        public Duration InitialDelay
        {
            get { return (Duration)GetValue(InitialDelayProperty); }
            set { SetValue(InitialDelayProperty, value); }
        }

    }

    /// <summary>
    /// Represents a service that provides static methods to display a tooltip.
    /// </summary>
    public static class ToolTipService
    {
        private static Dictionary<DependencyObject, ToolTip> elementsAndToolTips = new Dictionary<DependencyObject, ToolTip>();

        private static UIElement currentElement;
        private static FrameworkElement rootVisual;
        private static Size lastSize;
        private static readonly object locker = new object();
        private static bool isCloseAnimationInProgress;
        private static bool isOpenAnimationInProgress;

        #region Attached Dependency Properties

        ///<summary>
        /// An attached dependency property for the Placement of a ToolTip.
        ///</summary>
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.RegisterAttached("Placement", typeof(PlacementMode), typeof(ToolTipService), new PropertyMetadata(PlacementMode.Mouse));

        ///<summary>
        /// An attached DependencyProperty for the PlacementTarget of a ToolTip.
        ///</summary>
        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.RegisterAttached("PlacementTarget", typeof(UIElement), typeof(ToolTipService), new PropertyMetadata(null));

        #region DataContext Dependency Property

        /// <summary>
        /// Hidden dependency property that enables us to receive notifications when the source data context changes and 
        /// needs to be flushed into the context of the tooltip
        /// </summary>
        private static readonly DependencyProperty dataContextProperty =
            DependencyProperty.RegisterAttached("DataContext", typeof(object), typeof(ToolTipService), new PropertyMetadata(new PropertyChangedCallback(OnDataContextChanged)));

        /// <summary>
        /// When parent datacontext changes assign tooltip's datacontext to new datacontext
        /// </summary>
        public static void OnDataContextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var owner = sender as FrameworkElement;
            var toolTip = GetToolTip(owner);

            // make sure all non-relevant tooltips are closed!
            foreach (var kvp in elementsAndToolTips)
            {
                if (!(ReferenceEquals(kvp.Value, toolTip)))
                    kvp.Value.IsOpen = false;
            }

            Debug.Assert(!(ReferenceEquals(null, owner) ||
                           ReferenceEquals(null, toolTip)), "Unexpected null reference to attached FrameworkElement");

            toolTip.DataContext = owner.DataContext;
        }

        #endregion DataContext Dependency Property

        #region ToolTip Dependency Property

        /// <summary>
        /// Identifies the ToolTipService.ToolTip dependency property.
        /// </summary>        
        public static readonly DependencyProperty ToolTipProperty
            = DependencyProperty.RegisterAttached("ToolTip", typeof(object), typeof(ToolTipService),
            new PropertyMetadata(new PropertyChangedCallback(OnToolTipPropertyChanged)));

        private static void OnToolTipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var owner = (FrameworkElement)d;
            var newValue = (FrameworkElement)e.NewValue;

            if (e.OldValue != null)
            {
                UnregisterToolTip(owner);
            }
            if (newValue != null)
            {
                RegisterToolTip(owner, newValue);
            }
        }

        /// <summary>
        /// Gets the tooltip for an object.
        /// </summary>
        /// <param name="element">The UIElement from which the property value is read.</param>
        public static ToolTip GetToolTip(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (ToolTip)element.GetValue(ToolTipProperty);
        }

        /// <summary>
        /// Sets the tooltip for an object.
        /// </summary>
        /// <param name="element">The UIElement to which the attached property is written.</param>
        /// <param name="value">The value to set.</param>
        public static void SetToolTip(DependencyObject element, ToolTip value)
        {
            SetToolTipInternal(element, value);
        }

        /// <summary>
        /// Clear all tooltips
        /// </summary>
        public static void ClearAllToolTip()
        {
            elementsAndToolTips.Clear();            
        }
        #endregion ToolTip Depdendency Property

        #region ToolTipObject Dependency Property

        internal static readonly DependencyProperty ToolTipObjectProperty =
            DependencyProperty.RegisterAttached("ToolTipObject", typeof(object), typeof(ToolTipService), null);

        #endregion ToolTipObject Depdendency Property

        #endregion Attached Dependency Properties

        internal static Point MousePosition { get; set; }

        internal static FrameworkElement RootVisual
        {
            get
            {
                SetRootVisual();
                return rootVisual;
            }
        }

        internal static ToolTip CurrentToolTip { get; private set; }

        private static void OnElementIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (elementsAndToolTips.ContainsKey((UIElement)sender))
            {
                var toolTipTimer = elementsAndToolTips[(UIElement)sender].Timer;
                if (!(bool)e.NewValue && toolTipTimer.IsEnabled)
                {
                    toolTipTimer.StopAndReset();
                }
            }
        }

        internal static void OnTimerStopped(object sender, EventArgs e)
        {
            if (CurrentToolTip == null)
                return;

            lock (locker)
            {
                if (CurrentToolTip.CloseAnimation != null)
                {
                    try
                    {
                        isCloseAnimationInProgress = true;
                        CurrentToolTip.CloseAnimation.Begin();
                        CurrentToolTip.InvokeCloseAnimationStarted(EventArgs.Empty);
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "An exception of type {0} occured with the following message:\n{1}\nStacktrace:\n{2}",
                            invalidOperationException.GetType().Name, invalidOperationException.Message, invalidOperationException.StackTrace));
                    }
                }
                else CurrentToolTip.IsOpen = false;
            }
        }

        private static void OnElementMouseEnter(object sender, MouseEventArgs e)
        {
            MousePosition = e.GetPosition(null);
            lock (locker)
            {
                currentElement = (UIElement)sender;
                if (currentElement != null && elementsAndToolTips.Count > 0 && elementsAndToolTips.ContainsKey(currentElement))
                {
                    CurrentToolTip = elementsAndToolTips[currentElement];

                    SetRootVisual();

                    // do not trigger tooltips when there is no content defined for the tooltip
                    if (CurrentToolTip.Content == null) return;

                    if (CurrentToolTip.InitialDelay.TimeSpan.Ticks == 0 && CurrentToolTip.OpenAnimation == null)
                    {
                        CurrentToolTip.IsOpen = true;
                    }
                    else if (CurrentToolTip.InitialDelay.TimeSpan.Ticks == 0 && CurrentToolTip.OpenAnimation != null)
                    {
                        StartOpenAnimation();
                    }

                    if (isCloseAnimationInProgress && CurrentToolTip.CloseAnimation != null)
                    {
                        CurrentToolTip.CloseAnimation.Stop();
                    }

                    if (CurrentToolTip.Timer == null)
                    {
                        CurrentToolTip.SetToolTipTimer();
                    }

                    CurrentToolTip.Timer.StartAndReset();
                }
            }

        }

        internal static void OnTimerTick(object sender, EventArgs e)
        {
            if (CurrentToolTip.IsOpen) return;

            var tooltipTimer = (ToolTipTimer)sender;
            if (tooltipTimer.IsEnabled && CurrentToolTip.InitialDelay.TimeSpan.TotalMilliseconds <= tooltipTimer.CurrentTick)
            {
                StartOpenAnimation();
            }
        }

        private static void StartOpenAnimation()
        {
            if (CurrentToolTip.DisplayTime.HasTimeSpan && CurrentToolTip.DisplayTime.TimeSpan.TotalSeconds == 0)
                return;

            CurrentToolTip.IsOpen = true;
            if (CurrentToolTip.OpenAnimation != null)
            {
                isOpenAnimationInProgress = true;
                CurrentToolTip.OpenAnimation.Begin();
                CurrentToolTip.InvokeOpenAnimationStarted(EventArgs.Empty);
            }
        }

        private static void OnElementMouseLeave(object sender, MouseEventArgs e)
        {            
            var frameworkElement = (FrameworkElement)sender;
            if (frameworkElement != null && elementsAndToolTips.Count > 0 && elementsAndToolTips.ContainsKey(frameworkElement))
            {
                var tooltip = elementsAndToolTips[frameworkElement];

                // if there is no content defined for the tooltip, nothing happened
                if (tooltip.Content == null) return;

                var toolTipTimer = tooltip.Timer;
                if (toolTipTimer.IsEnabled)
                {
                    toolTipTimer.StopAndReset();
                }
                lock (locker)
                {
                    if (GetToolTip(frameworkElement) != CurrentToolTip)
                    {
                        return;
                    }

                    if (isOpenAnimationInProgress && CurrentToolTip.OpenAnimation != null)
                    {
                        CurrentToolTip.OpenAnimation.Stop();
                    }

                    if (!isCloseAnimationInProgress)
                        CurrentToolTip.IsOpen = false;
                }
            }
        }

        private static void OnRootMouseMove(object sender, MouseEventArgs e)
        {
            MousePosition = e.GetPosition(null);
        }

        private static void OnRootVisualSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (CurrentToolTip == null)
            {
                return;
            }
            if (CurrentToolTip.Parent == null)
            {
                return;
            }

            PerformPlacement(CurrentToolTip.HorizontalOffset, CurrentToolTip.VerticalOffset);
        }

        private static void OnToolTipSizeChanged(object sender, SizeChangedEventArgs e)
        {
            lastSize = e.NewSize;
            if (CurrentToolTip.Parent != null)
            {
                PerformPlacement(CurrentToolTip.HorizontalOffset, CurrentToolTip.VerticalOffset);
            }
        }

        private static ToolTip ConvertToToolTip(object obj)
        {
            var toolTip = obj as ToolTip;
            if (toolTip == null)
            {
                var element = obj as FrameworkElement;
                if ((element != null) && ((toolTip = element.Parent as ToolTip) != null))
                {
                    return toolTip;
                }
                toolTip = new ToolTip { Content = obj };
            }
            return toolTip;
        }

        private static void PerformPlacement(double horizontalOffset, double verticalOffset)
        {
            //calculate position based on browser zoom factor
            var browserZoomFactor = Application.Current.Host.Content.ZoomFactor;

            var placementMode = CurrentToolTip.Placement;
            var placementTarget = CurrentToolTip.PlacementTarget;

            var parentPopup = (Popup)CurrentToolTip.Parent;

            switch (placementMode)
            {
                case PlacementMode.Mouse:
                    var offsetX = (MousePosition.X + horizontalOffset) * browserZoomFactor;
                    var offsetY = (MousePosition.Y + new TextBlock().FontSize + verticalOffset) * browserZoomFactor;

                    offsetX = Math.Max(2.0, offsetX);
                    offsetY = Math.Max(2.0, offsetY);

                    //get actual and previous dimensions
                    var actualHeight = RootVisual.ActualHeight * browserZoomFactor;
                    var actualWidth = RootVisual.ActualWidth * browserZoomFactor;
                    var lastHeight = lastSize.Height;
                    var lastWidth = lastSize.Width;

                    var lastRectangle = new Rect(offsetX, offsetY, lastWidth, lastHeight);
                    var actualRectangle = new Rect(0.0, 0.0, actualWidth, actualHeight);
                    actualRectangle.Intersect(lastRectangle);

                    if ((Math.Abs(actualRectangle.Width - lastRectangle.Width) < 2.0) && (Math.Abs(actualRectangle.Height - lastRectangle.Height) < 2.0))
                    {
                        parentPopup.VerticalOffset = offsetY;
                        parentPopup.HorizontalOffset = offsetX;
                    }
                    else
                    {
                        if ((offsetY + lastRectangle.Height) > actualHeight)
                        {
                            offsetY = (actualHeight - lastRectangle.Height) - 2.0;
                        }
                        if (offsetY < 0.0)
                        {
                            offsetY = 0.0;
                        }
                        if ((offsetX + lastRectangle.Width) > actualWidth)
                        {
                            offsetX = (actualWidth - lastRectangle.Width) - 2.0;
                        }
                        if (offsetX < 0.0)
                        {
                            offsetX = 0.0;
                        }
                        parentPopup.VerticalOffset = offsetY;
                        parentPopup.HorizontalOffset = offsetX;

                        var clippingHeight = ((offsetY + lastRectangle.Height) + 2.0) - actualHeight;
                        var clippingWidth = ((offsetX + lastRectangle.Width) + 2.0) - actualWidth;
                        if ((clippingWidth >= 2.0) || (clippingHeight >= 2.0))
                        {
                            clippingWidth = Math.Max(0.0, clippingWidth);
                            clippingHeight = Math.Max(0.0, clippingHeight);
                            PerformClipping(new Size(lastRectangle.Width - clippingWidth, lastRectangle.Height - clippingHeight));
                        }
                    }
                    break;
                case PlacementMode.Bottom:
                case PlacementMode.Right:
                case PlacementMode.Left:
                case PlacementMode.Top:
                    var plugin = new Rect(0.0, 0.0, Application.Current.Host.Content.ActualWidth, Application.Current.Host.Content.ActualHeight);
                    var translatedPoints = GetTranslatedPoints((FrameworkElement)placementTarget);
                    var toolTip = GetTranslatedPoints((FrameworkElement)parentPopup.Child);
                    Point popupLocation = PlacePopup(plugin, translatedPoints, toolTip, placementMode);

                    parentPopup.VerticalOffset = popupLocation.Y + verticalOffset;
                    parentPopup.HorizontalOffset = popupLocation.X + horizontalOffset;
                    break;
            }


        }

        private static Point[] GetTranslatedPoints(FrameworkElement frameworkElement)
        {
            var pointArray = new Point[4];
            if (frameworkElement != null)
            {
                var generalTransform = frameworkElement.TransformToVisual(null);
                pointArray[0] = generalTransform.Transform(new Point(0.0, 0.0));
                pointArray[1] = generalTransform.Transform(new Point(frameworkElement.ActualWidth, 0.0));
                pointArray[1].X--;
                pointArray[2] = generalTransform.Transform(new Point(0.0, frameworkElement.ActualHeight));
                pointArray[2].Y--;
                pointArray[3] = generalTransform.Transform(new Point(frameworkElement.ActualWidth, frameworkElement.ActualHeight));
                pointArray[3].X--;
                pointArray[3].Y--;
            }
            return pointArray;
        }

        private static Point PlacePopup(Rect plugin, Point[] target, Point[] toolTip, PlacementMode placement)
        {
            var bounds = GetBounds(target);
            var rect2 = GetBounds(toolTip);
            var width = rect2.Width;
            var height = rect2.Height;

            placement = ValidatePlacement(target, placement, plugin, width, height);

            var pointArray = GetPointArray(target, placement, plugin, width, height);
            var index = GetIndex(plugin, width, height, pointArray);
            var point = CalculatePoint(target, placement, plugin, width, height, pointArray, index, bounds);

            return point;
        }

        private static int GetIndex(Rect plugin, double width, double height, IList<Point> pointArray)
        {
            var num13 = width * height;
            var index = 0;
            var num15 = 0.0;
            for (var i = 0; i < pointArray.Count; i++)
            {
                var rect3 = new Rect(pointArray[i].X, pointArray[i].Y, width, height);
                rect3.Intersect(plugin);
                var d = rect3.Width * rect3.Height;
                if (double.IsInfinity(d))
                {
                    index = pointArray.Count - 1;
                    break;
                }
                if (d > num15)
                {
                    index = i;
                    num15 = d;
                }
                if (d == num13)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private static Point CalculatePoint(IList<Point> target, PlacementMode placement, Rect plugin, double width, double height, IList<Point> pointArray, int index, Rect bounds)
        {
            var x = pointArray[index].X;
            var y = pointArray[index].Y;
            if (index > 1)
            {
                if ((placement == PlacementMode.Left) || (placement == PlacementMode.Right))
                {
                    if (((y != target[0].Y) && (y != target[1].Y)) && (((y + height) != target[0].Y) && ((y + height) != target[1].Y)))
                    {
                        var num18 = bounds.Top + (bounds.Height / 2.0);
                        if ((num18 > 0.0) && ((num18 - 0.0) > (plugin.Height - num18)))
                        {
                            y = plugin.Height - height;
                        }
                        else
                        {
                            y = 0.0;
                        }
                    }
                }
                else if (((placement == PlacementMode.Top) || (placement == PlacementMode.Bottom)) && (((x != target[0].X) && (x != target[1].X)) && (((x + width) != target[0].X) && ((x + width) != target[1].X))))
                {
                    var num19 = bounds.Left + (bounds.Width / 2.0);
                    if ((num19 > 0.0) && ((num19 - 0.0) > (plugin.Width - num19)))
                    {
                        x = plugin.Width - width;
                    }
                    else x = 0.0;
                }
            }
            return new Point(x, y);
        }

        private static Point[] GetPointArray(IList<Point> target, PlacementMode placement, Rect plugin, double width, double height)
        {
            Point[] pointArray;
            switch (placement)
            {
                case PlacementMode.Bottom:
                    pointArray = new[] { new Point(target[2].X, Math.Max(0.0, target[2].Y + 1.0)), new Point((target[3].X - width) + 1.0, Math.Max(0.0, target[2].Y + 1.0)), new Point(0.0, Math.Max(0.0, target[2].Y + 1.0)) };
                    break;

                case PlacementMode.Right:
                    pointArray = new[] { new Point(Math.Max(0.0, target[1].X + 1.0), target[1].Y), new Point(Math.Max(0.0, target[3].X + 1.0), (target[3].Y - height) + 1.0), new Point(Math.Max(0.0, target[1].X + 1.0), 0.0) };
                    break;

                case PlacementMode.Left:
                    pointArray = new[] { new Point(Math.Min(plugin.Width, target[0].X) - width, target[1].Y), new Point(Math.Min(plugin.Width, target[2].X) - width, (target[3].Y - height) + 1.0), new Point(Math.Min(plugin.Width, target[0].X) - width, 0.0) };
                    break;

                case PlacementMode.Top:
                    pointArray = new[] { new Point(target[0].X, Math.Min(target[0].Y, plugin.Height) - height), new Point((target[1].X - width) + 1.0, Math.Min(target[0].Y, plugin.Height) - height), new Point(0.0, Math.Min(target[0].Y, plugin.Height) - height) };
                    break;

                default:
                    pointArray = new[] { new Point(0.0, 0.0) };
                    break;
            }
            return pointArray;
        }

        private static PlacementMode ValidatePlacement(IList<Point> target, PlacementMode placement, Rect plugin, double width, double height)
        {
            switch (placement)
            {
                case PlacementMode.Right:
                    var num5 = Math.Max(0.0, target[0].X - 1.0);
                    var num6 = plugin.Width - Math.Min(plugin.Width, target[1].X + 1.0);
                    if ((num6 < width) && (num6 < num5))
                    {
                        placement = PlacementMode.Left;
                    }
                    break;
                case PlacementMode.Left:
                    var num7 = Math.Min(plugin.Width, target[1].X + width) - target[1].X;
                    var num8 = target[0].X - Math.Max(0.0, target[0].X - width);
                    if ((num8 < width) && (num8 < num7))
                    {
                        placement = PlacementMode.Right;
                    }
                    break;
                case PlacementMode.Top:
                    var num9 = target[0].Y - Math.Max(0.0, target[0].Y - height);
                    var num10 = Math.Min(plugin.Height, plugin.Height - height) - target[2].Y;
                    if ((num9 < height) && (num9 < num10))
                    {
                        placement = PlacementMode.Bottom;
                    }
                    break;
                case PlacementMode.Bottom:
                    var num11 = Math.Max(0.0, target[0].Y);
                    var num12 = plugin.Height - Math.Min(plugin.Height, target[2].Y);
                    if ((num12 < height) && (num12 < num11))
                    {
                        placement = PlacementMode.Top;
                    }
                    break;
            }
            return placement;
        }

        private static Rect GetBounds(params Point[] interestPoints)
        {
            double num2;
            double num4;
            var x = num2 = interestPoints[0].X;
            var y = num4 = interestPoints[0].Y;
            for (var i = 1; i < interestPoints.Length; i++)
            {
                var num6 = interestPoints[i].X;
                var num7 = interestPoints[i].Y;
                if (num6 < x)
                {
                    x = num6;
                }
                if (num6 > num2)
                {
                    num2 = num6;
                }
                if (num7 < y)
                {
                    y = num7;
                }
                if (num7 > num4)
                {
                    num4 = num7;
                }
            }
            return new Rect(x, y, (num2 - x) + 1.0, (num4 - y) + 1.0);
        }

        private static void PerformClipping(Size size)
        {
            var child = VisualTreeHelper.GetChild(CurrentToolTip, 0) as Border;
            if (child == null)
            {
                return;
            }

            if (size.Width < child.ActualWidth)
            {
                child.Width = size.Width;
            }
            if (size.Height < child.ActualHeight)
            {
                child.Height = size.Height;
            }
        }        

        private static void UnregisterToolTip(UIElement owner)
        {
            if (owner.GetValue(ToolTipObjectProperty) == null)
            {
                return;
            }

            if (owner is FrameworkElement)
            {
                ((FrameworkElement)owner).Unloaded -= FrameworkElementUnloaded;
            }
            owner.MouseEnter -= OnElementMouseEnter;
            owner.MouseLeave -= OnElementMouseLeave;

            var toolTip = (ToolTip)owner.GetValue(ToolTipObjectProperty);
            if (toolTip.IsOpen)
            {
                toolTip.IsOpen = false;
            }
            owner.ClearValue(ToolTipObjectProperty);

            if (elementsAndToolTips.ContainsKey(owner))
            {
                elementsAndToolTips.Remove(owner);
            }
        }

        private static void RegisterToolTip(FrameworkElement owner, FrameworkElement toolTip)
        {
            var tooltip = ConvertToToolTip(toolTip);

            // Clear up the memory leak by removing the element from the dictionary
            // when the owner is unloaded.
            owner.Unloaded += FrameworkElementUnloaded;
            owner.MouseEnter += OnElementMouseEnter;
            owner.MouseLeave += OnElementMouseLeave;

            if (ReferenceEquals(null, toolTip)) return;

            toolTip.DataContext = owner.DataContext;

            owner.SetBinding(dataContextProperty, new Binding());

            owner.SetValue(ToolTipObjectProperty, tooltip);
            SetToolTipInternal(owner, tooltip);
        }

        private static void FrameworkElementUnloaded(object sender, RoutedEventArgs e)
        {
            UnregisterToolTip((FrameworkElement)sender);
        }

        private static void SetRootVisual()
        {
            if ((rootVisual != null) || (Application.Current == null))
            {
                return;
            }

            rootVisual = Application.Current.RootVisual as FrameworkElement;
            if (rootVisual == null)
            {
                return;
            }

            rootVisual.MouseMove += OnRootMouseMove;
            rootVisual.SizeChanged += OnRootVisualSizeChanged;

        }

        private static void SetToolTipInternal(DependencyObject element, ToolTip toolTip)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            if (toolTip == null)
            {
                elementsAndToolTips.Remove(element);
                return;
            }

            toolTip.SizeChanged += OnToolTipSizeChanged;

            var control = element as Control;
            if (control != null)
            {
                control.IsEnabledChanged += OnElementIsEnabledChanged;
            }

            if (elementsAndToolTips.ContainsKey(element))
            {
                elementsAndToolTips.Remove(element);
            }

            if (toolTip.CloseAnimation != null)
            {
                toolTip.CloseAnimation.Completed += (s, args) =>
                {
                    toolTip.IsOpen = false;
                    isCloseAnimationInProgress = false;
                };
            }
            if (toolTip.OpenAnimation != null)
            {
                toolTip.OpenAnimation.Completed += (s, args) => isOpenAnimationInProgress = false;
            }

            elementsAndToolTips.Add(element, toolTip);

            element.SetValue(ToolTipProperty, toolTip);
        }

        ///<summary>
        /// Gets the PlacementProperty value for the ToolTip element.
        ///</summary>
        ///<param name="element">The ToolTip element.</param>
        ///<returns>The value for the PlacementProperty of the ToolTip element.</returns>
        ///<exception cref="ArgumentNullException">The DependencyObject can not be null.</exception>
        public static PlacementMode GetPlacement(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element", "The DependencyObject could not be found.");

            return (PlacementMode)element.GetValue(PlacementProperty);
        }

        ///<summary>
        /// Gets the PlacementTargetProperty value for the ToolTip element.
        ///</summary>
        ///<param name="element">The ToolTip element.</param>
        ///<returns>The value for the PlacementTargetProperty.</returns>
        public static UIElement GetPlacementTarget(DependencyObject element)
        {
            return element == null ? null : (UIElement)element.GetValue(PlacementTargetProperty);
        }

        ///<summary>
        /// Sets the PlacementProperty for the ToolTip element.
        ///</summary>
        ///<param name="element">The ToolTip element.</param>
        ///<param name="value">The value for the PlacementProperty.</param>
        public static void SetPlacement(DependencyObject element, PlacementMode value)
        {
            element.SetValue(PlacementProperty, value);
        }

        /// <summary>
        /// Sets the PlacementTargetProperty value for the ToolTip element.
        /// </summary>
        /// <param name="element">The ToolTip element.</param>
        /// <param name="value">The value for the PlacementTargetProperty.</param>
        public static void SetPlacementTarget(DependencyObject element, UIElement value)
        {
            element.SetValue(PlacementTargetProperty, value);
        }
    }

    internal sealed class ToolTipTimer : DispatcherTimer
    {
        private const int timerInterval = 50;

        /// <summary>
        /// This event occurs when the timer has stopped.
        /// </summary>
        public event EventHandler Stopped;

        public ToolTipTimer(TimeSpan maximumTicks, TimeSpan initialDelay)
        {
            InitialDelay = initialDelay;
            MaximumTicks = maximumTicks.TotalMilliseconds < timerInterval
                ? TimeSpan.FromMilliseconds(timerInterval)
                : maximumTicks;
            Interval = TimeSpan.FromMilliseconds(timerInterval);
            Tick += OnTick;
        }

        /// <summary>
        /// Stops the ToolTipTimer.
        /// </summary>
        public new void Stop()
        {
            base.Stop();
            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Resets the ToolTipTimer and starts it.
        /// </summary>
        public void StartAndReset()
        {
            CurrentTick = 0;
            Start();
        }

        /// <summary>
        /// Stops the ToolTipTimer and resets its tick count.
        /// </summary>
        public void StopAndReset()
        {
            Stop();
            CurrentTick = 0;
        }

        /// <summary>
        /// Gets the maximum number of seconds for this timer.
        /// When the maximum number of ticks is hit, the timer will stop itself.
        /// </summary>
        /// <remarks>The minimum number of seconds is 1.</remarks>
        public TimeSpan MaximumTicks { get; private set; }

        /// <summary>
        /// Gets the initial delay for this timer in seconds.
        /// When the maximum number of ticks is hit, the timer will stop itself.
        /// </summary>
        /// <remarks>The default delay is 0 seconds.</remarks>
        public TimeSpan InitialDelay { get; private set; }

        /// <summary>
        /// Gets the current tick of the ToolTipTimer.
        /// </summary>
        public int CurrentTick { get; private set; }

        private void OnTick(object sender, EventArgs e)
        {
            CurrentTick += timerInterval;
            if (CurrentTick >= (MaximumTicks.TotalMilliseconds + InitialDelay.TotalMilliseconds))
            {
                Stop();
            }
        }
    }

}
