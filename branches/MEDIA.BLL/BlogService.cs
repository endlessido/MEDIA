using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.IBLL;
using MEDIA.IDAL;
using MEDIA.Infrastructure.Ioc;
using Microsoft.Practices.Unity;
using MEDIA.Model;


namespace MEDIA.BLL
{
    public class BlogService : IBlogService
    {
        public bool Add(Model.Blog model)
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            return iBlogRepos.Add(model, false) > 0;
        }

        public bool Del(int Id)
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            return iBlogRepos.Delete(model => model.blogId == Id) > 0;
        }

        public bool DelBlogsByProjectId(int projectId,string imgPath)
        {
            try
            {
                IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
                List<Blog> result = iBlogRepos.Find(m => m.ProjectId == projectId).ToList();
                foreach (Blog item in result)
                {
                    System.IO.File.Delete(string.Format("{0}{1}", imgPath, item.ImgUrl));
                }
                return iBlogRepos.Delete(model => model.ProjectId == projectId) > 0;
            }
            catch {
                return false;
            }
        }

        public bool Modify(Blog model)
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            Blog oldModel = iBlogRepos.GetSingle(m => m.blogId == model.blogId);
            oldModel.Content = model.Content;
            return iBlogRepos.Update(model, false) > 0;
        }

        public List<Blog> GetBlogsByProjectId(int projectId)
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            return iBlogRepos.Find(m => m.ProjectId == projectId).OrderByDescending(m=>m.CreateDate).ToList();
        }

        public Blog GetModel(int Id)
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            return iBlogRepos.GetSingle(m => m.blogId == Id);
        }


        public List<Blog> UnCheckBlogs()
        {
            IBlogRepository iBlogRepos = IoCContext.Container.Resolve<IBlogRepository>();
            return iBlogRepos.Find(m => m.IsCheck == false).ToList();
        }
    }
}
