using System;

namespace RestaurantFavsApi.Contracts
{
    public class RestaurantData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FoodType { get; set; }
    }
}
