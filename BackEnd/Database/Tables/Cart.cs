﻿using System.ComponentModel.DataAnnotations;

namespace BackEnd.Database.Tables
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }

    }
}
