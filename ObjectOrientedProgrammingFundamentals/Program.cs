/*------LAB 02 DEYNNI ALMAZAN ----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

VendingMachine vendingMachine = new VendingMachine();

// ADD PRODUCTS
Product beans = new Product("Jelly Beans", 2, "A12", "1234");
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
List<int> money = new List<int> { 20 }; // Insert $20 
string vendResult = vendingMachine.VendItem("A12", money);
Console.WriteLine(vendResult);
    
public class VendingMachine
{
    private static int serialNumberCounter = 1;

    public int SerialNumber { get; }
    private Dictionary<int, int> MoneyFloat { get; }
    private Dictionary<Product, int> Inventory { get; }

    public VendingMachine()
    {
        SerialNumber = serialNumberCounter++;
        MoneyFloat = new Dictionary<int, int>();
        Inventory = new Dictionary<Product, int>();
    }

    public string StockItem(Product product, int quantity)
    {
        if (Inventory.ContainsKey(product))
            Inventory[product] += quantity;
        else
            Inventory[product] = quantity;

        return
            $"Product '{product.Name}', code '{product.Code}', price: " +
            $"{product.Price:C}";
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
        foreach (KeyValuePair<Product, int> kvp in Inventory)
        {
            Product product = kvp.Key;
            int quantity = kvp.Value;

            if (product.Code == code)
            {
                if (quantity == 0)
                    return $"Error: Item '{product.Name}' is out of stock.";

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
                        $"Please insert a different amount.";

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
    public string Name { get; }
    public int Price { get; }
    public string Code { get; }
    public string Barcode { get; }

    public Product(string name, int price, string code, string barcode)
    {
        Name = name;
        Price = price;
        Code = code;
        Barcode = barcode;
    }
}

