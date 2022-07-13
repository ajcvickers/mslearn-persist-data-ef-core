using ContosoPizza.Models;

namespace ContosoPizza.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(PizzaContext context)
    {
        var pepperoniTopping = new Topping("Pepperoni", calories: 130);
        var sausageTopping = new Topping("Sausage", calories: 100);
        var hamTopping = new Topping("Ham", calories: 70);
        var chickenTopping = new Topping("Chicken", calories: 50);
        var pineappleTopping = new Topping("Pineapple", calories: 75);

        var tomatoSauce = new Sauce("Tomato", isVegan: true);
        var alfredoSauce = new Sauce("Alfredo", isVegan: false);

        var pizzas = new Pizza[]
        {
            new("Meat Lovers", tomatoSauce)
            {
                Toppings =
                {
                    pepperoniTopping,
                    sausageTopping,
                    hamTopping,
                    chickenTopping
                }
            },
            new("Hawaiian", tomatoSauce)
            {
                Toppings =
                {
                    pineappleTopping,
                    hamTopping
                }
            },
            new("Alfredo Chicken", alfredoSauce)
            {
                Toppings =
                {
                    chickenTopping
                }
            }
        };

        context.Pizzas.AddRange(pizzas);
        await context.SaveChangesAsync();
    }
}
