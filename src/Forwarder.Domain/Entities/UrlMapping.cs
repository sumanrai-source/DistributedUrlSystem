using Forwarder.Domain.Premetives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Domain.Entities
{
    public class UrlMapping : Entity
    {
        public UrlMapping() : base(null)
        {

        }

        public UrlMapping(
            string id,
            string slug,
            string destinationUrl,
            DateTime createdAt
            ) : base(id)
        {
            Slug = slug;
            DestinationUrl = destinationUrl;
            CreatedAt = createdAt;

        }

        public string Slug { get; set; }
        public string DestinationUrl { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
