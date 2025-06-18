using System;

// Interface tetap sama
public interface ITable 
{
    public void Use(); 
}

public interface IChair 
{
    public void Sit(); 
}

// Concrete classes tetap sama
public class ModernChair : IChair 
{
    public void Sit()
    {
        Console.WriteLine("Sit On Modern Chair");
    }
}

public class ModernTable : ITable 
{
    public void Use()
    {
        Console.WriteLine("Use Modern Table");
    }
}

public class ClassicChair : IChair 
{
    public void Sit()
    {
        Console.WriteLine("Sit on Classic Chair");
    }
}

public class ClassicTable : ITable 
{
    public void Use()
    {
        Console.WriteLine("Use Classic table");
    }
}

// Client code - menerima objek langsung tanpa factory
public class ClientCode 
{
    private IChair _chair;
    private ITable _table;

    // Constructor menerima objek chair dan table secara langsung
    public ClientCode(IChair chair, ITable table)
    {
        _chair = chair;
        _table = table;
    }

    public void TestFurniture()
    {
        Console.WriteLine("Test Furniture");
        _chair.Sit();
        _table.Use();
    }
}

class Program
{
    public static void Main(string[] args)
    {
        // Modern furniture - instantiate setiap objek secara manual
        var modernChair = new ModernChair();
        var modernTable = new ModernTable();
        var modernFurniture = new ClientCode(modernChair, modernTable);
        modernFurniture.TestFurniture();

        Console.WriteLine();

        // Classic furniture - instantiate setiap objek secara manual
        var classicChair = new ClassicChair();
        var classicTable = new ClassicTable();
        var classicFurniture = new ClientCode(classicChair, classicTable);
        classicFurniture.TestFurniture();
    }
}