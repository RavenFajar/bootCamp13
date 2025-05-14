using Characther.NpcCharacter;

namespace Characther
{
    namespace NpcCharacter
    {
        public interface Npc
        {
            public void Speak();
            public void Move();
        }
        public class NpcPerson : Npc
        {
            public void Speak()
            {
                Console.WriteLine("hello");
            }

            public void Move()
            {
                Console.WriteLine("2");
            }

        }
    }
    namespace Animal
    {
        public class NpcDog : Npc
        {
            public void Speak()
            {
                Console.WriteLine("bark");
            }
            public void Move()
            {
                Console.WriteLine("4");
            }
        }
    }

    public class User1 : Npc
    {
        public void Name()
        {
            Console.WriteLine("User1");
        }
        public void Speak()
        {
            Console.WriteLine("I/'m The Main Character");
        }
        public void Move()
        {
            Console.WriteLine("3");
        }
    }

}
