using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Data.DAO
{
    public class MembershipTypeDAO
    {
        private ABFDbContext _context;

        public MembershipTypeDAO()
        {
            _context = new ABFDbContext();
        }

        public IList<MembershipType> GetMembershipTypes()
        {
            IQueryable<MembershipType> membershipTypes;

            membershipTypes = from membershipType
                              in _context.MembershipTypes
                              select membershipType;

            return membershipTypes.ToList();
        }

        public MembershipType GetMembershipType(int id)
        {
            IQueryable<MembershipType> _memtype = 
                from m
                in _context.MembershipTypes
                where m.Id == id
                select m;

            var memtype = _memtype.First();

            return memtype;
        }
    }
}
