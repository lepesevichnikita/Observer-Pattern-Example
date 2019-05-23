using System;
using System.Collections.Generic;

namespace Observer_Pattern_Example
{
  public interface INotifyer
  {
    string name();
    string whatsNew();
    void subscribe(ISubscriber subscriber);
  }

  public interface ISubscriber
  {
    void notify(INotifyer notifyer);
  }

  public class AppleStore: INotifyer
  {
    private List<ISubscriber> subscribers = new List<ISubscriber>();
    private List<string> itemsNames = new List<string>();

    private string lastAddedItemName;
    private static string STORE_NAME = "AppleStore";

    public string name() {
      return STORE_NAME; 
    }

    public string whatsNew() {
      return lastAddedItemName;
    }
    
    public void subscribe(ISubscriber subscriber) {
      subscribers.Add(subscriber);
    }

    public void addItem(string itemName) {
      lastAddedItemName = itemName;
      itemsNames.Add(itemName);
      foreach (ISubscriber subscriber in subscribers)
      {
        subscriber.notify(this);
      }
    }


  }

  public class Customer: ISubscriber
  {
    private List<string> missedNews = new List<string>();

    public void notify(INotifyer notifyer) {
      missedNews.Add("Новый продукт в " + notifyer.name() + ": " + notifyer.whatsNew());
    }

    public void showMissedNews() {
      Console.WriteLine("Уведомления:");
      foreach (string missedNew in missedNews)
      {
        Console.WriteLine("\t" + missedNew);
      }
    }
  }

  class Program
  {
      static void Main(string[] args)
      {
        AppleStore appleStore = new AppleStore();
        Customer customer = new Customer();
        appleStore.subscribe(customer);
        appleStore.addItem("IPhone X");
        appleStore.addItem("IPhone XE");
        appleStore.addItem("IPhone XS");
        customer.showMissedNews();
      }
  }
}
