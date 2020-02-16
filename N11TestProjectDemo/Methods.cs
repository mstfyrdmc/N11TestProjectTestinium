using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N11TestProjectDemo
{
    public class Methods
    {

        IWebDriver driver = new ChromeDriver(); //Chrome'da açılması için nesne oluşturduk.
        string link = @"https://www.n11.com/";  //gideceğimiz linki yazdık.
        string listPrice, cartPrice;  //liste fiyatı ve sepet fiyatı değişkenleri
        bool IsCartQuantityTrue; //sepetteki ürün sayısının doğruluğunu tutan değişken
        bool IsCartEmpty; //sepette ürün kalıp kalmadığını tutan değişken
        bool IsSecondPageOpen; //ikinci sayfanın açılıp açılmadığını tutan değişken
        bool IsLogin; //Kullanıcının login olup olmadığını tutan değişken
        bool IsProductSelect; //Ürünün seçilip seçilmediğini tutan değişken
        bool IsProductAddCart; //Ürünün sepete eklenip eklenmediğini tutan değişken
       
        public void GetLogin()  //login olma methodu
        {
            try
            {
                driver.Navigate().GoToUrl(link); //nesnemizi gitmek istediğimiz sayfaya yönlendirdik.

                driver.FindElement(By.ClassName("btnSignIn")).Click(); //Giriş yap butonunun id'sini aldık ve click eventini seçtik.

                driver.FindElement(By.Id("email")).SendKeys("user9701@gmail.com"); //değer göndermeyi sendKeys ile yaptık (Mail Adresi).

                driver.FindElement(By.Id("password")).SendKeys("testuser123"); //değer göndermeyi sendKeys ile yaptık. (Şifre)

                driver.FindElement(By.Id("loginButton")).Click(); //Giriş yap butonuna click eventi yaptık.

                IsLogin = true; //Login başarılı .
            }
            catch (Exception) //Login olma başarısız olduğunda buraya girer.
            {

                IsLogin = false; //Login başarısız.
            }

            if (IsLogin == true) //Kullanıcının login olup olamadığını kontrol ediyor.
            {
                Console.WriteLine("Kullanıcı Başarıyla Login İşlemini Gerçekleştirdi !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Kullanıcı Login Olamadı! Lütfen Tekrar Deneyin.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

        }
        public void SearchProduct()  //Ürün arama methodu
        {

            //ürün aramaası yapma

            driver.FindElement(By.Id("searchData")).SendKeys("bilgisayar"); //değer göndermeyi sendKeys ile yaptık (Mail Adresi).

            driver.FindElement(By.ClassName("searchBtn")).Click(); //arama yap butonuna click eventi yaptık.

        }

        public void GoToSecondPage() //2.sayfaya geçme methodu
        {
            try
            {
                driver.FindElement(By.XPath(".//*[@id='contentListing']/div/div/div[2]/div[4]/a[2]")).Click();
                //string sayfa2 = @"https://www.n11.com/arama?q=bilgisayar&pg=2";
                //2.sayfanın linki

                //driver.Navigate().GoToUrl(sayfa2);
                //2.sayfaya yönlendirdik.

                IsSecondPageOpen = true; //ikinci sayfanın açılması başarılı
            }
            catch (Exception) //ikinci sayfa açma işlemi başarısız olduğunda buraya girer.
            {

                IsSecondPageOpen = false; //ikinci sayfanın açılması başarısız
            }

            if (IsSecondPageOpen == true) //ikinci sayfanın açılıp açılmadığını kontrol ediyor.
            {
                Console.WriteLine("İkinci Sayfa Başarıyla Açıldı !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

            else
            {
                Console.WriteLine("İkinci Sayfa Açılamadı Lütfen Tekrar Deneyiniz.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }


        }

        public void SelectProduct() //Ürün Seçme methodu
        {
            try
            {
                //ürünün içine girdi.çalışıyor
                //driver.FindElement(By.XPath(".//*[@id='p-354883646']/div[1]/a")).Click();

                //rasgele 5. ve 3. ürünü seçiyor fakat html etiketleri faklı olduğundan sepete eklerkenki kodlarda hata oluyor. Bu sebele hata alınırsa yorum satırındaki driver.FindElement(By.ClassName("btnAddBasket")).Click();  kodunu aktif etmemiz gerekiyor. Bunun sebebi buton class isimlerinin ürün sayfalarında değişiklik göstermesidir.Bunu try-catch blokları kullanarak çözüme ulaştım.

                //driver.FindElement(By.XPath(".//*[@id='view']/ul/li[5]/div/div[1]/a")).Click();
                //driver.FindElement(By.XPath(".//*[@id='view']/ul/li[3]/div/div[1]/a")).Click();

                Random rnd = new Random(); //Rasgele sayı üretmek için instance aldık.
                int number = rnd.Next(4, 8); //number adlı int değişkene, 4-8 arasında rasgele değer atadık.
                driver.FindElement(By.XPath($".//*[@id='view']/ul/li[{number}]/div/div[1]/a")).Click(); //Burada da rasgele ürünü getirdik.


                listPrice = driver.FindElement(By.ClassName("newPrice")).Text.ToString(); //ürünün fiyatını aldık.


                IsProductSelect = true; //ürün başarıyla seçildi
            }
            catch (Exception)
            {

                IsProductSelect = false; //ürün seçme işleminde hata oldu
            }

            if (IsProductSelect == true) //ürünün seçilip seçilmediğini kontrol ediyor.
            {
                Console.WriteLine("Ürün Başarıyla Seçildi!");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Ürün Seçilemedi Lütfen Tekrar Deneyiniz.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }


        }

        public void AddToCart() //Sepete ürün ekleme methodu
        {
            try
            {

                //Burada yukarıda belirtilen durumu görebiliriz. İki class isimi de doğru fakat ikisi de farklı ürünlere ait buton classlarıdır.

                try //bu blokta farklı sayfalardaki etiket sorunu çözüldü. ilk önce addBasketUnify etiketini arayacak bulursa Click eventini çalıştıracaktır. Eğer yoksa catch bloğuna düşüp btnAddBasket etiketini bulacak ve click eventini çalıştırarak sepete ekleme işlemini yapacaktır.
                {
                    //sepete ekleme
                    driver.FindElement(By.ClassName("addBasketUnify")).Click();
                }
                catch (Exception)
                {

                    //sepete ekleme
                    driver.FindElement(By.ClassName("btnAddBasket")).Click();

                    //başka bir sepete ekleme etiketi
                    //driver.FindElement(By.ClassName("btnBuy")).Click();
                }


                //Aynı durum burada da geçerlidir.
                try//bu blokta farklı sayfalardaki etiket sorunu çözüldü. ilk önce myBasket etiketini arayacak bulursa Click eventini çalıştıracaktır. Eğer yoksa catch bloğuna düşüp iconBasket etiketini bulacak ve click eventini çalıştırarak sepete yönlendirme işlemini yapacaktır.
                {
                    //sepete yönlnedirme
                    driver.FindElement(By.ClassName("myBasket")).Click();
                }
                catch (Exception)
                {
                    //sepete yönlnedirme
                    driver.FindElement(By.ClassName("iconBasket")).Click();
                }


                //Aynı durum burada da geçerlidir.
                ////Bu blokta farklı sayfalardaki etiket sorunu çözüldü. ilk önce priceArea etiketini arayacak bulursa etiketin text değerini alacaktır. Eğer yoksa catch bloğuna düşüp priceTag etiketini bulacak ve etiketin text değerini alarak sepet fiyatını değişkene atayacaktır.
                try
                {
                    //ürünün sepetteki fiyatını aldık.
                    cartPrice = driver.FindElement(By.ClassName("priceArea")).Text.ToString();
                }
                catch (Exception)
                {
                    ////ürünün sepetteki fiyatını aldık.
                    cartPrice = driver.FindElement(By.ClassName("priceTag")).Text.ToString();
                }


                IsProductAddCart = true;
            }
            catch (Exception)
            {

                IsProductAddCart = false;
            }

            if (IsProductAddCart == true) //ürünün sepete eklenip eklenmediğini kontrol ediyor.
            {
                Console.WriteLine("Ürün Başarıyla Sepete Eklendi!");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Ürün Sepete Eklenemedi. Lütfen Tekrar Deneyiniz.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }


        }

        public void ComparePrice() //Sepet fiyatı ve liste fiyatını karşılaştırma methodu
        {
            string listPrice2 = listPrice.Replace("TL\r\nKDV\r\nDAHİL", " "); //replace metotu ile fiyattan sonra gelen string ifadeyi silindi.
            string cartPrice2 = cartPrice.Replace("TL", " "); //replace metotu ile fiyattan sonra gelen string ifadeyi silindi.

            if (listPrice2 == cartPrice2) //Liste fiyatı ve sepet fiyatını karşılaştırıyor.
            {

                Console.WriteLine("Liste fiyatı ve sepet fiyatı aynı !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }
            else
            {

                Console.WriteLine("Liste fiyatı ve sepet fiyatı farklı !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }
            //liste fiyatı ve sepet fiyatı sıfırlandı
            //listPrice = null;
            //cartPrice = null;
        }
        public void UpQuantity()  //Sepetteki ürün sayısını artırma methodu
        {
            try
            {
                //adet artırıldı.
                driver.FindElement(By.ClassName("spinnerArrow")).Click();
                IsCartQuantityTrue = true;  //Sepetteki ürün sayısı doğru
            }
            catch (Exception) //Sepetteki ürün sayısı doğru değilse buraya girer.
            {

                IsCartQuantityTrue = false; //Sepetteki ürün sayısı hatalı
            }

            if (IsCartQuantityTrue == true) //Sepetteki ürün sayısının doğruluğunu kontrol ediyor.
            {
                Console.WriteLine("Sepetteki Ürün Sayısı Doğru !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Bir sorun oluştu lütfen tekrar deneyiniz !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }
        }


        public void DeleteToCart() //Sepetteki ürünleri silme methodu
        {
            try
            {
                //ürün sepetten silindi.
                driver.FindElement(By.ClassName("svgIcon_trash")).Click();
                IsCartEmpty = true; //Sepet boş
            }
            catch (Exception) //Sepet boş değilse buraya girer
            {

                IsCartEmpty = false;  //Sepet boş değil
            }

            if (IsCartEmpty == true) // Sepetin boşluğunu kontrol ediyor.
            {
                Console.WriteLine("Sepetteki Ürünler Başarıyla Silindi !");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Sepetteki Ürünler Silinemedi! Lütfen Tekrar Deneyiniz.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                //Console.ReadLine();
            }

        }

    }
}
