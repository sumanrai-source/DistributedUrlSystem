using Assigner.Domain.Premetives;
using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Assigner.Domain.Entities
{
    public class Slugs : Entity
    {
        public Slugs() : base(null)
        {

        }

        public Slugs(
            string id,
            string value,
            SlugStatus status,
            DateTime createdAt,
            DateTime? assignedAt
            ) : base(id)
        {
            Value = value;
            Status = status;
            CreatedAt = createdAt;
            AssignedAt = assignedAt;


        }

        public string Value { get; set; }
        public SlugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }


    }
}
