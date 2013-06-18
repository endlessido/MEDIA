using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IGoodyService
    {
        List<Goody> GetGoodysByProjectId(int projectId);
        Goody GetGoodyById(int gId);
        List<Goody> GetGoodysByIds(string goodyIds);
        decimal? GetSumPrice(string goodyIds);
    }
}
