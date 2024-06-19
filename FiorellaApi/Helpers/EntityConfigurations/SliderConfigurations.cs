using System;
using FiorellaApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorellaApi.Helpers.EntityConfigurations
{
	public class SliderConfigurations
	{
        public void Configure(EntityTypeBuilder<Slider> builder)
        { 

            builder.Property(e => e.Image).IsRequired();

        }
    }
}

