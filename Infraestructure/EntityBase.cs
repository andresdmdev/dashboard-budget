namespace DomainModel
{
    public class EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }

        public static bool IsNullOrNew([System.Diagnostics.CodeAnalysis.NotNull] EntityBase? entity)
        {
            if (entity is null)
            {
                entity = new EntityBase();
                return true;
            }

            return false;
        }
    }
}
