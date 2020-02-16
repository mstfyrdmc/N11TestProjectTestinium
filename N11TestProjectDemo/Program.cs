using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N11TestProjectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Checking checking = new Checking(); //Sınıfların instanceları alındı.
            Methods methods = new Methods();
            try
            {
               methods.GetLogin();       //login olma methodu
               methods.SearchProduct();  //Ürün arama methodu
               methods.GoToSecondPage(); //2.sayfaya geçme methodu
               methods.SelectProduct();  //Ürün Seçme methodu
               methods.AddToCart();      //Sepete ürün ekleme methodu
               methods.ComparePrice();   //Sepet fiyatı ve liste fiyatını karşılaştırma methodu


               methods.UpQuantity();     //Sepetteki ürün sayısını artırma methodu
               methods.DeleteToCart();   //Sepetteki ürünleri silme methodu
               checking.IsTrue = true;   //tüm methodlar başarılı ise testin doğrunu kontrol ediyor
            }
            catch (Exception ) //Test başarısız olduğunda buraya girer.
            {
               checking.Message= "Bir sorun ile karşılaşıldı, lütfen tekrar deneyiniz.";
                Console.WriteLine(checking.Message);
                Console.ReadLine();
                
            }

           

            if (checking.IsTrue==true) //tüm testler başarılı mı kontrol ediyor.
            {
                Console.WriteLine("Test Başarılı !");
                Console.ReadLine();
                
            }
            else
            {
                Console.WriteLine("Test Başarısız !", checking.Message);
                Console.ReadLine();
            }

          
           
        }
    }
}
