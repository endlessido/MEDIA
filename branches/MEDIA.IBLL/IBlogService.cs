using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IBlogService
    {
        bool Add(Blog model);
        bool Del(int Id);
        bool DelBlogsByProjectId(int projectId, string imgPath);
        bool Modify(Blog model);
        List<Blog> GetBlogsByProjectId(int projectId);
        Blog GetModel(int Id);
        List<Blog> UnCheckBlogs();
    }
}
