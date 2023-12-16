﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Like
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public int VideoID { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }



        // Relationship
        public Posts Post { get; set; }
        public User User { get; set; }
        public Video Video { get; set; }
    }
}
