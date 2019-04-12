using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.DAO;
using ABF.Data.ABFDbModels;

namespace ABF.Service.Services
{
    public class MembershipTypeService
    {
        private MembershipTypeDAO membershipTypeDAO;

        public MembershipTypeService()
        {
            membershipTypeDAO = new MembershipTypeDAO();
        }

        public IList<MembershipType> GetMembershipTypes()
        {
            return membershipTypeDAO.GetMembershipTypes();
        }

        public MembershipType GetMembershipType(int id)
        {
            return membershipTypeDAO.GetMembershipType(id);
        }
    }
}
