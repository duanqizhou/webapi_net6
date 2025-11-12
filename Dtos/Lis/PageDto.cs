namespace webapi.Dtos.Lis
{
    public class SamSampleItemPageDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? SampleName { get; set; }
        public string? SampleCategory { get; set; }
    }
}
