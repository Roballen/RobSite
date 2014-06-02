using System;
using System.Web;
using oAuthTwitterWrapper.Configuration;

namespace RobSite.Helpers
{
    public class Twitter
    {
    
        private OAuthConfiguration _authconfig;
        private TwitterConfiguration _twitConfig;

        public Twitter(string secret, string token)
        {
            _authconfig = new OAuthConfiguration()
            {
                ConsumerToken = token,
                ConsumerSecret = secret,
                AuthUrl = "https://api.twitter.com/oauth2/token"
            };
            _twitConfig = new TwitterConfiguration()
            {
                ScreenName = "RobAllen_III",
                //Count = "4"
            };
        }

        public string ProcessRequest()
        {
            var responseString = "";
            try
            {
                //MapRequestParamsToConfigs();
                var oAuthTwitterWrapper = new OAuthTwitterWrapper.OAuthTwitterWrapper(_authconfig, _twitConfig);
    
                switch (HttpContext.Current.Request["url"])
                {
                    case "search":
                        responseString = oAuthTwitterWrapper.GetSearch();
                        break;
                    case "list":
                        responseString = oAuthTwitterWrapper.GetStatusList();
                        break;
                    default:
                        responseString = oAuthTwitterWrapper.GetMyTimeline();
                        break;
                }
            }
            catch (Exception)
            {
                
            }

            return responseString;
        }
    
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    
    
        private void MapRequestParamsToConfigs()
        {
            if (HttpContext.Current.Request["count"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["count"]))
                _twitConfig.Count = HttpContext.Current.Request["count"];
    
            if (HttpContext.Current.Request["include_rts"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["include_rts"]))
                _twitConfig.IncludeRts = HttpContext.Current.Request["include_rts"];
    
            if (HttpContext.Current.Request["exclude_replies"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["exclude_replies"]))
                _twitConfig.ExcludeReplies = HttpContext.Current.Request["exclude_replies"];
    
            if (HttpContext.Current.Request["screen_name"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["screen_name"]))
                _twitConfig.ScreenName = HttpContext.Current.Request["screen_name"];
    
            if (HttpContext.Current.Request["list_id"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["list_id"]))
                _twitConfig.ListId = HttpContext.Current.Request["list_id"];
    
            if (HttpContext.Current.Request["q"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request["q"]))
                _twitConfig.SearchQuery = HttpContext.Current.Request["q"];
        }
    }
}