using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;

namespace MEDIA.IBLL
{
    public interface IDonationProjectService
    {
        bool Add(DonationProject newModel,string goodyName, string goodyDesc, string goodyNum, string goodyPrice, string ddlCurrency);
        bool ReceiveFunding(string goodyIds, int projectId, int userId, string orderId);
        bool ReceiveFunding(decimal currency, string orderID,out int? projectId);
        DonationProject GetModelById(int projectId);
        List<DonationProject> GetNewlyProject();
        List<DonationProject> GetOnGoingProject();
        List<DonationProject> GetEndingProject();
        List<DonationProject> GetCheckProject();
        List<DonationProject> GetAllProject();
        List<DonationProject> GetRecentRandomProject(int count);
        List<DonationProject> GetSearchList(string serachName, string categoryName, string regionName);
        int GetEndedCount();
        int GetGoingCount();
        List<DonationProject> GetFoundedProjectsByUserId(int userId);
        bool IsProjectNameExist(string projectName);

    }
}
