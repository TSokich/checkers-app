using System;
using checkers_app.Models;

namespace checkers_app.utils;

public class BoardPointer
{
    private enum BoardLetters
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
        F = 5,
        G = 6,
        H = 7
    }

    public static BoardPoint? GetBoardPointByCellName(string name)
    {
        var letter = name[0].ToString();
        var digit = name[1].ToString();

        if (!Enum.TryParse(letter, out BoardLetters boardLetter)) return null;
        
        var x = Math.Abs(int.Parse(digit) - 8);
        var y = (int) boardLetter;
        
        return new BoardPoint(x, y);
    }
}