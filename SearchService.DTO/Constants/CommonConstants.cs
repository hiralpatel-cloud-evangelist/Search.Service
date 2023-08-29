using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DTO.Constants
{
    public class SearchParameters
    {
        public const string PageSize = "PageSize";
        public const string UserId = "user_id";
        public const string IsFromDevice = "IsFromDevice";
        public const string ScrollId = "ScrollId";
        public const string ShowOnlyActive = "ShowOnlyActive";
        public const string ShowShared = "ShowShared";
        public const string ShowMy = "ShowMy";
        public const string ShowAll = "ShowAll";
        public const string ModifiedAfter = "ModifiedAfter";
        public const string RequiredFields = "RequiredFields";
        public const string Filters = "Filters";
        public const string ContinuationToken = "ContinuationToken";
        public const string SortOrder = "SortOrder";
        public const string SortColumn = "SortColumn";
        public const string SearchText = "SearchText";
        public const string PageStart = "Page";
        public const string Conjuction = "Conjuction";
    }


    public enum Status
    {
        Active = 1,
        Inactive = 2,
        Delete = 3
    }

    public class CommonConstants
    {
        //public const string MakeId = "make_id";

        public const string PostName = "PostName";
        public const string PostDescription = "PostDescription";
        public const string LastModifiedDatetime = "LastModifiedDatetime";
        public const string DocumentsFileTypesRegex = (@"(.*?)\.(doc|DOC|docx|DOCX|xls|XLS|xlsx|XLSX|pptx|PPTX|txt|TXT|pdf|PDF|png|PNG|jpg|JPG|jpeg|JPEG|csv|CSV)$");
        public const string ImageFileRegex = (@"(.*?)\.(jpg|JPG|jpeg|JPEG|png|PNG|Jfif)$");
        public const Int64 FileSize = 5000;

        public const string Asc = "asc";
        public const string Desc = "desc";

        public static string NotFoundMessage = "The requested service is not available";
        public static string PostNotFoundMessage = "The requested post is not present";
        public static string SuccessCreatedMessage = "Created successfully";
        public static string SuccessUpdatedMessage = "Updated successfully";
        public static string SuccessDeletedMessage = "Deleted successfully";

        public static string FileNotValidErrorMessage = "Only Images are allowed";
    }
}

