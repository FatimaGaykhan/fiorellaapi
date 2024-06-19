using System;
using FiorellaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorellaApi.Helpers.EntityConfigurations
{
	public class BlogConfigurations:IEntityTypeConfiguration<Blog>
	{

        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(e => e.Title).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.Image).IsRequired();

        }
    }
}

