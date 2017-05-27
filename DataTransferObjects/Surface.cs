using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class Surface
    {
	    public string Username{get; set; }
	    public string SplineName{get; set; }
	    public string Color {get; set; }
        public List<Vertex> Verticies { get; set; }
    }
}
