using SimpleRPGServer.Extensions;
using SimpleRPGServer.Persistence.Models.Ingame;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleRPGServer.Service;

public class MapService : IMapService
{
    private readonly string _mapFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleRPGServer");
    private readonly string _mapFileName = "SimpleRPGServer.map";
    private readonly string[] _allowedFilenames = new string[] { "dirt1", "grass1", "grass2" };
    private Map _map;

    public MapService()
    {
        if (!Directory.Exists(this._mapFilePath))
            Directory.CreateDirectory(this._mapFilePath);

        string mapFile = Path.Combine(this._mapFilePath, this._mapFileName);
        if (File.Exists(mapFile))
        {
            try
            {
                this._map = Map.LoadFromJsonFile(mapFile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            this._map = GenerateRandomMap(20, 50, 20, 50);
            this._map.WriteToJsonFile(mapFile);
        }
    }

    public Map GetMap()
    {
        return this._map;
    }

    private Map GenerateRandomMap(int minX, int maxX, int minY, int maxY)
    {
        var list = new List<MapField>();
        ulong id = 0;
        // add fields the same way you would write a text:
        // left-to-right and top-to-bottom
        for (var y = maxY; y >= minY; y--)
        {
            for (var x = minX; x <= maxX; x++)
            {
                id++;
                list.Add(new MapField()
                {
                    Id = id,
                    X = x,
                    Y = y,
                    ImageFilename = this._allowedFilenames.Random(),
                });
            }
        }

        return new Map(list);
    }
}
