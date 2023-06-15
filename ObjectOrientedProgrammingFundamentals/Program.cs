/* ---------- LAB 01 - DEYNNI ALMAZAN --------------------------------*/

using System;
using System.Collections.Generic;


VendingMachine vendingMachine = new VendingMachine(123);

// ADD PRODUCTS
Product beans = new Product("Jelly Beans", 2, "A12");
Product chips = new Product("Potato Chips", 75, "B34");

// ADD STOCK
vendingMachine.StockItem(beans, 3);
vendingMachine.StockItem(chips, 5);

// ADD MONEY
vendingMachine.StockFloat(1, 4); // Add four $1 coins to the float


// SELL  PRODUCTS
List<int> money = new List<int> { 5 }; // Insert a $5 bill
string vendResult = vendingMachine.VendItem("A12", money);
Console.WriteLine(vendResult);


// CLASS
public class VendingMachine
{
    public int SerialNumber { get; }
    public Dictionary<int, int> MoneyFloat { get; }
    public Dictionary<Product, int> Inventory { get; }

    public VendingMachine(int serialNumber)
    {
        SerialNumber = serialNumber;
        MoneyFloat = new Dictionary<int, int>();
        Inventory = new Dictionary<Product, int>();
    }

    public string StockItem(Product product, int quantity)
    {
        if (Inventory.ContainsKey(product))
            Inventory[product] += quantity;
        else
            Inventory[product] = quantity;

        return $"Product '{product.Name}', code '{product.Code}', price: {product.Price:C}, new quantity: {Inventory[product]}";
    }

    public string StockFloat(int moneyDenomination, int quantity)
    {
        if (MoneyFloat.ContainsKey(moneyDenomination))
            MoneyFloat[moneyDenomination] += quantity;
        else
            MoneyFloat[moneyDenomination] = quantity;

        string floatStock = "Float inventory: ";
        foreach (var kvp in MoneyFloat)
        {
            floatStock += $"{kvp.Key}: {kvp.Value}, ";
        }

        return floatStock.TrimEnd(',', ' ');
    }

    public string VendItem(string code, List<int> money)
    {
        foreach (var kvp in Inventory)
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
                    return $"Error: Insufficient money provided for '{product.Name}'. Price: {product.Price:C}";

                int change = totalMoney - totalPrice;
                if (change < 0)
                    return $"Error: Insufficient money provided for '{product.Name}'. Price: {product.Price:C}";

                // Check if the machine has enough change
                if (!HasSufficientChange(change))
                    return $"Error: Unable to provide change for '{product.Name}'. Please insert exact amount.";

                // Update inventory
                Inventory[product]--;
                UpdateMoneyFloat(money, change);

                return $"Please enjoy your '{product.Name}' and take your change of {change:C}";
            }
        }

        return $"Error: No item with code '{code}'.";
    }

    private bool HasSufficientChange(int amount)
    {
        foreach (var kvp in MoneyFloat)
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

    private void UpdateMoneyFloat(List<int> money, int change)
    {
        foreach (int denomination in money)
        {
            if (MoneyFloat.ContainsKey(denomination))
                MoneyFloat[denomination]++;
            else
                MoneyFloat[denomination] = 1;
        }

        // Deduct change from money float
        foreach (var kvp in MoneyFloat)
        {
            int denomination = kvp.Key;
            int quantity = kvp.Value;

            int numCoins = change / denomination;
            if (numCoins > quantity)
                numCoins = quantity;

            change -= numCoins * denomination;
            MoneyFloat[denomination] -= numCoins;
        }
    }
}

public class Product
{
    public string Name
    { get; }
    public int Price { get; }
    public string Code { get; }

    public Product(string name, int price, string code)
    {
        Name = name;
        Price = price;
        Code = code;
    }
}
