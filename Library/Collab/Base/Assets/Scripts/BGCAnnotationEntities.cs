using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGC.Annotation.Basic
{

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class Rotation
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
    }

    public class Coordinates
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }


    public class AnnotationEntity
    {
        public string type { get; set; }  // Mandatory : Spot, Polyline, Polygon, Circle, Sphere, Cube, Free, Pointer
        public string coordinate_system { get; set; } // utm, tm, Cartesian
        public string label { get; set; } // any text
        public string line_color { get; set; } // red, blue, yellow, or #4542 - color code
        public string thickness { get; set; } // float
        public string line_style { get; set; } // solid, dotted, loosly dotted
        public string fill_color { get; set; } // red, blue, yellow, or #4542 - color code BUT, less than 50% transparency 
        public string symbol { get; set; } // 
        public Position position { get; set; }
        public Rotation rotation { get; set; }
        public Coordinates coordinates { get; set; }
        public string tag { get; set; } // any data for the annotation
        public List<AnnotationEntity> children { get; set; }
    }

    public class BgcAnnotation
    {
        public string datetime { get; set; } // updated date time
        public string coordinate_system { get; set; } // utm, tm, Cartesian
        public string group { get; set; } // group name which created the annotation
        public string tag { get; set; } // any data for the annotation
        public AnnotationEntity annotationEntity { get; set; }
    }

    public class RootObject
    {
        public BgcAnnotation bgc_annotation { get; set; }
    }
}