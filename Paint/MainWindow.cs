using System;
using Eto;
using Eto.Drawing;
using Eto.Forms;
using Paint.Drawing;
using Paint.UI;

namespace Paint
{
    internal class MainWindow : Form
    {
        private static readonly Size WindowSize = new(664, 560);

        private readonly Canvas _canvas = new();

        private ColorHSB _currentColor = Colors.Black.ToHSB();

        private Path? _currentPath;
        private float _currentWidth = 1F;

        private bool _mouseDown;

        private MainWindow()
        {
            Title = "SketchPad";
            ClientSize = WindowSize;

            Content = new TableLayout
            {
                Padding = new Padding(10),
                Spacing = new Size(10, 10),
                Rows =
                {
                    new Panel {Content = _canvas},

                    (Platform.SupportedFeatures & PlatformFeatures.DrawableWithTransparentContent) == 0
                        ? new TableRow(
                            "(Transparent content on drawable not supported on this platform)"
                        )
                        : null,

                    null
                }
            };

            SetupMenuBar();

            var toolbar = new ToolBar();
            ToolBar = toolbar;

            Dialog colorPickerDialog = new();
            ColorPicker colorPicker = new()
            {
                ToolTip = "Select brush color",
                AllowAlpha = true,
                Visible = true
            };
            colorPickerDialog.Content = new TableLayout
            {
                Rows =
                {
                    "Click below to select a color",
                    colorPicker,
                    TableLayout.HorizontalScaled(
                        new Button(delegate { colorPickerDialog.Close(); })
                        {
                            Text = "Cancel",
                            ImagePosition = ButtonImagePosition.Right
                        },
                        new Button(delegate
                        {
                            colorPickerDialog.Close();
                            _currentColor = colorPicker.Value;
                        })
                        {
                            Text = "OK",
                            ImagePosition = ButtonImagePosition.Right
                        }
                    )
                }
            };

            var colorPickerIntent = new Command();
            colorPickerIntent.Executed += delegate { colorPickerDialog.ShowModal(); };
            ToolBar.Items.Add(new ButtonToolItem(colorPickerIntent)
            {
                Text = "Color Picker",
                ToolTip = "Select a color to use for the brush"
            });
            
            ToolBar.Items.Add(new UI.ColorPickerToolItem()
            {
                Text = "Color Picker"
            });
        }

        private void SetupMenuBar()
        {
            // create menu & get standard menu items (e.g. needed for OS X)
            var menu = new MenuBar();
            ParentWindow.Menu = menu;

            menu.Items.GetSubmenu("&File").Items.Add(new ButtonMenuItem
            {
                Text = "Quit", Shortcut = Keys.Control | Keys.Q,
                Command = new Command(OnMenuItemClicked) {ID = "action_quit"}
            });
            menu.Items.GetSubmenu("&Edit").Items.Add(new ButtonMenuItem
            {
                Text = "Clear", Shortcut = Keys.Alt | Keys.C,
                Command = new Command(OnMenuItemClicked) {ID = "action_clear_canvas"}
            });
        }

        private void OnMenuItemClicked(object? sender, EventArgs args)
        {
            switch (((Command) sender).ID)
            {
                case "action_clear_canvas":
                    _canvas.Clear();
                    break;
                case "action_quit":
                    Close();
                    break;
                default:
                    Console.Error.WriteLine($"Menu item with {((Command) sender).ID} not handled");
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseDown = true;
            _currentPath = new Path(new Pen(_currentColor, _currentWidth));
            _canvas.Draw(_currentPath);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _mouseDown = false;
            base.OnMouseLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (e.Key)
            {
                case Keys.RightBracket:
                    _currentWidth += 5;
                    break;
                case Keys.LeftBracket:
                    _currentWidth = Math.Max(1, _currentWidth - 5);
                    break;
                case Keys.F:
                    _canvas.BackgroundColor.Invert();
                    break;
                case Keys.H:
                    _currentColor.H = (_currentColor.H + 10) % 360;
                    Console.WriteLine(_currentColor.ToColor().ToHex());
                    break;
                case Keys.S:
                    _currentColor.S = (_currentColor.S + 0.1F) % 1F;
                    Console.WriteLine(_currentColor.ToColor().ToHex());
                    break;
                case Keys.B:
                    _currentColor.B = (_currentColor.B + 0.1F) % 1F;
                    Console.WriteLine(_currentColor.ToColor().ToHex());
                    break;
                case Keys.A:
                    _currentColor.A = (_currentColor.A + 0.1F) % 1F;
                    Console.WriteLine(_currentColor.ToColor().ToHex());
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mouseDown)
            {
                _currentPath?.AddPoint(e.Location);
                e.Handled = true;
                _canvas.Refresh();
            }

            base.OnMouseMove(e);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            new Application().Run(new MainWindow());
        }
    }
}