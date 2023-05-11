namespace Trips.Models;

public class AgentOrdersL
{
    public Agent Agent { get; set; }

    public List<OrderL> Orders { get; set; }
}

public class AgentOrdersE
{
    public Agent Agent { get; set; }

    public List<OrderE>? Orders { get; set; }
}