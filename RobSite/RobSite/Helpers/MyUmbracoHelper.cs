using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Helpers
{
    public class MyUmbracoHelper
    {
        public static IEnumerable<BlogEntry> GetRecentPosts(IPublishedContent content)
        {
            //all posts
            var posts = content.Children.Where("Name == \"Blog\"").First().Descendants("uBlogsyPost").OrderByDescending(x => x.GetPropertyValue<DateTime>("uBlogsyPostDate")).Take(8);
            return posts.Select(TranslateSearchResultToBlogEntry).ToList();
        }

        public static BlogEntry TranslateSearchResultToBlogEntry(IPublishedContent results)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            var post = new BlogEntry();
            post.Title = results.GetPropertyValue<string>("uBlogsyContentTitle");
            var auth = helper.Content(results.GetPropertyValue<string>("uBlogsyPostAuthor"));
            post.Author = auth.uBlogsyAuthorName;
            var date = results.GetPropertyValue<string>("uBlogsyPostDate");
            post.Date = DateTime.Parse(date).ToString("D");
            var image = results.GetPropertyValue<string>("uBlogsyPostImage");
            if (!string.IsNullOrEmpty(image))
            post.Image =  helper.Media(image).umbracoFile;
            post.Summary = results.GetPropertyValue<string>("uBlogsyContentSummary");
            post.Url = results.Url();
            return post;
        }
    }

    public class BlogEntry
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }

    }
}