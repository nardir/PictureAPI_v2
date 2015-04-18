using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AOL;
using AOL.Model;
using AOL.Repository;
using System.Threading.Tasks;
using AOL.RESTAPI.Models;

namespace AOL.RESTAPI.Controllers
{
    [RoutePrefix("api/v1/articles")]
    public class ArticleController : ApiController
    {
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetArticles()
        {
            try
            {
                IEnumerable<ArticleDTO> articles = null;

                using(IAOLRepository repo = new AOLRepository())
                {
                    articles = await repo.GetArticlesAsync().ContinueWith<IEnumerable<ArticleDTO>>(t =>
                    {
                        return t.Result.Select(a => new ArticleDTO 
                        { 
                            ArticleKey = a.ArticleKey, 
                            Code = a.Code, 
                            Description = a.Description, 
                            LongDescription = a.LongDescription, 
                            State = a.State 
                        });
                    });
                }

                return Ok(articles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{code}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetArticle(string code)
        {
            try
            {
                ArticleDTO articleDTO;

                using (IAOLRepository repo = new AOLRepository())
                {
                    articleDTO = await repo.GetArticleAsync(code).ContinueWith<ArticleDTO>(t =>
                    {
                        var a = t.Result;

                        if (!(a==null))
                        {
                            return new ArticleDTO
                            {
                                ArticleKey = a.ArticleKey,
                                Code = a.Code,
                                Description = a.Description,
                                LongDescription = a.LongDescription,
                                State = a.State
                            };
                        }

                        return null;
                    });
                }

                if (articleDTO != null)
                    return Ok(articleDTO);
                else
                    return NotFound();
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("images")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateImage()
        {
            return BadRequest();
        }
    }
}
