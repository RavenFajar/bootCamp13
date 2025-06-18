public interface ITable
{
    public void Use();
}
public interface IChair
{
    public void Sit();
}
public interface IFurnitureFactory
{
    public IChair CreateChair();
    public ITable CreateTable();
}

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
        Console.WriteLine("SIt on Classic Chair");

    }
}
public class ClassicTable : ITable
{
    public void Use()
    {
        Console.WriteLine($"Use Classic table");
    }
}

public class ModernFactory : IFurnitureFactory
{
    public IChair CreateChair()
    {
        return new ModernChair();
    }
    public ITable CreateTable()
    {
        return new ModernTable();
    }
}

public class ClassicFactory : IFurnitureFactory
{
    public IChair CreateChair()
    {
        return new ClassicChair();
    }
    public ITable CreateTable()
    {
        return new ClassicTable();
    }
}

public class ClientCode
{
    private IChair _chair;
    private ITable _table;

    public ClientCode(IFurnitureFactory factory)
    {
        _chair = factory.CreateChair();
        _table = factory.CreateTable();
    }
    public void TestFurniture()
    {
        Console.WriteLine($"Test Furniture");
        _chair.Sit();
        _table.Use();
    }
    
}
class Program
{
    public static void Main(string[] args)
    {
        //Modern dengan modern FurnitureFactory
        var modernFurniture = new ClientCode(new ModernFactory());
        modernFurniture.TestFurniture();

        Console.WriteLine();
        
        var classicFurniture = new ClientCode(new ClassicFactory());
        classicFurniture.TestFurniture();
    }
}

