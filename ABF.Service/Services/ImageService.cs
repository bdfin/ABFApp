using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using System.IO;
using System.Web.Hosting;

namespace ABF.Service.Services
{
    public class ImageService
    {
        private ImageDAO imageDAO;

        public ImageService()
        {
            imageDAO = new ImageDAO();
        }

        public int GetNewImageId()
        {
            return imageDAO.GetNewImageId();
        }

        public Image GetImage(int id)
        {
            return imageDAO.GetImage(id);
        }

        public void SaveImage(Image image)
        {
            string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            string fileExtension = Path.GetExtension(image.ImageFile.FileName).ToLower();

            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
            {
                fileName = fileName + DateTime.Now.ToString("yymssfff") + fileExtension;
                image.ImagePath = "~/Content/Images/" + fileName;
                fileName = Path.Combine(HostingEnvironment.MapPath("~/Content/Images/" + fileName));

                image.ImageFile.SaveAs(fileName);
                imageDAO.SaveImagePath(image);
            }
            
        }

        public void DeleteImage(Image image)
        {
            var imageToDelete = imageDAO.GetImage(image.Id);
            string imagePath = HostingEnvironment.MapPath(imageToDelete.ImagePath);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            } 

            imageDAO.DeleteImage(image);
        }
    }
}
