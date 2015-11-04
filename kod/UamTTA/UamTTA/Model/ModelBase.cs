namespace UamTTA
{
    public abstract class ModelBase : IEntity
    {
        public bool IsTransient => Id.HasValue == false;

        public int? Id { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}