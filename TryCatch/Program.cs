// try
// {
//     int y = Calc(0);
//     Console.WriteLine(y);

//     int Calc(int x)
//     {
//         return 10 / x;
//     }

// }
// catch (DivideByZeroException)
// {
//     Console.WriteLine("Tidak bisa dibagi 0");
// }

Display1 oneOf = new Display1();
oneOf.Display(null);

public class Display1
{
    public void Display(string name)
    {
        try
        {
            throw new ArgumentNullException(nameof(name));
        }
        catch (ArgumentNullException ex) when (name == null)
        {

            Console.WriteLine(ex);
            Console.WriteLine("ini masuk ke block exception pertama");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("ini exception biasa");
        }
        finally
        { 
            Console.WriteLine("Jalan final");
            
        }
    }
}