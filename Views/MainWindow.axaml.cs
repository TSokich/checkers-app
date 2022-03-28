using System;
using Avalonia.Controls;
using Avalonia.Input;
using checkers_app.utils;
using checkers_app.ViewModels;

namespace checkers_app.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainWindowViewModel
        {
            get
            {
                if (DataContext is not MainWindowViewModel vm)
                    throw new NullReferenceException("Empty main view model");
                return vm;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputElement_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (sender is not Border s || s.Name == null) return;

            var point = BoardPointer.GetBoardPointByCellName(s.Name);
            MainWindowViewModel.SelectBoardCell(point);
        }
    }
}