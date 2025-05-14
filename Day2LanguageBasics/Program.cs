using Characther;
using Characther.NpcCharacter;
using Characther.Animal;
using CalculatorLib;
using Person;
using Employ;

//Naming Conventions
Console.WriteLine("Day 2");
int myVariabel = 3;
int @using = 123;
double firstDouble = 3.14;
double secondDouble = 3.16;
const string name = "Raven";
string upperCase = name.ToUpper();
string lowerCase = upperCase.ToLower();

Console.WriteLine(upperCase);
Console.WriteLine(lowerCase);

bool isReady = true;
if (isReady)
{
    Console.WriteLine("Process is ready!");
}

Calculator calObjCalculator = new Calculator();
Calculator calObjCalculator2 = new Calculator();
int sumInt = calObjCalculator.Add(myVariabel, @using);
double sumDouble = calObjCalculator2.Add(firstDouble, secondDouble);
Console.WriteLine(sumInt);
Console.WriteLine(sumDouble);


NpcPerson npc1 = new NpcPerson();
npc1.Speak();
npc1.Move();

NpcDog npc2 = new NpcDog();
npc2.Speak();

Anything person1 = new Anything();
person1.DoAnything();
Anything person2 = new Anything();

Employee employe1 = new Employee();
employe1.Name = "John";
Console.WriteLine(employe1.Name);

User1 userName = new User1();
userName.Name();
userName.Speak();
userName.Move();

Point p1 = new Point();
p1.X = 5;
Console.WriteLine(p1.X);  // Output: 5

Point p2 = p1;  // Copy of p1
p2.X = 10;

Console.WriteLine(p1.X);  // Output: 5
Console.WriteLine(p2.X);  // Output: 10

bool isRaining = true;
bool isSunny = !isRaining;

if (isSunny)
{
    Console.WriteLine($"No umbrella needed. {name}");
}
else
{
    Console.WriteLine(@"Take an umbrella!");
}

Console.WriteLine($"The weather is {(isSunny ? "sunny" : "not sunny")}");  

int x = 8;
Foo(ref x);  // Passes x by reference
Console.WriteLine(x);  // Output: 9

static void Foo(ref int p)
{
    p = p + 1;  // Modifies p, which also modifies x
    Console.WriteLine(p);  // Output: 9
}


public struct Point
{
    public int X, Y;
}

