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

        // checks to see if a row exists in the images database
        // if it dosen't, add one, increment by 1 and return
        public int GetNewImageId()
        {
            if (!_context.Images.Any())
            {
                Image i = new Image
                {
                    Id = 1,
                    ImagePath = "Intialiser"
                };

                _context.Images.Add(i);
                _context.SaveChanges();
            };

            var image = _context.Images
                .OrderByDescending(i => i.Id)
                .First();

            int newImageId = image.Id + 1;

            return newImageId;
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

        public void DeleteImage(Image image)
        {
            _context.Images.Remove(image);
            _context.SaveChanges();
        }
    }
}
