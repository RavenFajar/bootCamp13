// See https://aka.ms/new-console-template for more information
int x = Convert.ToInt32(Console.ReadLine());

for(int i = 1 ; i <= x  ; i++){
    if(i%3 == 0 && i%5 == 0){
        Console.WriteLine("foobar");
        continue;
    }else if(i%3 == 0){
        Console.WriteLine("foo");
        continue;
    }else if(i%5 == 0){
        Console.WriteLine("bar");
        continue;
    }else{
    Console.WriteLine(i);
    }
}