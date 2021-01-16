using System.Collections.Generic;
using Eto.Drawing;

namespace Paint.Drawing
{
    public class Path : BaseDrawable
    {
        private List<PointF> _points = new();

        public Path() : this(new Pen(Colors.Black))
        {
        }

        public Path(Pen pen)
        {
            Pen = pen;
            Pen.LineCap = PenLineCap.Round;
            Pen.LineJoin = PenLineJoin.Round;
        }

        public Pen Pen { get; private set; }

        public void AddPoint(PointF point) => _points.Add(point);

        public override void Draw(Graphics g)
        {
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddLines(_points);
            g.DrawPath(Pen, graphicsPath);
        }
    }
}