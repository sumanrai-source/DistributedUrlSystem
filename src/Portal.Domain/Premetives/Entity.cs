using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Domain.Premetives
{
    public abstract class Entity
    {
        protected Entity(string id) => Id = id;
        public string Id { get; set; }
    }
}
