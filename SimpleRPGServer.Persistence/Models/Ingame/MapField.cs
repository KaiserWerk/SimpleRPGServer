﻿namespace SimpleRPGServer.Persistence.Models.Ingame;

public class MapField
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageFilename { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool Accessible { get; set; }

    public static MapField BorderField(int x, int y)
    {
        return new MapField()
        {
            ImageFilename = "rock1",
            X = x,
            Y = y,
            Accessible = false,
        };
    }
}
