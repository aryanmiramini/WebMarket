﻿using System.ComponentModel.DataAnnotations;

namespace WebMarket.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="نام کمپانی")]
        public string Name { get; set; }
        [Display(Name = "آدرس خیابان")]
        public string? StreetAddress { get; set; }
        [Display(Name = "نام شهر")]
        public string? City { get; set; }
        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }
    }
}
