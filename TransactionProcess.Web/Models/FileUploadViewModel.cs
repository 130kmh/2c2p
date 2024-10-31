using System.ComponentModel.DataAnnotations;

namespace TransactionProcess.Web.Models
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Please select a file to upload.")]
        [Display(Name = "Transaction File")]
        public IFormFile File { get; set; }

        [Display(Name = "File Type")]
        public string FileType => Path.GetExtension(File?.FileName)?.ToLowerInvariant();

        public bool IsValidFileType => FileType == ".csv" || FileType == ".xml";

        [Range(typeof(bool), "true", "true", ErrorMessage = "The file must be either CSV or XML format.")]
        public bool ValidFileType => IsValidFileType;
    }
}
