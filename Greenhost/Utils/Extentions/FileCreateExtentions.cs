namespace Greenhost.Utils.Extentions
{
    public static class FileCreateExtentions
    {
        public static string CreateFile(this IFormFile formFile, string webroot, string foldername)
        {
            string filename = "";
            if (formFile.FileName.Length > 64)
            {
                filename = Guid.NewGuid() + formFile.FileName.Substring(filename.Length - 64);
            }
            else
            {
                filename = Guid.NewGuid() + formFile.FileName;
            }
            string path = Path.Combine(filename, webroot, foldername);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return filename;
        }

        public static void RemoveFile(this string filename, string webroot, string Foldername)
        {
            string path = Path.Combine(webroot, filename, Foldername);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
 