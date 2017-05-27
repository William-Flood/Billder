using Billder.Models;
using DataTransferObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Billder.Controllers
{
    public class SurfaceController : Controller
    {
        ISurfaceManager _surfaceManager;
        public SurfaceController(ISurfaceManager _surfaceManager)
        {
            this._surfaceManager = _surfaceManager;
        }
        // GET: Surface
        [Authorize]
        public ActionResult Index()
        {
            var noSaveFail = true;
            String userName = User.Identity.Name;
            var pageModel = new SplinePage();
            try
            {
                if (null != Request["save"])
                {
                    Surface toSave = new Surface
                    {
                        Color = Request["color"],
                        SplineName = Request["splineName"],
                        Username = userName
                    };
                    String[] vertexIndexes = new String[] { "One", "Two", "Three" };
                    List<Vertex> verticies = new List<Vertex>();
                    foreach(String indexName in vertexIndexes) {
                        Vertex newVertex = new Vertex() { 
                            PointIndex = indexName.ToUpper(),
                            AOne = Int32.Parse(Request["A"+indexName+"One"]),
                            ATwo = Int32.Parse(Request["A" + indexName + "Two"]),
                            BOne = Int32.Parse(Request["B" + indexName + "One"]),
                            BTwo = Int32.Parse(Request["B" + indexName + "Two"]),
                            COne = Int32.Parse(Request["C" + indexName + "One"]),
                            CTwo = Int32.Parse(Request["C" + indexName + "Two"]),
                            X = Int32.Parse(Request["X"+indexName]),
                            Y = Int32.Parse(Request["Y"+indexName]),
                            Z = Int32.Parse(Request["Z"+indexName])
                        };
                        verticies.Add(newVertex);
                    }
                    toSave.Verticies = verticies;
                    _surfaceManager.SaveSurface(toSave);
                }
            }
            catch (Exception ex)
            {
                noSaveFail = false;
                ViewBag.Error = "Internal error: " + ex.Message;
            }

            try
            {
                pageModel.SurfaceNames = _surfaceManager.ListSurfaces(userName);
            }
            catch (Exception ex)
            {
                //ViewBag["Error"] = "Internal error: " + ex.Message;
            }
            if (null != Request["splineName"] && noSaveFail)
            {
                try
                {
                    pageModel.CurrentSurface = _surfaceManager.GetSurface(userName, Request["splineName"].Replace(" ",""));
                    foreach (var vertex in pageModel.CurrentSurface.Verticies)
                    {
                        switch (vertex.PointIndex)
                        {
                            case "ONE":
                                pageModel.VertexOne = vertex;
                                break;
                            case "TWO":
                                pageModel.VertexTwo = vertex;
                                break;
                            case "THREE":
                                pageModel.VertexThree = vertex;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Internal error: " + ex.Message;
                }

            }
            else
            {
                pageModel.CurrentSurface = new Surface()
                {
                    Username = userName,
                    SplineName = "new",
                    Color = "#729",
                    Verticies = new List<Vertex>() {
                        new Vertex (){PointIndex = "ONE", X=-80,Y=20,Z=110,AOne=-20,BOne=-22,COne=-6,ATwo=3,BTwo=-70,CTwo=-10},
                        new Vertex (){PointIndex = "TWO", X=54,Y=9,Z=130,AOne=11,BOne=-6,COne=5,ATwo=-7,BTwo=-59,CTwo=11},
                        new Vertex (){PointIndex = "THREE", X=-40,Y=13,Z=67,AOne=-10,BOne=-16,COne=-6,ATwo=25,BTwo=17,CTwo=0}
                    }
                };
                pageModel.VertexOne = pageModel.CurrentSurface.Verticies[0];
                pageModel.VertexTwo = pageModel.CurrentSurface.Verticies[1];
                pageModel.VertexThree = pageModel.CurrentSurface.Verticies[2];
            }
            return View(pageModel);
        }
    }
}