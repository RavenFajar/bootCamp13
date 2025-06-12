using Tell_Dont_Ask.Models;

namespace Tell_Dont_Ask.BadExamples;

/// <summary>
/// BAD EXAMPLE - Classic "Ask" approach to monitoring
/// This class is just a data container - it has no real behavior
/// All the logic happens outside the object, which violates encapsulation
/// </summary>
public class AskMonitor
{
    private int _value;
    private readonly int _limit;
    private readonly string _name;
    private readonly Alarm _alarm;

    public AskMonitor(string name, int limit, Alarm alarm)
    {
        _name = name;
        _limit = limit;
        _alarm = alarm;
    }

    // Look at all these getters! This is a red flag.
    // We're exposing internal state for others to make decisions
    public int GetValue() => _value;
    public void SetValue(int value) => _value = value; // Just sets data, no logic
    public int GetLimit() => _limit;
    public string GetName() => _name;
    public Alarm GetAlarm() => _alarm;
}
class Program
{
    static void Main(string[] args)
    {
        // This is how you would use the AskMonitor class
        // Notice how it requires external logic to do anything useful
        var alarm = new Alarm();
        var monitor = new AskMonitor("Time Vortex Hocus", 2, alarm);
        
        monitor.SetValue(3); // Just sets data, no logic here
        
        // External logic checks the value and decides what to do
        if (monitor.GetValue() > monitor.GetLimit())
            monitor.GetAlarm().Warn(monitor.GetName() + " too high");
    }
}