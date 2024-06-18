using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Application.Uploads;
using TopShop.Implementation.Extensions;

namespace TopShop.Implementation.Uploads
{
    public class EfBase64FileUploader : IBase64FileUploader
    {
        private List<string> _allowedExtensions = new List<string>
        {
            "jpg", "png", "mp4"
        };
        public string GetExtenstion(string base64File)
        {
            return base64File.GetFileExtenstion();
        }

        public bool IsExtenstionValid(string base64File)
        {
            return _allowedExtensions.Contains(GetExtenstion(base64File));
        }

        public string Upload(string base64File, UploadType type)
        {
            if (!IsExtenstionValid(base64File))
            {
                throw new InvalidOperationException("Unspported file extension.");
            }

            string path = GetPath(type, GetExtenstion(base64File));

            System.IO.File.WriteAllBytes(path, Convert.FromBase64String(base64File));

            return path;
        }

        public IEnumerable<string> Upload(IEnumerable<string> files, UploadType type)
        {
            throw new NotImplementedException();
        }

        private string GetPath(UploadType type, string ext)
        {
            var path = _uploadPaths[type];

            var fileName = "";

            foreach (var pathItem in path)
            {
                fileName = Path.Combine(fileName, pathItem);
            }

            return Path.Combine(fileName, Guid.NewGuid().ToString() + "." + ext);
        }

        private Dictionary<UploadType, List<string>> _uploadPaths =
            new Dictionary<UploadType, List<string>>
            {
                { UploadType.User, new List<string> { "wwwroot", "images", "users" } },
                { UploadType.Product, new List<string> { "wwwroot", "images", "products" } },
            };
    }
}
