using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Client
{
    public interface IArticlesApi
    {
        [Get("/api/article/list")]
        Task<List<Article>> GetArticlesList();
        [Post("/api/article/create")]
        Task CreateArticle(Article article);
    }
}