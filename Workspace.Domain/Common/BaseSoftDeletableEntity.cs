namespace Workspace.Domain.Common
{
    public abstract class BaseSoftDeletableEntity : BaseEntity, ISoftDeletable
    {
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        protected BaseSoftDeletableEntity() : base()
        {
            IsDeleted = false;
        }
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}