namespace AnimalType
{
    public class Animal
    {
        public void eat()
        {
            Console.WriteLine("I can eat");
        }
        public void move()
        {
            Console.WriteLine("I can move");
        }
    }

    public class Dog : Animal
    {
        public Dog(){
            Console.WriteLine("Dog Spawn");
            
        }
        public void bark()
        {
            Console.WriteLine("Barking");
        }
    }

}