// See https://aka.ms/new-console-template for more information
using PAW3.Console;

Console.WriteLine("Hello, World!");
Console.WriteLine("\n \n \n ==============================================");

Actionizer.Invocation((text) => Console.WriteLine(text), "This is action 1");

//var a2 = new Actionizer2();
//a2.Invocation((text) => Console.WriteLine(text), "This is action 2");

// Console.WriteLine("This is action 1");
// Console.WriteLine("This is action 1");
Actionizer.Invocation((text) => Console.WriteLine(text), "This is action 2");

var readValue = Console.ReadLine();
//var isParsed = int.TryParse(readValue, out int xint);

Console.WriteLine(Actionizer.Func1(readValue));
Console.WriteLine(Actionizer.Func2(readValue));

//Console.WriteLine(func1(xint));
//Console.WriteLine(func2(readValue));

Console.ReadKey();

Console.WriteLine("\n \n \n ==============================================");

new Actionizer()
    .Func((p) => Console.WriteLine("This is action 100"), "100")
    .Func((p) => Console.WriteLine("This is action 200"), "200")
    .Func((p) => Console.WriteLine("This is action 300"), "300")
    .Func((p) => Console.WriteLine("This is action 400"), "400")
    .Func((p) => Console.WriteLine("This is action 500"), "500");

Console.WriteLine("\n \n \n ==============================================");
Console.WriteLine(new Actionizer()
    .AddToConsole("ALEX")
    .AddToConsole("ROBERT")
    .AddToConsole("ANDREY")
    .AddToConsole("MARIA")
    .AddToConsole("SOPHIA")
    .ToString());

// Design Patterns: Chain of Responsibility, Pipeline, Strategy

Console.ReadKey();