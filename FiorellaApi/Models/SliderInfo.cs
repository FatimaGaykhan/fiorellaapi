﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FiorellaApi.Models
{
	public class SliderInfo:BaseEntity
	{
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}

