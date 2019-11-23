using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly List<Article> _articles;

        public ArticleController()
        {
            _articles = new List<Article>();
        }

        [HttpPost("create")]
        public void CreateArticle(Article article)
        {
            _articles.Add(article);
        }

        [HttpGet("list")]
        public List<Article> GetArticles()
        {
            return _articles;
        }
    }
}