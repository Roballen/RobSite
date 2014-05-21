using System.Web.Mvc;
using Newtonsoft.Json;
using Umbraco.Web.Mvc;

namespace RobSite.controllers
{
    public class TwitterController : SurfaceController
    {
        // GET api/<controller>
        public ActionResult Get()
        {
            var current = Umbraco.Content(UmbracoContext.PageId);
            var settingsNode = current.AncestorOrSelf(1).Children.Where("Name == \"Settings\"").First();
            var tresp = new Helpers.Twitter(settingsNode.twittersecret, settingsNode.twittertoken).ProcessRequest();
            var dyno = JsonConvert.DeserializeObject<dynamic>(tresp);
            return View(dyno);
        }

    }
}
