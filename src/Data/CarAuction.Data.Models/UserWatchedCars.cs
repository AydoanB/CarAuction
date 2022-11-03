namespace CarAuction.Data.Models
{
    using System;
    using System.Collections.Generic;

    using CarAuction.Data.Common.Models;

    public class UserWatchedCars : BaseDeletableModel<string>
    {
        public UserWatchedCars()
        {
            this.Id = Guid.NewGuid().ToString();
            this.WatchedCars = new HashSet<Car>();
        }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Car> WatchedCars { get; set; }
    }
}
