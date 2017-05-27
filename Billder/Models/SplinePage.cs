using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billder.Models
{
    public class SplinePage
    {
        public Surface CurrentSurface { get; set; }
        public List<String> SurfaceNames { get; set; }
        public Vertex VertexOne { get; set; }
        public Vertex VertexTwo { get; set; }
        public Vertex VertexThree { get; set; }
    }
}