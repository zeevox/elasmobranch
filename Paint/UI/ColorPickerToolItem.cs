using Eto;
using Eto.Forms;

namespace Paint.UI
{
    [Handler(typeof(IHandler))]
    public class ColorPickerToolItem : ToolItem
    {
        private ColorPicker _colorPicker;

        public ColorPickerToolItem() : base()
        {
            _colorPicker = new ColorPicker();
        }

        private new IHandler Handler => (IHandler) base.Handler;
        
        public new interface IHandler : ToolItem.IHandler
        {
            
        }
    }
}