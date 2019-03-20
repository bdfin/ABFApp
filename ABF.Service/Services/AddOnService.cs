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

        public AddOn GetAddOn(int id)
        {
            return addOnDAO.GetAddOn(id);
        }

        public IList<AddOn> GetEventAddOns(int id)
        {
            return addOnDAO.GetEventAddOns(id);
        }

        public void SaveAddOn(AddOn addOn)
        {
            addOnDAO.SaveAddOn(addOn);
        }

        public void DeleteAddOn(AddOn addOn)
        {
            addOnDAO.DeleteAddOn(addOn);
        }
    }
}
