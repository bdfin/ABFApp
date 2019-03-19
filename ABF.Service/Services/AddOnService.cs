using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.DAO;
using ABF.Data.ABFDbModels;

namespace ABF.Service.Services
{
    public class AddOnService
    {
        private AddOnDAO addOnDAO;

        public AddOnService()
        {
            addOnDAO = new AddOnDAO();
        }

        public IList<AddOn> GetAllAddOns()
        {
            return addOnDAO.GetAllAddOns();
        }

        public IList<AddOn> GetAddOnsForEvent(int id)
        {
            return addOnDAO.GetAddOnsForEvent(id);
        }

        public void SaveAddOn(AddOn addOn)
        {
            addOnDAO.SaveAddOn(addOn);
        }
    }
}
