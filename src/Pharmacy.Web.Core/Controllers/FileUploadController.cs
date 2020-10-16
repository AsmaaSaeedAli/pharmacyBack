using Abp.AspNetCore.Mvc.Authorization;
using Abp.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Url;
using Shared.Guard;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Pharmacy.Web.Controllers
{
    [AbpMvcAuthorize]
    public class FileUploadController : PharmacyControllerBase
    {
        private readonly IAppFolders _appFolders;
        private readonly IWebUrlService _webUrlService;

        public FileUploadController(IAppFolders appFolders, IWebUrlService webUrlService)
        {
            _appFolders = appFolders;
            _webUrlService = webUrlService;
        }

        public string GetName()
        {
            return "TEST";
        }
        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            try
            {
                var files = Request.Form.Files;

                //Check input
                if (files == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                var uploadResults = new List<UploadResult>();

                foreach (var file in files)
                {
                    var fileName = await UploadFileAsync(file);
                    uploadResults.Add(fileName);
                }

                return Json(new AjaxResponse(uploadResults));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        [HttpPost]
        public async Task<UploadResult> UploadFileAsync()
        {
            var files = Request.Form.Files;

            //Check input
            if (files == null)
            {
                throw new UserFriendlyException(L("File_Empty_Error"));
            }

            return await UploadFileAsync(files[0]);
        }

        [HttpPost]
        public async Task<UploadResult> UploadBase64File([FromBody]string imageBase64)
        {
            Guard.AssertArgumentNotNullOrEmptyOrWhitespace(imageBase64, nameof(imageBase64));
            return await UploadFileAsync(Base64ToByteArray(imageBase64), ".jpg");
        }

        [HttpPost]
        public JsonResult RemoveFile(string fileName)
        {
            try
            {
                //Check input
                if (fileName.IsNullOrWhiteSpace())
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                FileSystemHelper.DeleteEvenIfReadOnly(GetFilePath(fileName));

                return Json(new AjaxResponse());
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }



        private byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
        private string GetFilePath(string fileName)
        {
            return Path.Combine(_appFolders.TempFileDownloadFolder, fileName);
        }

        private async Task<UploadResult> UploadFileAsync(IFormFile formFile)
        {
            Guard.AssertArgumentNotLessThanOrEqualToZero(formFile.Length, nameof(formFile));
            var fileBytes = await ConvertFormFileToBytes(formFile);
            var extension = formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
            return await UploadFileAsync(fileBytes, extension);
        }


        private async Task<UploadResult> UploadFileAsync(byte[] bytes, string extension)
        {
            var fileName = $"{Guid.NewGuid().ToString()}" + "." + extension;
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, fileName);

            int idx = filePath.IndexOf(PharmacyConsts.Wwwroot, StringComparison.Ordinal);
            var tempFilePath = filePath.Substring(idx + PharmacyConsts.Wwwroot.Length + 1);

            var virtualPath = Path.Combine(_webUrlService.GetServerRootAddress().TrimEnd('/'), tempFilePath);

            var localPath = new Uri(filePath).LocalPath;
            await FileSystemHelper.WriteFileAsync(bytes, localPath);

            return new UploadResult { FileName = fileName, VirtualPath = virtualPath, Size = bytes.Length };
        }

        private async Task<byte[]> ConvertFormFileToBytes(IFormFile formFile)
        {
            byte[] bytes = null;
            if (formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await formFile.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }
            }
            return bytes;
        }
        public class UploadResult
        {
            public string FileName { get; set; }
            public string VirtualPath { get; set; }
            public decimal? Size { get; set; }
        }


    }
}
