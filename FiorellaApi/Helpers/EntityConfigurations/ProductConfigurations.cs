using System;
using FiorellaApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiorellaApi.Helpers.EntityConfigurations
{
	public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

            builder.Property(e => e.Price).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.Property(e => e.CategoryId).IsRequired();


        }
    }
}

