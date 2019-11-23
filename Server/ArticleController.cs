using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Server
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("create")]
        public void CreateArticle(Article article)
        {
            _articleService.Articles.Add(article);
        }

        [HttpGet("list")]
        public List<Article> GetArticles()
        {
            return _articleService.Articles;
        }
    }
}