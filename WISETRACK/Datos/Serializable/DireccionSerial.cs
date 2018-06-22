using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Serializable
{
    [Serializable]
    public class DireccionSerial
    {
        public List<Feature> features { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    public class Feature
    {
        public Geometry geometry { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    [Serializable]
    public class Geometry
    {
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    public class Properties
    {
        public long osm_id { get; set; }
        public string osm_type { get; set; }
        public string country { get; set; }
        public string osm_key { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string osm_value { get; set; }
        public string name { get; set; }
        public string state { get; set; }
    }
}