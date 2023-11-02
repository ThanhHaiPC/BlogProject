﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Entities
{
    public class Follow
    {
        //Người được theo dõi
        public Guid FollowerId { get; set; }

        //Người theo dõi
        public Guid FolloweeId { get; set; }
        public DateTime? Date { get; set; }

        public AppUser Follower { get; set; }
        public AppUser Followee { get; set; }
    }
}
