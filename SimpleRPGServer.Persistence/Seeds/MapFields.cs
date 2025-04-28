using SimpleRPGServer.Persistence.Extensions;
using SimpleRPGServer.Persistence.Models.Ingame;

namespace SimpleRPGServer.Persistence.Seeds;

public static class MapFields
{
    public static MapField[] Get()
    {
        var filenames = new string[] { "dirt1", "grass1", "grass2" };
        var l = new List<MapField>();

        ulong id = 0;
        for (var x = -30; x <= 50; x++)
        {
            for (var y = -30; y <= 50; y++)
            {
                id++;
                l.Add(new MapField() 
                { 
                    Id = id, 
                    ImageFilename = filenames.Random(), 
                    X = x, 
                    Y = y, 
                    Description = "Field description", 
                    Name = "Field",
                });
            }
        }

        return l.ToArray();
    }
}
