using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShop.Application.Uploads
{
    public enum UploadType
    {
        User,
        Product
    }
    public interface IBase64FileUploader
    {
        bool IsExtenstionValid(string base64File);
        string GetExtenstion(string base64File);
        string Upload(string base64File, UploadType type);
        IEnumerable<string> Upload(IEnumerable<string> files, UploadType type);
    }
}
