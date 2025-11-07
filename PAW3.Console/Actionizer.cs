using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PAW3.Console;

internal class Actionizer
{
    public Actionizer Func<T>(Action<T> action, T param)
    {
        action?.Invoke(param);
        return this;
    }

    public static Action<Action<string>, string> Invocation => (executable, text) => executable(text);
    
    public static Func<string, string> Func1 => (xin) =>
    {
        if (TryParseInt(xin, out int result))
            return $"You passed in {result}";
        return "Invalid integer input";
    };

    public static Func<string, int> Func2 => (xin) =>
    {
        return xin.Length;
    };

    private static bool TryParseInt(string input, out int result)
    {
        result = 0;
        try
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return false;

            result = Convert.ToInt32(input);
            return true;
        }
        catch
        {
            return false;
        } 
    }

    public string Console { get; set; }

    public Actionizer AddToConsole(string text)
    {
        Console += text;
        return this;
    }

    public override string ToString()
    {
        return Console;
    }
}

internal class Actionizer2
{
    public Action<Action<string>, string> Invocation => (executable, text) => executable(text);

    public Func<string, string> Func1 => (xin) =>
    {
        if (TryParseInt(xin, out int result))
            return $"You passed in {result}";
        return "Invalid integer input";
    };

    public Func<string, int> Func2 => (xin) =>
    {
        return xin.Length;
    };

    private bool TryParseInt(string input, out int result)
    {
        result = 0;
        try
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return false;

            result = Convert.ToInt32(input);
            return true;
        }
        catch
        {
            return false;
        }
    }
}