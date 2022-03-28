using Avalonia.Media.Imaging;

namespace checkers_app.Models;

public interface ICell
{
    public Bitmap Image { get; set; }
}