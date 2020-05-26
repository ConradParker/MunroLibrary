using MunroLibrary.Domain;

namespace MunroLibrary.Api.Dtos
{
    public class MunroQueryDto
    {
        public string[] SortFields { get; set; }
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
        public MunroType? MunroType { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxHeight { get; set; }

    }
}
