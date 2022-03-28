using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace checkers_app.Models;

public class BlackCheckerCell : ICell
{
    public Bitmap Image { get; set; } = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>()
        ?.Open(new Uri($" avares://checkers_app/Assets/black_checker.png")));
}