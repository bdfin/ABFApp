using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Data.DAO
{
    public class ImageDAO
    {
        private ABFDbContext _context;

        public ImageDAO()
        {
            _context = new ABFDbContext();
        }

        public int GetNewImageId()
        {
            var image = _context.Images
                .OrderByDescending(i => i.Id)
                .First();

            int lastImageId = image.Id + 1;

            return lastImageId;
        }

        public Image GetImage(int id)
        {
            IQueryable<Image> images;

            images = from image
                     in _context.Images
                     where image.Id == id
                     select image;

            return images.ToList().First();
        }

        public void SaveImagePath(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
    }
}
