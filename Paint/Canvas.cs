using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Eto.Forms;
using Eto.Drawing;
using Paint.Drawing;

namespace Paint
{
    public class Canvas : Scrollable
    {
        private readonly PixelLayout _layout = new();

        private List<BaseDrawable> Drawn { get; set; } = new();

        public override Size Size { get; set; } = new(250, 250);

        public new Color BackgroundColor
        {
            get => Control.BackgroundColor;
            set => Control.BackgroundColor = value;
        }

        public readonly Drawable Control = new(true) {
            Size = new Size(640, 480),
            BackgroundColor = Colors.White,
        };
        public Canvas()
        {
            Control.Paint += (_, pe) => DrawAll(pe.Graphics);
            _layout.Add(Control, 0, 0);
            Content = _layout;
        }

        public void Clear() =>
            Drawn = new List<BaseDrawable>();

        public void Refresh() => Control.Invalidate();

        public void Draw(BaseDrawable drawable) => Drawn.Add(drawable);

        private void DrawAll(Graphics graphics)
        {
            foreach (var drawable in Drawn) drawable.Draw(graphics);
        }
    }
}