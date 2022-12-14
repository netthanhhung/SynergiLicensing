using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Synergi.Licensing.Silverlight.UI
{
    public class ClippedBottomGrid : Grid
    {
        /*  ======================================================================            
         *      PAGE MEMBERS
         *  ====================================================================== */
        private const double _arcSizeX = 8;
        private const double _arcSizeY = 8;
        private const double _offsetY = 2;




        /*  ======================================================================            
         *      EVENT HANDLERS
         *  ====================================================================== */
        public ClippedBottomGrid()
        {
            this.SizeChanged += new SizeChangedEventHandler(ClippedGrid_SizeChanged);
        }

        void ClippedGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double w = this.ActualWidth;
            double h = this.ActualHeight;

            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0, h);

            LineSegment left = new LineSegment();
            left.Point = new Point(0, _arcSizeY + _offsetY);
            figure.Segments.Add(left);

            ArcSegment topLeftRounding = new ArcSegment();
            topLeftRounding.Size = new Size(_arcSizeX, _arcSizeY);
            topLeftRounding.RotationAngle = 0;
            topLeftRounding.IsLargeArc = false;
            topLeftRounding.SweepDirection = SweepDirection.Clockwise;
            topLeftRounding.Point = new Point(_arcSizeX, _offsetY);
            figure.Segments.Add(topLeftRounding);

            LineSegment top = new LineSegment();
            top.Point = new Point(w - _arcSizeX, _offsetY);
            figure.Segments.Add(top);

            ArcSegment topRightRounding = new ArcSegment();
            topRightRounding.Size = new Size(_arcSizeX, _arcSizeY);
            topRightRounding.RotationAngle = 0;
            topRightRounding.IsLargeArc = false;
            topRightRounding.SweepDirection = SweepDirection.Clockwise;
            topRightRounding.Point = new Point(w, _arcSizeY + _offsetY);
            figure.Segments.Add(topRightRounding);

            LineSegment right = new LineSegment();
            right.Point = new Point(w, h - _arcSizeY);
            figure.Segments.Add(right);

            ArcSegment bottomRightRounding = new ArcSegment();
            bottomRightRounding.Size = new Size(_arcSizeX, _arcSizeY);
            bottomRightRounding.RotationAngle = 0;
            bottomRightRounding.IsLargeArc = false;
            bottomRightRounding.SweepDirection = SweepDirection.Clockwise;
            bottomRightRounding.Point = new Point(w - _arcSizeX, h);
            figure.Segments.Add(bottomRightRounding);

            LineSegment bottom = new LineSegment();
            bottom.Point = new Point(_arcSizeX, h);
            figure.Segments.Add(bottom);

            ArcSegment bottomLeftRounding = new ArcSegment();
            bottomLeftRounding.Size = new Size(_arcSizeX, _arcSizeY);
            bottomLeftRounding.RotationAngle = 0;
            bottomLeftRounding.IsLargeArc = false;
            bottomLeftRounding.SweepDirection = SweepDirection.Clockwise;
            bottomLeftRounding.Point = new Point(0, h - _arcSizeY);
            figure.Segments.Add(bottomLeftRounding);

            

            PathGeometry clip = new PathGeometry();
            clip.Figures.Add(figure);

            this.Clip = clip;
        }
    }
}
