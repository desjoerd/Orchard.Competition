using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeSjoerd.Competition
{
    public class Routes : IRouteProvider
    {
        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Teams",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "TeamAdmin"},
				            {"action", "List"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Games",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "GameAdmin"},
				            {"action", "List"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveAdmin"},
				            {"action", "List"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives/ChooseObjectiveType",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveAdmin"},
				            {"action", "ChooseObjectiveType"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives/Create",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveAdmin"},
				            {"action", "Create"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives/{objectiveId}/Results",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveResultsAdmin"},
				            {"action", "List"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives/{objectiveId}/Results/ChooseTeam",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveResultsAdmin"},
				            {"action", "ChooseTeam"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Objectives/{objectiveId}/Results/Create",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "ObjectiveResultsAdmin"},
				            {"action", "Create"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Admin/Competition/Permissions",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "CompetitionPermissionsAdmin"},
				            {"action", "Index"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
                new RouteDescriptor {
		            Route = new Route(
			            "Scoreboard",
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"},
				            {"controller", "Scoreboard"},
				            {"action", "Index"}
			            },
			            new RouteValueDictionary(),
			            new RouteValueDictionary {
				            {"area", "DeSjoerd.Competition"}
			            },
			            new MvcRouteHandler())
	            },
            };
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }
    }
}