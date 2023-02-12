﻿namespace NZwalks.APi.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        
        public Guid WalkDifficultyId { get; set; }

        //Nav properties


        public Region region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}