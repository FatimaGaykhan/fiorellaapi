using System;
using FiorellaApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorellaApi.Helpers.EntityConfigurations
{
	public class SliderInfoConfigurations
	{

        public void Configure(EntityTypeBuilder<SliderInfo> builder)
        {
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Description).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Image).IsRequired();

        }
    }
}

