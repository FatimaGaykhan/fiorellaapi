using System;
using FiorellaApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorellaApi.Helpers.EntityConfigurations
{
	public class ExpertConfigurations
	{
            public void Configure(EntityTypeBuilder<Expert> builder)
            {
                builder.Property(e => e.FullName).IsRequired().HasMaxLength(200);

                builder.Property(e => e.Image).IsRequired();

                builder.Property(e => e.Position).IsRequired().HasMaxLength(200);

            }
	}
}

