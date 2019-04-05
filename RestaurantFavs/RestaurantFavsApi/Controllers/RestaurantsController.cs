using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFavsApi.Contracts;


namespace RestaurantFavsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
      

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantData>> GetRestaurantsList()
        {
            return Ok(Favorites.Restaurants);
        }

        [HttpPost]
        public void CreateRestaurant(RestaurantData restaurantData)
        {
            Favorites.Restaurants.Add(restaurantData);
        }

        [HttpPut("{id}")]
        public RestaurantData UpdateRestaurant(Guid id, RestaurantData data)
        {
            var restaurant = Favorites.Restaurants.Where(x => x.Id.Equals(id)).FirstOrDefault();
            restaurant.Name = "ThisNameChange";

            return restaurant;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(Guid id)
        {
            var restaurant = Favorites.Restaurants.Where(x => x.Id.Equals(id)).FirstOrDefault();
           Favorites.Restaurants.Remove(restaurant);
            return Ok();
        }


    }
}