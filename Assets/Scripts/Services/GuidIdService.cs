using System;

public class GuidIdService : IUniqueIdService
{
    public string GetNewId()
    {
        return new Guid().ToString();
    }
}
