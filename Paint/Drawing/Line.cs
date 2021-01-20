using Eto.Drawing;

namespace Paint.Drawing
{
    public class Line : BaseDrawable
    {
        public Line(Pen p, PointF point1, PointF point2)
        {
            Pen = p;
            Start = point1;
            End = point2;
        }

        public Line(Pen p, int x1, int y1, int x2, int y2) :
            this(p, new PointF(x1, y1), new PointF(x2, y2))
        {
        }

        public Pen Pen { get; }
        public PointF Start { get; }
        public PointF End { get; private set; }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pen, Start, End);
        }

        public void GrowTo(PointF point)
        {
            End = point;
        }
    }
}