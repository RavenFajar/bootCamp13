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


