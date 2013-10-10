using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVTeleGather
{
    public class World
    {
        public List<Zone> zones { get; set; }
        public World(List<Zone> zones)
        {
            this.zones = zones;
        }
    }
    public class Zone
    {
        public String zoneName { get; set; }
        public List<Position> positions { get; set; }
        public Zone(String zoneName, List<Position> positions)
        {
            this.zoneName = zoneName;
            this.positions = positions;
        }
    }
    public class Vector4
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float rot { get; set; }
        public Vector4(float x, float y, float z, float rot)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.rot = rot;
        }
    }
    public class Position
    {
        public String name { get; set; }
        public Vector4 pos { get; set; }
        public String info { get { return name + " (" + pos.x.ToString("0.0") + ", " + pos.y.ToString("0.0") + ", " + pos.z.ToString("0.0") + ")"; } }
        public Position(String name, Vector4 pos)
        {
            this.name = name;
            this.pos = pos;
        }
    }
    class LocationStructure
    {
    }
}
