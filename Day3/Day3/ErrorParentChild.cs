//ParentChild Constructor
class Component
{
    public string brand;
    public Component(string brand)
    {
        this.brand = brand;
    }
}
class Bell : Component
{
    public int size;
    public Bell(string brand, int size) : base(brand)
    {
        this.size = size;
    }
}
