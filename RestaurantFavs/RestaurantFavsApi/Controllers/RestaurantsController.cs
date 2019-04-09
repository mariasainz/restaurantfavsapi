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
            var restaurants = Favorites.Restaurants;
            if (!restaurants.Any())
            {
                return NotFound();
            }

            return Ok(Favorites.Restaurants);
        }

        [HttpGet("{id}", Name = "RestaurantById")]
        public ActionResult<RestaurantData> GetRestaurantById(Guid id)
        {
            var restaurant = Favorites.Restaurants.Where(x => x.Id == id).FirstOrDefault();
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public IActionResult CreateRestaurant(RestaurantData restaurantData)
        {
            if (restaurantData == null || string.IsNullOrEmpty(restaurantData.Name) || string.IsNullOrEmpty(restaurantData.FoodType))
            {
                return BadRequest();
            }

            restaurantData.Id = Guid.NewGuid();

            Favorites.Restaurants.Add(restaurantData);

            return this.CreatedAtRoute(
                routeName: "RestaurantById",
                routeValues: new { id = restaurantData.Id },
                value: restaurantData);
        }

        [HttpPut("{id}")]
        public ActionResult<RestaurantData> UpdateRestaurant(Guid id, RestaurantData restaurantData)
        {
            if (restaurantData == null || string.IsNullOrEmpty(restaurantData.Name) || string.IsNullOrEmpty(restaurantData.FoodType))
            {
                return BadRequest();
            }

            var restaurant = Favorites.Restaurants.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if (restaurant == null)
            {
                return NotFound();
            }

            restaurant.Name = restaurantData.Name;
            restaurant.FoodType = restaurantData.FoodType;
            return restaurant;

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(Guid id)
        {
            var restaurant = Favorites.Restaurants.Where(x => x.Id.Equals(id)).FirstOrDefault();
            if (restaurant == null)
            {
                return NotFound();
            }
            Favorites.Restaurants.Remove(restaurant);
            return Ok();
        }


    }
}