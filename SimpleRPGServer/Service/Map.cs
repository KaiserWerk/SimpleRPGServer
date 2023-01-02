using SimpleRPGServer.Models.Ingame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SimpleRPGServer.Service
{
    public class Map
    {
        private List<MapField> _fields;

        public Map()
        {
            this._fields = new List<MapField>();
        }

        public Map(List<MapField> fields)
        {
            if (fields == null || !fields.Any())
                throw new Exception("supplied list of fields is null or empty");

            this._fields = fields;
        }

        public void AddField(MapField field)
        {
            this._fields.Add(field);
        }

        public void RemoveField(MapField field)
        {
            this._fields.Remove(field);
        }

        public void RemoveField(int x, int y)
        {
            var field = this._fields.SingleOrDefault(mf => mf.X == x && mf.Y == y);
            if (field == null)
                throw new Exception($"map field (X: {x}, Y: {y}) not found");

            this._fields.Remove(field);
        }

        public void RemoveField(long id)
        {
            var field = this._fields.SingleOrDefault(mf => mf.Id == id);
            if (field == null)
                throw new Exception($"map field (Id: {id}) not found");

            this._fields.Remove(field);
        }

        public static Map LoadFromJsonFile(string jsonFile)
        {
            string fileContent = File.ReadAllText(jsonFile);
            return JsonSerializer.Deserialize<Map>(fileContent);
        }

        public void WriteToJsonFile(string jsonFile)
        {
            string jsonContent = JsonSerializer.Serialize(this._fields, new JsonSerializerOptions() { WriteIndented = true });
            File.WriteAllText(jsonFile, jsonContent);
        }
    }
}
