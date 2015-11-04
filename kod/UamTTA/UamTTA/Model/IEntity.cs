using System;

namespace UamTTA
{
    public interface IEntity : ICloneable
    {
        int? Id { get; set; }
    }
}