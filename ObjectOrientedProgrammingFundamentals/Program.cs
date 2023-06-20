/*------LAB 03 DEYNNI ALMAZAN ----------------------------------------------*/
//Adding proper error handling

using System;
using System.Collections.Generic;
using System.Linq;

try
{

    VendingMachine vendingMachine = new VendingMachine();

    // ADD PRODUCTS
    Product beans = new Product("Jelly Beans", 2, "A12", "78963");
    Product chocolate = new Product("Hershey Chocolate", 2, "A13", "5678");
    Product chips = new Product("Potato Chips", 12, "B34", "9876");

    // ADD STOCK
    Console.WriteLine(vendingMachine.StockItem(beans, 3));
    Console.WriteLine(vendingMachine.StockItem(chocolate, 10));
    Console.WriteLine(vendingMachine.StockItem(chips, 5));

    Dictionary<int, int> coins = new Dictionary<int, int>
    {
        { 1, 4 },    // Add 4 coins of $1
        { 2, 10 },   // Add 10 coins of $2
        { 5, 5 },    // Add 5 coins of $5
        { 10, 3 },   // Add 3 coins of $10
        { 20, 2 }    // Add 2 coins of $20
    };

    Console.WriteLine(vendingMachine.StockFloat(coins));


    // SELL PRODUCTS
    List<int> money = new List<int> { 10 }; // Insert $20 
    string vendResult = vendingMachine.VendItem("A12", money);
    Console.WriteLine(vendResult);

} catch(Exception ex)
    { Console.WriteLine(ex.Message); }

    
public class VendingMachine
{
    private static int serialNumberCounter = 1;

    public int SerialNumber { get; }
    private Dictionary<int, int> MoneyFloat { get; }
    private Dictionary<Product, int> Inventory { get; }

    public VendingMachine()
    {
        try
        {
            SerialNumber = serialNumberCounter++;
            MoneyFloat = new Dictionary<int, int>();
            Inventory = new Dictionary<Product, int>();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public string StockItem(Product product, int quantity)
    {
        try
        {
            if(quantity < 0)
            {
                throw new ArgumentOutOfRangeException("Please enter a valid quantity." +
                    " It cannot be negative");
            }

            if (Inventory.ContainsKey(product))
                Inventory[product] += quantity;
            else
                Inventory[product] = quantity;

            return
                $"Product '{product.Name}', code '{product.Code}', price: " +
                $"{product.Price:C}";

        } catch(Exception ex) 
        { 
            return ex.Message;
        }
    }

    public string StockFloat(Dictionary<int, int> coins)
    {
       foreach (KeyValuePair<int, int> coin in coins)
        {
            int denomination = coin.Key;
            int quantity = coin.Value;

            if (MoneyFloat.ContainsKey(denomination))
                MoneyFloat[denomination] += quantity;
            else
                MoneyFloat[denomination] = quantity;
        }

        return string.Empty;

    }

    public string VendItem(string code, List<int> money)
    {
        try
        {
            if(string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("Code cannot be empty. Please" +
                    " enter a valid code.");
            }

        } catch(ArgumentNullException ex)
        {
            return ex.Message;
        }

        foreach (KeyValuePair<Product, int> kvp in Inventory)
        {
            Product product = kvp.Key;
            int quantity = kvp.Value;

            if (product.Code == code)
            {
                if (quantity == 0)
                    return $"Error: Item '{product.Name}' is out of stock.";

                foreach (int amount in money)
                    
                if (amount < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid money" +
                        " provided. Amount cannot be negative");
                }
                int totalPrice = product.Price;
                int totalMoney = money.Sum();

                if (totalMoney < totalPrice)
                    return
                        $"Error: Insufficient money provided for '{product.Name}'. " +
                        $"Price: {product.Price:C}";

                int change = totalMoney - totalPrice;
                if (change < 0)
                    return
                        $"Error: Insufficient money provided for '{product.Name}'. " +
                        $"Price: {product.Price:C}";

                // Check if the machine has enough change
                if (!HasSufficientChange(change))
                    return
                        $"Error: Unable to provide change for '{product.Name}'. " +
                        $" Please insert a different amount.";

                // Update inventory
                Inventory[product]--;
                Dictionary<int, int> changeDenominations = UpdateMoneyFloat(change);

                string changeString = "Change: ";
                foreach (KeyValuePair<int, int> denomination in changeDenominations)
                {
                    changeString += $"{denomination.Value} coins of ${denomination.Key}, ";
                }
                changeString = changeString.TrimEnd(',', ' ');

                return $"Please enjoy your '{product.Name}'. {changeString}";

            }
        }

        return $"Error: No item with code '{code}'.";
    }

    private bool HasSufficientChange(int amount)
    {
        foreach (KeyValuePair<int, int> kvp in MoneyFloat)
        {
            int denomination = kvp.Key;
            int quantity = kvp.Value;

            int numCoins = amount / denomination;
            if (numCoins > quantity)
                numCoins = quantity;

            amount -= numCoins * denomination;
        }

        return amount == 0;
    }

    private Dictionary<int, int> UpdateMoneyFloat(int change)
    {
        Dictionary<int, int> changeDenominations = new Dictionary<int, int>();

        foreach (int denomination in MoneyFloat.Keys.OrderByDescending(x => x))
        {
            int quantity = MoneyFloat[denomination];

            int numCoins = change / denomination;
            if (numCoins > quantity)
                numCoins = quantity;

            change -= numCoins * denomination;
            MoneyFloat[denomination] -= numCoins;

            if (numCoins > 0)
                changeDenominations[denomination] = numCoins;

            if (change == 0)
                break;
        }

        return changeDenominations;
    }
}
public class Product
{
    private int _price;
    private string? _name;
    private string? _code;
    private string _barcode;

    public int Price
    {
        get { return _price; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Product price cannot be zero or negative");
            }
            _price = value;
        }
    }

    public string Name
    {
        get { return _name!; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The name of the product is null or empty");
            }
            _name = value;
        }
    }

    public string Code
    {
        get { return _code!; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The product code is null or empty");
            }
            _code = value;
        }
    }

    public string Barcode
    {
        get { return _barcode; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The product barcode is null or empty");
            }
            _barcode = value;
        }
    }

    public Product(string name, int price, string code, string barcode)
    {
            Price = price;
            Name = name;
            Code = code;
            Barcode = barcode;
    }
}
