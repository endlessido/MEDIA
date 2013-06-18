using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MEDIA.Model;
using MEDIA.BLL.Entity;

namespace MEDIA.IBLL
{
    public interface IDonateRecordService
    {
        List<DonatedProjectBusiEntity> GetDonationProsByUserID(int userId);
        bool Add(DonateRecord model);
        int GetGoodyDonatedCount();
        List<Goody> GetGoodyDonated();
        List<DonatedRecordBusiEntity> GetDonatorByGoodieId(int gId);
        DonateRecord GetModelByIds(int uId, int gId);
        List<Goody> GetGoodybyOrderId(string orderId);
    }
}
