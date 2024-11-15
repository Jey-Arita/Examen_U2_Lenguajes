﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PartidasContables.DataBase.Entities
{
    public class UserEntity : IdentityUser
    {
        [StringLength(70, MinimumLength = 3)]
        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(70, MinimumLength = 3)]
        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("refresh_token")]
        [StringLength(450)]
        public string RefreshToken { get; set; }

        [Column("refresh_token_expire")]
        public DateTime RefreshTokenExpire { get; set; }
    }
}
