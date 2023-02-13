namespace NZwalks.APi.Models.DTO
{
    public class Updatewalkreq
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }  
    }
}
